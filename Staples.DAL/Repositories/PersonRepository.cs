using Staples.DAL.Abstracts;
using Staples.DAL.Interfaces;
using Staples.DAL.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Staples.DAL.Repositories
{
    public class PersonRepository : AbstractRepository<Person>, IPersonRepository
    {
        public async Task<List<Person>> GetPeopleByFirstAndLastNameAsync(string firstName, string lastName)
            => await GetWhereAsync(person =>
                    person.FirstName.Trim().ToUpper() == firstName.Trim().ToUpper() &&
                    person.LastName.Trim().ToUpper() == lastName.Trim().ToUpper()
               );

        public async Task<List<Person>> GetAllPeople()
            => await GetWhereAsync(x => true);
    }
}