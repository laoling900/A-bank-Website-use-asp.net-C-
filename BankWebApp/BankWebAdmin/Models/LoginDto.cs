using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace BankWebAdmin.Models
{
    public class LoginDto
    {
        
        [StringLength(8)]
        public string LoginID { get; set; }

        public int CustomerID { get; set; }
        public virtual CustomerDto? Customer { get; set; }


        [Required, StringLength(64)]
        public string PasswordHash { get; set; }

        public string? LoginState { get; set; }
    }
}
