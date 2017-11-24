using System;
using Dbgo.Core.Configuration;
using Dbgo.Core.Infrastructure.DependencyManagement;
using Autofac;

namespace Dbgo.Core.Infrastructure
{
    /// <summary>
    /// 默认引擎
    /// </summary>
    public class DbgoEngine : IEngine
    {
        private static ContainerManager _containerManager;
        public ContainerManager ContainerManager
        {
            get
            {
                return _containerManager;
            }
        }

        public void Initialize(DbgoConfig config)
        {
            var builder = new ContainerBuilder();
            var container = builder.Build();
            _containerManager = new ContainerManager(container);
        }

        public object Resolve(Type type)
        {
            return ContainerManager.Resove(type);
        }

        public T Resolve<T>() where T : class
        {
            return ContainerManager.Resove<T>();
        }

        public T[] ResolveAll<T>()
        {
            return ContainerManager.ResolveAll<T>();
        }
    }
}
