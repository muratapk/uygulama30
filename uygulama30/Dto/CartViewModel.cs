using uygulama30.Models;

namespace uygulama30.Dto
{
    public class CartViewModel
    {
        public List<CartItem>? CartItem { get; set; }
        public decimal GrandTotal { get; set; }
    }
}
