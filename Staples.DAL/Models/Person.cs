using System.ComponentModel.DataAnnotations;

namespace Staples.DAL.Models
{
    public class Person
    {
        [Key]
        public int ID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}