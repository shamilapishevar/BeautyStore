using BeautyStore.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BeautyStore.Application.Interfaces
{
    public interface IProductImageRepository
    {
        Task<IEnumerable<ProductImage>> GetByProductIdAsync(Guid productId);
        Task AddAsync(ProductImage productImage);
        Task DeleteAsync(Guid id);
    }
}
