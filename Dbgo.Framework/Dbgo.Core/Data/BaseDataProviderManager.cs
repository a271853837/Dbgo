using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dbgo.Core.Data
{
    public abstract class BaseDataProviderManager
    {


        public abstract IDataProvider LoadDataProvider();
    }
}
