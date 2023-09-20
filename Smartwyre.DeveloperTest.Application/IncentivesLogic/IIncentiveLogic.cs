using Smartwyre.DeveloperTest.Domain;
using Smartwyre.DeveloperTest.Model;

namespace Smartwyre.DeveloperTest.Application.IncentivesLogic
{
    public interface IIncentiveLogic
    {
        (bool Result, decimal Amount) Calculate(Product product, Rebate rebate, decimal volume = 0m);
    }
}
