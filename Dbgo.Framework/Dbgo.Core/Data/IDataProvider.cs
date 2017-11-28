using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dbgo.Core.Data
{
    public interface IDataProvider
    {
        void InitConnectionFactory();
        void SetDatabaseInitializer();
        void InitDatabase();
    }
}
