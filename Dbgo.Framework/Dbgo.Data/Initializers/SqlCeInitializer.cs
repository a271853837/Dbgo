using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dbgo.Data.Initializers
{
    public abstract class SqlCeInitializer<T> : IDatabaseInitializer<T> where T : DbContext
    {
        public abstract void InitializeDatabase(T context);

    }
}
