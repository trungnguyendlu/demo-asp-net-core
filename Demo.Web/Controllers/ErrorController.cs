using Microsoft.AspNetCore.Mvc;

namespace Demo.Controllers
{
    public class ErrorController : BaseController
    {
        [Route("/error/{statusCode}")]
        public IActionResult Index([FromRoute]int statusCode)
        {
            SetTitle(statusCode.ToString(), $"error/{statusCode}");
            return View(statusCode);
        }

        public IActionResult Maintenance()
        {
            SetTitle("Đang Bảo Trì", "offline");
            return View();
        }

        public IActionResult CommingSoon()
        {
            SetTitle("Sắp Khai Trương", "comming-soon");
            return View();
        }
    }
}