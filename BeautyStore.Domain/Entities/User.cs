using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BeautyStore.Domain.Entities
{
    public class User
    {
        public Guid Id { get; set; } = Guid.NewGuid();

        [Required]
        [MaxLength(30)]
        public string Username { get; set; } = string.Empty;

        [Required]
        [EmailAddress]
        [MaxLength(100)]
        public string Email { get; set; } = string.Empty;

        [Required]
        [MinLength(6)]
        public string PasswordHash { get; set; } = string.Empty;

        // رابطه با آیتم‌های سبد خرید
        public ICollection<CartItem> CartItems { get; set; } = new List<CartItem>();

        // نقش کاربر با مقدار پیش‌فرض "User"
        public string Role { get; set; } = "User";

        // (اختیاری) فعال/غیرفعال بودن حساب کاربری
        public bool IsActive { get; set; } = true;
    }
}
