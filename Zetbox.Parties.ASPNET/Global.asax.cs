
namespace Zetbox.Parties.ASPNET
{
    using System.Web.Mvc;
    using System.Web.Routing;
    using Autofac;
    using Autofac.Integration.Mvc;
    using Zetbox.API;
    using Zetbox.Client;
    using Zetbox.Client.ASPNET;

    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801

    public class MvcApplication : ZetboxMvcApplication
    {
        public override void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }

        public override void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                "Default", // Route name
                "{controller}/{action}/{id}", // URL with parameters
                new { controller = "Home", action = "Index", id = UrlParameter.Optional } // Parameter defaults
            );
        }

        protected override void ConfigureContainerBuilder(Autofac.ContainerBuilder builder)
        {
            base.ConfigureContainerBuilder(builder);

            builder.RegisterControllers(typeof(MvcApplication).Assembly);
            builder.RegisterViewModels(typeof(MvcApplication).Assembly);

            builder.RegisterModule<Zetbox.Parties.Client.ClientModule>();
        }
    }
}