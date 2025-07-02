using BeautyStore.Application.Interfaces;
using BeautyStore.Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace BeautyStore.API.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class CartController : ControllerBase
    {
        private readonly ICartRepository _cartRepository;

        public CartController(ICartRepository cartRepository)
        {
            _cartRepository = cartRepository;
        }

        // ✅ Helper Method: گرفتن UserId از توکن JWT
        private Guid GetUserIdFromToken()
        {
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            return Guid.TryParse(userIdClaim, out var userId) ? userId : Guid.Empty;
        }

        // 📦 گرفتن اقلام سبد خرید کاربر وارد شده
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CartItem>>> GetCartItems()
        {
            var userId = GetUserIdFromToken();
            var cartItems = await _cartRepository.GetCartItemsAsync(userId);
            return Ok(cartItems);
        }

        // ➕ افزودن آیتم به سبد
        [HttpPost]
        public async Task<ActionResult> AddToCart([FromBody] CartItem item)
        {
            item.UserId = GetUserIdFromToken();
            await _cartRepository.AddToCartAsync(item);
            return Ok(new { message = "محصول به سبد اضافه شد" });
        }

        // ✏️ بروزرسانی تعداد محصول
        [HttpPut("{cartItemId}")]
        public async Task<ActionResult> UpdateQuantity(Guid cartItemId, [FromQuery] int quantity)
        {
            await _cartRepository.UpdateQuantityAsync(cartItemId, quantity);
            return Ok(new { message = "تعداد به‌روزرسانی شد" });
        }

        // ❌ حذف یک آیتم از سبد
        [HttpDelete("{cartItemId}")]
        public async Task<ActionResult> RemoveFromCart(Guid cartItemId)
        {
            await _cartRepository.RemoveFromCartAsync(cartItemId);
            return Ok(new { message = "محصول حذف شد" });
        }

        // 🧹 پاک کردن کل سبد خرید کاربر
        [HttpDelete("clear")]
        public async Task<ActionResult> ClearCart()
        {
            var userId = GetUserIdFromToken();
            await _cartRepository.ClearCartAsync(userId);
            return Ok(new { message = "سبد خرید پاک شد" });
        }
    }
}
