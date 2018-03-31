using Autofac;

namespace Staples.GUI.Infrastructure
{
    public class DependencyInjectionSetup
    {
        public static void RegisterDependencies(ContainerBuilder builder)
        {
            SL.Infrastructure.DependencyInjectionSetup.RegisterDependencies(builder);
        }
    }
}