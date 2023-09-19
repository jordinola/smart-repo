using Moq;
using Smartwyre.DeveloperTest.Application;
using Smartwyre.DeveloperTest.Application.IncentivesLogic;
using Smartwyre.DeveloperTest.Application.IncentivesLogic.Factory;
using Smartwyre.DeveloperTest.Domain;
using Smartwyre.DeveloperTest.Infrastructure;
using Smartwyre.DeveloperTest.Model;
using Xunit;

namespace Smartwyre.DeveloperTest.Tests;

public class RebateServiceTests
{
    [Fact]
    public void RebateService_Calculate_RebateNotFound()
    {
        var productDataStoreMock = new Mock<IProductDataStore>();
        var rebateDataStoreMock = new Mock<IRebateDataStore>();
        var incentivesLogicFactoryMock = new Mock<IIncetivesLogicFactory>();

        rebateDataStoreMock
            .Setup(x => x.GetRebate(It.IsAny<string>()))
            .Returns(default(Rebate));

        productDataStoreMock
            .Setup(x => x.GetProduct(It.IsAny<string>()))
            .Returns(new Product());

        var rebateService = new RebateService(
            rebateDataStoreMock.Object, 
            productDataStoreMock.Object,
            incentivesLogicFactoryMock.Object);

        var result = rebateService.Calculate(new CalculateRebateRequest());

        Assert.NotNull(result);
        Assert.False(result.Success);
        rebateDataStoreMock.Verify(x => x.GetRebate(It.IsAny<string>()), Times.Once());
        productDataStoreMock.Verify(x => x.GetProduct(It.IsAny<string>()), Times.Once());
        rebateDataStoreMock.Verify(x => x.StoreCalculationResult(It.IsAny<Rebate>(), It.IsAny<decimal>()), Times.Never());
        incentivesLogicFactoryMock.Verify(x => x.GetIncentiveLogic(It.IsAny<IncentiveType>()), Times.Never());
    }

    [Fact]
    public void RebateService_Calculate_ProductNotFound()
    {
        var productDataStoreMock = new Mock<IProductDataStore>();
        var rebateDataStoreMock = new Mock<IRebateDataStore>();
        var incentivesLogicFactoryMock = new Mock<IIncetivesLogicFactory>();

        rebateDataStoreMock
            .Setup(x => x.GetRebate(It.IsAny<string>()))
            .Returns(new Rebate());

        productDataStoreMock
            .Setup(x => x.GetProduct(It.IsAny<string>()))
            .Returns(default(Product));

        var rebateService = new RebateService(
            rebateDataStoreMock.Object,
            productDataStoreMock.Object,
            incentivesLogicFactoryMock.Object);

        var result = rebateService.Calculate(new CalculateRebateRequest());

        Assert.NotNull(result);
        Assert.False(result.Success);
        rebateDataStoreMock.Verify(x => x.GetRebate(It.IsAny<string>()), Times.Once());
        productDataStoreMock.Verify(x => x.GetProduct(It.IsAny<string>()), Times.Once());
        rebateDataStoreMock.Verify(x => x.StoreCalculationResult(It.IsAny<Rebate>(), It.IsAny<decimal>()), Times.Never());
        incentivesLogicFactoryMock.Verify(x => x.GetIncentiveLogic(It.IsAny<IncentiveType>()), Times.Never());
    }

    [Fact]
    public void RebateService_Calculate_IncentiveLogicNotFound()
    {
        var productDataStoreMock = new Mock<IProductDataStore>();
        var rebateDataStoreMock = new Mock<IRebateDataStore>();
        var incentivesLogicFactoryMock = new Mock<IIncetivesLogicFactory>();

        rebateDataStoreMock
            .Setup(x => x.GetRebate(It.IsAny<string>()))
            .Returns(new Rebate());

        productDataStoreMock
            .Setup(x => x.GetProduct(It.IsAny<string>()))
            .Returns(new Product());

        incentivesLogicFactoryMock
            .Setup(x => x.GetIncentiveLogic(It.IsAny<IncentiveType>()))
            .Returns(default(IIncentiveLogic));

        var rebateService = new RebateService(
            rebateDataStoreMock.Object,
            productDataStoreMock.Object,
            incentivesLogicFactoryMock.Object);

        var result = rebateService.Calculate(new CalculateRebateRequest());

        Assert.NotNull(result);
        Assert.False(result.Success);
        rebateDataStoreMock.Verify(x => x.GetRebate(It.IsAny<string>()), Times.Once());
        productDataStoreMock.Verify(x => x.GetProduct(It.IsAny<string>()), Times.Once());
        rebateDataStoreMock.Verify(x => x.StoreCalculationResult(It.IsAny<Rebate>(), It.IsAny<decimal>()), Times.Never());
        incentivesLogicFactoryMock.Verify(x => x.GetIncentiveLogic(It.IsAny<IncentiveType>()), Times.Once());
    }

