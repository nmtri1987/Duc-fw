using System.Web.Mvc;

namespace ifinds.API.Areas.CA
{
    public class INAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "IN";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "IN_default",
                "IN/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional },
                namespaces: new[] { "ifinds.Object.IN.Controllers" }
            );
        }
    }
}