using System.Web.Mvc;

namespace RBVH.HR
{
    public class INAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "HR";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "IN_default",
                "IN/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional },
                namespaces: new[] { "RBVH.HR.Controllers" }
            );
        }
    }
}