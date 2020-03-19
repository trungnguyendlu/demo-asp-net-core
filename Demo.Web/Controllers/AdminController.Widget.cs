using System.Collections.Generic;
using System.Threading.Tasks;
using Demo.Data;
using Demo.Model.Admin.Widget;
using MongoDB.Bson;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Demo.Entity;
using Demo.Infrastructure.Helper;
using Demo.Infrastructure.Utils;
using Demo.Util;

namespace Demo.Controllers
{
    public partial class AdminController
    {
        [Authorize(Policy = nameof(FunctionType.WidgetView))]
        public async Task<ActionResult> Widget()
        {
            var model = new WidgetIndexModel();

            await SearchWidgetAsync(model);
            await PopulateWidgetPageAsync(model);

            SetTitle("Quản Lý Widget");
            return View(model);
        }

        [HttpPost]
        [Authorize(Policy = nameof(FunctionType.WidgetView))]
        public async Task<ActionResult> Widget(WidgetIndexModel model)
        {
            await SearchWidgetAsync(model);
            await PopulateWidgetPageAsync(model);

            return PartialView("Widget/_Result", model);
        }

        [Authorize(Policy = nameof(FunctionType.WidgetView))]
        public ActionResult CreateWidget()
        {
            SetTitle("Thêm Widget Mới");
            return View("WidgetDetail", new WidgetEditModel());
        }

        [Authorize(Policy = nameof(FunctionType.WidgetView))]
        public async Task<ActionResult> EditWidget(ObjectId id)
        {
            var entity = await _widgetService.GetByIdAsync(id);
            if (entity == null)
            {
                return NotFound();
            }

            var model = new WidgetEditModel
            {
                Entity = _mapper.Map<WidgetModel>(entity),
                IsEdit = true
            };

            SetTitle("Cập Nhật Widget");
            return View("WidgetDetail", model);
        }

        [Authorize(Policy = nameof(FunctionType.WidgetView))]
        public async Task<IActionResult> CopyWidget(ObjectId id)
        {
            SetTitle("Copy Widget");
            var entity = await _widgetService.GetByIdAsync(id);
            if (entity == null)
            {
                return NotFound();
            }
            entity.Id = ObjectId.GenerateNewId();

            var model = new WidgetEditModel
            {
                Entity = _mapper.Map<WidgetModel>(entity)
            };

            return View("WidgetDetail", model);
        }

        [HttpPost]
        [Authorize(Policy = nameof(FunctionType.WidgetCreate))]
        [Authorize(Policy = nameof(FunctionType.WidgetEdit))]
        public async Task<ActionResult> SaveWidget(WidgetEditModel model)
        {
            if (!ModelState.IsValid)
            {
                return Json(new { success = false, message = GetModelStateErrors() });
            }

            UpdateBaseInfo(model.IsEdit, model.Entity);
            var request = new SaveRequest<Widget>
            {
                IsEdit = model.IsEdit,
                UserId = _sessionHelper.CurrentUserId,
                Entity = _mapper.Map<Widget>(model.Entity)
            };

            var response = await _widgetService.SaveAsync(request);
            if (response.Success)
            {
                if (!model.IsEdit)
                {
                    TempData[Constants.Message] = string.Join(", ", response.Messages);
                }
                ClearWidgetCache(model.Entity.Position);
            }

            return Json(new { success = response.Success, message = response.Messages });
        }

        [Authorize(Policy = nameof(FunctionType.WidgetDelete))]
        public async Task<ActionResult> DeleteWidget(ObjectId id)
        {
            var entity = await _widgetService.GetByIdAsync(id);
            if (entity == null)
            {
                return Json(new { success = false, message = "Không tìm thấy widget" });
            }

            var reponse = await _widgetService.DeleteAsync(id);

            if (reponse.Success)
            {
                ClearWidgetCache((WidgetPosition)entity.Position);
            }

            return Json(new { success = reponse.Success, message = reponse.Messages });
        }


        private async Task SearchWidgetAsync(WidgetIndexModel model)
        {
            var response = await _widgetService.FindAsync(_mapper.Map<WidgetFindRequest>(model));

            model.Results = _mapper.Map<List<WidgetModel>>(response.Results);
            model.Paging.TotalRecords = response.TotalRecords;
        }

        private async Task PopulateWidgetPageAsync(WidgetIndexModel model)
        {
            model.Paging.PageClickFunction = "comdy.widget.search({0})";
            model.PopulateCreatedUser(await _userService.GetReferencesAsync());
        }

        private void ClearWidgetCache(WidgetPosition position)
        {
            _cacheHelper.Clear(CacheKey.GetAllWidgets);
        }
    }
}