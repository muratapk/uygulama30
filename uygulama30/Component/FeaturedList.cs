using Microsoft.AspNetCore.Mvc;
using uygulama30.Context;

namespace uygulama30.Component
{
    public class FeaturedList:ViewComponent
    {
        private readonly ApplicationDbContext _context;

        public FeaturedList(ApplicationDbContext context)
        {
            _context = context;
        }

        public IViewComponentResult Invoke()
        {
            var liste=_context.Products.ToList();
            return View(liste);
            
        }
    }
}
