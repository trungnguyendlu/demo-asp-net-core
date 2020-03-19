using System;
using System.IO;
using System.Security.Claims;
using System.Threading.Tasks;
using Demo.Data;
using Demo.Infrastructure.Helper;
using Demo.Infrastructure.Helper.Email;
using Demo.Infrastructure.Utils;
using Demo.Model.Account;
using Demo.BusinessLogic;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Serilog;
using Demo.Util;

namespace Demo.Controllers
{
    [Authorize]
    public class AccountController : BaseController
    {
        private readonly IUserService _userService;
        private readonly IRoleService _roleService;
        private readonly ISessionHelper _sessionHelper;

        public AccountController(IUserService userService,
            IRoleService roleService,
            ISessionHelper sessionHelper)
        {
            _userService = userService;
            _roleService = roleService;
            _sessionHelper = sessionHelper;
        }


        [Route("/login")]
        [HttpGet, AllowAnonymous]
        public IActionResult Login(string returnUrl = null)
        {
            SetTitle("Đăng nhập");
            if (User.Identity.IsAuthenticated)
            {
                return LocalRedirect(returnUrl ?? "/admin");
            }
            ViewData["ReturnUrl"] = returnUrl;
            return View();
        }

        [Route("/login")]
        [HttpPost, AllowAnonymous, ValidateAntiForgeryToken]
        public async Task<IActionResult> LoginAsync(string returnUrl, LoginModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    //hash password
                    var password = Crypto.HashPassword(model.Password);
                    var user = await _userService.LoginAsync(model.Email, password);
                    if (user != null && user.Status == (int)UserStatus.Active)
                    {
                        await _userService.UpdateLastLoginDateAsync(user.Email);

                        _sessionHelper.SetString(SessionKey.CurrentUserId, user.Id.ToString());
                        _sessionHelper.Set(SessionKey.CurrentUser, user);
                        _sessionHelper.Set(SessionKey.CurrentRole, await _roleService.GetByIdAsync(user.RoleId));

                        var identity = new ClaimsIdentity(CookieAuthenticationDefaults.AuthenticationScheme);
                        identity.AddClaim(new Claim(ClaimTypes.Name, model.Email));

                        var principle = new ClaimsPrincipal(identity);
                        var properties = new AuthenticationProperties
                        {
                            IsPersistent = model.RememberMe
                        };
                        await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principle, properties);

                        if (!string.IsNullOrEmpty(returnUrl) && returnUrl.ToLowerInvariant().Contains("logout"))
                        {
                            returnUrl = "/admin";
                        }

                        return LocalRedirect(returnUrl ?? "/admin");
                    }
                }

                SetTitle("Đăng nhập", "login");
                ViewData["ReturnUrl"] = returnUrl;
                ModelState.AddModelError(string.Empty, "Email hoặc Mật khẩu không chính xác.");
                return View("Login", model);
            }
            catch (Exception ex)
            {
                Log.Fatal(ex, "AccountController > LoginAsync");
                return NotFound();
            }
        }

        [Route("/logout")]
        public async Task<IActionResult> LogOutAsync()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return LocalRedirect("/");
        }

        [HttpGet, AllowAnonymous]
        [Route("/password-recover")]
        public IActionResult PasswordRecover()
        {
            SetTitle("Khôi phục mật khẩu", "password-recover");
            return View("PasswordRecover");
        }

        [Route("/password-recover")]
        [HttpPost, AllowAnonymous, ValidateAntiForgeryToken]
        public async Task<IActionResult> PasswordRecoverAsync(PasswordRecoverModel model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    SetTitle("Khôi phục mật khẩu", "password-recover");
                    ModelState.AddModelError(string.Empty, string.Join(", ", GetModelStateErrors()));
                    return View("PasswordRecover", model);
                }

                var user = await _userService.GetByEmailAsync(model.Email);
                if (user == null)
                {
                    SetTitle("Khôi phục mật khẩu", "password-recover");
                    ModelState.AddModelError(string.Empty, "Email không tồn tại trong hệ thống");
                    return View("PasswordRecover", model);
                }

                //generate new password
                var newPassword = Common.RandomString(8);

                //update user's password
                user.Password = Crypto.HashPassword(newPassword);
                var response = await _userService.SaveAsync(new SaveRequest<Entity.User>
                {
                    Entity = user,
                    IsEdit = true,
                    UserId = user.Id
                });

                //send mail with new password
                if (response.Success)
                {
                    var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "emailtemplate", "recovery-password.html");
                    var htmlTemplate = System.IO.File.ReadAllText(path);
                    htmlTemplate = htmlTemplate.Replace("{{FullName}}", user.FullName)
                        .Replace("{{NewPassword}}", newPassword);

                    await EmailHelper.SendEmailAsync(
                        Common.DefaultConfig,
                        new EmailMessage
                        {
                            From = Common.DefaultConfig.SmtpUsername,
                            To = model.Email,
                            Subject = "[NO REPLY] KHÔI PHỤC MẬT KHẨU",
                            Content = htmlTemplate
                        });

                    SetTitle("Khôi phục mật khẩu", "password-recover");
                    return View("PasswordRecovered", new PasswordRecoveredModel
                    {
                        AvatarUrl = user.AvatarUrl,
                        FullName = user.FullName
                    });
                }

                SetTitle("Khôi phục mật khẩu", "password-recover");
                ModelState.AddModelError(string.Empty, "Đã xảy ra sự cố. Vui lòng thử lại.");
                return View("PasswordRecover", model);
            }
            catch (Exception ex)
            {
                Log.Fatal(ex, "AccountController > PasswordRecoverAsync");
                return NotFound();
            }
        }
    }
}