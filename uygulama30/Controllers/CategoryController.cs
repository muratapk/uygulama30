using Microsoft.AspNetCore.Mvc;
using uygulama30.Context;
using uygulama30.Models;

namespace uygulama30.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CategoryController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var liste = _context.Categories.ToList();
            return View(liste);
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(Category gelen)
        { 
            _context.Categories.Add(gelen);
            _context.SaveChanges();
            TempData["Success"] = "İşlem Başarılı Şekilde Yapıldı";
            return RedirectToAction("Index");
        }
        [HttpGet]
        public IActionResult Edit(int id)
        {
           var veri =_context.Categories.Find(id);
            return View(veri);

        }
        [HttpPost]
        public IActionResult Edit(Category gelen)
        {
            _context.Categories.Update(gelen);
            _context.SaveChanges();
            TempData["Success"] = "İşlem Başarılı Şekilde Yapıldı";
            return RedirectToAction("Index");
        }
           
        
        [HttpGet]
        public IActionResult Delete(int id)
        {
            var veri = _context.Categories.Find(id);
            return View(veri);

        }
        [HttpPost]
        public IActionResult Delete(Category gelen)
        {
            _context.Categories.Remove(gelen);
            _context.SaveChanges();
            TempData["Success"] = "İşlem Başarılı Şekilde Yapıldı";
            return RedirectToAction("Index");
        }

    }
}
