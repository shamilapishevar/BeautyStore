using BeautyStore.Domain.Entities;

namespace BeautyStore.Application.Interfaces
{
    public interface ICartRepository
    {
        Task<IEnumerable<CartItem>> GetCartItemsAsync(Guid userId);
        Task AddToCartAsync(CartItem item);
        Task UpdateQuantityAsync(Guid cartItemId, int quantity);
        Task RemoveFromCartAsync(Guid cartItemId);
        Task ClearCartAsync(Guid userId);
    }
}
