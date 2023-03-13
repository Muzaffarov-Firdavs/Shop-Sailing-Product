using ShopProductSailing.Data.IRepositories;
using ShopProductSailing.Data.Repositories;
using ShopProductSailing.Domain.Entities;
using ShopSailingCRM.Domain.Entities;
using ShopSailingCRM.Service.Interfaces;
using Upstorm.Service.Helpers;

namespace ShopSailingCRM.Service.Services
{
    public class ProductService : IPruductService
    {
        private readonly IProductRepository productRepo = new ProductRepository();
        private readonly ISoldProductRepository soldProductRepo = new SoldProductRepository();

        public async Task<Response<Product>> CreateAsync(Product product)
        {
            var entities = await productRepo.SelectAllAsync();
            var model = entities.FirstOrDefault(x => x.Name == product.Name);
            if (model is not null)
                return new Response<Product>();


            var result = await productRepo.InsertAsync(product);
            
            return new Response<Product>()
            {
                StatusCode = 200,
                Message = "Succeess..!",
                Result = result
            };
        }
            

        public async Task<Response<bool>> RemoveAsync(long id)
        {
            var model = await productRepo.SelectByIdAsync(id);
            if (model is not null)
            {
                var result = await productRepo.DeleteAsync(id);
                return new Response<bool>()
                {
                    StatusCode = 200,
                    Message = "Succeess",
                    Result = result
                };
            }
            return new Response<bool>()
            {
                Result = false 
            };
        }

        public async Task<Response<List<Product>>> GetAllAsync()
        {
            List<Product> products = await productRepo.SelectAllAsync();
            return new Response<List<Product>>()
            {
                StatusCode = 200,
                Message = "Success",
                Result = products
            };
        }

        public async Task<Response<Product>> GetByIdAsync(long id)
        {
            List<Product> products = await productRepo.SelectAllAsync();
            Product checkingModel = products.FirstOrDefault(x => x.Id == id);

            if (checkingModel is null)
            {
                return new Response<Product>() { };
            }

            Product takenModel = await productRepo.SelectByIdAsync(id);

            return new Response<Product>()
            {
                StatusCode = 200,
                Message = "Success",
                Result = takenModel
            };
        }

        public async Task<Response<Product>> UpdateAsync(long id,Product product)
        {
            List<Product> products = await productRepo.SelectAllAsync();
            Product checkingModel = products.FirstOrDefault(x => x.Id == id);

            if (checkingModel is null)
            {
                return new Response<Product>() { };
            }

            Product updatedProduct = await productRepo.UpdateAsync(id, product);
            return new Response<Product>()
            {
                StatusCode = 200,
                Message = "Success",
                Result = updatedProduct
            };
        }

        // selling product action 
        public async Task<Response<SoldProduct>> SellProductAsync(string name, int count)
        {
            // to check product have or don't have
            List<Product> products = await productRepo.SelectAllAsync();
            Product checkingModel = products.FirstOrDefault(x => x.Name == name);

            if (checkingModel is null)
            {
                return new Response<SoldProduct>() { };
            }

            // to check sold product, which before had been sold this name product
            // if was sold join this count and price
            List<SoldProduct> soldproducts = await soldProductRepo.SelectAllAsync();
            Product productWasSold = products.FirstOrDefault(x => x.Name == name);

            var result = await soldProductRepo.InsertAsync(
                new SoldProduct()
                {
                    Name = checkingModel.Name,
                    Count = count,
                    Price = checkingModel.Price*count,
                });

            return new Response<SoldProduct>()
            {
                StatusCode = 200,
                Message = "Success",
                Result = result
            };

        }
    }
}
