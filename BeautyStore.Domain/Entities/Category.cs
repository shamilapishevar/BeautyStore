using System.ComponentModel.DataAnnotations;


namespace BeautyStore.Domain.Entities
{

    public class Category
    {
        public Guid Id { get; set; } = Guid.NewGuid();

        [Required, MaxLength(50)]
        public string Name { get; set; } = string.Empty;

        public ICollection<Product> Products { get; set; } = new List<Product>();

        public string? Description { get; set; }  // این خط رو اضافه کنید اگر میخواید توضیح داشته باشه
    }
}
