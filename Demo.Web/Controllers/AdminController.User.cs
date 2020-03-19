using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Demo.Data;
using Demo.Entity;
using Demo.Infrastructure.Helper;
using Demo.Model.Admin.User;
using MongoDB.Driver;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using Demo.Util;

namespace Demo.Controllers
{
    public partial class AdminController
    {
        [Authorize(Policy = nameof(FunctionType.UserView))]
        public async new Task<ActionResult> User()
        {
            SetTitle("Người dùng");
            var model = new UserIndexModel();

            await Search(model);
            await PopulateUserPageAsync(model);

            return View(model);
        }

        [HttpPost]
        [Authorize(Policy = nameof(FunctionType.UserView))]
        public async new Task<ActionResult> User(UserIndexModel model)
        {
            await Search(model);
            await PopulateUserPageAsync(model);

            return PartialView("User/_UserResult", model);
        }

        [Authorize(Policy = nameof(FunctionType.UserCreate))]
        public async Task<ActionResult> CreateUser()
        {
            var model = new UserEditModel();
            await PopulateEditUserPageAsync(model);

            return PartialView("User/_UserDetail", model);
        }

        [Authorize(Policy = nameof(FunctionType.UserEdit))]
        public async Task<ActionResult> EditUser(ObjectId id)
        {
            var entity = await _userService.GetByIdAsync(id);
            if (entity == null || entity.IsAdmin)
            {
                return NotFound();
            }

            var model = new UserEditModel
            {
                Entity = _mapper.Map<UserModel>(entity),
                IsEdit = true
            };
            await PopulateEditUserPageAsync(model);

            return PartialView("User/_UserDetail", model);
        }

        [HttpPost]
        [Authorize(Policy = nameof(FunctionType.UserCreate))]
        [Authorize(Policy = nameof(FunctionType.UserEdit))]
        public async Task<ActionResult> SaveUser(UserEditModel model)
        {
            if (!ModelState.IsValid)
            {
                return Json(new { success = false, message = GetModelStateErrors() });
            }
            if (!model.IsEdit)
            {
                if (string.IsNullOrEmpty(model.Entity.Password))
                {
                    return Json(new { success = false, message = "Chưa nhập mật khẩu" });
                }
                if (model.Entity.Password.Length < 6)
                {
                    return Json(new { success = false, message = "Chiều dài mật khẩu tối thiểu là 6 ký tự" });
                }
            }

            UpdateBaseInfo(model.IsEdit, model.Entity);
            var request = new SaveRequest<User>
            {
                IsEdit = model.IsEdit,
                UserId = _sessionHelper.CurrentUserId,
                Entity = _mapper.Map<User>(model.Entity)
            };
            if (!model.IsEdit)
            {
                request.Entity.Password = Crypto.HashPassword(request.Entity.Password);
            }

            var response = await _userService.SaveAsync(request, (dbUser, user) =>
            {
                user.Password = dbUser.Password;
                user.AvatarUrl = dbUser.AvatarUrl;
            });

            return Json(new { success = response.Success, message = response.Messages });
        }

        [Authorize(Policy = nameof(FunctionType.UserDelete))]
        public async Task<ActionResult> DeleteUser(ObjectId id)
        {
            var reponse = await _userService.DeleteAsync(id);

            return Json(new { success = reponse.Success, message = reponse.Messages });
        }


        private async Task Search(UserIndexModel model)
        {
            var response = await _userService.FindAsync(new UserFindRequest
            {
                RoleId = model.RoleId,
                Email = model.Email,
                PageSize = model.Paging.PageSize,
                PageNumber = model.Paging.PageNumber,
                Sort = model.Sort
            });

            model.Results = _mapper.Map<List<UserModel>>(response.Results);
            model.Paging.TotalRecords = response.TotalRecords;
        }

        private async Task PopulateUserPageAsync(UserIndexModel model)
        {
            model.Paging.PageClickFunction = "comdy.user.search({0})";
            model.Roles = await _roleService.GetReferencesAsync();
            model.PopulateCreatedUser(await _userService.GetReferencesAsync());
            foreach(var item in model.Results)
            {
                item.RoleName = model.Roles.Where(a => a.Id == item.RoleId).Select(a => a.Name).FirstOrDefault();
            }
        }

        private async Task PopulateEditUserPageAsync(UserEditModel model)
        {
            model.Roles = await _roleService.GetReferencesAsync();
        }
    }
}