using System.Data.Entity;

namespace Staples.DAL.Models
{
    public class StaplesDBContext : DbContext
    {
        public DbSet<Person> People { get; set; }
    }
}