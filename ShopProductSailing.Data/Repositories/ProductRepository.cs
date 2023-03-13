using ShopProductSailing.Data.IRepositories;
using ShopSailingCRM.Domain.Entities;

namespace ShopProductSailing.Data.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly IDapperRepository<Product> dapperRepository = new DapperRepository<Product>();
        public async Task<bool> DeleteAsync(long id)
        {
            if (await SelectByIdAsync(id) is null)
            {
                return false;
            }

            string query = $"delete from products " +
                           $"where id = {id};";

             await dapperRepository.DeleteAsync(query);

            return true;
        }

        public async Task<Product> InsertAsync(Product product)
        {
            var query = $"INSERT INTO products (name, count, price, createdat) " +
                $"VALUES('{product.Name.Replace("'", "''")}', {product.Count}, '{product.Price}', now());";

            await dapperRepository.InsertAsync(query);

            return await dapperRepository.SelectAsync(
                "select * from products where id = (select max(id) from products);");
        }

        public async Task<List<Product>> SelectAllAsync()
        {
            return await dapperRepository.SelectAllAsync("select * from products;");
        }

        public async Task<List<Product>> SelectAllFillteredAsync(long index, long size)
        {
            string query = $"select * from products" +
                $" offset {(index - 1) * size} limit {size};";

            return await dapperRepository.SelectAllAsync(query);
        }

        public async Task<Product> SelectByIdAsync(long id)
        {
            string query = $"select * from products " +
                $" where id = {id};";

            return await dapperRepository.SelectAsync(query);
        }

        public async Task<Product> UpdateAsync(long id,Product product)
        {
            if (await SelectByIdAsync(id) is not null)
            {
                var query = $"update products " +
                $"set name = '{product.Name.Replace("'", "''")}', " +
                $"count = {product.Count}, " +
                $"price = {product.Price}, " +
                $"updatedat = now() " +
                $"where id = {id}";
                await dapperRepository.UpdateAsync(query);
                return await SelectByIdAsync(id);
            }
            return null;
        }
    }
}
