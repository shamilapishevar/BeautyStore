using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BeautyStore.Domain.Entities
{
    public class Product
    {
        public Guid Id { get; set; } = Guid.NewGuid();

        [Required, MaxLength(100)]
        public string Name { get; set; } = string.Empty;

        [MaxLength(500)]
        public string Description { get; set; } = string.Empty;

        [Range(0, 10000000)]
        public decimal Price { get; set; }

        [Required]
        public Guid CategoryId { get; set; }

        public Category Category { get; set; } = null!;

        // اضافه کردن لیست تصاویر محصول
        public ICollection<ProductImage> ProductImages { get; set; } = new List<ProductImage>();
    }
}
