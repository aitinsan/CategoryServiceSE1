using CategoryServiceSE1.Data;
using CategoryServiceSE1.Models;
using CategoryServiceSE1.Protos;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CategoryServiceSE1.Repositories.Interfaces
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly DataContext _context;
        public CategoryRepository(DataContext context)
        {
            _context = context;
        }
        public async Task<Category> AddCategory(CategoryCreate category)
        {
            var categoryToCreate = new Category()
            {
                Name = category.Name,
                ParentCategoryId = category.ParentCategoryId
            };
            await _context.Categories.AddAsync(categoryToCreate);
            await _context.SaveChangesAsync();
            return categoryToCreate;
        }

        public async Task<Product> AddProduct(ProductCreate product)
        {
            var productToCreate = new Product()
            {
                Name = product.Name,
                Description = product.Description,
                Price = product.Price,
                CategoryId = product.CategoryId
            };
            await _context.Products.AddAsync(productToCreate);
            await _context.SaveChangesAsync();
            return productToCreate;
        }

        public async Task<bool> ChangeCategory(ProductInfo product)
        {
            var productToUpdate = await _context.Products.Where(x => x.Id == product.Id).FirstOrDefaultAsync();
            productToUpdate.CategoryId = product.CategoryId.Id;
            return await (_context.SaveChangesAsync()) > 0;
        }

        public async Task<Category> GetCategoryById(int id)
        {
            return await _context.Categories.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<Product> GetProductById(int id)
        {
            return await _context.Products.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<IEnumerable<Product>> GetProductsByCategoryId(int id)
        {
            return await _context.Products.Where(x => x.CategoryId == id).ToListAsync();
        }
    }
}
