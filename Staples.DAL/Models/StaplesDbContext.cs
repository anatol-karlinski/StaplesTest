using System.Data.Entity;

namespace Staples.DAL.Models
{
    public class StaplesDBContext : DbContext
    {
        public StaplesDBContext()
        {
            Database.SetInitializer(new CreateDatabaseIfNotExists<StaplesDBContext>());
        }

        public DbSet<Person> People { get; set; }
    }
}