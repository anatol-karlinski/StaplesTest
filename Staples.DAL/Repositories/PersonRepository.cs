using Staples.DAL.Abstracts;
using Staples.DAL.Helpers;
using Staples.DAL.Interfaces;
using Staples.DAL.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Staples.DAL.Repositories
{
    public class PersonRepository : AbstractRepository<Person>, IPersonRepository
    {
        private XMLHelper<Person> _xmlDbHelper;

        public PersonRepository()
        {
            _xmlDbHelper = new XMLHelper<Person>();
        }

        public async Task<List<Person>> GetPeopleByFirstAndLastNameAsync(string firstName, string lastName)
        => await GetWhereAsync(person =>
                person.FirstName.Trim().ToUpper() == firstName.Trim().ToUpper() &&
                person.LastName.Trim().ToUpper() == lastName.Trim().ToUpper()
           );

        public async Task<int> AddPersonAsync(Person person)
        {
            var taskArray = new List<Task<int>> {
                Task.Run(async () => await AddAsync(person)),
                Task.Run(async () => await _xmlDbHelper.AddAsync(person))
            };
            await Task.WhenAll(taskArray);
            return taskArray[1].Result;
        }

    }
}