using Microsoft.AspNetCore.Mvc;
using uygulama30.Oturum;

using uygulama30.Models;
using uygulama30.Context;
using uygulama30.Dto;

namespace uygulama30.Component
{
    public class CartSumList:ViewComponent
    {
        private readonly ApplicationDbContext _context;

        public CartSumList(ApplicationDbContext context)
        {
            _context = context;
        }
        public IViewComponentResult Invoke()
        {
            List<CartItem> carts = HttpContext.Session.GetJson<List<CartItem>>("Cart") ?? new List<CartItem>();

            CartViewModel cartVm = new()
            {
                CartItem = carts,
                GrandTotal = carts.Sum(x => x.Quantity * x.Price)
            };
            return View(cartVm);
        }

    }
}
