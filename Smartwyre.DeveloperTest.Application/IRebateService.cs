using Smartwyre.DeveloperTest.Model;

namespace Smartwyre.DeveloperTest.Application
{
    public interface IRebateService
    {
        CalculateRebateResult Calculate(CalculateRebateRequest request);
    }
}
