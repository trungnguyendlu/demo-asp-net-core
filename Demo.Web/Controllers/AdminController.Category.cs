using System.Collections.Generic;
using System.Threading.Tasks;
using Demo.Data;
using Demo.Model.Admin.Category;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Demo.Entity;
using MongoDB.Bson;
using Demo.Infrastructure.Utils;

namespace Demo.Controllers
{
    public partial class AdminController
    {
        [Authorize(Policy = nameof(FunctionType.CategoryView))]
        public async Task<ActionResult> Category()
        {
            SetTitle("Quản Lý Chuyên Mục");
            var model = new CategoryIndexModel();

            await SearchCategoryAsync(model);
            await PopulateCategoryPageAsync(model);

            return View(model);
        }

        [HttpPost]
        [Authorize(Policy = nameof(FunctionType.CategoryView))]
        public async Task<ActionResult> Category(CategoryIndexModel model)
        {
            await SearchCategoryAsync(model);
            await PopulateCategoryPageAsync(model);

            return PartialView("Category/_CategoryResult", model);
        }

        [Authorize(Policy = nameof(FunctionType.CategoryView))]
        public ActionResult CreateCategory()
        {
            var model = new CategoryEditModel();

            return PartialView("Category/_CategoryDetail", model);
        }

        [Authorize(Policy = nameof(FunctionType.CategoryView))]
        public async Task<ActionResult> EditCategory(ObjectId id)
        {
            var entity = await _categoryService.GetByIdAsync(id);
            if (entity == null)
            {
                return NotFound();
            }

            var model = new CategoryEditModel
            {
                Entity = _mapper.Map<CategoryModel>(entity),
                IsEdit = true
            };

            return PartialView("Category/_CategoryDetail", model);
        }

        [Authorize(Policy = nameof(FunctionType.CategoryView))]
        public async Task<ActionResult> CopyCategory(ObjectId id)
        {
            var entity = await _categoryService.GetByIdAsync(id);
            if (entity == null)
            {
                return NotFound();
            }
            entity.Id = ObjectId.GenerateNewId();

            var model = new CategoryEditModel
            {
                Entity = _mapper.Map<CategoryModel>(entity)
            };

            return PartialView("Category/_CategoryDetail", model);
        }

        [HttpPost]
        [Authorize(Policy = nameof(FunctionType.CategoryCreate))]
        [Authorize(Policy = nameof(FunctionType.CategoryEdit))]
        public async Task<ActionResult> SaveCategory(CategoryEditModel model)
        {
            if (!ModelState.IsValid)
            {
                return Json(new { success = false, message = GetModelStateErrors() });
            }

            UpdateBaseInfo(model.IsEdit, model.Entity);
            var request = new SaveRequest<Category>
            {
                IsEdit = model.IsEdit,
                UserId = _sessionHelper.CurrentUserId,
                Entity = _mapper.Map<Category>(model.Entity)
            };

            var response = await _categoryService.SaveAsync(request);
            if (response.Success)
            {
                ClearCategoryCache(request.Entity.Slug);
            }

            return Json(new { success = response.Success, message = response.Messages });
        }

        [Authorize(Policy = nameof(FunctionType.CategoryDelete))]
        public async Task<ActionResult> DeleteCategory(ObjectId id)
        {
            var entity = await _categoryService.GetByIdAsync(id);
            if (entity == null)
            {
                return Json(new { success = false, message = "Không tìm thấy danh mục" });
            }

            var reponse = await _categoryService.DeleteAsync(id);

            if (reponse.Success)
            {
                ClearCategoryCache(entity.Slug);
            }

            return Json(new { success = reponse.Success, message = reponse.Messages });
        }


        private async Task SearchCategoryAsync(CategoryIndexModel model)
        {
            var response = await _categoryService.FindAsync(_mapper.Map<CategoryFindRequest>(model));

            model.Results = _mapper.Map<List<CategoryModel>>(response.Results);
            model.Paging.TotalRecords = response.TotalRecords;
        }

        private async Task PopulateCategoryPageAsync(CategoryIndexModel model)
        {
            model.Paging.PageClickFunction = "comdy.category.search({0})";
            model.PopulateCreatedUser(await _userService.GetReferencesAsync());
        }

        private void ClearCategoryCache(string slug)
        {
        }
    }
}