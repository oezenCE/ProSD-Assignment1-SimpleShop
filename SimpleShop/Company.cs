using System;
using System.Collections.Generic;
using System.Text;

namespace SimpleShop
{

    // Child class for company customer type. Inherits from SimpleShop::Customer.
    public class Company : Customer
    {

        // Implementation of tax-free price for companies. Overrides SimpleShop::Customer::CalculatePrice().
        public override decimal CalculatePrice(decimal basePrice)
        {
            return basePrice;
        }
    }
}