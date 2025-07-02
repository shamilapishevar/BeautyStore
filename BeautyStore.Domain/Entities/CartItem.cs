using System.ComponentModel.DataAnnotations;


namespace BeautyStore.Domain.Entities
{
    public class CartItem
    {
        public Guid Id { get; set; } = Guid.NewGuid();

        [Required]
        public Guid UserId { get; set; }

        public User User { get; set; } = null!;

        [Required]
        public Guid ProductId { get; set; }

        public Product Product { get; set; } = null!;

        [Range(1, 100)]
        public int Quantity { get; set; }
    }
}

