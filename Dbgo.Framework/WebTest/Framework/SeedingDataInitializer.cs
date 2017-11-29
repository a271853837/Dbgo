using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using WebTest.Models;

namespace WebTest.Framework
{
    public class SeedingDataInitializer: DropCreateDatabaseAlways<MyContext>
    {
        protected override void Seed(MyContext context)
        {
            User model = new User();
            model.UserName = "1234";
            context.Users.Add(model);
            base.Seed(context);
        }
    }
}