using CategoryServiceSE1.Protos;
using CategoryServiceSE1.Repositories.Interfaces;
using Grpc.Core;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CategoryServiceSE1.Services
{
    public class CategoryService : Shop.ShopBase
    {
        private readonly ILogger<CategoryService> _logger;
        private readonly ICategoryRepository _categoryRepository;

        public CategoryService(ILogger<CategoryService> logger, ICategoryRepository categoryRepository)
        {
            _logger = logger;
            _categoryRepository = categoryRepository;
        }
        public override async Task<CategoryInfo> AddCategory(CategoryCreate request, ServerCallContext context)
        {

            var res = await _categoryRepository.AddCategory(request);
            var categoryInfo = new CategoryInfo()
            {
                Id = res.Id,
                Name = res.Name,
                ParentCategoryId = res.ParentCategoryId
            };
            return categoryInfo;
        }
        public override Task<CategoryInfo> GetCategoryById(CategoryLookup request, ServerCallContext context)
        {
            return base.GetCategoryById(request, context);
        }
        public override async Task<ProductInfo> AddProduct(ProductCreate request, ServerCallContext context)
        {
            var res = await _categoryRepository.AddProduct(request);
            var productInfo = new ProductInfo()
            {
                Id = res.Id,
                Name = res.Name,
                Description = res.Description,
                Price = res.Price,
                CategoryId = res.CategoryId

            };
            return productInfo;
        }
        public override Task<ProductInfo> GetProductById(ProductLookup request, ServerCallContext context)
        {
            return base.GetProductById(request, context);
        }
        public override Task GetProductByCategoryId(CategoryLookup request, IServerStreamWriter<ProductInfo> responseStream, ServerCallContext context)
        {
            return base.GetProductByCategoryId(request, responseStream, context);
        }

    }
}
