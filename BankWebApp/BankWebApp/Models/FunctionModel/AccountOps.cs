using Newtonsoft.Json;
using BankWebApp.Converters;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BankWebApp.Models;


public class AccountOps
{
    //Judge whrther the balance is enough
    public bool enoughBalance(Account account , decimal deducte)
    {
       if (account.AccountType.Equals(AccountType.Saving)){
            if ((account.Balance - deducte) > 0)
            {
                return true;
            }
        }
        if (account.AccountType.Equals(AccountType.Checking))
        {
            if((account.Balance-deducte-300) > 0)
            {
                return true;
            }
        }

            return false;
    }

}
