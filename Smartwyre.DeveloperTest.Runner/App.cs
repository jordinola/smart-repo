using Smartwyre.DeveloperTest.Application;
using Smartwyre.DeveloperTest.Model;
using System;

namespace Smartwyre.DeveloperTest.Runner
{
    internal class App
    {
        private readonly IRebateService _rebateService;

        public App(IRebateService rebateService)
        {
            _rebateService = rebateService;
        }

        public void Run(string[] args)
        {
            _rebateService.Calculate(new CalculateRebateRequest());
            Console.WriteLine("Entro");
        }
    }
}
