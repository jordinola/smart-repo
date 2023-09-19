using Smartwyre.DeveloperTest.Application.IncentivesLogic;
using Smartwyre.DeveloperTest.Domain;
using Smartwyre.DeveloperTest.Infrastructure;
using Smartwyre.DeveloperTest.Model;

namespace Smartwyre.DeveloperTest.Application
{
    public class RebateService : IRebateService
    {
        private readonly RebateDataStore _rebateDataStore;
        private readonly ProductDataStore _productDataStore;
        private readonly IEnumerable<IIncentiveLogic> _incentivesLogic;

        public RebateService(RebateDataStore dataStore, ProductDataStore productDataStore, IEnumerable<IIncentiveLogic> rebateIncentiveLogic)
        {

            _rebateDataStore = dataStore;
            _productDataStore = productDataStore;
            _incentivesLogic = rebateIncentiveLogic;

        }
        public CalculateRebateResult Calculate(CalculateRebateRequest request)
        {

            Rebate rebate = _rebateDataStore.GetRebate(request.RebateIdentifier);
            Product product = _productDataStore.GetProduct(request.ProductIdentifier);

            var rebateResult = new CalculateRebateResult();

            (bool result, decimal amount) incentiveLogicResult = (false, 0m);

            if (rebate is null || product is null)
            {
                return rebateResult;
            }
            else
            {
                var incentiveLogic = _incentivesLogic.SingleOrDefault(incentive => incentive.GetIncentiveType() == rebate.Incentive);
                if (incentiveLogic is null)
                {
                    return rebateResult;
                }

                incentiveLogicResult = incentiveLogic.Calculate(product, rebate, request.Volume);
                rebateResult.Success = incentiveLogicResult.result;
            }

            if (rebateResult.Success)
            {
                var storeRebateDataStore = new RebateDataStore();
                storeRebateDataStore.StoreCalculationResult(rebate, incentiveLogicResult.amount);
            }

            return rebateResult;
        }

    }
}