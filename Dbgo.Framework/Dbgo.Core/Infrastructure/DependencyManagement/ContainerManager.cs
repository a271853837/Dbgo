using Autofac;
using Autofac.Core.Lifetime;
using Autofac.Integration.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Dbgo.Core.Infrastructure.DependencyManagement
{
    public class ContainerManager
    {
        private readonly IContainer _container;

        public ContainerManager(IContainer container)
        {
            this._container = container;
        }

        public virtual IContainer Container
        {
            get
            {
                return _container;
            }
        }

        public virtual T Resove<T>(string key = "", ILifetimeScope scope = null) where T : class
        {
            if (scope == null)
                scope = Scope();
            if (string.IsNullOrEmpty(key))
                return scope.Resolve<T>();
            return scope.ResolveKeyed<T>(key);
        }

        public virtual object Resove(Type type , ILifetimeScope scope = null)
        {
            if (scope==null)
            {
                scope = Scope();
            }
            return scope.Resolve(type);
        }


        public virtual T[] ResolveAll<T>(string key="",ILifetimeScope scope = null)
        {
            if (scope==null)
                scope = Scope();
            if (string.IsNullOrEmpty(key))
                return scope.Resolve<IEnumerable<T>>().ToArray();
            return scope.ResolveKeyed<IEnumerable<T>>(key).ToArray();
        }

        
        public  virtual ILifetimeScope Scope()
        {
            try
            {
                if (HttpContext.Current!=null)
                    return AutofacDependencyResolver.Current.RequestLifetimeScope;
                return Container.BeginLifetimeScope(MatchingScopeLifetimeTags.RequestLifetimeScopeTag);
            }
            catch (Exception)
            {
                return Container.BeginLifetimeScope(MatchingScopeLifetimeTags.RequestLifetimeScopeTag);
            }
        }
    }
}
