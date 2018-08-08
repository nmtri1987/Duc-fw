using Helpers;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.Runtime.Serialization;
using Biz.Core.Models;
using Biz.Core.Services;

namespace Biz.Core.Security
{
    public class CustomAuthorizeAttribute : AuthorizeAttribute
    {
        public string UsersConfigKey { get; set; }
        public string RolesConfigKey { get; set; }

        //protected virtual CustomPrincipal CurrentUser
        //{
        //    get { return HttpContext.Current.User as CustomPrincipal; }
        //}

        public override void OnAuthorization(AuthorizationContext filterContext)
        {
            if (!CustomerAuthorize.CheckLogin())
            {
                filterContext.HttpContext.Response.Redirect("~/ErrorPages/AccessDenied.html");
            }
            string action = "";
            string controllerName = string.Empty;
            var routeValues = filterContext.HttpContext.Request.RequestContext.RouteData.Values;
            if (routeValues.ContainsKey("controller"))
                controllerName = (string)routeValues["controller"];

            if (routeValues.ContainsKey("action"))
            {
                action = (string)routeValues["action"];
            }
            //if (routeValues.ContainsKey("action"))
            //{
            //    string Action = (string)routeValues["action"];
            //    if (Action.ToLower() != "index")
            //    {
            //        controllerName += "/" + (string)routeValues["action"];
            //    }
            //}

            //bool isAllow = CheckSiteMapPermission(CustomerAuthorize.CurrentUser, controllerName);
            if (!CustomerAuthorize.CheckSiteMapPermission(CustomerAuthorize.CurrentUser, controllerName, action))
            {
                filterContext.HttpContext.Response.Redirect("~/ErrorPages/AccessDenied.html");
            }
        }

        public AuthenticatedUser GetUserAuthenticated()
        {
            AuthenticatedUser user = new AuthenticatedUser();
            if (HttpContext.Current.Request.Cookies["user"] != null)
            {
                string strCookie = HttpContext.Current.Request.Cookies["user"].Value;
                user.UserName = (strCookie.Split('&')[0]).Split('=')[1];
                user.CompanyID = Convert.ToInt32((strCookie.Split('&')[3]).Split('=')[1]);
                user.isNonAutoLogout =(strCookie.Split('&')[4]).Split('=')[1]=="True"?true:false;
                user.isViewAllCompanyReport = (strCookie.Split('&')[5]).Split('=')[1] == "True" ? true : false;
                user.EmpCD = (strCookie.Split('&')[6]).Split('=')[1] ;
            }

            return user;
        }
     
    }
}