using Autofac;
using Staples.DAL.Interfaces;
using Staples.DAL.Repositories;

namespace Staples.DAL.Infrastructure
{
    public class DependencyInjectionSetup
    {
        public static void RegisterRepositories(ContainerBuilder builder)
        {
            builder.RegisterType<PersonRepository>().As<IPersonRepository>();
        }
    }
}