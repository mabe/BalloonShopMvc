using BalloonShop.Mvc.Config;
using NHibernate;
using NHibernate.Context;
using StructureMap;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace BalloonShop.Admin.Mvc
{
    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801

    public class MvcApplication : System.Web.HttpApplication
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }

        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                "Default", // Route name
                "{controller}/{action}/{id}", // URL with parameters
                new { controller = "Home", action = "Index", id = UrlParameter.Optional } // Parameter defaults
            );

        }

        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();

            RegisterGlobalFilters(GlobalFilters.Filters);
            RegisterRoutes(RouteTable.Routes);
            RegisterDependencyInjector(ObjectFactory.Container);
        }

        private void RegisterDependencyInjector(IContainer container)
        {
            container.Configure(ctx =>
            {
                ctx.Scan(x =>
                {
                    x.AssembliesFromApplicationBaseDirectory();
                    x.LookForRegistries();
                });
            });

            DependencyResolver.SetResolver(new StructureMapDependencyResolver(container));
        }

        public MvcApplication()
        {
            BeginRequest += (sender, e) =>
            {
                CurrentSessionContext.Bind(ObjectFactory.GetInstance<ISessionFactory>().OpenSession());
            };

            EndRequest += (sender, e) =>
            {
                var session = CurrentSessionContext.Unbind(ObjectFactory.GetInstance<ISessionFactory>());

                if (session != null)
                {
                    session.Flush();
                    session.Dispose();
                }

                ObjectFactory.ReleaseAndDisposeAllHttpScopedObjects();
            };
        }
    }
}