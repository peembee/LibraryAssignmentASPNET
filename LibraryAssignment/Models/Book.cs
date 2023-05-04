using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace LibraryAssignment.Models
{
    public class Book
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int BookID { get; set; }


        [Required]
        [MinLength(1), MaxLength(30)]
        public string Titel { get; set; }


        [Required]
        [MinLength(5), MaxLength(100)]
        public string Description { get; set; }


        [Required]
        [MinLength(1), MaxLength(50)]
        public string Author { get; set; }


        [Required]
        [MinLength(10), MaxLength(30)]
        public string ISBN { get; set; }


        [Required]
        public DateTime RegisteredBookDate { get; set; }

        public virtual ICollection<CustomerBookList> CustomerBookLists { get; set; }
    }
}
