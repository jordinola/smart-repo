using Smartwyre.DeveloperTest.Domain;
using Smartwyre.DeveloperTest.Model;

namespace Smartwyre.DeveloperTest.Application.IncentivesLogic
{
    public interface IIncentiveLogic
    {
        IncentiveType GetIncentiveType();
        (bool Result, decimal Amount) Calculate(Product product, Rebate rebate, decimal volume = 0m);
    }
}
