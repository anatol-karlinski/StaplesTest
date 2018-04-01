using Staples.DAL.Enums;
using System.ComponentModel.DataAnnotations;

namespace Staples.DAL.Models
{
    public class PersonDetails
    {
        [Required]
        [Display(Name = "First name")]
        public string FirstName { get; set; }
        [Display(Name = "Middle name")]
        public string MiddleName { get; set; }
        [Required]
        [Display(Name = "Last name")]
        public string LastName { get; set; }
        [Display(Name = "Email address")]
        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        public string Email { get; set; }
        public Gender? Gender { get; set; }
        public int? Age { get; set; }
    }
}