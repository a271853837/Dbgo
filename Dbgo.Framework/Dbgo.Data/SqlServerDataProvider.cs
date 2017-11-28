using Dbgo.Core.Data;
using Dbgo.Data.Initializers;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dbgo.Data
{
    public class SqlServerDataProvider : IDataProvider
    {
        public void InitConnectionFactory()
        {
            var connectionFactory = new SqlConnectionFactory();
#pragma warning disable
            Database.DefaultConnectionFactory = connectionFactory;
        }

        public void InitDatabase()
        {
            InitConnectionFactory();
            SetDatabaseInitializer();
        }

        public void SetDatabaseInitializer()
        {
            var initializer = new CreateTablesIfNotExist<DbgoObjectContext>();
            Database.SetInitializer(initializer);
        }
    }
}
