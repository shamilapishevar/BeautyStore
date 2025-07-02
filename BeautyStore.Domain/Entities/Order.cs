using System.ComponentModel.DataAnnotations;

namespace BeautyStore.Domain.Entities
{
    public class Order
    {
        public Guid Id { get; set; } = Guid.NewGuid();

        [Required]
        public Guid UserId { get; set; }
        public User User { get; set; } = null!;

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        [Required]
        public decimal TotalAmount { get; set; }  // مبلغ کل سفارش

        [Required]
        [MaxLength(20)]
        public string Status { get; set; } = "Pending";  // وضعیت سفارش

        public ICollection<OrderItem> Items { get; set; } = new List<OrderItem>();
    }
}
