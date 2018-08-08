using System.Web.Mvc;

namespace Biz.CS
{
    public class CSAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return "CS";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
                "CS_default",
                "CS/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional },
                namespaces: new[] { "Biz.CS.Controllers" }
            );
        }
    }
}
