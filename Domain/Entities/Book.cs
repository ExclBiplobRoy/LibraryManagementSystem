using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities
{
    public class Book : Base
    {
        [Key]
        public int BookID { get; set; }

        [Required]
        [Display(Name = "Published Date")]
        public DateTime PublishedDate { get; set; }

        [Required]
        [StringLength(200)]
        public string Title { get; set; }

        [Required]
        [Display(Name = "Available Copies")]
        public int AvailableCopies { get; set; }

        [Required]
        [StringLength(20)]
        public string ISBN { get; set; }

        [Required]
        [Display(Name = "Total Copies")]
        public int TotalCopies { get; set; }

        public int AuthorID { get; set; }

        [ForeignKey("AuthorID")]
        public virtual Author? Author { get; set; }

        public virtual IEnumerable<BorrowedBook>? BorrowedBooks { get; set; }
    }
}