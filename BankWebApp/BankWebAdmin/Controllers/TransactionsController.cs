using System.Globalization;
using Microsoft.AspNetCore.Mvc;
using BankWebAdmin.Models;
using Newtonsoft.Json;

namespace BankWebAdmin.Controllers
{
    public class TransactionsController: Controller
    {
        //Connect WebApi
        private readonly IHttpClientFactory _ClientFactory;
        private HttpClient Client => _ClientFactory.CreateClient("api");

        public TransactionsController(IHttpClientFactory clientFactory) => _ClientFactory = clientFactory;

        //Get Transaction/Index
        public async Task<ActionResult> Index(int?id, DateTime StartDate, DateTime EndDate)
        {
            var response = await Client.GetAsync("api/Transactions");
            if (!response.IsSuccessStatusCode)
                throw new Exception();

            var result = await response.Content.ReadAsStringAsync();
            var transactions = JsonConvert.DeserializeObject<List<TransactionDto>>(result);
            List<TransactionViewModel> Transactions=new List<TransactionViewModel>();

            foreach (var transaction in transactions)
            {
                if (transaction.AccountNumber == id)
                {
                    if (transaction.TransactionTimeUtc>=StartDate && transaction.TransactionTimeUtc<=EndDate)
                    {
                        TransactionViewModel transaction1 = new TransactionViewModel();
                        transaction1.TransactionID = transaction.TransactionID;
                        transaction1.TransactionType = transaction.TransactionType;
                        transaction1.AccountNumber = transaction.AccountNumber;
                        transaction1.DestinaAccountNumber = transaction.DestinaAccountNumber;
                        transaction1.Amount = transaction.Amount;
                        transaction1.Comment = transaction.Comment;
                        transaction1.TransactionTimeUtc = transaction.TransactionTimeUtc.ToLocalTime().ToString("dd/MM/yyyy h:mm tt", new CultureInfo("en-AU"));
                        Transactions.Add(transaction1);

                    }
                }

            }
            TransactionTimeViewModel transactionTime=new TransactionTimeViewModel();
            transactionTime.Transaction = Transactions;
            return View(transactionTime);
        }




    }
}
