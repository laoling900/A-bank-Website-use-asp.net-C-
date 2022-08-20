using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BankWebAdmin.Models;

    public class TransactionDto
    {

        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int TransactionID { get; set; }
        public TransactionType TransactionType { get; set; }

        [ForeignKey("Account")]
        public int AccountNumber { get; set; }
        public virtual AccountDto Account { get; set; }
        public int? DestinaAccountNumber { get; set; }

        [Column(TypeName = "money")]
        public decimal Amount { get; set; }

        [StringLength(30)]
        public string? Comment { get; set; }


        [DisplayFormat(DataFormatString = "{0:yyyy/MM/dd hh:mm:ss}", ApplyFormatInEditMode =true)]
        public DateTime TransactionTimeUtc { get; set; }



        }


    

