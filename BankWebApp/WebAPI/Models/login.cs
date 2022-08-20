using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;



namespace BankWebAdmin.Models
{
    public class login
    {
        [Column(TypeName = "char")]
        [StringLength(8)]
        public string LoginID { get; set; }

        public int CustomerID { get; set; }
        public virtual Customer? Customer { get; set; }


        [Column(TypeName = "char")]
        [Required, StringLength(64)]
        public string PasswordHash { get; set; }

        public string? LoginState { get; set; }
    }

}
