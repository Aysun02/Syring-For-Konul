

using Core.Entities;
using DataAccess.Repositories.Abstract;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.Rendering;
using WebApp.Services.Abstract;
using WebApp.ViewModels.Product;


namespace WebApp.Services.Concrete
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;
        private readonly ICategoryRepository _categoryRepository;
        private readonly IWebHostEnvironment _webHostEnviroment;
        private readonly ModelStateDictionary _modelState;


        public ProductService(IProductRepository productRepository,
            IActionContextAccessor actionContextAccessor,
            ICategoryRepository categoryRepository,
            IWebHostEnvironment webHostEnviroment)
        {
            _productRepository = productRepository;
            _categoryRepository = categoryRepository;
            _webHostEnviroment = webHostEnviroment;
            _modelState = actionContextAccessor.ActionContext.ModelState;
        }

        #region Index
        public async Task<ProductIndexVM> GetAllAsync()
        {
            var model = new ProductIndexVM()
            {
                Products = await _productRepository.GetAllWithCategoryAsync()
            };
            return model;

        }
        #endregion

        #region Create
        public async Task<bool> CreateAsync(ProductCreateVM model)
        {
            if (!_modelState.IsValid) return false;
            var isExist = await _productRepository.AnyAsync(c => c.Title.Trim().ToLower() == model.Name.Trim().ToLower());
            if (isExist)
            {
                _modelState.AddModelError("Name", "This product already is exist");
                return false;
            }
         

            var product = new Product
            {
                Title = model.Name,
                Price = model.Price,
                Quantity = model.Quantity,
                CategoryId = model.CategoryId,
                CreatedAt = DateTime.Now
            };
            await _productRepository.CreateAsync(product);
            return true;
        }

        public async Task<ProductCreateVM> GetCreateModelAsync()
        {
            var categories = await _categoryRepository.GetAllAsync();

            var model = new ProductCreateVM
            {
                Categories = categories.Select(c => new SelectListItem
                {
                    Text = c.Title,
                    Value = c.Id.ToString()
                }).ToList()

            }; return model;
        }
        #endregion

        #region Update
        public async Task<ProductUpdateVM> GetUpdateModelAsync(int id)
        {
            var product = await _productRepository.GetAsync(id);

            if (product != null)
            {
                var categories = await _categoryRepository.GetAllAsync();

                var model = new ProductUpdateVM
                {
                    Id = product.Id,
                    Name = product.Title,
                    Categories = categories.Select(c => new SelectListItem
                    {
                        Text = c.Title,
                        Value = c.Id.ToString()
                    }).ToList(),
                    CategoryId = product.CategoryId,
                };
                return model;

            }
            return null;
        }

        public async Task<bool> UpdateAsync(ProductUpdateVM model)
        {
            var isExist = await _productRepository.AnyAsync(c => c.Title.Trim().ToLower() == model.Name.Trim().ToLower() && c.Id != model.Id);
            if (isExist)
            {
                _modelState.AddModelError("Name", "This product already is exist");
                return false;
            }
            var product = await _productRepository.GetAsync(model.Id);
            if (product == null) return false;

            product.Title = model.Name;
            product.Quantity = model.Quantity;
            product.Price = model.Price;
            product.CategoryId = model.CategoryId;
            product.ModifiedAt = DateTime.Now;
            await _productRepository.UpdateAsync(product);

            return true;
        }
        #endregion

        #region Delete
        public async Task DeleteAsync(int id)
        {
            var product = await _productRepository.GetAsync(id);
            if (product != null)
            {
                await _productRepository.DeleteAsync(product);
            }
        }
        #endregion

    }
}
