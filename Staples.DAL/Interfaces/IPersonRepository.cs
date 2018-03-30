using Staples.DAL.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Staples.DAL.Interfaces
{
    public interface IPersonRepository
    {
        Task<List<Person>> GetPeopleByFirstAndLastNameAsync(string firstName, string lastName);
        Task<int> AddPersonAsync(Person person);
    }
}