using Moq;
using Smartwyre.DeveloperTest.Application.IncentivesLogic;
using Smartwyre.DeveloperTest.Application.IncentivesLogic.Factory;
using Smartwyre.DeveloperTest.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Smartwyre.DeveloperTest.Tests;

public class IncentivesLogicFactoryTests
{
    [Fact]
    public void IncentivesLogicFactory_GetLogic_LogicNotImplemented()
    {
        var fixedCashLogicMock = new FixedCashAmountTypeLogic();
        var fixedRateLogicMock = new FixedRateRebateTypeLogic();

        IEnumerable<IIncentiveLogic> incentiveLogics = new List<IIncentiveLogic>
        {
            fixedCashLogicMock,
            fixedRateLogicMock
        };

        var factory = new IncentivesLogicFactory(incentiveLogics);

        Assert.Throws<InvalidOperationException>(() => factory.GetIncentiveLogic(IncentiveType.NotImplemented));
    }

    [Fact]
    public void IncentivesLogicFactory_GetLogic_LogicNotFound()
    {
        var fixedCashLogicMock = new FixedCashAmountTypeLogic();
        var fixedRateLogicMock = new FixedRateRebateTypeLogic();

        IEnumerable<IIncentiveLogic> incentiveLogics = new List<IIncentiveLogic>
        {
            fixedCashLogicMock,
            fixedRateLogicMock
        };

        var factory = new IncentivesLogicFactory(incentiveLogics);

        var result = factory.GetIncentiveLogic(IncentiveType.AmountPerUom);

        Assert.Null(result);
    }

    [Theory]
    [InlineData(IncentiveType.FixedRateRebate)]
    [InlineData(IncentiveType.FixedCashAmount)]
    public void IncentivesLogicFactory_GetLogic_LogicFound(IncentiveType incentiveType)
    {
        var fixedCashLogicMock = new FixedCashAmountTypeLogic();
        var fixedRateLogicMock = new FixedRateRebateTypeLogic();

        IEnumerable<IIncentiveLogic> incentiveLogics = new List<IIncentiveLogic>
        {
            fixedCashLogicMock,
            fixedRateLogicMock
        };

        var factory = new IncentivesLogicFactory(incentiveLogics);

        var result = factory.GetIncentiveLogic(incentiveType);

        Assert.NotNull(result);
    }
}
