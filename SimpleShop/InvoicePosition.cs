using System;
using System.Collections;
using System.Runtime.CompilerServices;
using System.Linq;
using System.Text.RegularExpressions;
using Microsoft.VisualBasic.CompilerServices;


namespace SimpleShop
{
    public class InvoicePosition{
        public uint ItemIdentifier = 0;
        public string ItemName = "";
        public uint Orders = 0;
        public decimal SingleUnitPrice = 0.0m;
        public Customer Customer;

        public virtual decimal Price(){
            return this.Customer.CalculatePrice(this.SingleUnitPrice * this.Orders);
        }

        public static InvoicePosition CreateFromPairs(KeywordPair[] pairs){

            // Creates an instanceof InvoicePosition
            var invoicePosition = new InvoicePosition();

            // Number of pairs.
            int lengthOfPairs = pairs.Length;

            // Constructing a hash table in order to avoid unnecessary repetead iterations but instead, key-based searching.
            Hashtable hashMap = new Hashtable();
            
            // Fills the table with (hash) keys and respective values.
            for (int i = 0; i < lengthOfPairs; i++)
            {
                hashMap.Add(pairs[i].Key.GetStart(), pairs[i].Value);
            }

            // Gets item number from hash table, if exists.
            if (hashMap.ContainsKey("<ItemNumber>"))
            {

                var itemNumber = hashMap["<ItemNumber>"];
                invoicePosition.ItemIdentifier = Convert.ToUInt32(itemNumber);
            }

            // Gets item name from hash table, if exists.
            if (hashMap.ContainsKey("<ItemName>"))
            {
                
                var itemName = hashMap["<ItemName>"];
                invoicePosition.ItemName = itemName.ToString();
            }

            // Gets order amount from hash table, if exists.
            if (hashMap.ContainsKey("<AmountOrdered>"))
            {
                var amountOrdered = hashMap["<AmountOrdered>"].ToString();
                
                // Baypassing potential wrong serialization.
                invoicePosition.Orders =
                System.Convert.ToUInt32(System.Text.RegularExpressions.Regex.Replace(amountOrdered, "[^0-9 _]", ""));
            }

            // Gets net price from hash table, if exists.
            if (hashMap.ContainsKey("<NetPrice>"))
            {
                var netPrice = hashMap["<NetPrice>"].ToString();

                // Baypassing potential wrong serialization.
                invoicePosition.SingleUnitPrice = Decimal.Parse(Regex.Match(netPrice, @"\d+.+\d").ToString());
            }

            // Gets net price from hash table, if exists.
            var customerName = hashMap["<CustomerName>"].ToString();

            // Gets customer type from hash table, if exists.
            if (hashMap.ContainsKey("<CustomerType>"))
            {
                var customerType = hashMap["<CustomerType>"].ToString();

                // If customer type is "Company".
                if (customerType == "Company")
                {
                    invoicePosition.Customer = Customer.CreateCustomer(customerName, "Company");
                    return invoicePosition;
                }

                // If customer type is "Student".
                else if (customerType == "Student")
                {
                    invoicePosition.Customer = Customer.CreateCustomer(customerName, "Student");
                    return invoicePosition;
                }
                // If customer type input is neither Student or Company.
                
                else
                {
                    invoicePosition.Customer = Customer.CreateCustomer(customerName);
                    return invoicePosition;
                }
            }

            // If customer type is not defined, returns default base class customer instance.
            invoicePosition.Customer = Customer.CreateCustomer(customerName);
            return invoicePosition;
        }
    }
}