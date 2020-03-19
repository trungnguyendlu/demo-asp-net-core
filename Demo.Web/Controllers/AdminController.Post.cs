using System.Collections.Generic;
using System.Threading.Tasks;
using Demo.Data;
using Demo.Model.Admin.Post;
using MongoDB.Driver;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Demo.Entity;
using System;
using System.Linq;
using FlexWebClient.Infrastructure.Helper;
using MongoDB.Bson;
using Demo.Util;

namespace Demo.Controllers
{
    public partial class AdminController
    {
        [Authorize(Policy = nameof(FunctionType.PostView))]
        public async Task<ActionResult> Post()
        {
            SetTitle("Bài Viết");
            var model = new PostIndexModel();

            await Search(model);
            await PopulatePostPageAsync(model);

            return View(model);
        }

        [HttpPost]
        [Authorize(Policy = nameof(FunctionType.PostView))]
        public async Task<ActionResult> Post(PostIndexModel model)
        {
            await Search(model);
            await PopulatePostPageAsync(model);

            return PartialView("Post/_PostResult", model);
        }

        [Authorize(Policy = nameof(FunctionType.PostView))]
        public async Task<ActionResult> CreatePost()
        {
            SetTitle("Thêm bài viết mới");
            var model = new PostEditModel();
            await PopulatePostDetailPageAsync(model);

            return View("PostDetail", model);
        }

        [Authorize(Policy = nameof(FunctionType.PostView))]
        public async Task<ActionResult> EditPost(ObjectId id)
        {
            SetTitle("Cập nhật bài viết");
            var post = await _blogService.GetByIdAsync(id);
            if (post == null)
            {
                return NotFound();
            }

            var model = new PostEditModel
            {
                Entity = _mapper.Map<PostModel>(post),
                IsEdit = true
            };
            model.Entity.PostUrl = $"https://{Request.Host}/blog/{model.Entity.Slug}";
            await PopulatePostDetailPageAsync(model);

            return View("PostDetail", model);
        }

        [Authorize(Policy = nameof(FunctionType.PostView))]
        public async Task<ActionResult> CopyPost(ObjectId id)
        {
            SetTitle("Copy bài viết");
            var post = await _blogService.GetByIdAsync(id);
            if (post == null)
            {
                return NotFound();
            }
            post.Id = ObjectId.GenerateNewId();

            var model = new PostEditModel
            {
                Entity = _mapper.Map<PostModel>(post)
            };
            await PopulatePostDetailPageAsync(model);

            return View("PostDetail", model);
        }

        [HttpPost]
        [Authorize(Policy = nameof(FunctionType.PostCreate))]
        [Authorize(Policy = nameof(FunctionType.PostEdit))]
        public async Task<ActionResult> SavePost(PostEditModel model)
        {
            if (!ModelState.IsValid)
            {
                return Json(new { success = false, message = GetModelStateErrors() });
            }

            UpdateBaseInfo(model.IsEdit, model.Entity);

            var entity = _mapper.Map<Post>(model.Entity);
            CreateThumbnail(entity);

            //save post
            var request = new SaveRequest<Post>
            {
                IsEdit = model.IsEdit,
                UserId = _sessionHelper.CurrentUserId,
                Entity = entity
            };
            var response = await _blogService.SaveAsync(request, (dbPost, post) =>
            {
                if (post.IsPublished)
                {
                    post.PublishedDate = dbPost.PublishedDate ?? DateTime.Now;
                }
                else
                {
                    post.PublishedDate = null;
                }
            });

            if (response.Success)
            {
                if (!model.IsEdit)
                {
                    TempData[Constants.Message] = string.Join(", ", response.Messages);
                }
                ClearPostCache(entity);
            }

            return Json(new { success = response.Success, message = response.Messages });
        }

        [HttpPost]
        [Authorize(Policy = nameof(FunctionType.PostDelete))]
        public async Task<ActionResult> DeletePost(ObjectId id)
        {
            var post = await _blogService.GetByIdAsync(id);
            if (post == null)
            {
                return Json(new { success = false, message = "Không tìm thấy bài viết" });
            }

            var reponse = await _blogService.DeleteAsync(id);

            if (reponse.Success)
            {
                ClearPostCache(post);
            }

            return Json(new { success = reponse.Success, message = reponse.Messages });
        }


        private async Task Search(PostIndexModel model)
        {
            var response = await _blogService.FindAsync(_mapper.Map<PostFindRequest>(model));

            model.Results = _mapper.Map<List<PostModel>>(response.Results);
            model.Paging.TotalRecords = response.TotalRecords;
        }

        private async Task PopulatePostPageAsync(PostIndexModel model)
        {
            model.Paging.PageClickFunction = "comdy.post.search({0})";
            model.Categories = await _categoryService.GetReferencesAsync();
            foreach (var item in model.Results)
            {
                item.CategoryName = model.Categories.Where(a => a.Id == item.CategoryId).Select(a => a.Name).FirstOrDefault();
            }
            model.PopulateCreatedUser(await _userService.GetReferencesAsync());
        }

        private async Task PopulatePostDetailPageAsync(PostEditModel model)
        {
            model.Categories = await _categoryService.GetReferencesAsync();
        }

        private void ClearPostCache(Post post)
        {
            //blog, category, tag page: popular section
            _cacheHelper.Clear(CacheKey.GetPopularPosts);
            //home page: feature posts section
            _cacheHelper.Clear(CacheKey.GetFeaturePosts);
        }

        private void CreateThumbnail(Post entity)
        {
            if (string.IsNullOrEmpty(entity.ThumbnailUrl) || entity.ThumbnailUrl.Replace("_thumb", "") != entity.PhotoUrl)
            {
                var filename = entity.PhotoUrl.Substring(entity.PhotoUrl.LastIndexOf("/") + 1);
                var filepath = System.IO.Path.Combine("wwwroot", "images", "upload", filename);
                entity.ThumbnailUrl = ImageHelper.Resize(filepath, filename, 480);
            }
        }
    }
}