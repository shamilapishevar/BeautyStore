using System;
using System.ComponentModel.DataAnnotations;

namespace BeautyStore.Domain.Entities
{
    public class ProductImage
    {
        public Guid Id { get; set; } = Guid.NewGuid();

        [Required]
        public string ImageUrl { get; set; } = string.Empty;

        [Required]
        public Guid ProductId { get; set; }

        public Product Product { get; set; } = null!;
    }
}
