using BeautyStore.Application.DTOs.Auth;
using BeautyStore.Domain.Entities;

namespace BeautyStore.Application.Interfaces
{
    public interface IAuthService
    {
        Task<string> RegisterAsync(RegisterDto dto);
        Task<string> LoginAsync(LoginDto dto);
    }
}