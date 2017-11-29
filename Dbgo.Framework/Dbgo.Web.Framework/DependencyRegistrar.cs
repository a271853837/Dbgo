using Dbgo.Core.Infrastructure.DependencyManagement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autofac;
using Dbgo.Data;
using Dbgo.Core.Data;

namespace Dbgo.Web.Framework
{
    public class DependencyRegistrar : IDependencyRegistrar
    {
        public int Order
        {
            get { return 0; }
        }

        public void Register(ContainerBuilder builder)
        {
            builder.Register(x => new EfDataProviderManager()).As<BaseDataProviderManager>().InstancePerDependency();
            builder.Register(x => x.Resolve<BaseDataProviderManager>().LoadDataProvider()).As<IDataProvider>().InstancePerDependency();

            var efDataProviderManager = new EfDataProviderManager();
            var dataProvider = efDataProviderManager.LoadDataProvider();
            dataProvider.InitConnectionFactory();

            builder.Register<IDbContext>(c => new DbgoObjectContext("Dbgo")).InstancePerLifetimeScope();

            builder.RegisterGeneric(typeof(EfRepository<>)).As(typeof(IRepository<>)).InstancePerLifetimeScope();



        }
    }
}
