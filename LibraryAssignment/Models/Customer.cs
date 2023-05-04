using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LibraryAssignment.Models
{
    public class Customer
    {
        //--------

        public string FullName => $"ID #{CustomerID} | {FirstName} {LastName}";

        // table - Database
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int CustomerID { get; set; }

        [Required]
        [MinLength(1), MaxLength(30)]
        public string FirstName { get; set; }


        [Required]
        [MinLength(1), MaxLength(30)]
        public string LastName { get; set; }


        [Required]
        [MinLength(10), MaxLength(30)]
        public string Phone { get; set; }


        [Required]
        public DateTime RegisteredDate { get; set; }


        public virtual ICollection<CustomerBookList> CustomerBookLists { get; set; }
    }
}
