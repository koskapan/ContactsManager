using Autofac;
using Autofac.Integration.WebApi;
using ContactsManager.Domain.Abstract;
using ContactsManager.Domain.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;

namespace ContactsManager.Web.Utils
{
    public static class AutofacConfig
    {
        public static void ConfigureContainer()
        {
            var builder = new ContainerBuilder();
            var configuration = GlobalConfiguration.Configuration;
            builder.RegisterApiControllers(Assembly.GetExecutingAssembly());
            builder.RegisterType<ContactRepository>().As<IContactRepository>().InstancePerLifetimeScope();

            IContainer container = builder.Build();
            configuration.DependencyResolver = new AutofacWebApiDependencyResolver(container);
        }
    }
}