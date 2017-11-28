using Dbgo.Core.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dbgo.Data
{
    public partial class EfDataProviderManager : BaseDataProviderManager
    {
        public override IDataProvider LoadDataProvider()
        {
            return new SqlServerDataProvider();
        }
    }
}
