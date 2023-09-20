using Smartwyre.DeveloperTest.Domain;
using Smartwyre.DeveloperTest.Model;

namespace Smartwyre.DeveloperTest.Application.IncentivesLogic
{
    public class AmountPerUomTypeLogic : IIncentiveLogic
    {
        public (bool Result, decimal Amount) Calculate(Product product, Rebate rebate, decimal volume = 0m)
        {
            if (!product.SupportedIncentives.HasFlag(SupportedIncentiveType.FixedRateRebate)
                || rebate.Percentage == 0 || product.Price == 0 || volume == 0m)
            {
                return (false, 0m);
            }
            
            return (true, product.Price * rebate.Percentage * volume);
            
        }
    }
}
