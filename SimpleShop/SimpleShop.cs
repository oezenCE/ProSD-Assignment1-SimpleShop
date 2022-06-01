using System;
using System.IO;

namespace SimpleShop
{
    public static class SimpleShop
    {
        public static string[] ReadFileLineByLine(string path){
            var reader = new System.IO.StreamReader(path);
            var line_counter = 0;
            var needed_space = 0;
            // determine number of lines to create the correct sized of array
            for (var line = ""; line != null; line = reader.ReadLine(), ++line_counter){
                if (line.Length > 0 && line[0] != '#'){
                    ++needed_space;
                }
            }

            // Set Position to beginning of file
            reader.BaseStream.Seek(0, SeekOrigin.Begin);
            reader.DiscardBufferedData();
            
            // Read actual data
            var lines = new string[needed_space];
            
            for (var tag_lines=0; line_counter > 1; --line_counter){
                var tmp = reader.ReadLine();
                if (tmp[0] == '#'){continue;}
                lines[tag_lines++] = tmp;
            }
            return lines;
        }

        static void PrintWelcome(){
            var tmp = Console.ForegroundColor;
            Console.ForegroundColor = ConsoleColor.DarkRed;
            Console.Write("#########################################\n" +
                          "#\t\t\t\t\t#\n" +
                          "#\tWelcome to the SimpleShop\t#\n" +
                          "#\t\t\t\t\t#\n#" +
                          "########################################\n\n");
            Console.ForegroundColor = tmp;
        }

        static void PrintInvoice(InvoicePosition ivp){
            Console.WriteLine(String.Join(", ",new string[]{
                ivp.Customer.Name, ivp.ItemName,ivp.Orders.ToString(), ivp.Price().ToString("0.##")
            }));
        }

        public static int Main(string[] args){
            if (args.Length != 1){
                Console.WriteLine("That is not how you use this shop!");
                return 1;
            }
            
            if (!File.Exists(args[0])){
                ReadFileLineByLine(args[0]);
                Console.WriteLine("Orders not found!");
                return 1;
            }
            
            PrintWelcome();
            var orders = ReadFileLineByLine(args[0]);
            Console.WriteLine("Invoices:");

            
            // Setting up the ShopParser
            var shopParser= new ShopParser();
            shopParser.SetKeywords(new Keyword[]{
                new Keyword("ItemNumber"),
                new Keyword("ItemName"),
                new Keyword("CustomerName"),
                new Keyword("CustomerType"),
                new Keyword("AmountOrdered"),
                new Keyword("NetPrice")});
            
            // Parsing orders using ExtractFromTAG.
            int noOfOrders = orders.Length;
            var parsedOrders = new KeywordPair[noOfOrders][];

            for (int i = 0; i < noOfOrders; i++)
            {
                parsedOrders[i] = ShopParser.ExtractFromTAG(shopParser, orders[i]);
            }
            // Create invoices from orders in TAG format.
            var invoices = new InvoicePosition[noOfOrders];
            for (int i = 0; i < noOfOrders; i++)
            {
                invoices[i] = InvoicePosition.CreateFromPairs(parsedOrders[i]);
            }

            // Output a the sum for each customer using PrintInvoice function.
            for (int i = 0; i < noOfOrders; i++)
            {
                PrintInvoice(invoices[i]);
            }


            return 0;
        }
    }
}
