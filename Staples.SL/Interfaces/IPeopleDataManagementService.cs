using Staples.DAL.Models;
using Staples.SL.Models;
using System.Threading.Tasks;

namespace Staples.SL.Interfaces
{
    public interface IPeopleDataManagementService
    {
        Task<ServiceResponse> AddNewPerson(PersonDetails personDetails);
        Task<bool> PersonIsInDatabase(PersonDetails person);
    }
}