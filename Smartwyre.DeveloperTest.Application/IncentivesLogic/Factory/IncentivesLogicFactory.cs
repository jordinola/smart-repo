using Smartwyre.DeveloperTest.Model;

namespace Smartwyre.DeveloperTest.Application.IncentivesLogic.Factory
{
    public class IncentivesLogicFactory : IIncetivesLogicFactory
    {
        private readonly IEnumerable<IIncentiveLogic> _incentivesLogic;

        public IncentivesLogicFactory(IEnumerable<IIncentiveLogic> rebateIncentiveLogic)
        {
            _incentivesLogic = rebateIncentiveLogic;
        }

        public IIncentiveLogic GetIncentiveLogic(IncentiveType incentiveType)
        {
            IIncentiveLogic incentiveLogic = null;
            switch (incentiveType)
            {
                case IncentiveType.FixedRateRebate:
                    incentiveLogic = GetService(typeof(FixedRateRebateTypeLogic));
                    break;

                case IncentiveType.FixedCashAmount:
                    incentiveLogic = GetService(typeof(FixedCashAmountTypeLogic));
                    break;

                case IncentiveType.AmountPerUom:
                    incentiveLogic = GetService(typeof(AmountPerUomTypeLogic));
                    break;

                default: throw new InvalidOperationException();
            };

            // In case we want to prevent returning false results due to the lack of implementation of an
            // incentive logic, we should un comment below lines

            //if (incentiveLogic == null)
            //{
            //    throw new NotImplementedException($"Incentive type {incentiveType} not implemented in system");
            //}

            return incentiveLogic;
        }

        private IIncentiveLogic GetService(Type incentiveType)
        {
            return _incentivesLogic.FirstOrDefault(type => type.GetType() == incentiveType)!;
        }
    }
}
