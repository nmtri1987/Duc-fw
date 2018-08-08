using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using Biz.Core;
using Biz.Core.Services.Tasks;
using Biz.Core.Infrastructure;
using Biz.Core.Security;
namespace Web
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            ModelBinders.Binders.Add(typeof(decimal), new DecimalModelBinder());
            ModelBinders.Binders.Add(typeof(decimal?), new DecimalModelBinder());

            ModelBinders.Binders.Add(typeof(DateTime), new CustomDateModelBinder());
            ModelBinders.Binders.Add(typeof(DateTime?), new NullableDateTimeBinder());

            EngineContext.Initialize(false);

            // add RBVH Session 

            //start scheduled tasks
            TaskManager.Instance.Initialize();
            TaskManager.Instance.Start();

            
        }

        //protected void Session_Start(object sender, EventArgs e)
        //{
        //    if(AuthenticatedUser)
        //    AuthenticatedUser user = new AuthenticatedUser();
        //    user.CompanyID = 1;
        //    user.UserName = "DGN2HC";
        //    HttpContext.Current.Session[SystemConfig.loginKey] = user;

        //}
    }
}
