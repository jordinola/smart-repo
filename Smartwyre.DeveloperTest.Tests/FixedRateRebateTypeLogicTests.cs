using Smartwyre.DeveloperTest.Application.IncentivesLogic;
using Smartwyre.DeveloperTest.Domain;
using Smartwyre.DeveloperTest.Model;
using Xunit;

namespace Smartwyre.DeveloperTest.Tests
{
    public class FixedRateRebateTypeLogicTests
    {
        [Fact]
        public void FixedRateRebateTypeLogic_Calculate_ZeroAmount_ReturnFalse()
        {
            var rebate = new Rebate { Amount = 0 };
            var product = new Product { SupportedIncentives = SupportedIncentiveType.FixedCashAmount };

            var incentiveLogic = new FixedRateRebateTypeLogic();

            var result = incentiveLogic.Calculate(product, rebate);

            Assert.False(result.Result);
            Assert.Equal(0m, result.Amount);
        }


        [Fact]
        public void FixedRateRebateTypeLogic_Calculate_InvalidIncentive_ReturnFalse()
        {
            var rebate = new Rebate { Amount = 1 };
            var product = new Product { SupportedIncentives = SupportedIncentiveType.AmountPerUom };

            var incentiveLogic = new FixedRateRebateTypeLogic();

            var result = incentiveLogic.Calculate(product, rebate);

            Assert.False(result.Result);
            Assert.Equal(0m, result.Amount);
        }

        [Fact]
        public void FixedRateRebateTypeLogic_Calculate_ReturnTrue()
        {
            var rebate = new Rebate { Amount = 1 };
            var product = new Product { SupportedIncentives = SupportedIncentiveType.FixedCashAmount };

            var incentiveLogic = new FixedRateRebateTypeLogic();

            var result = incentiveLogic.Calculate(product, rebate);

            Assert.True(result.Result);
            Assert.Equal(1, result.Amount);
        }
    }
}
