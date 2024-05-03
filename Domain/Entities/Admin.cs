using System.ComponentModel.DataAnnotations;

namespace Domain.Entities
{
    public class Admin : Base
    {
        [Key]
        public int AdminID { get; set; }

        [Required]
        [StringLength(50)]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Required]
        [StringLength(50)]
        [Display(Name = "Surname")]
        public string SurName { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [StringLength(50)]
        public string Password { get; set; }

        [Range(1, 5)]
        public int Level { get; set; }
    }
}