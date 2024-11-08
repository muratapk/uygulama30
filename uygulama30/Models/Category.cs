using System.ComponentModel.DataAnnotations;

namespace uygulama30.Models
{
    public class Category
    { 

        [Key]
        public int CategoryId {  get; set; }
        [Required(ErrorMessage ="Kategori Adını Girmek Zorundasınız")]
        [Display(Name ="Kategori Adın")]
        public string CategoryName { get; set; } = string.Empty;   
        virtual public List<Product> ? Products { get; set; }
    }
}
