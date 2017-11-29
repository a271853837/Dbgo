using Dbgo.Core.Data;
using Dbgo.Core.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dbgo.Data
{
    public partial class EfStartUpTask : IStartupTask
    {
        public int Order
        {
            get
            {
                return -1000;
            }
        }

        public void Excute()
        {
            var provider = EngineContext.Current.Resolve<IDataProvider>();
            if (provider == null)
                throw new Exception("No IDataProvider found");
            provider.SetDatabaseInitializer();
        }
    }
}
