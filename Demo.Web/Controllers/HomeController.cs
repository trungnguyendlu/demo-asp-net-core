using Microsoft.AspNetCore.Mvc;
using Demo.Infrastructure.Utils;
using System;
using Serilog;

namespace Demo.Controllers
{
    public class HomeController : BaseController
    {
        public IActionResult Index()
        {
            try
            {
                SetTitle("Trang Chủ", isHomePage: true);
                return View($"{Common.WebSetting.ThemeId}/Index");
            }
            catch (Exception ex)
            {
                Log.Fatal(ex, "HomeController > Index");
                return NotFound();
            }
        }
    }
}
