using Smartwyre.DeveloperTest.Application.IncentivesLogic;
using Smartwyre.DeveloperTest.Domain;
using Smartwyre.DeveloperTest.Model;
using Xunit;

namespace Smartwyre.DeveloperTest.Tests
{
    public class FixedCashAmountTypeLogicTests
    {
        [Fact]
        public void FixedCashAmountTypeLogic_Calculate_ZeroAmount_ReturnFalse()
        {
            var rebate = new Rebate { Amount = 0 };
            var product = new Product { SupportedIncentives = SupportedIncentiveType.AmountPerUom };

            var incentiveLogic = new FixedCashAmountTypeLogic();

            var result = incentiveLogic.Calculate(product, rebate, volume: 1);

            Assert.False(result.Result);
            Assert.Equal(0m, result.Amount);
        }

        [Fact]
        public void FixedCashAmountTypeLogic_Calculate_ZeroVolume_ReturnFalse()
        {
            var rebate = new Rebate { Amount = 1 };
            var product = new Product { SupportedIncentives = SupportedIncentiveType.AmountPerUom };

            var incentiveLogic = new FixedCashAmountTypeLogic();

            var result = incentiveLogic.Calculate(product, rebate, volume: 0);

            Assert.False(result.Result);
            Assert.Equal(0m, result.Amount);
        }


        [Fact]
        public void FixedCashAmountTypeLogic_Calculate_InvalidIncentive_ReturnFalse()
        {
            var rebate = new Rebate { Amount = 1 };
            var product = new Product { SupportedIncentives = SupportedIncentiveType.FixedRateRebate };

            var incentiveLogic = new FixedCashAmountTypeLogic();

            var result = incentiveLogic.Calculate(product, rebate, volume: 1);

            Assert.False(result.Result);
            Assert.Equal(0m, result.Amount);
        }

        [Fact]
        public void FixedCashAmountTypeLogic_Calculate_ReturnTrue()
        {
            var rebate = new Rebate { Amount = 3 };
            var product = new Product { SupportedIncentives = SupportedIncentiveType.AmountPerUom };

            var incentiveLogic = new FixedCashAmountTypeLogic();

            var result = incentiveLogic.Calculate(product, rebate, volume: 2);

            Assert.True(result.Result);
            Assert.Equal(3 * 2, result.Amount);
        }
    }
}
