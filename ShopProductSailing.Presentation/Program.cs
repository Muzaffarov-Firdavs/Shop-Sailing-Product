using ShopSailingCRM.Domain.Entities;
using ShopSailingCRM.Service.Interfaces;
using ShopSailingCRM.Service.Services;

namespace ShopProductSailing.Presentation
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            IPruductService productServ = new ProductService();

            Product product = new Product()
            {
                Name = "Buhanka",
                Count = 50,
                Price = 18,
            };
            //await productRepo.InsertAsync(product);
            var results = await productServ.RemoveAsync(8);

        }
    }
}