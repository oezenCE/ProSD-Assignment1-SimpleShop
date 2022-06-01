using NUnit.Framework;
using SimpleShop;
// Remember [UnitOfWork_StateUnderTest_ExpectedBehaviour]

namespace SimpleShop.Test
{
    public class Tests
    {
       

        [SetUp]
        public void Setup()
        {
           var invoicePositionTest = new InvoicePosition
            {
                Customer = new Customer(),
                ItemIdentifier = 2,
                ItemName = "Coke",
                Orders = 2,
                SingleUnitPrice = 1.50m
            };

           var keywordsTest = new Keyword[]{
                new Keyword("ItemNumber"),
                new Keyword("ItemName"),
                new Keyword("CustomerName"),
                new Keyword("AmountOrdered"),
                new Keyword("NetPrice"),
                new Keyword("CustomerType")
            };

            var keywordPairsTest = new KeywordPair[] {
                new KeywordPair(keywordsTest[0], invoicePositionTest.ItemIdentifier.ToString()),
                new KeywordPair(keywordsTest[1], invoicePositionTest.ItemName),
                new KeywordPair(keywordsTest[2], invoicePositionTest.Customer.Name),
                new KeywordPair(keywordsTest[3], invoicePositionTest.Orders.ToString()),
                new KeywordPair(keywordsTest[4], invoicePositionTest.SingleUnitPrice.ToString()),
                new KeywordPair(keywordsTest[5],invoicePositionTest.Customer.GetType().ToString())
            };

        }

        /// <summary>
        /// Check if the Keyword opening is modified with added <Keyword> 
        /// Rating 0
        /// </summary>
        [Test]
        [Category("Keyword")]
        public void Parsing_KeywordStartTag_AddedBraces(){
            Keyword test = new Keyword("test1", KeywordTypes.String);

            Assert.AreEqual("<test1>", test.GetStart());
        }
        
        /// <summary>
        /// Check if the Keyword closing is modified with added </Keyword>
        /// Rating 0
        /// </summary>
        [Test]
        [Category("Keyword")]
        public void Parsing_KeywordEndTag_AddedSlashAndBraces(){
            Keyword test = new Keyword("test1", KeywordTypes.String);

            Assert.AreEqual("</test1>", test.GetEnd());
        }
        
        /// <summary>
        /// Set the Keywords an check if they are valid.
        /// Rating 1
        /// </summary>
        [Test]
        [Category("ShopParser")]
        public void Parsing_SetKeywords_OrderOfKeywordsIsCorrect(){
            Keyword test1 = new Keyword("test1", KeywordTypes.String);
            Keyword test2 = new Keyword("test1", KeywordTypes.String);


            Keyword[] test = new Keyword[] {test1, test2};
            ShopParser simpleTest = new ShopParser();
            simpleTest.SetKeywords(test);

            Assert.AreEqual(simpleTest.GetKeywords(), test);
        }
        
        /// <summary>
        /// Set the Keyword types and check if they are valid.
        /// Rating 0
        /// </summary>
        [Test]
        [Category("ShopParser")]
        public void ShopParser_SetKeyword_Typ(){
            Keyword test1 = new Keyword("test1", KeywordTypes.String);
            Keyword[] test = new Keyword[] {test1};


            var shopParserTest = new ShopParser();
            shopParserTest.SetKeywords(test);
            var returnedKeywordsTest = shopParserTest.GetKeywords();

            Assert.AreEqual(KeywordTypes.String, returnedKeywordsTest[0].WhichType());
         }
        
        
        /// <summary>
        /// Check if the parser works correctly. Make examples and see if you can find problems with the code.
        /// Literals represent KeywordPairs with different Keywords
        /// A B C D
        /// Rating 2
        /// </summary>
        [Test]
        [Category("ShopParser")]
        public void Parsing_ValidFindings_True(){
            var parserTest = new ShopParser();
            var serializedInputTest =
                "<A>1</A><B>Burger</B><C>James T. Kirk</C><D>2</D>";
            var returnedKeywords = ShopParser.ExtractFromTAG(parserTest, serializedInputTest);

            Assert.AreEqual(true, ShopParser.ValidateFindings(returnedKeywords));


        }

        /// <summary>
        /// Check if the parser works correctly. This time you should check if repetition invalidates the findings.
        /// A A B B C C
        /// Rating 2
        /// </summary>
        [Test]
        [Category("ShopParser")]
        public void Parsing_InvalidatedFindingsWithRepeatedEntry_False(){

            var shopParserTest = new ShopParser();
            var serializedInputTest =
                "<A>1</A><A>2</A><B>Burger</B><B>Coke</B><C>James T. Kirk</C><C>Emin Can Oezen</C>";
            var returnedKeywordsTest = ShopParser.ExtractFromTAG(shopParserTest, serializedInputTest);

            Assert.AreEqual(true, ShopParser.ValidateFindings(returnedKeywordsTest));
        }
        
