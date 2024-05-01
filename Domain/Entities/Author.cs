using System.ComponentModel.DataAnnotations;

namespace Domain.Entities
{
    public class Author : Base
    {
        [Key]
        public int AuthorID { get; set; }

        [Required]
        [StringLength(100)]
        [Display(Name = "Author Name")]
        public string AuthorName { get; set; }

        [StringLength(500)]
        [Display(Name = "Author Biography")]
        public string? AuthorBio { get; set; }

        public virtual IEnumerable<Book>? Books { get; set; }
    }
}