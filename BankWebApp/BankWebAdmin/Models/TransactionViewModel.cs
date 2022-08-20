
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace BankWebAdmin.Models;


public enum TransactionType
{
    Deposit = 1,
    Withdraw = 2,
    Transfer = 3,
    ServiceCharge = 4,
    BillPay = 5
}

public class TransactionViewModel {
    public int TransactionID { get; set; }
    public TransactionType TransactionType { get; set; }

    public int AccountNumber { get; set; }
 
    public int? DestinaAccountNumber { get; set; }


    public decimal Amount { get; set; }


    public string? Comment { get; set; }

    public string TransactionTimeUtc { get; set; }

}



