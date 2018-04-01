using Staples.DAL.Models;
using Staples.SL.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Staples.SL.Interfaces
{
    public interface IPeopleService
    {
        Task<ServiceResponse> AddNewPerson(PersonDetails personDetails);
        Task<List<Person>> GetAllPeople();
        Task<bool> PersonIsInDatabase(PersonDetails person);
    }
}