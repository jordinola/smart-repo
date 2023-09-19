using Smartwyre.DeveloperTest.Domain;
using Smartwyre.DeveloperTest.Model;

namespace Smartwyre.DeveloperTest.Application.IncentivesLogic
{
    public class FixedRateRebateTypeLogic : IIncentiveLogic
    {
        public (bool Result, decimal Amount) Calculate(Product product, Rebate rebate, decimal volume = 0m)
        {
            if (rebate.Amount == 0 || !product.SupportedIncentives.HasFlag(SupportedIncentiveType.FixedCashAmount))
            {
                return (false, 0m);
            }

            return (true, rebate.Amount);
        }

        public IncentiveType GetIncentiveType() => IncentiveType.FixedRateRebate;
    }
}
