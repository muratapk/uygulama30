using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Client;
using uygulama30.Component;
using uygulama30.Context;
using uygulama30.Dto;
using uygulama30.Models;
using uygulama30.Oturum;

namespace uygulama30.Controllers
{
    public class CartController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CartController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            List<CartItem> items = HttpContext.Session.GetJson<List<CartItem>>("Cart") ?? new List<CartItem>();
            CartViewModel cartvm = new()
            {
                CartItem = items,
                GrandTotal = items.Sum(x => x.Quantity * x.Price)
            };

            return View(cartvm);
        }
        public  IActionResult Add(int id)
        {
            Product pd = _context.Products.Find(id);
            //ürün id numarasını göre ürünü seç
           
                List<CartItem> sepet = HttpContext.Session.GetJson<List<CartItem>>("Cart") ?? new List<CartItem>();
                //session içindeki tüm verileri sepetin içindeki verileri sepet nesnesine 
                CartItem cartItem = sepet.FirstOrDefault(x => x.ProductId == id);
            //sepetin içindeki gönderdiği id numarasına ürün var mı bir bak
                if (cartItem == null)
                {
                    sepet.Add(new CartItem(pd));
                  //ürün yok ise bunu ürünü sepette ekle
                }
                else
                {
                    cartItem.Quantity++;
                  // bu ürün sepette varsa adedini bir arttır
                }
          
            HttpContext.Session.SetJson("Cart", sepet);
            //yeni eklenen sepetin içindeki verileri tekrardan session içine ata
            TempData["Basket"] = "Sepete Eklendi";
            //Ekrana Sepete eklendi mesajı için bir tempdata oluştur
            return Redirect(Request.Headers["Referer"].ToString());
            //linkle gönderilen sayfa tekrardan dön
        }
        public async Task<IActionResult>Remove(int? id)
        {
            List<CartItem> sepet = HttpContext.Session.GetJson<List<CartItem>>("Cart");
            //session içindeki tüm ürünleri çekmiş oldum
            sepet.RemoveAll(c => c.ProductId == id);
            if(sepet.Count>0)
            {
                HttpContext.Session.Remove("Cart");
            }
            else
            {
                HttpContext.Session.SetJson("Cart", sepet);
            }
            TempData["Basket"] = "Sepetteki Ürün Silindi";
            return RedirectToAction("Index");
            
        }
        public async Task<IActionResult>Clear()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Index");
        }
        public async Task<IActionResult> Decrease(int id)
        {
            List<CartItem> sepet = HttpContext.Session.GetJson<List<CartItem>>("Cart");
            //session içindeki tüm ürünleri çekmiş oldum
            CartItem carts = sepet.Where(x => x.ProductId == id).FirstOrDefault();
            if (carts.Quantity > 0)
            {
                carts.Quantity--;
            }
            else
            {
                sepet.RemoveAll(c => c.ProductId == id);
            }
            if(sepet.Count>0)
            {
                HttpContext.Session.SetJson("Cart", carts);
            }
            TempData["Basket"] = "Sepetteki Ürün Silindi";
            return RedirectToAction("Index");
            
        }

        public async Task<IActionResult> Increase(int id)
        {
            List<CartItem> sepet = HttpContext.Session.GetJson<List<CartItem>>("Cart");
            //session içindeki tüm ürünleri çekmiş oldum
            CartItem carts = sepet.Where(x => x.ProductId == id).FirstOrDefault();
            if (carts.Quantity > 0)
            {
                carts.Quantity++;
            }
            
            if (sepet.Count > 0)
            {
                HttpContext.Session.SetJson("Cart", carts);
            }
            TempData["Basket"] = "Sepetteki Ürün Silindi";
            return RedirectToAction("Index");

        }
    }
}
