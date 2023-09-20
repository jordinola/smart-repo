using Smartwyre.DeveloperTest.Domain;
using Smartwyre.DeveloperTest.Model;

namespace Smartwyre.DeveloperTest.Infrastructure
{
    public class ProductDataStore : IProductDataStore
    {
        private readonly List<Product> _products;

        public ProductDataStore()
        {
            _products = new List<Product>
            {
                new Product
                {
                    Id = 1,
                    Identifier = "1",
                    Price = 1,
                    SupportedIncentives = SupportedIncentiveType.FixedRateRebate,
                    Uom = "1"
                },
                new Product
                {
                    Id = 2,
                    Identifier = "2",
                    Price = 3,
                    SupportedIncentives = SupportedIncentiveType.AmountPerUom,
                    Uom = "1"
                }
            };
        }

        public Product GetProduct(string productIdentifier)
        {
            return _products.FirstOrDefault(x => x.Identifier == productIdentifier)!;
        }
    }
}
