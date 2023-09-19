using Smartwyre.DeveloperTest.Domain;

namespace Smartwyre.DeveloperTest.Infrastructure
{
    public class ProductDataStore : IProductDataStore
    {
        public Product GetProduct(string productIdentifier)
        {
            // Access database to retrieve account, code removed for brevity 
            return new Product();
        }
    }
}
