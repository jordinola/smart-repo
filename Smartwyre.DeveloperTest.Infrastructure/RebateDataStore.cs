using Smartwyre.DeveloperTest.Domain;
using Smartwyre.DeveloperTest.Model;

namespace Smartwyre.DeveloperTest.Infrastructure
{
    public class RebateDataStore : IRebateDataStore
    {
        private readonly List<Rebate> _rebates;

        public RebateDataStore()
        {
            _rebates = new List<Rebate>
            {
                new Rebate
                {
                    Identifier = "1",
                    Amount = 1,
                    Incentive = IncentiveType.FixedRateRebate,
                    Percentage = 10
                },
                new Rebate
                {
                    Identifier = "2",
                    Amount = 4,
                    Incentive = IncentiveType.FixedCashAmount,
                    Percentage = 20
                }
            };
        }

        public Rebate GetRebate(string rebateIdentifier)
        {
            return _rebates.FirstOrDefault(x => x.Identifier == rebateIdentifier)!;
        }

        public void UpdateRebateAmount(string rebateIdentifier, decimal rebateAmount)
        {
            var rebate = _rebates.FirstOrDefault(x => x.Identifier == rebateIdentifier)!;
            if (rebate == null)
            {
                return;
            }

            rebate.Amount = rebateAmount;
        }
    }
}