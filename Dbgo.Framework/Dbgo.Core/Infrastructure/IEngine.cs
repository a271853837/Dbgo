using Dbgo.Core.Configuration;
using Dbgo.Core.Infrastructure.DependencyManagement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dbgo.Core.Infrastructure
{
    public interface IEngine
    {
         ContainerManager ContainerManager { get; }

        void Initialize(DbgoConfig config);

        T Resolve<T>() where T : class;

        object Resolve(Type type);

        T[] ResolveAll<T>();
    }
}
