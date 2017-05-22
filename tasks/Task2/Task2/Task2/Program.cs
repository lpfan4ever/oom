using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using Newtonsoft.Json;
using System.IO;

namespace Task2
{
    public interface IItem
    {
        /// <summary>
        /// Gets a textual description of this item.
        /// </summary>
        string Description { get; }

        string GetCountry(decimal number);
    }
    public class customer
    {
        static void Main(string[] args)
        {
            var customer = new[]
            {
                new customer("Firma 1", 1234, 0),
                new customer("Firma 2", 8745, 0),
                new customer("Firma 3", 1257, 0),
                new customer("Firma 4", 2585, 0),
                new customer("Firma 5", 1901, 0),
            };
            var amount = new Storage("Firma 6", 50, 1257, 100);

            foreach (var b in customer)
            {
                Console.WriteLine("Auflistung der Kunden: {0}, {1}, {2}, {3}, {4}", b.Name, b.Number,b.GetCountry(b.Number), b.countryid2, b.Description);
            }
            Console.WriteLine("Lager: {0}, {1}, {2}, {3}", amount.Name, amount.Number, amount.GetCountry(amount.Number), amount.Amount);
            Serialization.Run(customer);
        }
    
            private string country;
            private decimal countryid2;

            /// <summary>
            /// Creates a new book object.
            /// </summary>
            /// <param name="name">Name must not be empty.</param>
            /// <param name="number">Number.</param>
            /// <param name="countryid">ID.</param>
            public customer(string name, decimal number, decimal countryid)
            {
                if (string.IsNullOrWhiteSpace(name)) throw new ArgumentException("Name must not be empty.", nameof(name));
                if (number < 0) throw new ArgumentOutOfRangeException("Number must not be negative.");
                if (countryid < 0) throw new ArgumentOutOfRangeException("ID must not be negative.");

                Number = number;
                Name = name;
                UpdateNumber(countryid);
            
            }

            /// <summary>
            /// Gets the number from the customer.
            /// </summary>
            public decimal Number { get; }

            /// <summary>
            /// Gets the name from the customer.
            /// </summary>
            public string Name { get; }

            #region IItem 

            public string Description => Name;
            /// <summary>
            /// Gets the country to the number
            /// </summary>
            public string GetCountry(decimal number)
            {

                // request : http://stats.cybergreen.net/api/v1/count?limit=20&page=1&asn=1234&format=csv&filename=cg-statistics
                // response: FI,1,1234,"2017-04-23",2,82
                var key = number; 

                // create the request URL, ...
                var url = string.Format(@"http://stats.cybergreen.net/api/v1/count?limit=20&page=1&asn={0}&format=csv&filename=cg-statistics", key);
                // download the response as string
                var data = new WebClient().DownloadString(url);
                // split the string at ','
                var parts = data.Split(',','\n');
                // and finally perform the currency conversion
                country = parts[6];
                return country;
            }
            #endregion 

            /// <summary>
            /// Updates the countrid
            /// </summary>
            /// <param decimal="countryid">ID must not be negative.</param>
            public void UpdateNumber(decimal countryid)
            {

                if (countryid < 0) throw new ArgumentException("Number is negativ.", nameof(countryid));
                countryid2 = countryid + 100;
        }
        
    }
    public class Storage : customer
    {
        public int Amount { get; private set; } = 10;
       
        public Storage(string name, decimal number, decimal countryid, int amount) : base(name, number, countryid)
        {
            Amount = amount + 1;
        }
    }
    public class Serialization
    {
        public static void Run(customer[] items)
        {
            var customer = items;
            var text = JsonConvert.SerializeObject(customer);
            var desktop = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            var filename = Path.Combine(desktop, "customer.json");
            File.WriteAllText(filename, text);
            
            Console.WriteLine(JsonConvert.SerializeObject(customer));

            var textfromFile = File.ReadAllText(filename);
            var itemsfromFile = JsonConvert.DeserializeObject<customer[]>(textfromFile);
            foreach(var x in itemsfromFile)
            Console.WriteLine($"Name: {x.Description} Country: {x.GetCountry(x.Number)}");
        }
    }
}
