using Autofac;
using Staples.SL.Interfaces;
using Staples.SL.Services;

namespace Staples.SL.Infrastructure
{
    public class DependencyInjectionSetup
    {
        public static void RegisterDependencies(ContainerBuilder builder)
        {
            RegisterOutsideDependencies(builder);
            RegisterServices(builder);
        }

        public static void RegisterServices(ContainerBuilder builder)
        {
            builder.RegisterType<PeopleService>().As<IPeopleService>();
        }

        private static void RegisterOutsideDependencies(ContainerBuilder builder)
        {
            DAL.Infrastructure.DependencyInjectionSetup.RegisterRepositories(builder);
        }
    }
}