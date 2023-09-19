using Smartwyre.DeveloperTest.Domain;

namespace Smartwyre.DeveloperTest.Infrastructure
{
    public interface IRebateDataStore
    {
        Rebate GetRebate(string rebateIdentifier);
        void StoreCalculationResult(Rebate account, decimal rebateAmount);
    }
}
