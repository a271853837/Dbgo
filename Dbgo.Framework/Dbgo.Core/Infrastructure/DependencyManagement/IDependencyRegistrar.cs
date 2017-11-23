using Autofac;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dbgo.Core.Infrastructure.DependencyManagement
{
    /// <summary>
    /// 依赖注入
    /// </summary>
    public interface IDependencyRegistrar
    {
        void Register(ContainerBuilder builder);

        int Order { get; }
    }
}
