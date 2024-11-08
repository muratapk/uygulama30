using System.ComponentModel.DataAnnotations.Schema;

namespace uygulama30.Models
{
    public class ProductImage
    {
        public int Id { get; set; } // Resim ID'si (benzersiz)

        public int ProductId { get; set; } // Ürünün ID'si (Bu resim hangi ürüne ait olacak)
        virtual public Product? Product { get; set; }
        public string FileName { get; set; } = string.Empty; // Resim dosyasının adı

        public string FilePath { get; set; } = string.Empty; // Resim dosyasının tam yolu

        public DateTime? CreatedAt { get; set; }  // Resmin yüklenme tarihi
        [NotMapped]
        public IFormFile? Picture { get; set; }

    }
}
