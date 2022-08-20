using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BankWebApp.Models
{
    public class Payee
    {
        public Payee(string name, string address, string suburb, string state, string postcode, string phone){
            this.Name = name;
            this.Address = address;
            this.Suburb = suburb;
            this.State = state;
            this.Postcode = postcode;
            this.Phone = phone;
         }

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
        public String Postcode{ get; set; }

        [StringLength(14)]
        public String Phone{ get; set; }
    }
}
