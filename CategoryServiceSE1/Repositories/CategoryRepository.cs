using CategoryServiceSE1.Data;
using CategoryServiceSE1.Models;
using CategoryServiceSE1.Protos;
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
            //context.add
            throw new NotImplementedException();
        }

        public Task<Product> AddProduct(ProductCreate product)
        {
            throw new NotImplementedException();
        }

        public async Task<Category> GetCategoryById(int id)
        {
            throw new NotImplementedException();
        }

        public Task<Product> GetProductById(int id)
        {
            throw new NotImplementedException();
        }
    }
}
