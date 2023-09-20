using Smartwyre.DeveloperTest.Domain;
using Smartwyre.DeveloperTest.Model;

namespace Smartwyre.DeveloperTest.Application.IncentivesLogic
{
    public class FixedCashAmountTypeLogic : IIncentiveLogic
    {
        public (bool Result, decimal Amount) Calculate(Product product, Rebate rebate, decimal volume = 0)
        {
            if (!product.SupportedIncentives.HasFlag(SupportedIncentiveType.AmountPerUom)
                || rebate.Amount == 0 || volume == 0)
            {
                return (false, 0m);
            }

            return (true, rebate.Amount * volume);
        }
    }
}
