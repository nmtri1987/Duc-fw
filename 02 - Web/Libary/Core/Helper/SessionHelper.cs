using Helpers;
//using ifinds.Client.Models;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Biz.Core.Security;

using Biz.Core.Models;
//using ifinds.ClientData.Services;

namespace Biz.Core.Helpers
{
    public class SessionHelper
    {
        public static string SessionEndpoint
        {
            get
            {
                return XMLHelper.WebApiReturnConfig(SystemConst.HouseBanking, true);
            }
        }

        public static string RegisterEndpoint
        {
            get
            {
                return XMLHelper.WebApiReturnConfig(SystemConst.HouseBanking);
            }
        }

        public static AuthenticatedUser GetAuthenticatedUser()
        {
            var user = HttpContext.Current.Session[SystemConfig.loginKey] as AuthenticatedUser;
            return user;
        }

        public static void SetRBVH(DNHUsers objUser)
        {
            HttpContext.Current.Session[SystemConfig.loginKey] = objUser;
        }
        public static DNHUsers CurrentUser
        {
            get {
                return Biz.Core.Security.CustomerAuthorize.RBVHUser();
            }
            set
            {
                HttpContext.Current.Session[SystemConfig.loginKey] = value;
            }
        }

        public static async Task<HttpStatusCode> PasswordSignInAsync(LoginViewModel model)
        {
            HttpResponseMessage response = null;
            using (var client = WebApiHelper.myclient(SessionEndpoint, ContentType.FormData))
            {
                string userName = model.UsernameOrEmail +"_"+ model.Company;
                StringBuilder sb = new StringBuilder();
                sb.AppendUrlEncoded("grant_type", "password", true);
                sb.AppendUrlEncoded("username", userName, true);
                sb.AppendUrlEncoded("password", model.Password);

                response = await client.PostAsync("Token", new StringContent(sb.ToString()));

                if (response.IsSuccessStatusCode)
                {
                    //get user tokken
                    JObject json = JObject.Parse(response.Content.ReadAsStringAsync().GetAwaiter().GetResult());
                    HttpContext.Current.Session["apiKey"] = json;
                    AuthenticatedUser user = new AuthenticatedUser();
                    string userNameLog = json["userName"].ToString();
                    user.AccessToken = json["access_token"].ToString();
                    user.UserName = userNameLog.Substring(0, userNameLog.Length - 2);
                    user.ExpiresIn = Convert.ToInt32(json["expires_in"].ToString());
                    user.TokenType = json["token_type"].ToString();

                    //start check user in company
                    //ifinds.Object.CS.Models.UserInfo userInfo = UserInfoManager.GetByUserName(user.UserName, model.Company);

                    //if (userInfo.UserId != null)
                    //{
                    //    user.CompanyID = userInfo.CompanyID;
                    //    user.isNonAutoLogout = userInfo.isNonAutoLogout;
                    //    user.isViewAllCompanyReport= userInfo.isViewAllCompanyReport;
                    //    ifinds.Object.OG.Models.EPEmployeeCollection objEmp = ifinds.Object.OG.Services.EPEmployeeManager.GetByUserID(user.UserName, userInfo.CompanyID);
                    //    if (objEmp.Count>0)
                    //    {
                    //        user.EmpCD = objEmp[0].EmpCD;
                    //    }
                    //    //user.BrandCD = userInfo.BranchID;
                    //    HttpContext.Current.Session[SystemConfig.loginKey] = user;
                            
                    //}
                    //else
                    //{
                    //    response.StatusCode = HttpStatusCode.Unauthorized;
                    //}
                    //end check user in company
                    //HttpContext.Current.Session[SystemConfig.loginKey] = user;
                }
            }
            return response.StatusCode;
        }

        public static HttpStatusCode Login(LoginViewModel model)
        {
            HttpResponseMessage response = null;
            using (var client = WebApiHelper.myclient(SessionEndpoint, ContentType.FormData))
            {
                StringBuilder sb = new StringBuilder();
                sb.AppendUrlEncoded("grant_type", "password", true);
                sb.AppendUrlEncoded("username", model.UsernameOrEmail, true);
                sb.AppendUrlEncoded("password", model.Password);

                response = client.PostAsync("Token", new StringContent(sb.ToString())).GetAwaiter().GetResult();
                if (response.IsSuccessStatusCode)
                {
                    JObject json = JObject.Parse(response.Content.ReadAsStringAsync().GetAwaiter().GetResult());
                    AuthenticatedUser user = new AuthenticatedUser();
                    user.AccessToken = json["access_token"].ToString();
                    user.UserName = json["userName"].ToString();
                    user.ExpiresIn = Convert.ToInt32(json["expires_in"].ToString());
                    user.TokenType = json["token_type"].ToString();
                    HttpContext.Current.Session[SystemConfig.loginKey] = user;
                }
            }
            return response.StatusCode; 
        }
        public static async Task<HttpStatusCode> CreateAsync(RegisterViewModel model)
        {
            HttpResponseMessage response = null;
            using (var client = WebApiHelper.myclient(RegisterEndpoint, SystemConst.APIJosonReturnValue))
            {
                response = await client.PostAsJsonAsync("Account/Register", model);

                if (response.IsSuccessStatusCode)
                {
                    return response.StatusCode;
                }
            }
            return response.StatusCode;
        }

        public static void SignOut()
        {
            HttpContext.Current.Session[SystemConfig.loginKey] = null;
          
        }
    }
}
