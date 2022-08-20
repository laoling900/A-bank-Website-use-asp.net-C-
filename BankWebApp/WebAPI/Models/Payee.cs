using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BankWebAdmin.Models
{
    public class Payee
    {

        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]

        public int PayeeID { get; set; }

        [StringLength(50)]
        public String Name { get; set; }

        [StringLength(50)]
        public string Address { get; set; }

        [StringLength(40)]
        public String Suburb { get; set; }

        [StringLength(3)]
        public String State { get; set; }

        [StringLength(4)]
        public String Postcode { get; set; }

        [StringLength(14)]
        public String Phone { get; set; }
    }
}