    [Fact]
    public void RebateService_Calculate_CalculateResultFail()
    {
        var productDataStoreMock = new Mock<IProductDataStore>();
        var rebateDataStoreMock = new Mock<IRebateDataStore>();
        var incentiveLogicMock = new Mock<IIncentiveLogic>();
        var incentivesLogicFactoryMock = new Mock<IIncetivesLogicFactory>();

        rebateDataStoreMock
            .Setup(x => x.GetRebate(It.IsAny<string>()))
            .Returns(new Rebate());

        productDataStoreMock
            .Setup(x => x.GetProduct(It.IsAny<string>()))
            .Returns(new Product());

        incentiveLogicMock
            .Setup(x => x.Calculate(It.IsAny<Product>(), It.IsAny<Rebate>(), It.IsAny<decimal>()))
            .Returns((false, 0m));

        incentivesLogicFactoryMock
            .Setup(x => x.GetIncentiveLogic(It.IsAny<IncentiveType>()))
            .Returns(incentiveLogicMock.Object);

        var rebateService = new RebateService(
            rebateDataStoreMock.Object,
            productDataStoreMock.Object,
            incentivesLogicFactoryMock.Object);

        var result = rebateService.Calculate(new CalculateRebateRequest());

        Assert.NotNull(result);
        Assert.False(result.Success);
        rebateDataStoreMock.Verify(x => x.GetRebate(It.IsAny<string>()), Times.Once());
        productDataStoreMock.Verify(x => x.GetProduct(It.IsAny<string>()), Times.Once());
        incentiveLogicMock.Verify(x => x.Calculate(It.IsAny<Product>(), It.IsAny<Rebate>(), It.IsAny<decimal>()), Times.Once());
        incentivesLogicFactoryMock.Verify(x => x.GetIncentiveLogic(It.IsAny<IncentiveType>()), Times.Once());
        rebateDataStoreMock.Verify(x => x.StoreCalculationResult(It.IsAny<Rebate>(), It.IsAny<decimal>()), Times.Never());
    }

    [Fact]
    public void RebateService_Calculate_CalculateResultSuccess()
    {
        var productDataStoreMock = new Mock<IProductDataStore>();
        var rebateDataStoreMock = new Mock<IRebateDataStore>();
        var incentiveLogicMock = new Mock<IIncentiveLogic>();
        var incentivesLogicFactoryMock = new Mock<IIncetivesLogicFactory>();

        rebateDataStoreMock
            .Setup(x => x.GetRebate(It.IsAny<string>()))
            .Returns(new Rebate());

        rebateDataStoreMock
            .Setup(x => x.StoreCalculationResult(It.IsAny<Rebate>(), It.IsAny<decimal>()));

        productDataStoreMock
            .Setup(x => x.GetProduct(It.IsAny<string>()))
            .Returns(new Product());

        incentiveLogicMock
            .Setup(x => x.Calculate(It.IsAny<Product>(), It.IsAny<Rebate>(), It.IsAny<decimal>()))
            .Returns((true, 16m));

        incentivesLogicFactoryMock
            .Setup(x => x.GetIncentiveLogic(It.IsAny<IncentiveType>()))
            .Returns(incentiveLogicMock.Object);

        var rebateService = new RebateService(
            rebateDataStoreMock.Object,
            productDataStoreMock.Object,
            incentivesLogicFactoryMock.Object);

        var result = rebateService.Calculate(new CalculateRebateRequest());

        Assert.NotNull(result);
        Assert.True(result.Success);
        rebateDataStoreMock.Verify(x => x.GetRebate(It.IsAny<string>()), Times.Once());
        productDataStoreMock.Verify(x => x.GetProduct(It.IsAny<string>()), Times.Once());
        incentiveLogicMock.Verify(x => x.Calculate(It.IsAny<Product>(), It.IsAny<Rebate>(), It.IsAny<decimal>()), Times.Once());
        incentivesLogicFactoryMock.Verify(x => x.GetIncentiveLogic(It.IsAny<IncentiveType>()), Times.Once());
        rebateDataStoreMock.Verify(x => x.StoreCalculationResult(It.IsAny<Rebate>(), It.IsAny<decimal>()), Times.Once());
    }
}
