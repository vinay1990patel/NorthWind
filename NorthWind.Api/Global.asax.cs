using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using NorthWind.Api;
using Unity;
using System.Web.Http.Dispatcher;
using StructureMap.AutoMocking;
namespace NorthWind.Api
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            UnityConfig.RegisterComponents();
            //  IUnityContainer container = BuildUnityContainer();
            //IUnityContainer container = new UnityContainer();
            //container.RegisterInstance<IHttpControllerActivator>(new UnityConfig(container));
          
          
        }
    }
}
