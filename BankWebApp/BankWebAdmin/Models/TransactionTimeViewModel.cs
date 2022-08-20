using System.ComponentModel.DataAnnotations;
using BankWebAdmin.Models;

namespace BankWebAdmin.Models
{
    //This method is used to define the start time and end time,and display the transactions that can filter the time.
    public class TransactionTimeViewModel
    {
        public List<TransactionViewModel> Transaction { get; set; }
        [DataType(DataType.Date)]
        public DateTime StartDate { get; set; }
        [DataType(DataType.Date)]
        public DateTime EndDate { get; set; }

    }
}
 