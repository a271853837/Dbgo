using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dbgo.Data.Mapping
{
    public  class DbgoEntityTypeConfiguration<T> : EntityTypeConfiguration<T> where T : class
    {
        protected DbgoEntityTypeConfiguration()
        {
            PostInitialize();
        }

        protected virtual void PostInitialize()
        {

        }
    }
}
