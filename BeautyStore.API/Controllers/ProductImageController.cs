using BeautyStore.Application.Interfaces;
using BeautyStore.Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BeautyStore.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]  // نیازمند لاگین
    public class ProductImageController : ControllerBase
    {
        private readonly IProductImageRepository _productImageRepository;

        public ProductImageController(IProductImageRepository productImageRepository)
        {
            _productImageRepository = productImageRepository;
        }

        // GET: api/ProductImage/by-product/{productId}
        [HttpGet("by-product/{productId}")]
        [AllowAnonymous]
        public async Task<IActionResult> GetImagesByProduct(Guid productId)
        {
            var images = await _productImageRepository.GetByProductIdAsync(productId);
            return Ok(images);
        }

        // POST: api/ProductImage/upload
        [HttpPost("upload")]
        public async Task<IActionResult> UploadImage([FromForm] Guid productId, [FromForm] IFormFile file)
        {
            if (file == null || file.Length == 0)
                return BadRequest("No file uploaded.");

            // مسیر ذخیره فایل (مثال در wwwroot/images)
            var uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images");
            if (!Directory.Exists(uploadsFolder))
                Directory.CreateDirectory(uploadsFolder);

            // نام فایل منحصربه‌فرد بساز
            var fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
            var filePath = Path.Combine(uploadsFolder, fileName);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            // آدرس ذخیره شده در فایل
            var imageUrl = $"/images/{fileName}";

            var productImage = new ProductImage
            {
                ProductId = productId,
                ImageUrl = imageUrl
            };

            await _productImageRepository.AddAsync(productImage);

            return Ok(new { message = "Image uploaded successfully", imageUrl });
        }

        // DELETE: api/ProductImage/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteImage(Guid id)
        {
            await _productImageRepository.DeleteAsync(id);
            return NoContent();
        }
    }
}
