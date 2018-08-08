using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.SessionState;
using System.Net;
using Biz.Core.Helpers;
using System.Web.Security;
using System.Configuration;
using Biz.Core.Security;


namespace Biz.Core.Models
{
    public class UserModel
    {
        public static AuthenticatedUser objUser
        {
            get
            {
                LoginViewModel usr = new LoginViewModel();
                if (HttpContext.Current.Session[SystemConfig.loginKey] != null)
                {
                    return HttpContext.Current.Session[SystemConfig.loginKey] as AuthenticatedUser;
                }
                else if (HttpContext.Current.Request.Cookies["user"] != null)
                {
                    HttpCookie myCookie = HttpContext.Current.Request.Cookies["user"];
                    usr.UsernameOrEmail = myCookie["email"].ToString();
                    usr.Password = myCookie["pass"].ToString();
                    HttpStatusCode result = SessionHelper.Login(usr);
                    if (result == HttpStatusCode.OK)
                    {
                        return HttpContext.Current.Session[SystemConfig.loginKey] as AuthenticatedUser;
                    }
                }
                return null;
            }
        }
        public static string CreatePasswordHash(string Password, string Salt)
        {
            //MD5, SHA1
            string passwordFormat = "18HC72@";
            if (String.IsNullOrEmpty(passwordFormat))
                passwordFormat = "SHA1";

            return FormsAuthentication.HashPasswordForStoringInConfigFile(Password + Salt, passwordFormat);
        }
    }

    public class LoginViewModel
    {
        [Required]
        [Display(Name = "Email")]
        public string UsernameOrEmail { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [MinLength(6)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [Display(Name = "Remember me?")]
        public bool RememberMe { get; set; }

        [Required]
        [Display(Name = "Company")]
        public int Company { get; set; }
    }

    public class RegisterViewModel
    {
        [Required]
        [Display(Name = "Username")]
        [StringLength(50, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        public string Username { get; set; }

        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }
    }

    //public class AuthenticatedUser
    //{
    //    public string AccessToken { get; set; }
    //    public string TokenType { get; set; }
    //    public int ExpiresIn { get; set; }
    //    public string UserName { get; set; }
    //    public ifinds.ClientData.Models.UserInfo userInfo
    //    {
    //        get
    //        {
    //            return ifinds.ClientData.Services.UserInfoManager.GetById(UserName);
    //        }
    //    }
    //}
}