using Dbgo.Core.Infrastructure;
using Dbgo.Data;
using Dbgo.Services.Logging;
using Dbgo.Services.Tasks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace Dbgo.Web
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            EngineContext.Initialize();


            var log = EngineContext.Current.Resolve<ILogger>();
            //var logs = log.GetAllLogs();
            log.Information("123");

            //TaskManager.Instance.Initialize();
            //TaskManager.Instance.Start();
        }
    }
}
