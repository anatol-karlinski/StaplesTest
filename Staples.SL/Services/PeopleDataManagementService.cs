using Staples.Adapters;
using Staples.Adapters.Interfaces;
using Staples.DAL.Interfaces;
using Staples.DAL.Models;
using Staples.DAL.Repositories;
using Staples.SL.Interfaces;
using Staples.SL.Models;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Staples.SL.Services
{
    public class PeopleDataManagementService : IPeopleDataManagementService
    {
        private readonly IPersonRepository _peopleRepository;
        private readonly IAdapter<Person, PersonDetails> _personAdapter;

        public PeopleDataManagementService()
        {
            _peopleRepository = new PersonRepository();
            _personAdapter = new PersonAdapter();
        }

        //public PeopleService(IPeopleRepository peopleRepository, IAdapter<Person, PersonDetails> personAdapter)
        //{
        //    _peopleRepository = peopleRepository;
        //    _personAdapter = personAdapter;
        //}

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

                var addedEntitiesCount = await _peopleRepository.AddPersonAsync(basePersonEntity);
                if (addedEntitiesCount == 0)
                {
                    response.AddError("Unknown error has occurred during communication with database");
                    return response;
                }

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