using AutoMapper;
using Staples.DAL.Models;

namespace Staples.Adapters.Infrastructure
{
    public class InfrastructureSetup
    {
        public static void SetupAutoMapper()
        {
            Mapper.Initialize(cfg =>
            {
                cfg.CreateMap<Person, PersonDetails>();
                cfg.CreateMap<PersonDetails, Person>()
                .ForMember(dest => dest.ID, opts => opts.MapFrom(src => 0));
            });
        }
    }
}
