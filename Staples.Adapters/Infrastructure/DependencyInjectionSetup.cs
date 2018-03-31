using Autofac;
using Staples.Adapters.Interfaces;

namespace Staples.Adapters.Infrastructure
{
    public class DependencyInjectionSetup
    {
        public static void RegisterAdapters(ContainerBuilder builder)
        {
            builder.RegisterType<PersonAdapter>().As<IPersonAdapter>();
        }
    }
}