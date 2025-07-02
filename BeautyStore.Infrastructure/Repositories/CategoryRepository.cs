﻿using BeautyStore.Application.Interfaces;
using BeautyStore.Domain.Entities;
using BeautyStore.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace BeautyStore.Infrastructure.Repositories
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly BeautyStoreDbContext _context;

        public CategoryRepository(BeautyStoreDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Category>> GetAllAsync()
        {
            return await _context.Categories.Include(c => c.Products).ToListAsync();
        }

        public async Task<Category?> GetByIdAsync(Guid id)
        {
            return await _context.Categories.Include(c => c.Products)
                                            .FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task AddAsync(Category category)
        {
            _context.Categories.Add(category);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Category category)
        {
            _context.Categories.Update(category);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Guid id)
        {
            var category = await _context.Categories.FindAsync(id);
            if (category != null)
            {
                _context.Categories.Remove(category);
                await _context.SaveChangesAsync();
            }
        }
    }
}
