using BeautyStore.Domain.Entities;

namespace BeautyStore.Application.Interfaces
{
    public interface IJwtTokenGenerator
    {
        string GenerateToken(User user);
    }
}
