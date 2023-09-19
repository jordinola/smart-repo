using Smartwyre.DeveloperTest.Application.IncentivesLogic.Factory;
using Smartwyre.DeveloperTest.Domain;
using Smartwyre.DeveloperTest.Infrastructure;
using Smartwyre.DeveloperTest.Model;

namespace Smartwyre.DeveloperTest.Application
{
    public class RebateService : IRebateService
    {
        private readonly IRebateDataStore _rebateDataStore;
        private readonly IProductDataStore _productDataStore;
        private readonly IIncetivesLogicFactory _incidentivesLogicFactory;

        public RebateService(IRebateDataStore rebateDataStore, IProductDataStore productDataStore, IIncetivesLogicFactory incidentivesLogicFactory)
        {

            _rebateDataStore = rebateDataStore;
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
                _rebateDataStore.StoreCalculationResult(rebate, amount);
            }

            return rebateResult;
        }

    }
}