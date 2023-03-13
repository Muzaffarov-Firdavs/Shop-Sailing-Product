using ShopProductSailing.Data.IRepositories;
using ShopProductSailing.Domain.Entities;

namespace ShopProductSailing.Data.Repositories
{
    public class SoldProductRepository : ISoldProductRepository
    {
        private readonly IDapperRepository<SoldProduct> dapperRepository = new DapperRepository<SoldProduct>();

        public async Task<bool> DeleteAsync(long id)
        {

            var model = SelectByIdAsync(id);
            if (model is null)
            {
                return false;
            }

            string query = $"delete from soldproducts " +
                $"where id = {id};";

            await dapperRepository.DeleteAsync(query);

            return true;
        }

        public async Task<SoldProduct> InsertAsync(SoldProduct product)
        {
            var query = $"INSERT INTO soldproducts (name, count, price, soldat) " +
               $"VALUES('{product.Name.Replace("'", "''")}', {product.Count}, {product.Price}, now());";

            await dapperRepository.InsertAsync(query);

            string resultQuery = "select * from products;";

            return await dapperRepository.SelectAsync(
                "select * from soldproducts where id = (select max(id) from soldproducts);");
        }

        public async Task<List<SoldProduct>> SelectAllAsync()
        {
            return await dapperRepository.SelectAllAsync("select * from products;");
        }

        public async Task<List<SoldProduct>> SelectAllFillteredAsync(long index, long size)
        {
            string query = $"select * from products" +
                $" offset {(index - 1) * size} limit {size};";

            return await dapperRepository.SelectAllAsync(query);
        }

        public async Task<SoldProduct> SelectByIdAsync(long id)
        {
            string query = $"select * from products " +
               $" where id = {id};";

            return await dapperRepository.SelectAsync(query);
        }

        public async Task<SoldProduct> UpdateAsync(long id, SoldProduct product)
        {
            SoldProduct model = await SelectByIdAsync(id);
            if (model is null)
            {
                return null;
            }

            var query = $"update products set " +
                $"name = '{product.Name.Replace("'", "''")}', " +
                $"count = {product.Count}, " +
                $"price = {product.Price}, " +
                $"updateat = now() " +
                $"where id = {id}";

            await dapperRepository.UpdateAsync(query);

            return await SelectByIdAsync(id);
        }
    }
}
