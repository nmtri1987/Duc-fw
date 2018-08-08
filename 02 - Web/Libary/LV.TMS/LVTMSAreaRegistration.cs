using System.Web.Mvc;

namespace Biz.PDM
{
    public class PDMAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return "LVTMS";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
                "LVTMS_default",
                "LVTMS/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional },
                namespaces: new[] { "LV.TMS.Controllers" }
            );
        }
    }
}
