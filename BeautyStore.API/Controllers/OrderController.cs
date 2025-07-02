using BeautyStore.Application.DTOs;
using BeautyStore.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace BeautyStore.API.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class OrderController : ControllerBase
    {
        private readonly IOrderService _orderService;
        private readonly ICartRepository _cartRepository;

        public OrderController(IOrderService orderService, ICartRepository cartRepository)
        {
            _orderService = orderService;
            _cartRepository = cartRepository;
        }

        // ثبت سفارش
        [HttpPost]
        public async Task<IActionResult> PlaceOrder()
        {
            var userId = GetUserIdFromToken();
            if (userId == Guid.Empty)
                return Unauthorized("شناسه کاربر نامعتبر است.");

            var cartItems = await _cartRepository.GetCartItemsAsync(userId);
            if (cartItems == null || !cartItems.Any())
                return BadRequest("سبد خرید شما خالی است.");

            var createOrderDto = new CreateOrderDto
            {
                UserId = userId,
                Items = cartItems.Select(ci => new OrderItemDto
                {
                    ProductId = ci.ProductId,
                    Quantity = ci.Quantity
                }).ToList()
            };

            var orderId = await _orderService.CreateOrderAsync(createOrderDto);
            await _cartRepository.ClearCartAsync(userId);

            return Ok(new { message = "سفارش با موفقیت ثبت شد", orderId });
        }

        // دریافت لیست سفارش‌های کاربر
        [HttpGet]
        public async Task<IActionResult> GetMyOrders()
        {
            var userId = GetUserIdFromToken();
            if (userId == Guid.Empty)
                return Unauthorized("شناسه کاربر نامعتبر است.");

            var orders = await _orderService.GetOrdersByUserIdAsync(userId);
            return Ok(orders);
        }

        // دریافت جزئیات سفارش خاص
        [HttpGet("{orderId}")]
        public async Task<IActionResult> GetOrderById(Guid orderId)
        {
            var order = await _orderService.GetOrderByIdAsync(orderId);
            if (order == null)
                return NotFound("سفارش مورد نظر یافت نشد.");

            return Ok(order);
        }

        // استخراج شناسه کاربر از توکن JWT
        private Guid GetUserIdFromToken()
        {
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            return Guid.TryParse(userIdClaim, out var userId) ? userId : Guid.Empty;
        }
    }
}
