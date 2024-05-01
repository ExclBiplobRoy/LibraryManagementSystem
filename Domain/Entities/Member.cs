using System.ComponentModel.DataAnnotations;

namespace Domain.Entities
{
    public class Member : Base
    {
        [Key]
        public int MemberID { get; set; }

        [Required]
        [StringLength(20)]
        [Display(Name = "Phone Number")]
        public string PhoneNumber { get; set; }

        [Required]
        [StringLength(50)]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Required]
        [StringLength(50)]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [Display(Name = "Registration Date")]
        public DateTime RegistrationDate { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }
    }
}