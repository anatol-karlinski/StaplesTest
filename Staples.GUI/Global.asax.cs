using Autofac;
using Autofac.Integration.Mvc;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace Staples.GUI
{
    public class MvcApplication : HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            DataAnnotationsModelValidatorProvider.AddImplicitRequiredAttributeForValueTypes = false;
            SL.Infrastructure.AutoMapperSetup.SetupAutoMapper();
            DependencyResolver.SetResolver(InitializeDepedencyResolver());
        }

        private IDependencyResolver InitializeDepedencyResolver()
        {
            var builder = new ContainerBuilder();
            builder.RegisterControllers(typeof(MvcApplication).Assembly);
            builder.RegisterModule<AutofacWebTypesModule>();
            Infrastructure.DependencyInjectionSetup.RegisterDependencies(builder);
            var container = builder.Build();
            return new AutofacDependencyResolver(container);
        }
    }
}
