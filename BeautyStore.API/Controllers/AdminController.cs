using BeautyStore.Application.Interfaces;
using BeautyStore.Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BeautyStore.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(Roles = "Admin")]
    public class AdminController : ControllerBase
    {
        private readonly IUserRepository _userRepository;

        public AdminController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        [HttpGet("users")]
        public async Task<IActionResult> GetAllUsers()
        {
            var users = await _userRepository.GetAllAsync();
            return Ok(users);
        }

        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> DeleteUser(Guid id)
        {
            var user = await _userRepository.GetByIdAsync(id);
            if (user == null)
                return NotFound("User not found");

            await _userRepository.DeleteAsync(user.Id); // ✅ اصلاح‌شده
            return Ok("User deleted successfully");
        }

        [HttpPut("change-role/{id}")]
        public async Task<IActionResult> ChangeUserRole(Guid id, [FromBody] string newRole)
        {
            var user = await _userRepository.GetByIdAsync(id);
            if (user == null)
                return NotFound("User not found");

            user.Role = newRole;
            await _userRepository.UpdateAsync(user);
            return Ok($"User role changed to {newRole}");
        }

        [HttpPut("deactivate/{id}")]
        public async Task<IActionResult> DeactivateUser(Guid id)
        {
            var user = await _userRepository.GetByIdAsync(id);
            if (user == null)
                return NotFound("User not found");

            user.IsActive = false;
            await _userRepository.UpdateAsync(user);
            return Ok("User deactivated");
        }
    }
}
