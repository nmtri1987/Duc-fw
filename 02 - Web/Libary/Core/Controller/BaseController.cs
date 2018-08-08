
using Security.DAL.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

public class BaseController : Controller
{
    protected override void Initialize(RequestContext requestContext)
    {
        ///get controller name
        var routeValues = requestContext.HttpContext.Request.RequestContext.RouteData.Values;
        string controllerName = "";
        if (routeValues.ContainsKey("controller"))
            controllerName = (string)routeValues["controller"];
        else
            controllerName = string.Empty;
        ViewBag.Controller = controllerName;


        base.Initialize(requestContext);
    }

    public static RBVHUser CurrentUser
    {
        get
        {
            return CustomerAuthorize.CurrentUser;
        }

    }
    protected virtual new CustomPrincipal User
    {
        get { return HttpContext.User as CustomPrincipal; }
    }

    
}

