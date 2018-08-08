using System.Web.Mvc;

namespace Biz.PDM
{
    public class PDMAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return "OG";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
                "PDM_default",
                "PDM/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional },
                namespaces: new[] { "Biz.PDM.Controllers" }
            );
        }
    }
}
