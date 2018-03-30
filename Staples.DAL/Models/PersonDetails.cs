using Staples.DAL.Enums;
using System.ComponentModel.DataAnnotations;

namespace Staples.DAL.Models
{
    public class PersonDetails
    {
        [Required]
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        [Required]
        public string LastName { get; set; }
        public string Email { get; set; }
        public Gender? Gender { get; set; }
        public int? Age { get; set; }
    }
}