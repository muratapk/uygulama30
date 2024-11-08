using Microsoft.AspNetCore.Mvc;

namespace uygulama30.Controllers
{
    public class AdminController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
