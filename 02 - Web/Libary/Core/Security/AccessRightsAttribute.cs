using Helpers;
using Biz.Core.Models;
using Biz.Core.Services;

using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;


namespace Biz.Core.Security
{
    public class AccessRightsAttribute : AuthorizeAttribute
    {
        private string _controllerName = string.Empty;
        public AccessRightsAttribute()
        {

        }
        public AccessRightsAttribute(string controllerName)
        {
            this._controllerName = controllerName;
        }
        public override void OnAuthorization(AuthorizationContext filterContext)
        {
            //if (Biz.Core.Helpers.SessionHelper.CurrentUser != null)
            //{
            //    string strNTId = Biz.Core.Helpers.SessionHelper.CurrentUser.NTId.ToLower();
            //    if (SystemConfig.UserAcess.ToLower().IndexOf(strNTId) == -1)
            //    {
            //        filterContext.HttpContext.Response.Redirect("~/ErrorPages/AccessDenied.html");
            //    }
            //    return;
            //}
            
            //filterContext.HttpContext.Response.Redirect("~/ErrorPages/AccessDenied.html");
            //if (!CustomerAuthorize.CheckLogin())
            //{
               
            //    ////SiteMapCollection result = SiteMapManager.GetSiteMap(SiteMapManager.GetAll(CustomerAuthorize.objUser.CompanyID));
            //    ////int count = result.Where(m => Array.IndexOf(m.Url.ToLower().Split('/'),_controllerName.ToLower())!=-1).Count<Biz.Core.Models.SiteMap>();
            //    ////if (count <= 0 )
            //    ////{
            //    ////    //unckech to excute security
            //    ////    //DNH
            //    ////    //filterContext.HttpContext.Response.Redirect("~/ErrorPages/AccessDenied.html");
            //    ////}
            //    //if (CustomerAuthorize.CurrentUser== null)
            //    //{
            //    //    //unckech to excute security
            //    //    //DNH
                    
                    
            //    //}
            //    filterContext.HttpContext.Response.Redirect("~/ErrorPages/AccessDenied.html");
            //}
        }
    }
}