using Smartwyre.DeveloperTest.Domain;

namespace Smartwyre.DeveloperTest.Infrastructure
{
    public interface IRebateDataStore
    {
        Rebate GetRebate(string rebateIdentifier);
        void UpdateRebateAmount(string rebateIdentifier, decimal rebateAmount);
    }
}
