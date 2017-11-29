using System;
using Dbgo.Core.Configuration;
using Dbgo.Core.Infrastructure.DependencyManagement;
using Autofac;
using System.Collections.Generic;
using System.Reflection;
using System.Linq;

namespace Dbgo.Core.Infrastructure
{
    /// <summary>
    /// 默认引擎
    /// </summary>
    public class DbgoEngine : IEngine
    {
        private ContainerManager _containerManager;
        public ContainerManager ContainerManager
        {
            get
            {
                return _containerManager;
            }
        }

        public void Initialize(DbgoConfig config)
        {
            RegisterDependencies(config);
            RunStartupTasks();
        }


        public virtual void RunStartupTasks()
        {
            var typeFinder = _containerManager.Resove<ITypeFinder>();
            var tasks = typeFinder.FindClassesOfType<IStartupTask>();
            var startUpTasks = new List<IStartupTask>();
            foreach (var item in tasks)
                startUpTasks.Add((IStartupTask)Activator.CreateInstance(item));
            startUpTasks = startUpTasks.AsQueryable().OrderBy(st => st.Order).ToList();
            foreach (var item in startUpTasks)
                item.Excute();
        }

        public virtual void RegisterDependencies(DbgoConfig config)
        {
            var builder = new ContainerBuilder();

            var typeFinder = new WebAppTypeFinder();
            builder.RegisterInstance(typeFinder).As<ITypeFinder>().SingleInstance();

            var drInstances = new List<IDependencyRegistrar>();
            var drTypes = typeFinder.FindClassesOfType<IDependencyRegistrar>();
            foreach (var drType in drTypes)
                drInstances.Add((IDependencyRegistrar)Activator.CreateInstance(drType));
            //sort
            drInstances = drInstances.AsQueryable().OrderBy(t => t.Order).ToList();
            foreach (var dependencyRegistrar in drInstances)
                dependencyRegistrar.Register(builder);

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
