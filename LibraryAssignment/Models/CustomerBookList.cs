using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace LibraryAssignment.Models
{
    public class CustomerBookList
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int CustomerBookListID { get; set; }

        [ForeignKey("Customers")]
        public int FK_CustomerID { get; set; }
        public virtual Customer Customers {get; set;}
        

        [ForeignKey("Books")]
        public int FK_BookID { get; set; }
        public virtual Book Books { get; set; }

        public bool? Retrieved { get; set; }


        [Required]
        public DateTime StartBookedDate { get; set; }


        [Required]
        public DateTime EndBookedDate { get; set; }

        public bool? Returned { get; set; }       
    }
}
