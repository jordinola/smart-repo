using Smartwyre.DeveloperTest.Domain;

namespace Smartwyre.DeveloperTest.Infrastructure
{
    public interface IProductDataStore
    {
        Product GetProduct(string productIdentifier);
    }
}
