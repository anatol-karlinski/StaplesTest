using AutoMapper;
using Staples.Adapters.Interfaces;
using Staples.DAL.Models;

namespace Staples.Adapters
{
    public class PersonAdapter : IAdapter<Person, PersonDetails>
    {
        public Person Adapt(PersonDetails source, Person target)
            => Mapper.Map(source, target);

        public PersonDetails Adapt(Person source, PersonDetails target)
            => Mapper.Map(source, target);

        public PersonDetails Adapt(Person source)
            => Mapper.Map(source, new PersonDetails());

        public Person Adapt(PersonDetails source)
            => Mapper.Map(source, new Person());
    }
}
