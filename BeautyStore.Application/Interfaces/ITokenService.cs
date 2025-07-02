using BeautyStore.Domain.Entities;

namespace BeautyStore.Application.Interfaces
{
    public interface ITokenService
    {
        string GenerateToken(User user);
    }
}
