using BeautyStore.Application.Interfaces;
using BeautyStore.Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BeautyStore.API.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class ProductController : ControllerBase
    {
        private readonly IProductRepository _productRepository;

        public ProductController(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        /// <summary>
        /// دریافت همه محصولات (قابل مشاهده برای همه)
        /// </summary>
        [HttpGet]
        [AllowAnonymous]
        public async Task<ActionResult<IEnumerable<Product>>> GetAll()
        {
            var products = await _productRepository.GetAllAsync();
            return Ok(products);
        }

        /// <summary>
        /// دریافت محصول خاص با شناسه
        /// </summary>
        [HttpGet("{id}")]
        [AllowAnonymous]
        public async Task<ActionResult<Product>> GetById(Guid id)
        {
            var product = await _productRepository.GetByIdAsync(id);
            if (product == null)
                return NotFound(new { message = "Product not found." });

            return Ok(product);
        }

        /// <summary>
        /// افزودن محصول جدید (فقط توسط کاربران احراز هویت‌شده)
        /// </summary>
        [HttpPost]
        public async Task<ActionResult> Create([FromBody] Product product)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            await _productRepository.AddAsync(product);
            return CreatedAtAction(nameof(GetById), new { id = product.Id }, product);
        }

        /// <summary>
        /// به‌روزرسانی اطلاعات محصول
        /// </summary>
        [HttpPut("{id}")]
        public async Task<ActionResult> Update(Guid id, [FromBody] Product product)
        {
            if (id != product.Id)
                return BadRequest(new { message = "ID in URL does not match product ID." });

            var existing = await _productRepository.GetByIdAsync(id);
            if (existing == null)
                return NotFound(new { message = "Product not found." });

            await _productRepository.UpdateAsync(product);
            return Ok(new { message = "Product updated successfully." });
        }

        /// <summary>
        /// حذف محصول با شناسه
        /// </summary>
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(Guid id)
        {
            var existing = await _productRepository.GetByIdAsync(id);
            if (existing == null)
                return NotFound(new { message = "Product not found." });

            await _productRepository.DeleteAsync(id);
            return Ok(new { message = "Product deleted successfully." });
        }
    }
}
