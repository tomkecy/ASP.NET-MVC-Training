using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Blog.Domain.Abstract;
using Blog.Domain.Concrete;
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
        }//END of GetService method

        public IEnumerable<object> GetServices(Type serviceType)
        {
            return kernel.GetAll(serviceType);
        }//END of GetServices method

        private void AddBindings()
        {
            kernel.Bind<IPostRepository>().To<PostRepository>();
        }//END of AddBindings method
    }// END of public class NinjectDependencyResolver
}