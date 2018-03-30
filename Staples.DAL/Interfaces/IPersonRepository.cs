using Staples.DAL.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Staples.DAL.Interfaces
{
    public interface IPersonRepository : IRepository<Person>
    {
        Task<List<Person>> GetPeopleByFirstAndLastNameAsync(string firstName, string lastName);
    }
}