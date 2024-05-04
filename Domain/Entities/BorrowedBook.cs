using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities
{
    public class BorrowedBook : Base
    {
        [Key]
        public int BorrowID { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [Display(Name = "Borrow Date")]
        public DateTime BorrowDate { get; set; }

        [Required]
        public int MemberID { get; set; }

        [ForeignKey("MemberID")]
        public virtual Member? Member { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "Return Date")]
        public DateTime? ReturnDate { get; set; }

        [Required]
        public int BookID { get; set; }

        [ForeignKey("BookID")]
        public virtual Book? Book { get; set; }

        [Required]
        [StringLength(20)]
        public string Status { get; set; }
    }
}