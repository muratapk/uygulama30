using System.ComponentModel.DataAnnotations;

namespace eticaretapi.Models
{
    public class Category
    {
        [Key]
        public int CategoryId { get; set; }
      
        public string CategoryName { get; set; } = string.Empty;
        virtual public List<Product>? Products { get; set; }
    }
}
