﻿using Smartwyre.DeveloperTest.Domain;

namespace Smartwyre.DeveloperTest.Infrastructure
{
    public class RebateDataStore : IRebateDataStore
    {
        public Rebate GetRebate(string rebateIdentifier)
        {
            // Access database to retrieve account, code removed for brevity 
            return new Rebate();
        }

        public void StoreCalculationResult(Rebate account, decimal rebateAmount)
        {
            // Update account in database, code removed for brevity
        }
    }
}