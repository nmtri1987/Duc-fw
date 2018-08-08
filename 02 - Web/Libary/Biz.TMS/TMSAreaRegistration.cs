﻿using System.Web.Mvc;

namespace Biz.PDM
{
    public class TMSAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return "TMS";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
                "TMS_default",
                "TMS/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional },
                namespaces: new[] { "Biz.TMS.Controllers" }
            );
        }
    }
}
