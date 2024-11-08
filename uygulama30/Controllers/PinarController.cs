using Microsoft.AspNetCore.Mvc;

namespace uygulama30.Controllers
{
    public class PinarController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Can(int? id)
        {
            ViewBag.Id = id;
            TempData["Mesaj"] = "Web Tasarım Dersi";
            ViewData["isim"] = "Can ile Pınar";
            return View();
        }
    }
}
