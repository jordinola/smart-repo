using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Smartwyre.DeveloperTest.Application;
using Smartwyre.DeveloperTest.Application.IncentivesLogic;
using Smartwyre.DeveloperTest.Application.IncentivesLogic.Factory;
using Smartwyre.DeveloperTest.Infrastructure;
using Smartwyre.DeveloperTest.Model;
using System;

HostApplicationBuilder builder = Host.CreateApplicationBuilder(args);
builder.Services.AddTransient<IProductDataStore, ProductDataStore>();
builder.Services.AddTransient<IRebateDataStore, RebateDataStore>();
builder.Services.AddTransient<IIncentiveLogic, AmountPerUomTypeLogic>();
builder.Services.AddTransient<IIncentiveLogic, FixedCashAmountTypeLogic>();
builder.Services.AddTransient<IIncentiveLogic, FixedRateRebateTypeLogic>();
builder.Services.AddTransient<IIncetivesLogicFactory, IncentivesLogicFactory>();
builder.Services.AddTransient<IRebateService, RebateService>();
using IHost host = builder.Build();

RunProcess(host.Services);

await host.RunAsync();

// Service are not being added to the container
static void RunProcess(IServiceProvider hostProvider)
{
    using IServiceScope serviceScope = hostProvider.CreateScope();
    IServiceProvider serviceProvider = serviceScope.ServiceProvider;

    IRebateService rebateService = serviceProvider.GetRequiredService<IRebateService>();
    rebateService.Calculate(new CalculateRebateRequest());
}