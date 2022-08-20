using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BankWebAdmin.Models;


public enum BillPayType
{
    // waiting for the date to pay
    Pending = 1,
    // Fail to pay
    Decline = 2,
    //Successful pay
    Finish = 3,
    //Customer delete it itself
    Delete = 4

}


public class BillPay
{

    public BillPay(int AccountNumber, int PayeeID, decimal Amount, DateTime ScheduleTimeUtc, string period, BillPayType BillPayType)
    {
        this.AccountNumber = AccountNumber;
        this.PayeeID = PayeeID;
        this.Amount = Amount;
        this.ScheduleTimeUtc = ScheduleTimeUtc;
        this.BillPayPeriod = period;
        this.BillPayType = BillPayType;
        this.LockState = null;
    }

    public BillPay()
    {

    }


    [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int BillPayId { get; set; }

    [Display(Name = "AccountNumber")]
    [ForeignKey("Account")]
    public int AccountNumber { get; set; }

    public virtual Account? Account { get; set; }

    [ForeignKey("Payee")]
    public int PayeeID { get; set; }
    public virtual Payee? Payee { get; set; }

    [Column(TypeName = "money")]
    [DataType(DataType.Currency)]
    public decimal Amount { get; set; }

    [Display(Name = "ScheduleTimeUtc"), DataType(DataType.Date)]
    public DateTime ScheduleTimeUtc { get; set; }

    [StringLength(50)]
    public string BillPayPeriod { get; set; }

    public BillPayType BillPayType { get; set; }

    public string? LockState { get; set; }

}

