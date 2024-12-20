﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace uygulama30.Models
{
    public class Product
    {
        public int Id { get; set; } // Ürünün benzersiz kimliği

        [Required]
        [StringLength(100, MinimumLength = 3)]
        [Display(Name="Ürün Adı")]
        public string Name { get; set; } = string.Empty; // Ürün adı

        [Range(0.01, double.MaxValue, ErrorMessage = "Fiyat sıfırdan büyük olmalıdır.")]
        [Display(Name = "Ürün Fiyatı")]

        public decimal ? Price { get; set; } // Ürün fiyatı

        [StringLength(500)]
        [Display(Name = "Ürün Açıklaması")]

        public string Description { get; set; } = string.Empty; // Ürün açıklaması

        [Display(Name = "Ürün Kategorisi")]

        public int? CategoryId { get; set; } // Ürün kategorisi
        [Display(Name = "Ürün Kategorisi")]
        virtual public Category? Category { get; set; }
        [Display(Name = "Stok Miktarı")]

        public string ProductPicture { get; set; } = String.Empty;
        public int? StockQuantity { get; set; } // Stok miktarı
        [Display(Name = "Oluşturma Tarihi")]


        [DataType(DataType.Date)]
        public DateTime? CreatedAt { get; set; } // Ürünün oluşturulma tarihi
      
        virtual public List<ProductImage>? ProductImages { get; set; }
        [NotMapped]
        virtual public IFormFile? Picture { get; set; }
    }
}
