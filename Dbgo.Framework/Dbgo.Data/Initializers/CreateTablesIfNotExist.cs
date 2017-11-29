﻿using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace Dbgo.Data.Initializers
{
    public class CreateTablesIfNotExist<TContext> : IDatabaseInitializer<TContext> where TContext : DbContext
    {
        public void InitializeDatabase(TContext context)
        {
            bool dbExists;
            using (new TransactionScope(TransactionScopeOption.Suppress))
            {
                dbExists = context.Database.Exists();
            }
            if (dbExists)
            {
                //var dbCreationScript = ((IObjectContextAdapter)context).ObjectContext.CreateDatabaseScript();
                //context.Database.ExecuteSqlCommand(dbCreationScript);
                //context.SaveChanges();
            }
            else
            {
                context.Database.CreateIfNotExists();
                //throw new ApplicationException("No database instance");
            }
        }
    }
}