        /// <summary>
        ///  Check if the parser works correctly. This time with circular keywords.
        /// A B C A
        /// Rating 2
        /// </summary>
        [Test]
        [Category("ShopParser")]
        public void Parsing_InvalidatedFindingsCircular_False(){
            var shopParserTest = new ShopParser();
            var serializedInputTest =
                "<A>1</A><B>Burger</B><C>James T. Kirk</C><A>5</A>";
            var returnedKeywordsTest = ShopParser.ExtractFromTAG(shopParserTest, serializedInputTest);

            Assert.AreEqual(true, ShopParser.ValidateFindings(returnedKeywordsTest));
        }
        
        /// <summary>
        /// See Tagfile (SampleOrder.tag) for more information. Are the correct number of keywords recognized ? 
        /// Rating 1
        /// </summary>
        [Test]
        [Category("ShopParser")]
        public void Parsing_KeywordsSetTagString_CorrectNumberOfEntries(){
            var Test = new ShopParser();
            var keywordsTest = new Keyword[]{
                new Keyword("ItemNumber"),
                new Keyword("ItemName"),
                new Keyword("CustomerName"),
                new Keyword("AmountOrdered"),
                new Keyword("NetPrice"),
                new Keyword("CustomerType")
            };
            Test.SetKeywords(keywordsTest);
            var serializedInputTest =
                "<ItemNumber>2</ItemNumber><ItemName>IceCream</ItemName><CustomerName>S'chn T'gai Spock</CustomerName><AmountOrdered>7</AmountOrdered><NetPrice>4.50</NetPrice><CustomerType>Student</CustomerType>";
                
            var output = ShopParser.ExtractFromTAG(Test, serializedInputTest);

            Assert.AreEqual(6, output.Length);
        }
        
        /// <summary>
        /// Again consult the Tagfile for more information. The parsing should follow the order of the keywords you provided.
        /// Make sure to make it adaptable to different configurations.
        /// Rating 2
        /// </summary>
        [Test]
        [Category("ShopParser")]
        public void Parsing_KeywordsSetTagString_ListOfProvidedTagsInOrder(){

           var invoicePositionTest = new InvoicePosition
            {
                Customer = new Customer(),
                ItemIdentifier = 2,
                ItemName = "Coke",
                Orders = 2,
                SingleUnitPrice = 1.50m
            };

            var keywordsTest = new Keyword[]{
                new Keyword("ItemNumber"),
                new Keyword("ItemName"),
                new Keyword("CustomerName"),
                new Keyword("AmountOrdered"),
                new Keyword("NetPrice"),
                new Keyword("CustomerType")
            };

            var keywordPairsTest = new KeywordPair[] {
                new KeywordPair(keywordsTest[0], invoicePositionTest.ItemIdentifier.ToString()),
                new KeywordPair(keywordsTest[1], invoicePositionTest.ItemName),
                new KeywordPair(keywordsTest[2], invoicePositionTest.Customer.Name),
                new KeywordPair(keywordsTest[3], invoicePositionTest.Orders.ToString()),
                new KeywordPair(keywordsTest[4], invoicePositionTest.SingleUnitPrice.ToString()),
                new KeywordPair(keywordsTest[5],invoicePositionTest.Customer.GetType().ToString())
            };
            var shopParserTest = new ShopParser();
            shopParserTest.SetKeywords(keywordsTest);
            Assert.AreEqual(keywordsTest, shopParserTest.GetKeywords());
        }

        /// <summary>
        /// Test if the VAT is calculated correctly for the Customer.CalculatePrice
        /// Rating 1
        /// </summary>
        [Test]
        [Category("Customer")]
        public void Invoice_CalculateNormalCustomer_AddValueAddedTax(){
            Customer test = new Customer();
            var testCase = test.CalculatePrice (100.00m);

            Assert.IsTrue(119m == testCase);
        }
        
        /// <summary>
        /// Test if the function CreateCustomer returns a customer
        /// Rating 0
        /// </summary>
        [Test]
        [Category("Customer")]
        public void Invoice_CreateCustomer_ReturnsCustomer(){


            Assert.IsInstanceOf<Customer>(Customer.CreateCustomer("test"));
        }
        
        /// <summary>
        /// Test if the InvoicePosition.Price calculates correctly:
        /// Provided Orders, NetPrice is set.
        /// Rating 1
        /// </summary>
        [Test]
        [Category("Invoice")]
        public void Invoice_OrdersAndNetPriceValid_CalculateCorrectPrice(){
            InvoicePosition test = new InvoicePosition();
            test.Customer = new Customer();
            test.SingleUnitPrice = 10;
            test.Orders = 10;

            Assert.IsTrue(test.Price() == 119m);

        }
    }
}