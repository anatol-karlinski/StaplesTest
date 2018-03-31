using Staples.Adapters.Interfaces;
using Staples.DAL.Interfaces;
using Staples.DAL.Models;
using Staples.SL.Interfaces;
using Staples.SL.Models;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Staples.SL.Services
{
    public class PeopleService : IPeopleService
    {
        private readonly IPersonRepository _peopleRepository;
        private readonly IPersonAdapter _personAdapter;

        public PeopleService(
            IPersonRepository peopleRepository,
            IPersonAdapter personAdapter
            )
        {
            _peopleRepository = peopleRepository;
            _personAdapter = personAdapter;
        }

        public async Task<ServiceResponse> AddNewPerson(PersonDetails personDetails)
        {
            var response = new ServiceResponse();
            var basePersonEntity = _personAdapter.Adapt(personDetails);
            try
            {
                var personAlreadyExists = await PersonIsInDatabase(basePersonEntity);

                if (personAlreadyExists)
                {
                    response.AddError("Provided person is already registered in the database");
                    return response;
                }

                await _peopleRepository.AddAsync(basePersonEntity);

                return response;
            }
            catch (Exception e)
            {
                response.AddError(e.Message);
                return response;
            }
        }

        public async Task<bool> PersonIsInDatabase(PersonDetails personDetails)
        => await PersonIsInDatabase(_personAdapter.Adapt(personDetails));

        private async Task<bool> PersonIsInDatabase(Person person)
        {
            var matchingPeople = await _peopleRepository.GetPeopleByFirstAndLastNameAsync(person.FirstName, person.LastName);
            return matchingPeople.Any();
        }

    }
}