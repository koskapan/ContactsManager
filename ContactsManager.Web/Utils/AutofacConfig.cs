using Autofac;
using Autofac.Core;
using Autofac.Integration.Mvc;
using ContactsManager.Domain.Abstract;
using ContactsManager.Domain.Concrete;
using System.Data.Entity.Infrastructure;
using System.Web.Mvc;

namespace ContactsManager.Web.Utils
{
    public class AutofacConfig
    {
        public static void ConfigureContainer()
        {
            var builder = new ContainerBuilder();
            builder.RegisterControllers(typeof(ContactsManager.Web.Controllers.ContactsController).Assembly);
            var context = new EfDbContext();
            var adaprer = (IObjectContextAdapter)context;
            builder.RegisterType<ObjectContextAdapter>().As<IObjectContext>().WithParameter("context", adaprer.ObjectContext).InstancePerLifetimeScope();
            builder.RegisterType<UnitOfWork>().As<IUnitOfWork>();

            var container = builder.Build();

            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));
        }
    }
}