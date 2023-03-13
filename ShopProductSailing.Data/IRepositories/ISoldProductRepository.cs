using ShopProductSailing.Domain.Entities;

namespace ShopProductSailing.Data.IRepositories
{
    public interface ISoldProductRepository 
    {
        Task<SoldProduct> InsertAsync(SoldProduct product);
        Task<SoldProduct> UpdateAsync(long id, SoldProduct product);
        Task<bool> DeleteAsync(long id);
        Task<SoldProduct> SelectByIdAsync(long id);
        Task<List<SoldProduct>> SelectAllAsync();
        Task<List<SoldProduct>> SelectAllFillteredAsync(long index, long size);
    }
}
