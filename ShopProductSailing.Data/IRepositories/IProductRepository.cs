using ShopSailingCRM.Domain.Entities;

namespace ShopProductSailing.Data.IRepositories
{
    public interface IProductRepository
    {
        Task<Product> InsertAsync(Product product);
        Task<Product> UpdateAsync(long id,Product product);
        Task<bool> DeleteAsync(long id);
        Task<Product> SelectByIdAsync(long id);
        Task<List<Product>> SelectAllAsync();
        Task<List<Product>> SelectAllFillteredAsync(long index, long size);
    }
}
