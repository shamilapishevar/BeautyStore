using System.ComponentModel.DataAnnotations;

namespace BeautyStore.Domain.Entities
{
    public class OrderItem
    {
        public Guid Id { get; set; } = Guid.NewGuid();

        [Required]
        public Guid OrderId { get; set; }
        public Order Order { get; set; } = null!;

        [Required]
        public Guid ProductId { get; set; }
        public Product Product { get; set; } = null!;

        [Range(1, 100)]
        public int Quantity { get; set; }

        [Required]
        public decimal Price { get; set; }  // قیمت واحد محصول هنگام سفارش
    }
}
