using Newtonsoft.Json;
using BankWebApp.Converters;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BankWebApp.Models;

public enum AccountType
{
    Checking=1,
    Saving =2
}
public class Account
{
   [Key,DatabaseGenerated(DatabaseGeneratedOption.None)]
   [Display (Name ="Account Number")]
   public int AccountNumber { get; set; }

    [Display (Name ="Type")]
    [JsonConverter(typeof(AccountTypeStringToAccountTypeEnumConverter))]
    public  AccountType AccountType { get; set; }

    public int CustomerID { get; set; }
    public virtual Customer Customer { get; set; }

    [Column (TypeName ="money")]
    [DataType (DataType.Currency )]

    public decimal Balance { get; set; }
    [InverseProperty("Account")]

    public virtual List<Transaction>?Transactions { get; set; }
    public virtual List<BillPay>? BillPays { get; set; }
}
