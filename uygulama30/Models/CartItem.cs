namespace uygulama30.Models
{
    public class CartItem
    {
       

        public long ProductId  { get; set; }
        public string ProductName  { get; set; }
        public int Quantity  { get; set; }
        public Decimal Price { get; set; }
        public string Image { get; set; }
        public CartItem()
        {
        }
        public CartItem(Product product)
        {
            ProductId = product.Id;
            ProductName = product.Name;
            Quantity = 1;
            Price = Convert.ToDecimal(product.Price);
            Image = product.ProductPicture;
        }
        public Decimal Total()
        {
            return (this.Quantity * this.Price);
        }

    }
}
