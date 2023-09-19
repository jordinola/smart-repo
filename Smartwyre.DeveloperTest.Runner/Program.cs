using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Smartwyre.DeveloperTest.Application;
using Smartwyre.DeveloperTest.Application.IncentivesLogic;
using Smartwyre.DeveloperTest.Infrastructure;
using Smartwyre.DeveloperTest.Runner;
using System;

using IHost host = CreateHostBuilder(args).Build();
using var scope = host.Services.CreateScope();

var services = scope.ServiceProvider;

try
{
    host.Services.GetRequiredService<App>().Run(args);
}
catch (Exception ex)
{
    Console.WriteLine(ex.Message);
}

IHostBuilder CreateHostBuilder(string[] args)
{
    return Host.CreateDefaultBuilder()
        .ConfigureServices((_, services) =>
        {
            services.AddTransient<IRebateService, RebateService>();
            services.AddTransient<IProductDataStore, ProductDataStore>();
            services.AddTransient<IRebateDataStore, RebateDataStore>();

            services.AddTransient<IIncentiveLogic, AmountPerUomTypeLogic>();
            services.AddTransient<IIncentiveLogic, FixedCashAmountTypeLogic>();
            services.AddTransient<IIncentiveLogic, FixedRateRebateTypeLogic>();

            services.AddSingleton<App>();
        });
}

//private static IHost CreateHost() => Host
//    .CreateDefaultBuilder()
//    .ConfigureServices((context, services) => 
//    { 
//        services.AddTransient<IRebateService, RebateService>();
//        services.AddTransient<IProductDataStore, ProductDataStore>();
//        services.AddTransient<IRebateDataStore, RebateDataStore>();

//        services.AddTransient<IIncentiveLogic, AmountPerUomTypeLogic>();
//        services.AddTransient<IIncentiveLogic, FixedCashAmountTypeLogic>();
//        services.AddTransient<IIncentiveLogic, FixedRateRebateTypeLogic>();
//    })
//    .Build();

//static void Main(string[] args)
//{
//    IHost host = CreateHost();
//    IRebateService rebateService = ActivatorUtilities.CreateInstance<RebateService>(host.Services);
//    rebateService.Calculate(new CalculateRebateRequest());
//}
