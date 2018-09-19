using System.Web.Http;
using Unity;
using Unity.AspNet.WebApi;
using System.Web.Http.Dispatcher;

[assembly: WebActivatorEx.PreApplicationStartMethod(typeof(NorthWind.Api.UnityWebApiActivator), nameof(NorthWind.Api.UnityWebApiActivator.Start))]
[assembly: WebActivatorEx.ApplicationShutdownMethod(typeof(NorthWind.Api.UnityWebApiActivator), nameof(NorthWind.Api.UnityWebApiActivator.Shutdown))]

namespace NorthWind.Api
{
    /// <summary>
    /// Provides the bootstrapping for integrating Unity with WebApi when it is hosted in ASP.NET.
    /// </summary>
    public static class UnityWebApiActivator
    {
        /// <summary>
        /// Integrates Unity when the application starts.
        /// </summary>
       
        public static void Start() 
        {
            // Use UnityHierarchicalDependencyResolver if you want to use
            // a new child container for each IHttpController resolution.
            // var resolver = new UnityHierarchicalDependencyResolver(UnityConfig.Container);
            var resolver = new UnityDependencyResolver(UnityConfig.GetConfiguredContainer());

            GlobalConfiguration.Configuration.DependencyResolver = resolver;
        }

        /// <summary>
        /// Disposes the Unity container when the application is shut down.
        /// </summary>
        public static void Shutdown()
        {
            var container = UnityConfig.GetConfiguredContainer();
            container.Dispose();
        }
    }
}