using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Ninject;

namespace Blog
{
    public class NinjectDependencyResolver : IDependencyResolver
    {
        #region PrivateFields

        private IKernel kernel;

        #endregion

        #region Constructors

        public NinjectDependencyResolver(IKernel kernel)
        {
            this.kernel = kernel;
            AddBindings();
        }

        #endregion


        public object GetService(Type serviceType)
        {
            return kernel.TryGet(serviceType);
        }

        public IEnumerable<object> GetServices(Type serviceType)
        {
            return kernel.GetAll(serviceType);
        }

        private void AddBindings()
        {
            
        }
    }// END of public class NinjectDependencyResolver
}