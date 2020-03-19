using AutoMapper;
using Demo.Infrastructure.Utils;
using Demo.Model.Post;
using Demo.BusinessLogic;
using Microsoft.AspNetCore.Mvc;
using Serilog;
using System;
using System.Threading.Tasks;

namespace Demo.Controllers
{
    public class BlogController : BaseController
    {
        private readonly IBlogService _blogService;
        private readonly ICategoryService _categoryService;
        private readonly IMapper _mapper;

        public BlogController(IBlogService blogService,
            ICategoryService categoryService,
            IMapper mapper)
        {
            _blogService = blogService;
            _categoryService = categoryService;
            _mapper = mapper;
        }

        public async Task<IActionResult> Index([FromQuery]string q, [FromQuery]int p = 1)
        {
            try
            {
                var response = await _blogService.SearchPostsAsync(new Data.PostSearchRequest
                {
                    Keyword = q,
                    PageNumber = p,
                    PageSize = Common.Settings.PageSize
                });
                var popularPosts = await _blogService.GetPopularPostsAsync();
                var categories = await _categoryService.GetAllCategoriesAsync();

                var model = new PostIndexModel
                {
                    Results = response.Results
                };
                model.Paging.PageNumber = p;
                model.Paging.PageClickUrl = !string.IsNullOrEmpty(q) ? $"/blog?q={q}&p=" : "/blog?p=";
                model.Paging.TotalRecords = response.TotalRecords;
                model.RightBar.Keyword = q;
                model.RightBar.Categories = categories;
                model.RightBar.Popular = popularPosts;

                SetTitle("Blog", "blog");

                return View($"{Common.WebSetting.ThemeId}/Index", model);
            }
            catch (Exception ex)
            {
                Log.Fatal(ex, "BlogController > Index");
                return NotFound();
            }
        }

        public async Task<IActionResult> Category([FromRoute]string slug, [FromQuery]int p = 1)
        {
            try
            {
                if (string.IsNullOrEmpty(slug))
                {
                    return NotFound();
                }

                var category = await _categoryService.GetCategoryAsync(slug.ToLowerInvariant());
                if (category == null)
                {
                    return NotFound();
                }

                var response = await _blogService.SearchPostsAsync(new Data.PostSearchRequest
                {
                    CategoryId = category.Id,
                    PageNumber = p,
                    PageSize = Common.Settings.PageSize
                });
                var popularPosts = await _blogService.GetPopularPostsAsync();
                var categories = await _categoryService.GetAllCategoriesAsync();

                var model = new PostIndexModel
                {
                    CurrentCategory = category,
                    Results = response.Results
                };
                model.Paging.PageNumber = p;
                model.Paging.PageClickUrl = $"{category.GetUrl()}?p=";
                model.Paging.TotalRecords = response.TotalRecords;
                model.RightBar.Categories = categories;
                model.RightBar.Popular = popularPosts;
                model.RightBar.CurrentCategory = category;

                SetTitle(category.Name, category.GetUrl());

                return View($"{Common.WebSetting.ThemeId}/Category", model);
            }
            catch (Exception ex)
            {
                Log.Fatal(ex, "BlogController > Category");
                return NotFound();
            }
        }

        public async Task<IActionResult> Detail([FromRoute]string cat, [FromRoute]string slug)
        {
            try
            {
                if (string.IsNullOrEmpty(cat))
                {
                    return NotFound();
                }

                if (string.IsNullOrEmpty(slug))
                {
                    return NotFound();
                }

                var post = await _blogService.GetPostAsync(slug.ToLowerInvariant());
                if (post == null)
                {
                    return NotFound();
                }

                if (post.Category.Slug.ToLowerInvariant() != cat.ToLowerInvariant())
                {
                    return NotFound();
                }

                var popularPosts = await _blogService.GetPopularPostsAsync();
                var relatedPosts = await _blogService.GetRelatedPostsAsync(new Data.RelatedPostFindRequest
                {
                    PostId = post.Id,
                    CategoryId = post.CategoryId,
                    CreatedUserId = post.CreatedUserId,
                    Top = 2
                });
                var categories = await _categoryService.GetAllCategoriesAsync();

                var model = new PostDetailModel
                {
                    Entity = post,
                    RelatedPosts = relatedPosts
                };
                model.RightBar.Categories = categories;
                model.RightBar.Popular = popularPosts;
                model.RightBar.CurrentCategory = post.Category;

                SetTitle(post.Title, post.GetUrl(), $"https://{Request.Host}{post.PhotoUrl}");

                return View($"{Common.WebSetting.ThemeId}/Detail", model);
            }
            catch (Exception ex)
            {
                Log.Fatal(ex, "BlogController > Detail");
                return NotFound();
            }
        }
    }
}