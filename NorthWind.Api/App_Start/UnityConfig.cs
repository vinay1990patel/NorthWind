using Microsoft.Practices.Unity.Configuration;
using Northwind.Entities.Models;
using Northwind.Service;
using NorthWind.Api.Controllers;
using Repository.Pattern.DataContext;
using Repository.Pattern.Ef6;
using Repository.Pattern.Repositories;
using Repository.Pattern.UnitOfWork;
using System;
using System.Web.Http;
using System.Web.Mvc;
using Unity;
using Unity.Lifetime;
using Unity.WebApi;

namespace NorthWind.Api
{
    public static class UnityConfig
    {

     
        private static Lazy<IUnityContainer> container = new Lazy<IUnityContainer>(() =>
        {
            var container = new UnityContainer();
            RegisterComponents();
            return container;
        });

       


        public static void RegisterComponents()
        {
			var container = new UnityContainer();

            // register all your components with the container here
            // it is NOT necessary to register your controllers
            //   container.RegisterType<IDataContextAsync>("NorthwindContext");
            // e.g. container.RegisterType<ITestService, TestService>();
           // container.LoadConfiguration();
            container
             
               .RegisterType<IDataContextAsync, NorthwindContext>(new HierarchicalLifetimeManager())
               .RegisterType<IUnitOfWorkAsync, UnitOfWork>(new HierarchicalLifetimeManager())
               .RegisterType<IUnitOfWork, UnitOfWork>()
               .RegisterType<IRepositoryAsync<Customer>, Repository<Customer>>()
               .RegisterType<IRepositoryAsync<CustomerDemographic>, Repository<CustomerDemographic>>()
               .RegisterType<IRepositoryAsync<Product>, Repository<Product>>()
               .RegisterType<IProductService, ProductService>()
               .RegisterType<ICustomerService, CustomerService>(new TransientLifetimeManager())
               .RegisterType<INorthwindStoredProcedures, NorthwindContext>(new HierarchicalLifetimeManager())
               .RegisterType<IStoredProcedureService, StoredProcedureService>();
               GlobalConfiguration.Configuration.DependencyResolver = new UnityDependencyResolver(container);
            
        }
        /// <summary>
        /// Gets the configured Unity container.
        /// </summary>
        public static IUnityContainer GetConfiguredContainer()
        {
            return container.Value;
        }
    }
}