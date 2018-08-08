
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Threading;
using System.Net;
using Biz.Core.Security;
using Biz.Core.Services;
using Biz.Core.Models;
using Biz.OG.Models;
using Biz.OG.Services;
namespace Biz.Core.Models
{
    public class CustomWebViewPage<T> : WebViewPage<T>
    {

        public override void InitHelpers()
        {
            base.InitHelpers();
        }
        public string LayoutPage
        {
            get{
                //return "~/Views/Shared/_Layout.cshtml";
                return "~/Views/Shared/_Layout_V2.cshtml";

            }
        }
        public static DNHUsers CurrentUser
        {
            get
            {
                return Security.CustomerAuthorize.CurrentUser;
            }

        }


        public static string GetBaseUrl()
        {
            var request = HttpContext.Current.Request;
            var appUrl = HttpRuntime.AppDomainAppVirtualPath;

            if (appUrl != "/") appUrl += "/";

            var baseUrl = string.Format("{0}://{1}{2}", request.Url.Scheme, request.Url.Authority, appUrl);

            return baseUrl;
        }


        public static string DNHUrl(string url)
        {
            return GetBaseUrl() + url;
        }
      
       
        public const string SitemapTemplate = "<li class='{0}'><a href='{1}'>{2}</a></li>";
        public static string SiteMapBuilder { get; set; }
        public static string AddSiteMap(string strclass, string webUrl, string SiteMapTitle)
        {
            return string.Format(SitemapTemplate, strclass, webUrl, SiteMapTitle);
        }
        public string L(string ResourceKey)
        {
            return CommonHelper.L(ResourceKey, CurrentUser.CompanyID, CurrentUser.UserLanguageID);// Biz.CS.Services.DNHLocaleStringResourceManager.LResource(ResourceKey, CurrentUser.CompanyID, CurrentUser.UserLanguageID).ResourceValue;
        }
      
        public LanguageCollection getAllLanguages()
        {
            return LanguageManager.GetAll(CurrentUser.CompanyID);
        }
        
        public override void Execute()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        ///  Convert string to datetime
        /// </summary>
        /// <param name="anyString"></param>
        /// <param name="DateFirst">true : dd/mm/yyyy | False: mm/dd/yyyy</param>
        /// <returns></returns>
        public static DateTime ConvertDate(string anyString, bool DateFirst)
        {
            DateTime dummyDate = SystemConfig.CurrentDate;
            try
            {
                if (DateFirst)
                {
                    System.Globalization.CultureInfo ci = new System.Globalization.CultureInfo("en-GB");
                    dummyDate = Convert.ToDateTime(anyString, ci);
                    
                }
                else
                {
                    dummyDate = DateTime.Parse(anyString);
                }
            }
            catch (Exception objEx)
            {
            }
            return dummyDate;
        }
       
        public static string QueryString(string Name)
        {
            string result = string.Empty;
            if (HttpContext.Current != null && HttpContext.Current.Request.QueryString[Name] != null)
                result = HttpContext.Current.Request.QueryString[Name].ToString();
            return result;
        }
        public static int QueryInt(string Name)
        {
            int result = 0;
            if (HttpContext.Current != null && HttpContext.Current.Request.QueryString[Name] != null)
                result =int.Parse(HttpContext.Current.Request.QueryString[Name].ToString());
            return result;
        }
        public static bool QueryStringBool(string Name)
        {
            string resultStr = QueryString(Name).ToUpperInvariant();
            return (resultStr == "YES" || resultStr == "TRUE" || resultStr == "1");
        }
    }
}