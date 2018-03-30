using System.Data.Entity;

namespace Staples.DAL.Models
{
    public class StaplesDbContext : DbContext
    {
        public DbSet<Person> People { get; set; }
    }
}