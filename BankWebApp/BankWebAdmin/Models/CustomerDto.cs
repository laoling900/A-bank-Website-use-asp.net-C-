using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

 namespace BankWebAdmin.Models;

    public class CustomerDto
    {
 
            public int CustomerID { get; set; }

            [Required, StringLength(50)]
            public string Name { get; set; }

            [StringLength(11)]
            public string? TFN { get; set; }

            [StringLength(50)]
            public string? Address { get; set; }

            
            [StringLength(40)]
            public string? Suburb { get; set; }

            [StringLength(3)]
            public string? State { get; set; }

            [StringLength(4)]
            public string? Postcode { get; set; }

            [StringLength(12)]
            public string? Mobile { get; set; }

            public string? LockStae { get; set; }

            public virtual List<AccountDto>? Accounts { get; set; }

    
            [NotMapped]  //EF core ignores this property
            public LoginDto? Login { get; set; }
        }

