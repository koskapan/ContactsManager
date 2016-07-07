using Autofac;
using Autofac.Core;
using Autofac.Integration.Mvc;
using ContactsManager.Domain.Abstract;
using ContactsManager.Domain.Concrete;
using System.Web.Mvc;

namespace ContactsManager.Web.Utils
{
    public class AutofacConfig
    {
        public static void ConfigureContainer()
        {
            var builder = new ContainerBuilder();
            builder.RegisterControllers(typeof(ContactsManager.Web.Controllers.ContactsController).Assembly);

            builder.RegisterType<UnitOfWork>().As<IUnitOfWork>().WithParameter("context", new EfDbContext());

            var container = builder.Build();

            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));
        }
    }
}