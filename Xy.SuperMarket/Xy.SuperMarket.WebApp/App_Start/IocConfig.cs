using Autofac;
using Autofac.Integration.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using Xy.SuperMarket.Domain.Abstract;
using Xy.SuperMarket.Domain.Concrete;
using Xy.SuperMarket.WebApp.Abstract;
using Xy.SuperMarket.WebApp.Concrete;

namespace Xy.SuperMarket.WebApp
{
    public class IocConfig
    {
        public static void ConfigIoc()
        {
            var builder = new ContainerBuilder();

            builder
                .RegisterControllers(typeof(MvcApplication).Assembly)
                .PropertiesAutowired();

            //builder
            //    .RegisterInstance<IProductsRepository>(new InMemoryProductsRepository())
            //    .PropertiesAutowired();
            builder
                .RegisterInstance<IProductsRepository>(new EFProductsRepository())
                .PropertiesAutowired();

            builder
                .RegisterInstance<IOrderProcessor>(new EmailOrderProcessor(new EmailSettings()))
                .PropertiesAutowired();

            builder.RegisterType<EFDbContext>();

            builder
                .RegisterInstance<IAuthProvider>(new EFAuthProvider())
                .PropertiesAutowired();

            var container = builder.Build();
            DependencyResolver
                .SetResolver(new AutofacDependencyResolver(container));
        }
    }
}