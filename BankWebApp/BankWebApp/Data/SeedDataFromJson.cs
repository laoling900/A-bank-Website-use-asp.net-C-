using BankWebApp.Models;
using Newtonsoft.Json;

namespace BankWebApp.Data;
    public static class SeedDataFromJson
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            var context = serviceProvider.GetRequiredService<BankAppContext>();

            //Look for database any Customers
            if (context.Customers.Any())
            {
                Console.WriteLine("Database already has data ,loading the system now......");
                return;
            }

            const string Url = "https://coreteaching01.csit.rmit.edu.au/~e103884/wdt/services/customers/";

            //contact webservice
            using var client = new HttpClient();
            var json = client.GetStringAsync(Url).Result;

            var customers = JsonConvert.DeserializeObject<List<Customer>>(json, new JsonSerializerSettings
            {

                //See here for DataTime Format string documentation:
                //https://docs.microsoft.com/en-au/dotnet/standard/base-types/custom-date-and-time-format-strings

                DateFormatString = "dd/MM/yyyy"

            });

      
            foreach (var c in customers)
            {
                context.Add(c);
                foreach (var account in c.Accounts)
                {

                    account.CustomerID = c.CustomerID;
                    context.Add(account);
                    foreach (var transaction in account.Transactions)
                    {
                        transaction.AccountNumber = account.AccountNumber;
                        account.Balance += transaction.Amount;
                        context.Add(transaction);
                        transaction.TransactionType = TransactionType.Deposit;
                    }
                    
                }

                c.Login.CustomerID = c.CustomerID;
                context.Logins.Add(c.Login);    

            }
        //Make 3 fake  payee  instead of connected with outside the system
        Payee Telstra = new Payee("Telstra", "Mel", "vsc", "Vic", "3000", "1234");
        Payee AGL = new Payee("AGL", "Mel", "vsc", "Vic", "3000", "5678");
        Payee Optus = new Payee("Optus", "Mel", "vsc", "Vic", "3000", "9012");
        context.Payees.Add(Telstra);
        context.Payees.Add(AGL);
        context.Payees.Add(Optus);

        context.SaveChanges(); 
        
    }


}
