using Autofac;
using Autofac.Integration.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Xy.SuperMarket.Domain.Abstract;
using Xy.SuperMarket.Domain.Concrete;

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

            builder
                .RegisterInstance<IProductsRepository>(new InMemoryProductsRepository())
                .PropertiesAutowired();


            var container = builder.Build();
            DependencyResolver
                .SetResolver(new AutofacDependencyResolver(container));
        }
    }
}