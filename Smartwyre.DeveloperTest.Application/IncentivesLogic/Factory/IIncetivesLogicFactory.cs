using Smartwyre.DeveloperTest.Model;

namespace Smartwyre.DeveloperTest.Application.IncentivesLogic.Factory
{
    public interface IIncetivesLogicFactory
    {
        IIncentiveLogic GetIncentiveLogic(IncentiveType incentiveType);
    }
}
