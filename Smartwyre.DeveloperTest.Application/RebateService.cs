using Smartwyre.DeveloperTest.Application.IncentivesLogic.Factory;
using Smartwyre.DeveloperTest.Domain;
using Smartwyre.DeveloperTest.Infrastructure;
using Smartwyre.DeveloperTest.Model;

namespace Smartwyre.DeveloperTest.Application
{
    public class RebateService : IRebateService
    {
        private readonly RebateDataStore _rebateDataStore;
        private readonly ProductDataStore _productDataStore;
        private readonly IIncetivesLogicFactory _incidentivesLogicFactory;

        public RebateService(RebateDataStore dataStore, ProductDataStore productDataStore, IIncetivesLogicFactory incidentivesLogicFactory)
        {

            _rebateDataStore = dataStore;
            _productDataStore = productDataStore;
            _incidentivesLogicFactory = incidentivesLogicFactory;
        }
        public CalculateRebateResult Calculate(CalculateRebateRequest request)
        {

            Rebate rebate = _rebateDataStore.GetRebate(request.RebateIdentifier);
            Product product = _productDataStore.GetProduct(request.ProductIdentifier);

            var rebateResult = new CalculateRebateResult();
            if (rebate is null || product is null)
            {
                return rebateResult;
            }

            var incentiveLogic = _incidentivesLogicFactory.GetIncentiveLogic(rebate.Incentive);
            if (incentiveLogic is null)
            {
                return rebateResult;
            }

            (rebateResult.Success, decimal amount) = incentiveLogic.Calculate(product, rebate, request.Volume);

            if (rebateResult.Success)
            {
                var storeRebateDataStore = new RebateDataStore();
                storeRebateDataStore.StoreCalculationResult(rebate, amount);
            }

            return rebateResult;
        }

    }
}