using Smartwyre.DeveloperTest.Application.IncentivesLogic;
using Smartwyre.DeveloperTest.Domain;
using Smartwyre.DeveloperTest.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Smartwyre.DeveloperTest.Tests
{
    public class AmountPerUomTypeLogicTests
    {
        [Fact]
        public void AmountPerUomTypeLogicTests_Calculate_ZeroPercentage_ReturnFalse()
        {
            var rebate = new Rebate { Percentage = 0 };
            var product = new Product { Price = 1, SupportedIncentives = SupportedIncentiveType.FixedRateRebate };

            var incentiveLogic = new AmountPerUomTypeLogic();

            var result = incentiveLogic.Calculate(product, rebate, volume: 1);

            Assert.False(result.Result);
            Assert.Equal(0m, result.Amount);
        }

        [Fact]
        public void AmountPerUomTypeLogicTests_Calculate_ZeroPrice_ReturnFalse()
        {
            var rebate = new Rebate { Percentage = 1 };
            var product = new Product { Price = 0, SupportedIncentives = SupportedIncentiveType.FixedRateRebate };

            var incentiveLogic = new AmountPerUomTypeLogic();

            var result = incentiveLogic.Calculate(product, rebate, volume: 1);

            Assert.False(result.Result);
            Assert.Equal(0m, result.Amount);
        }

        [Fact]
        public void AmountPerUomTypeLogicTests_Calculate_ZeroVolume_ReturnFalse()
        {
            var rebate = new Rebate { Percentage = 1 };
            var product = new Product { Price = 1, SupportedIncentives = SupportedIncentiveType.FixedRateRebate };

            var incentiveLogic = new AmountPerUomTypeLogic();

            var result = incentiveLogic.Calculate(product, rebate, volume: 0);

            Assert.False(result.Result);
            Assert.Equal(0m, result.Amount);
        }


        [Fact]
        public void AmountPerUomTypeLogicTests_Calculate_InvalidIncentive_ReturnFalse()
        {
            var rebate = new Rebate { Percentage = 1 };
            var product = new Product { Price = 1, SupportedIncentives = SupportedIncentiveType.AmountPerUom };

            var incentiveLogic = new AmountPerUomTypeLogic();

            var result = incentiveLogic.Calculate(product, rebate, volume: 1);

            Assert.False(result.Result);
            Assert.Equal(0m, result.Amount);
        }

        [Fact]
        public void AmountPerUomTypeLogicTests_Calculate_ReturnTrue()
        {
            var rebate = new Rebate { Percentage = 3 };
            var product = new Product { Price = 2, SupportedIncentives = SupportedIncentiveType.FixedRateRebate };

            var incentiveLogic = new AmountPerUomTypeLogic();

            var result = incentiveLogic.Calculate(product, rebate, volume: 2);

            Assert.True(result.Result);
            Assert.Equal(3 * 2 * 2, result.Amount);
        }
    }
}
