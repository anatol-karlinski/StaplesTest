using AutoMapper;
using Staples.DAL.Interfaces;
using Staples.DAL.Models;
using Staples.SL.Interfaces;
using Staples.SL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Staples.SL.Services
{
    public class PeopleService : IPeopleService
    {
        private readonly IPersonRepository _peopleRepository;

        public PeopleService(IPersonRepository peopleRepository)
        {
            _peopleRepository = peopleRepository;
        }

        public async Task<ServiceResponse> AddNewPerson(PersonDetails personDetails)
        {
            var response = new ServiceResponse();
            var basePersonEntity = Mapper.Map<Person>(personDetails);

            try
            {
                var personAlreadyExists = await PersonIsInDatabase(basePersonEntity);

                if (personAlreadyExists)
                {
                    response.AddError("Provided person is already registered in the database.");
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

        public async Task<List<Person>> GetAllPeople()
            => await _peopleRepository.GetAllPeople();

        public async Task<bool> PersonIsInDatabase(PersonDetails personDetails)
            => await PersonIsInDatabase(Mapper.Map<Person>(personDetails));

        private async Task<bool> PersonIsInDatabase(Person person)
            => (await _peopleRepository
                    .GetPeopleByFirstAndLastNameAsync(person.FirstName, person.LastName))
                    .Any();
    }
}