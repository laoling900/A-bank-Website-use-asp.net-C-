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
public class Transaction
{

    public Transaction(TransactionType TransactionType, int AccountNumber, int? DestinaAccountNumber, decimal Amount, string? Comment)
    {

        this.TransactionType = TransactionType;
        this.AccountNumber = AccountNumber;
        this.DestinaAccountNumber = DestinaAccountNumber;
        this.Amount = Amount;
        this.Comment = Comment;
        this.TransactionTimeUtc = DateTime.UtcNow;
    }

    [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int TransactionID { get; set; }
    public TransactionType TransactionType { get; set; }

    [ForeignKey("Account")]
    public int AccountNumber { get; set; }
    public virtual Account Account { get; set; }
    public int? DestinaAccountNumber { get; set; }

    [Column(TypeName = "money")]
    public decimal Amount { get; set; }

    [StringLength(30)]
    public string? Comment { get; set; }

    public DateTime TransactionTimeUtc { get; set; }



}

