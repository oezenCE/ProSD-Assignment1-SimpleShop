using System;
using System.Collections.Generic;
using System.Text;

namespace SimpleShop
{   
    // Child class for student customer type. Inherits from SimpleShop::Customer.
    public class Student: Customer
    {   
        // Implementation of discount (20% before tax) for student customers. Overrides SimpleShop::Customer::CalculatePrice().
        public override decimal CalculatePrice(decimal basePrice)
        {
            return (1 + ValueAddedTax) * (basePrice*0.8m);
        }
    }
}
