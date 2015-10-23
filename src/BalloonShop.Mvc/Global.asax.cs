using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using StructureMap;
using NHibernate.Context;
using NHibernate;
using BalloonShop.Mvc.Config;
using BalloonShop.Mvc.Services;
using Rhino.ServiceBus;
using Rhino.ServiceBus.Impl;
using SquishIt.Framework;

namespace BalloonShop.Mvc
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
                "Default",                                              // Route name
                "{controller}/{action}/{id}",                           // URL with parameters
                new { controller = "Home", action = "Index", id = "" }  // Parameter defaults
            );

        }

        public static void RegisterBundles()
        {
            Bundle.Css().Add("~/Content/themes/base/jquery-ui.css")
                .Add("~/Content/jquery.fancybox.css")
                .Add("~/Content/jquery.rating.css")
                .Add("~/Content/bootstrap.css")
                .Add("~/Content/app.css")
                .AsCached("balloonshop", "~/assets/css/balloonshop");
            Bundle.JavaScript().Add("~/Scripts/jquery-2.1.4.js")
                .Add("~/Scripts/jquery-ui-1.11.4.js")
                .Add("~/Scripts/jquery.fancybox.pack.js")
                .Add("~/Scripts/jquery.rating.js")
                .Add("~/Scripts/bootstrap.js")
                .Add("~/Scripts/app.js")
                .AsCached("balloonshop", "~/assets/scripts/balloonshop");
        }

        public static void RegisterValueProviders(ValueProviderFactoryCollection factories)
        {
            factories.Add(new CookieValueProviderFactory());
        }

        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();

            RegisterGlobalFilters(GlobalFilters.Filters);

            RegisterRoutes(RouteTable.Routes);

            RegisterBundles();

            RegisterValueProviders(ValueProviderFactories.Factories);

            ObjectFactory.Initialize(ctx => {
                //ctx.Scan(x => {
					//x.AssembliesFromApplicationBaseDirectory();
                    //x.LookForRegistries();
                //});

				ctx.AddRegistry<CoreRegistry>();
				ctx.AddRegistry<WebRegistry>();
            });

            DependencyResolver.SetResolver(new StructureMapDependencyResolver(ObjectFactory.Container));

            RegisterBus(ObjectFactory.Container);
        }

        private void RegisterBus(IContainer container)
        {
            new OnewayRhinoServiceBusConfiguration().UseStructureMap(container).Configure();
        }

        public MvcApplication()
        {
            BeginRequest += (sender, e) => {
                CurrentSessionContext.Bind(ObjectFactory.GetInstance<ISessionFactory>().OpenSession());

                if (HttpContext.Current.Request.Cookies["customerCartId"] == null) {
                    HttpContext.Current.Response.SetCookie(new HttpCookie("customerCartId", Guid.NewGuid().ToString()));
                }
            };

            EndRequest += (sender, e) => {
                var session = CurrentSessionContext.Unbind(ObjectFactory.GetInstance<ISessionFactory>());

                if (session != null) {
                    session.Flush();
                    session.Dispose();
                }

                //ObjectFactory.ReleaseAndDisposeAllHttpScopedObjects();
            };

			AuthenticateRequest += FormsAuthenticationService.AuthenticateRequest;
        }
    }
}