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
        public async override Task<CategoryInfo> GetCategoryById(CategoryLookup request, ServerCallContext context)
        {
            var res = await _categoryRepository.GetCategoryById(request.Id);
            var categoryInfo = new CategoryInfo()
            {
                Id = res.Id,
                Name = res.Name,
                ParentCategoryId = res.ParentCategoryId
            };

            return await Task.FromResult(categoryInfo);
        }
        public override async Task<ProductInfo> AddProduct(ProductCreate request, ServerCallContext context)
        {
            var res = await _categoryRepository.AddProduct(request);
            
            var category = await _categoryRepository.GetCategoryById(res.CategoryId);
            var categoryInfo = new CategoryInfo()
            {
                Id = category.Id,
                Name = category.Name,
                ParentCategoryId = category.ParentCategoryId
            };
            var productInfo = new ProductInfo()
            {
                Id = res.Id,
                Name = res.Name,
                Description = res.Description,
                Price = res.Price,
                //CategoryId = res.CategoryId

            };
            return productInfo;
        }
        public override Task<ProductInfo> GetProductById(ProductLookup request, ServerCallContext context)
        {
            return base.GetProductById(request, context);
        }
        public override async Task GetProductByCategoryId(CategoryLookup request, IServerStreamWriter<ProductInfo> responseStream, ServerCallContext context)
        {
            var res = await _categoryRepository.GetProductsByCategoryId(request.Id);
            foreach (var product in res)
            {

                await Task.Delay(1000);
                var category = await _categoryRepository.GetCategoryById(product.CategoryId);
                var categoryInfo = new CategoryInfo()
                {
                    Id = category.Id,
                    Name = category.Name,
                    ParentCategoryId = category.ParentCategoryId
                };
                var productInfo = new ProductInfo()
                {
                    Id = product.Id,
                    Name = product.Name,
                    Description = product.Description,
                    Price = product.Price,
                    CategoryId = categoryInfo
                };
                await responseStream.WriteAsync(productInfo);
            }
        }

        public override async Task<ProductInfo> ChangeCategoryOfProduct(ProductInfo request, ServerCallContext context)
        {
            var res = await _categoryRepository.ChangeCategory(request);
            if (res)
                return await Task.FromResult(request);
            throw new Exception("Failed");
        }
    }
}
}
