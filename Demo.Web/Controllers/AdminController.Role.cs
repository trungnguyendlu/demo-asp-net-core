using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Demo.Data;
using Demo.Entity;
using Demo.Model.Admin.Role;
using MongoDB.Driver;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;

namespace Demo.Controllers
{
    public partial class AdminController
    {
        [Authorize(Policy = nameof(FunctionType.RoleView))]
        public async Task<ActionResult> Role()
        {
            SetTitle("Phân quyền");
            var model = new RoleIndexModel();

            await SearchRoleAsync(model);
            await PopulateRolePageAsync(model);

            return View(model);
        }

        [HttpPost]
        [Authorize(Policy = nameof(FunctionType.RoleView))]
        public async Task<ActionResult> Role(RoleIndexModel model)
        {
            await SearchRoleAsync(model);
            await PopulateRolePageAsync(model);

            return PartialView("Role/_RoleResult", model);
        }

        [Authorize(Policy = nameof(FunctionType.RoleView))]
        public ActionResult CreateRole()
        {
            var model = new RoleEditModel();
            PopulateEditRolePage(model);

            return PartialView("Role/_RoleDetail", model);
        }

        [Authorize(Policy = nameof(FunctionType.RoleView))]
        public async Task<ActionResult> EditRole(ObjectId id)
        {
            var role = await _roleService.GetByIdAsync(id);
            if (role == null)
            {
                return NotFound();
            }

            var model = new RoleEditModel
            {
                Entity = _mapper.Map<RoleModel>(role),
                IsEdit = true
            };
            PopulateEditRolePage(model);

            return PartialView("Role/_RoleDetail", model);
        }

        [HttpPost]
        [Authorize(Policy = nameof(FunctionType.RoleCreate))]
        [Authorize(Policy = nameof(FunctionType.RoleEdit))]
        public async Task<ActionResult> SaveRole(RoleEditModel model)
        {
            if (!ModelState.IsValid)
            {
                return Json(new { success = false, message = GetModelStateErrors() });
            }

            UpdateBaseInfo(model.IsEdit, model.Entity);
            var request = new SaveRequest<Role>
            {
                IsEdit = model.IsEdit,
                UserId = _sessionHelper.CurrentUserId,
                Entity = _mapper.Map<Role>(model.Entity)
            };

            var response = await _roleService.SaveAsync(request);

            return Json(new { success = response.Success, message = response.Messages });
        }

        [Authorize(Policy = nameof(FunctionType.RoleDelete))]
        public async Task<ActionResult> DeleteRole(ObjectId id)
        {
            var reponse = await _roleService.DeleteAsync(id);

            return Json(new { success = reponse.Success, message = reponse.Messages });
        }


        private async Task SearchRoleAsync(RoleIndexModel model)
        {
            var response = await _roleService.FindAsync(new RoleFindRequest
            {
                Name = model.Name,
                PageSize = model.Paging.PageSize,
                PageNumber = model.Paging.PageNumber,
                Sort = model.Sort
            });

            model.Results = _mapper.Map<List<RoleModel>>(response.Results);
            model.Paging.TotalRecords = response.TotalRecords;
        }

        private async Task PopulateRolePageAsync(RoleIndexModel model)
        {
            model.Paging.PageClickFunction = "comdy.role.search({0})";
            model.PopulateCreatedUser(await _userService.GetReferencesAsync());
        }

        private void PopulateEditRolePage(RoleEditModel model)
        {
            var roleFeatures = (from object value in Enum.GetValues(typeof(FunctionType))
                                select new PermissionModel
                                {
                                    Function = (FunctionType)value
                                }).ToList();

            model.Entity.Permissions = (from roleFeature in roleFeatures
                                     join userFeature in model.Entity.Permissions on roleFeature.Function equals userFeature.Function into ljUserFeatures
                                     from ljUserFeature in ljUserFeatures.DefaultIfEmpty(new PermissionModel())
                                     select new PermissionModel
                                     {
                                         Function = roleFeature.Function,
                                         Enable = ljUserFeature.Enable
                                     }).ToList();
        }
    }
}