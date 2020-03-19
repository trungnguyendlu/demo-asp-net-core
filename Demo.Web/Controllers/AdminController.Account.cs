using System.Threading.Tasks;
using Demo.Data;
using Demo.Entity;
using Demo.Infrastructure.Helper;
using Demo.Model.Admin.User;
using Microsoft.AspNetCore.Mvc;
using Demo.Util;

namespace Demo.Controllers
{
    public partial class AdminController
    {
        public async Task<ActionResult> MyProfile()
        {
            var model = _mapper.Map<UserModel>(_sessionHelper.CurrentUser);
            if (model == null)
            {
                model = new UserModel();
            }

            model.RoleName = (await _roleService.GetByIdAsync(_sessionHelper.CurrentRole.Id)).Name;

            return PartialView("User/_MyProfile", model);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateAvatar(string avatarUrl)
        {
            var user = await _userService.GetByIdAsync(_sessionHelper.CurrentUserId);
            if (user != null)
            {
                user.AvatarUrl = avatarUrl;
                var response = await _userService.SaveAsync(new SaveRequest<User>
                {
                    Entity = user,
                    IsEdit = true,
                    UserId = _sessionHelper.CurrentUserId
                });
                if (response.Success)
                {
                    _sessionHelper.Set(SessionKey.CurrentUser, user);
                    return Json(new { success = true, message = "Cập nhật ảnh đại diện thành công" });
                }
            }
            return Json(new { success = false, message = "Cập nhật ảnh đại diện không thành công" });
        }


        public ActionResult ChangePassword()
        {
            return PartialView("User/_ChangePassword", new ChangePasswordModel());
        }

        [HttpPost]
        public async Task<ActionResult> ChangePassword(ChangePasswordModel model)
        {
            if (model.OldPassword == model.NewPassword)
            {
                ModelState.AddModelError("NewPassword", "Mật khẩu mới phải khác mật khẩu hiện tại");
            }
            if (model.NewPassword != model.ConfirmNewPassword)
            {
                ModelState.AddModelError("ConfirmNewPassword", "Mật khẩu mới và Xác nhận mật khẩu mới không khớp");
            }
            if (!ModelState.IsValid)
            {
                return Json(new { success = false, message = string.Join(", ", GetModelStateErrors()) });
            }

            var password = Crypto.HashPassword(model.OldPassword);
            var user = await _userService.LoginAsync(_sessionHelper.CurrentUser.Email, password);
            if (user != null)
            {
                user.Password = Crypto.HashPassword(model.NewPassword);
                var response = await _userService.SaveAsync(new SaveRequest<User>
                {
                    Entity = user,
                    IsEdit = true,
                    UserId = user.Id
                });

                if (response.Success)
                {
                    return Json(new { success = true, message = "Đổi mật khẩu thành công" });
                }
            }
            return Json(new { success = false, message = "Mật khẩu hiện tại không chính xác" });
        }
    }
}