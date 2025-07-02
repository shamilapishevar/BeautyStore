using BeautyStore.Application.Interfaces;
using BeautyStore.Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BeautyStore.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserRepository _userRepository;

        public UserController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        /// <summary>
        /// دریافت لیست همه کاربران
        /// </summary>
        [HttpGet]
        [Authorize(Roles = "Admin")] // فقط ادمین‌ها می‌تونن لیست کاربرها رو ببینن
        public async Task<ActionResult<IEnumerable<User>>> GetAllUsers()
        {
            var users = await _userRepository.GetAllAsync();
            return Ok(users);
        }

        /// <summary>
        /// دریافت اطلاعات کاربر خاص
        /// </summary>
        [HttpGet("{id}")]
        [Authorize]
        public async Task<ActionResult<User>> GetUserById(Guid id)
        {
            var user = await _userRepository.GetByIdAsync(id);
            if (user == null)
                return NotFound(new { message = "User not found" });

            return Ok(user);
        }

        /// <summary>
        /// حذف کاربر
        /// </summary>
        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteUser(Guid id)
        {
            var user = await _userRepository.GetByIdAsync(id);
            if (user == null)
                return NotFound(new { message = "User not found" });

            // فرض می‌کنیم IUserRepository متدی برای حذف داره
            await _userRepository.DeleteAsync(id);
            return Ok(new { message = "User deleted successfully" });
        }

        /// <summary>
        /// ویرایش اطلاعات کاربر
        /// </summary>
        [HttpPut("{id}")]
        [Authorize]
        public async Task<IActionResult> UpdateUser(Guid id, [FromBody] User updatedUser)
        {
            if (id != updatedUser.Id)
                return BadRequest(new { message = "Mismatched user ID" });

            var existingUser = await _userRepository.GetByIdAsync(id);
            if (existingUser == null)
                return NotFound(new { message = "User not found" });

            // اینجا می‌تونی فقط فیلدهایی که قابل ویرایش هستند رو تغییر بدی
            existingUser.Username = updatedUser.Username;
            existingUser.Email = updatedUser.Email;

            await _userRepository.UpdateAsync(existingUser);
            return Ok(new { message = "User updated successfully" });
        }
    }
}

