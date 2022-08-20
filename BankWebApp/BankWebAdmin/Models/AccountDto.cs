using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BankWebAdmin.Models;

public enum AccountType
{
    Checking = 1,
    Saving = 2
}
public class AccountDto
{
    [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
    [Display(Name = "Account Number")]
    public int AccountNumber { get; set; }

    [Display(Name = "Type")]
   // [JsonConverter(typeof(AccountTypeStringToAccountTypeEnumConverter))]
    public AccountType AccountType { get; set; }

    public int CustomerID { get; set; }
    public virtual CustomerDto Customer { get; set; }

    [Column(TypeName = "money")]
    [DataType(DataType.Currency)]

    public decimal Balance { get; set; }
    [InverseProperty("Account")]

    public virtual List<TransactionViewModel>? Transactions { get; set; }
    public virtual List<BillPayDto>? BillPays { get; set; }
}


