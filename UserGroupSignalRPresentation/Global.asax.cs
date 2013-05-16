using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using UserGroupSignalRPresentation.PersistentConnections;

namespace UserGroupSignalRPresentation
{
    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801

    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            // Used for SignalR Hubs, this will ensure all hubs get routed accordingly (automatically)
            RouteTable.Routes.MapHubs();

            // Used for SignalR PersistentConnection, all routes must be set up individually
            RouteTable.Routes.MapConnection<ChatPersistentConnection>("chatcon", "/chatcon");


            AreaRegistration.RegisterAllAreas();

            WebApiConfig.Register(GlobalConfiguration.Configuration);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }
    }
}