using ContactsManager.Domain.Abstract;
using ContactsManager.Domain.Concrete;
using Ninject;
using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ContactsManager.Web.Utils
{
    public class NinjectDependencyResolver : IDependencyResolver
    {
        private IKernel kernel;

        public NinjectDependencyResolver(IKernel kernel)
        {
            this.kernel = kernel;
            RegisterDependencies();
        }

        private void RegisterDependencies()
        {
            var context = new EfDbContext();
            var adapter = (IObjectContextAdapter)context;
            kernel.Bind<IObjectContext>().To<ObjectContextAdapter>().WithConstructorArgument("context", adapter.ObjectContext);
        }

        public object GetService(Type serviceType)
        {
            return kernel.TryGet(serviceType);
        }

        public IEnumerable<object> GetServices(Type serviceType)
        {
            return kernel.GetAll(serviceType);
        }
    }
}