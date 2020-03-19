using System.Collections.Generic;
using System.Threading.Tasks;
using Demo.Data;
using Demo.Model.Admin.Media;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Demo.Entity;
using System.IO;
using Demo.Infrastructure.Utils;
using MongoDB.Bson;

namespace Demo.Controllers
{
    public partial class AdminController
    {
        [Authorize(Policy = nameof(FunctionType.MediaView))]
        public async Task<ActionResult> Media()
        {
            var model = new MediaIndexModel();

            await SearchMediaAsync(model);
            await PopulateMediaPageAsync(model);

            SetTitle("Quản Lý Thư Viện");
            return View(model);
        }

        [HttpPost]
        [Authorize(Policy = nameof(FunctionType.MediaView))]
        public async Task<ActionResult> Media(MediaIndexModel model)
        {
            await SearchMediaAsync(model);
            await PopulateMediaPageAsync(model);

            return PartialView("Media/_MediaResult", model);
        }

        [Authorize(Policy = nameof(FunctionType.MediaCreate))]
        public ActionResult CreateMedia()
        {
            var model = new MediaEditModel();

            return PartialView("Media/_MediaDetail", model);
        }

        [Authorize(Policy = nameof(FunctionType.MediaEdit))]
        public async Task<ActionResult> EditMedia(ObjectId id)
        {
            var entity = await _mediaService.GetByIdAsync(id);
            if (entity == null)
            {
                return NotFound();
            }

            var model = new MediaEditModel
            {
                Entity = _mapper.Map<MediaModel>(entity),
                IsEdit = true
            };

            return PartialView("Media/_MediaDetail", model);
        }

        [HttpPost]
        [Authorize(Policy = nameof(FunctionType.MediaCreate))]
        [Authorize(Policy = nameof(FunctionType.MediaEdit))]
        public async Task<ActionResult> SaveMedia(MediaEditModel model)
        {
            if (!ModelState.IsValid)
            {
                return Json(new { success = false, message = GetModelStateErrors() });
            }

            UpdateBaseInfo(model.IsEdit, model.Entity);
            var request = new SaveRequest<Media>
            {
                IsEdit = model.IsEdit,
                UserId = _sessionHelper.CurrentUserId,
                Entity = _mapper.Map<Media>(model.Entity)
            };

            var response = await _mediaService.SaveAsync(request, (dbMedia, media) =>
            {
                media.Width = dbMedia.Width;
                media.Height = dbMedia.Height;
                media.FileName = dbMedia.FileName;
                media.FileType = dbMedia.FileType;
                media.FileSize = dbMedia.FileSize;
            });
            
            return Json(new { success = response.Success, message = response.Messages });
        }

        [HttpPost]
        [Consumes("multipart/form-data")]
        [Authorize(Policy = nameof(FunctionType.MediaCreate))]
        [Authorize(Policy = nameof(FunctionType.MediaEdit))]
        public async Task<ActionResult> UploadMedia([FromForm] MediaUploadModel model)
        {
            if (model.File == null || model.File.Length == 0)
            {
                return Content("Chưa chọn file để tải lên");
            }

            var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images", "upload", model.File.FileName);
            var filename = Path.GetFileNameWithoutExtension(path);
            var extension = Path.GetExtension(path);

            var newFileName = $"{filename}-{Common.RandomString(6)}";

            var entity = new Media
            {
                Id = ObjectId.GenerateNewId(),
                Type = (int)model.Entity.Type,
                Title = newFileName,
                FileName = $"{newFileName}{extension}",
                FileSize = model.File.Length / 1024,
                FileType = model.File.ContentType
            };

            var response = await _mediaService.SaveAsync(new SaveRequest<Media>
            {
                Entity = entity,
                UserId = _sessionHelper.CurrentUserId
            });

            if (response.Success)
            {
                path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images", "upload", entity.FileName);
                using (var stream = new FileStream(path, FileMode.Create))
                {
                    await model.File.CopyToAsync(stream);
                    //get image size
                    var image = System.Drawing.Image.FromStream(stream);
                    if (image != null)
                    {
                        entity.Width = image.Width;
                        entity.Height = image.Height;

                        await _mediaService.SaveAsync(new SaveRequest<Media>
                        {
                            Entity = entity,
                            IsEdit = true,
                            UserId = _sessionHelper.CurrentUserId
                        });
                    }
                }

            }
            return RedirectToAction("Media");
        }

        [Authorize(Policy = nameof(FunctionType.MediaDelete))]
        public async Task<ActionResult> DeleteMedia(ObjectId id)
        {
            var entity = await _mediaService.GetByIdAsync(id);
            if (entity == null)
            {
                return Json(new { success = false, message = "Không tìm thấy hình ảnh" });
            }

            var response = await _mediaService.DeleteAsync(id);

            if (response.Success)
            {
                var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images", "upload", entity.FileName);
                if (System.IO.File.Exists(path))
                {
                    System.IO.File.Delete(path);
                }
            }

            return Json(new { success = response.Success, message = response.Messages });
        }

        public async Task<ActionResult> GetMediaPopup(MediaType? type)
        {
            var model = new MediaIndexModel
            {
                Type = type
            };
            model.Paging.PageSize = 20;

            await SearchMediaAsync(model);
            await PopulateMediaPageAsync(model);
            model.Paging.CalculatePaging();

            return PartialView("Media/_MediaPopup", model);
        }

        [HttpPost]
        public async Task<ActionResult> GetMediaPopup(MediaIndexModel model)
        {
            await SearchMediaAsync(model);
            await PopulateMediaPageAsync(model);
            model.Paging.CalculatePaging();

            return PartialView("Media/_MediaItems", model);
        }

        private async Task SearchMediaAsync(MediaIndexModel model)
        {
            var response = await _mediaService.FindAsync(new MediaFindRequest
            {
                Type = (int?)model.Type,
                Name = model.Name,
                PageSize = model.Paging.PageSize,
                PageNumber = model.Paging.PageNumber,
                Sort = model.Sort
            });

            model.Results = _mapper.Map<List<MediaModel>>(response.Results);
            model.Paging.TotalRecords = response.TotalRecords;
        }

        private async Task PopulateMediaPageAsync(MediaIndexModel model)
        {
            model.Paging.PageClickFunction = "comdy.media.search({0})";
            model.PopulateCreatedUser(await _userService.GetReferencesAsync());
        }

        private string GetFileNameWithoutExtension(string filename)
        {
            const string dot = ".";
            if (string.IsNullOrEmpty(filename) || !filename.Contains(dot))
            {
                return string.Empty;
            }

            return filename.Substring(0, filename.LastIndexOf(dot) - 1);
        }
    }
}