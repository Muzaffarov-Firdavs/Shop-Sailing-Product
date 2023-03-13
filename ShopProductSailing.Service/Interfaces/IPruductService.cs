using ShopProductSailing.Domain.Entities;
using ShopSailingCRM.Domain.Entities;
using Upstorm.Service.Helpers;

namespace ShopSailingCRM.Service.Interfaces
{
    public interface IPruductService
    {
        // CRUD actions
        Task<Response<Product>> CreateAsync(Product product);
        Task<Response<Product>> UpdateAsync(long id,Product product);
        Task<Response<bool>> RemoveAsync(long id);
        Task<Response<Product>> GetByIdAsync(long id);
        Task<Response<List<Product>>> GetAllAsync();
        //   Selling actions
        Task<Response<SoldProduct>> SellProductAsync(string name, int count);
    }
}