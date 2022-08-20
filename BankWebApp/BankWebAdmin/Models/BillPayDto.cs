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
public class BillPayDto
{

    public int BillPayId { get; set; }

    [Display(Name = "AccountNumber")]
    public int AccountNumber { get; set; }

    public virtual AccountDto? Account { get; set; } 

    [ForeignKey("Payee")]
    public int PayeeID { get; set; }
    public virtual PayeeDto? Payee { get; set; }

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

