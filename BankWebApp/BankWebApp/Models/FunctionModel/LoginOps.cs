
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BankWebApp.Models;

    public class LoginOps
{

    public string LoginID { get; set; }

    public int CustomerID { get; set; }

    public string PasswordHash { get; set; }
}










    

