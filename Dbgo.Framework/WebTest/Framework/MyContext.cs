using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using WebTest.Models;

namespace WebTest.Framework
{
    public class MyContext:DbContext
    {
        public MyContext() : base("TestDb")
        {
            Database.SetInitializer<MyContext>(new SeedingDataInitializer());
        }

        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().Map(c =>
            {
                c.ToTable("U");
            });
            base.OnModelCreating(modelBuilder);
        }
    }
}