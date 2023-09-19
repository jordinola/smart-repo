using Smartwyre.DeveloperTest.Model;

namespace Smartwyre.DeveloperTest.Domain
{
    public class Rebate
    {
        public string Identifier { get; set; }
        public IncentiveType Incentive { get; set; }
        public decimal Amount { get; set; }
        public decimal Percentage { get; set; }
    }
}
