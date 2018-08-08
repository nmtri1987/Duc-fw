//using ifinds.Api.Models;
//using ifinds.Api.Providers;
//using ifinds.Api.Results;
//using Microsoft.AspNet.Identity;
//using Microsoft.AspNet.Identity.EntityFramework;
//using Microsoft.AspNet.Identity.Owin;
//using Microsoft.Owin.Security;
//using Microsoft.Owin.Security.Cookies;
//using Microsoft.Owin.Security.OAuth;
//using System;
//using System.Collections.Generic;
//using System.Net.Http;
//using System.Security.Claims;
//using System.Security.Cryptography;
//using System.Threading.Tasks;
//using System.Web;
//using System.Web.Http;
//using System.Web.Http.ModelBinding;
////using DTP.Object;
//namespace ifinds.Api.Controllers
//{
//    /// <summary>
//    ///
//    /// </summary>
//    /// <seealso cref="System.Web.Http.ApiController" />
//    [Authorize]
//    [RoutePrefix("api/Account")]
//    public class AccountController : ApiController
//    {
//        /// <summary>
//        /// The local login provider
//        /// </summary>
//        private const string LocalLoginProvider = "Local";

//        /// <summary>
//        /// The _user manager
//        /// </summary>
//        private ApplicationUserManager _userManager;

//        /// <summary>
//        /// Initializes a new instance of the <see cref="AccountController"/> class.
//        /// </summary>
//        public AccountController()
//        {
//        }

//        /// <summary>
//        /// Initializes a new instance of the <see cref="AccountController"/> class.
//        /// </summary>
//        /// <param name="userManager">The user manager.</param>
//        /// <param name="accessTokenFormat">The access token format.</param>
//        public AccountController(ApplicationUserManager userManager,
//            ISecureDataFormat<AuthenticationTicket> accessTokenFormat)
//        {
//            UserManager = userManager;
//            AccessTokenFormat = accessTokenFormat;
//        }

//        /// <summary>
//        /// Gets the user manager.
//        /// </summary>
//        /// <value>
//        /// The user manager.
//        /// </value>
//        public ApplicationUserManager UserManager
//        {
//            get
//            {
//                return _userManager ?? Request.GetOwinContext().GetUserManager<ApplicationUserManager>();
//            }
//            private set
//            {
//                _userManager = value;
//            }
//        }

//        /// <summary>
//        /// Gets the access token format.
//        /// </summary>
//        /// <value>
//        /// The access token format.
//        /// </value>
//        public ISecureDataFormat<AuthenticationTicket> AccessTokenFormat { get; private set; }

//        // GET api/Account/UserInfo
//        /// <summary>
//        /// Gets the user information.
//        /// </summary>
//        /// <returns></returns>
//        [HostAuthentication(DefaultAuthenticationTypes.ExternalBearer)]
//        [Route("UserInfo")]
//        public UserInfoViewModel GetUserInfo()
//        {
//            ExternalLoginData externalLogin = ExternalLoginData.FromIdentity(User.Identity as ClaimsIdentity);
//            var user = UserManager.FindById(User.Identity.GetUserId());

//            return new UserInfoViewModel
//            {
//                Email = user.Email,
//                HasRegistered = externalLogin == null,
//                LoginProvider = externalLogin != null ? externalLogin.LoginProvider : null
//            };
//        }

//        // POST api/Account/Logout
//        /// <summary>
//        /// Logouts this instance.
//        /// </summary>
//        /// <returns></returns>
//        [Route("Logout")]
//        public IHttpActionResult Logout()
//        {
//            Authentication.SignOut(CookieAuthenticationDefaults.AuthenticationType);
//            return Ok();
//        }

//        // GET api/Account/ManageInfo?returnUrl=%2F&generateState=true
//        /// <summary>
//        /// Gets the manage information.
//        /// </summary>
//        /// <param name="returnUrl">The return URL.</param>
//        /// <param name="generateState">if set to <c>true</c> [generate state].</param>
//        /// <returns></returns>
//        [Route("ManageInfo")]
//        public async Task<ManageInfoViewModel> GetManageInfo(string returnUrl, bool generateState = false)
//        {
//            IdentityUser user = await UserManager.FindByIdAsync(User.Identity.GetUserId());

//            if (user == null)
//            {
//                return null;
//            }

//            List<UserLoginInfoViewModel> logins = new List<UserLoginInfoViewModel>();

//            foreach (IdentityUserLogin linkedAccount in user.Logins)
//            {
//                logins.Add(new UserLoginInfoViewModel
//                {
//                    LoginProvider = linkedAccount.LoginProvider,
//                    ProviderKey = linkedAccount.ProviderKey
//                });
//            }

//            if (user.PasswordHash != null)
//            {
//                logins.Add(new UserLoginInfoViewModel
//                {
//                    LoginProvider = LocalLoginProvider,
//                    ProviderKey = user.UserName,
//                });
//            }

//            return new ManageInfoViewModel
//            {
//                UserName = user.UserName,
//                LocalLoginProvider = LocalLoginProvider,
//                Email = user.Email,
//                Logins = logins,
//                ExternalLoginProviders = GetExternalLogins(returnUrl, generateState)
//            };
//        }

//        // POST api/Account/ChangePassword
//        /// <summary>
//        /// Changes the password.
//        /// </summary>
//        /// <param name="model">The model.</param>
//        /// <returns></returns>
//        [Route("ChangePassword")]
//        public async Task<IHttpActionResult> ChangePassword(ChangePasswordBindingModel model)
//        {
//            if (!ModelState.IsValid)
//            {
//                return BadRequest(ModelState);
//            }

//            IdentityResult result = await UserManager.ChangePasswordAsync(User.Identity.GetUserId(), model.OldPassword,
//                model.NewPassword);

//            if (!result.Succeeded)
//            {
//                return GetErrorResult(result);
//            }

//            return Ok();
//        }

//        // POST api/Account/SetPassword
//        /// <summary>
//        /// Sets the password.
//        /// </summary>
//        /// <param name="model">The model.</param>
//        /// <returns></returns>
//        [Route("SetPassword")]
//        public async Task<IHttpActionResult> SetPassword(SetPasswordBindingModel model)
//        {
//            if (!ModelState.IsValid)
//            {
//                return BadRequest(ModelState);
//            }

//            IdentityResult result = await UserManager.AddPasswordAsync(User.Identity.GetUserId(), model.NewPassword);

//            if (!result.Succeeded)
//            {
//                return GetErrorResult(result);
//            }

//            return Ok();
//        }

//        // POST api/Account/AddExternalLogin
//        /// <summary>
//        /// Adds the external login.
//        /// </summary>
//        /// <param name="model">The model.</param>
//        /// <returns></returns>
//        [Route("AddExternalLogin")]
//        public async Task<IHttpActionResult> AddExternalLogin(AddExternalLoginBindingModel model)
//        {
//            if (!ModelState.IsValid)
//            {
//                return BadRequest(ModelState);
//            }

//            Authentication.SignOut(DefaultAuthenticationTypes.ExternalCookie);

//            AuthenticationTicket ticket = AccessTokenFormat.Unprotect(model.ExternalAccessToken);

//            if (ticket == null || ticket.Identity == null || (ticket.Properties != null
//                && ticket.Properties.ExpiresUtc.HasValue
//                && ticket.Properties.ExpiresUtc.Value < DateTimeOffset.UtcNow))
//            {
//                return BadRequest("External login failure.");
//            }

//            ExternalLoginData externalData = ExternalLoginData.FromIdentity(ticket.Identity);

//            if (externalData == null)
//            {
//                return BadRequest("The external login is already associated with an account.");
//            }

//            IdentityResult result = await UserManager.AddLoginAsync(User.Identity.GetUserId(),
//                new UserLoginInfo(externalData.LoginProvider, externalData.ProviderKey));

//            if (!result.Succeeded)
//            {
//                return GetErrorResult(result);
//            }

//            return Ok();
//        }

//        // POST api/Account/RemoveLogin
//        /// <summary>
//        /// Removes the login.
//        /// </summary>
//        /// <param name="model">The model.</param>
//        /// <returns></returns>
//        [Route("RemoveLogin")]
//        public async Task<IHttpActionResult> RemoveLogin(RemoveLoginBindingModel model)
//        {
//            if (!ModelState.IsValid)
//            {
//                return BadRequest(ModelState);
//            }

//            IdentityResult result;

//            if (model.LoginProvider == LocalLoginProvider)
//            {
//                result = await UserManager.RemovePasswordAsync(User.Identity.GetUserId());
//            }
//            else
//            {
//                result = await UserManager.RemoveLoginAsync(User.Identity.GetUserId(),
//                    new UserLoginInfo(model.LoginProvider, model.ProviderKey));
//            }

//            if (!result.Succeeded)
//            {
//                return GetErrorResult(result);
//            }

//            return Ok();
//        }

//        // GET api/Account/ExternalLogin
//        /// <summary>
//        /// Gets the external login.
//        /// </summary>
//        /// <param name="provider">The provider.</param>
//        /// <param name="error">The error.</param>
//        /// <returns></returns>
//        [OverrideAuthentication]
//        [HostAuthentication(DefaultAuthenticationTypes.ExternalCookie)]
//        [AllowAnonymous]
//        [Route("ExternalLogin", Name = "ExternalLogin")]
//        public async Task<IHttpActionResult> GetExternalLogin(string provider, string error = null)
//        {
//            if (error != null)
//            {
//                return Redirect(Url.Content("~/") + "#error=" + Uri.EscapeDataString(error));
//            }

//            if (!User.Identity.IsAuthenticated)
//            {
//                return new ChallengeResult(provider, this);
//            }

//            ExternalLoginData externalLogin = ExternalLoginData.FromIdentity(User.Identity as ClaimsIdentity);

//            if (externalLogin == null)
//            {
//                return InternalServerError();
//            }

//            if (externalLogin.LoginProvider != provider)
//            {
//                Authentication.SignOut(DefaultAuthenticationTypes.ExternalCookie);
//                return new ChallengeResult(provider, this);
//            }

//            ApplicationUser user = await UserManager.FindAsync(new UserLoginInfo(externalLogin.LoginProvider,
//                externalLogin.ProviderKey));

//            bool hasRegistered = user != null;

//            if (hasRegistered)
//            {
//                Authentication.SignOut(DefaultAuthenticationTypes.ExternalCookie);

//                ClaimsIdentity oAuthIdentity = await user.GenerateUserIdentityAsync(UserManager,
//                   OAuthDefaults.AuthenticationType);
//                ClaimsIdentity cookieIdentity = await user.GenerateUserIdentityAsync(UserManager,
//                    CookieAuthenticationDefaults.AuthenticationType);

//                AuthenticationProperties properties = ApplicationOAuthProvider.CreateProperties(user.UserName);
//                Authentication.SignIn(properties, oAuthIdentity, cookieIdentity);
//            }
//            else
//            {
//                IEnumerable<Claim> claims = externalLogin.GetClaims();
//                ClaimsIdentity identity = new ClaimsIdentity(claims, OAuthDefaults.AuthenticationType);
//                Authentication.SignIn(identity);
//            }

//            return Ok();
//        }

//        // GET api/Account/ExternalLogins?returnUrl=%2F&generateState=true
//        /// <summary>
//        /// Gets the external logins.
//        /// </summary>
//        /// <param name="returnUrl">The return URL.</param>
//        /// <param name="generateState">if set to <c>true</c> [generate state].</param>
//        /// <returns></returns>
//        [AllowAnonymous]
//        [Route("ExternalLogins")]
//        public IEnumerable<ExternalLoginViewModel> GetExternalLogins(string returnUrl, bool generateState = false)
//        {
//            IEnumerable<AuthenticationDescription> descriptions = Authentication.GetExternalAuthenticationTypes();
//            List<ExternalLoginViewModel> logins = new List<ExternalLoginViewModel>();

//            string state;

//            if (generateState)
//            {
//                const int strengthInBits = 256;
//                state = RandomOAuthStateGenerator.Generate(strengthInBits);
//            }
//            else
//            {
//                state = null;
//            }

//            foreach (AuthenticationDescription description in descriptions)
//            {
//                ExternalLoginViewModel login = new ExternalLoginViewModel
//                {
//                    Name = description.Caption,
//                    Url = Url.Route("ExternalLogin", new
//                    {
//                        provider = description.AuthenticationType,
//                        response_type = "token",
//                        client_id = Startup.PublicClientId,
//                        redirect_uri = new Uri(Request.RequestUri, returnUrl).AbsoluteUri,
//                        state = state
//                    }),
//                    State = state
//                };
//                logins.Add(login);
//            }

//            return logins;
//        }

//        // POST api/Account/Register
//        /// <summary>
//        /// Registers the specified model.
//        /// </summary>
//        /// <param name="model">The model.</param>
//        /// <returns></returns>
//        [AllowAnonymous]
//        [Route("Register")]
//        public async Task<IHttpActionResult> Register(RegisterBindingModel model)
//        {
//            if (!ModelState.IsValid)
//            {
//                return BadRequest(ModelState);
//            }

//            var user = new ApplicationUser() { UserName = model.Username, Email = model.Email };

//            IdentityResult result = await UserManager.CreateAsync(user, model.Password);

//            if (!result.Succeeded)
//            {
//                return GetErrorResult(result);
//            }
//            else
//            {
//                //add user profile
//                UserInfo objItem = UserInfoManager.GetItemByUserName(model.Username,1);
//                objItem.BranchID = 1;//chinh
//                objItem.CompanyID = 1;//chinh
//                if (objItem != null)
//                {
//                    objItem = UserInfoManager.AddItem(new UserInfo()
//                    {
//                        UserName = model.Username
//                    });
//                }
//            }

//            return Ok();
//        }

//        // POST api/Account/RegisterExternal
//        /// <summary>
//        /// Registers the external.
//        /// </summary>
//        /// <param name="model">The model.</param>
//        /// <returns></returns>
//        [OverrideAuthentication]
//        [HostAuthentication(DefaultAuthenticationTypes.ExternalBearer)]
//        [Route("RegisterExternal")]
//        public async Task<IHttpActionResult> RegisterExternal(RegisterExternalBindingModel model)
//        {
//            if (!ModelState.IsValid)
//            {
//                return BadRequest(ModelState);
//            }

//            var info = await Authentication.GetExternalLoginInfoAsync();
//            if (info == null)
//            {
//                return InternalServerError();
//            }

//            var user = new ApplicationUser() { UserName = model.Username, Email = model.Email };

//            IdentityResult result = await UserManager.CreateAsync(user);
//            if (!result.Succeeded)
//            {
//                return GetErrorResult(result);
//            }

//            result = await UserManager.AddLoginAsync(user.Id, info.Login);
//            if (!result.Succeeded)
//            {
//                return GetErrorResult(result);
//            }
//            return Ok();
//        }

//        /// <summary>
//        /// Releases the unmanaged resources that are used by the object and, optionally, releases the managed resources.
//        /// </summary>
//        /// <param name="disposing">true to release both managed and unmanaged resources; false to release only unmanaged resources.</param>
//        protected override void Dispose(bool disposing)
//        {
//            if (disposing && _userManager != null)
//            {
//                _userManager.Dispose();
//                _userManager = null;
//            }

//            base.Dispose(disposing);
//        }

//        #region Helpers

//        /// <summary>
//        /// Gets the authentication.
//        /// </summary>
//        /// <value>
//        /// The authentication.
//        /// </value>
//        private IAuthenticationManager Authentication
//        {
//            get { return Request.GetOwinContext().Authentication; }
//        }

//        /// <summary>
//        /// Gets the error result.
//        /// </summary>
//        /// <param name="result">The result.</param>
//        /// <returns></returns>
//        private IHttpActionResult GetErrorResult(IdentityResult result)
//        {
//            if (result == null)
//            {
//                return InternalServerError();
//            }

//            if (!result.Succeeded)
//            {
//                if (result.Errors != null)
//                {
//                    foreach (string error in result.Errors)
//                    {
//                        ModelState.AddModelError("", error);
//                    }
//                }

//                if (ModelState.IsValid)
//                {
//                    // No ModelState errors are available to send, so just return an empty BadRequest.
//                    return BadRequest();
//                }

//                return BadRequest(ModelState);
//            }

//            return null;
//        }

//        /// <summary>
//        ///
//        /// </summary>
//        private class ExternalLoginData
//        {
//            /// <summary>
//            /// Gets or sets the login provider.
//            /// </summary>
//            /// <value>
//            /// The login provider.
//            /// </value>
//            public string LoginProvider { get; set; }

//            /// <summary>
//            /// Gets or sets the provider key.
//            /// </summary>
//            /// <value>
//            /// The provider key.
//            /// </value>
//            public string ProviderKey { get; set; }

//            /// <summary>
//            /// Gets or sets the name of the user.
//            /// </summary>
//            /// <value>
//            /// The name of the user.
//            /// </value>
//            public string UserName { get; set; }

//            /// <summary>
//            /// Gets the claims.
//            /// </summary>
//            /// <returns></returns>
//            public IList<Claim> GetClaims()
//            {
//                IList<Claim> claims = new List<Claim>();
//                claims.Add(new Claim(ClaimTypes.NameIdentifier, ProviderKey, null, LoginProvider));

//                if (UserName != null)
//                {
//                    claims.Add(new Claim(ClaimTypes.Name, UserName, null, LoginProvider));
//                }

//                return claims;
//            }

//            /// <summary>
//            /// Froms the identity.
//            /// </summary>
//            /// <param name="identity">The identity.</param>
//            /// <returns></returns>
//            public static ExternalLoginData FromIdentity(ClaimsIdentity identity)
//            {
//                if (identity == null)
//                {
//                    return null;
//                }

//                Claim providerKeyClaim = identity.FindFirst(ClaimTypes.NameIdentifier);

//                if (providerKeyClaim == null || String.IsNullOrEmpty(providerKeyClaim.Issuer)
//                    || String.IsNullOrEmpty(providerKeyClaim.Value))
//                {
//                    return null;
//                }

//                if (providerKeyClaim.Issuer == ClaimsIdentity.DefaultIssuer)
//                {
//                    return null;
//                }

//                return new ExternalLoginData
//                {
//                    LoginProvider = providerKeyClaim.Issuer,
//                    ProviderKey = providerKeyClaim.Value,
//                    UserName = identity.FindFirstValue(ClaimTypes.Name)
//                };
//            }
//        }

//        /// <summary>
//        ///
//        /// </summary>
//        private static class RandomOAuthStateGenerator
//        {
//            /// <summary>
//            /// The _random
//            /// </summary>
//            private static RandomNumberGenerator _random = new RNGCryptoServiceProvider();

//            /// <summary>
//            /// Generates the specified strength in bits.
//            /// </summary>
//            /// <param name="strengthInBits">The strength in bits.</param>
//            /// <returns></returns>
//            /// <exception cref="System.ArgumentException">strengthInBits must be evenly divisible by 8.;strengthInBits</exception>
//            public static string Generate(int strengthInBits)
//            {
//                const int bitsPerByte = 8;

//                if (strengthInBits % bitsPerByte != 0)
//                {
//                    throw new ArgumentException("strengthInBits must be evenly divisible by 8.", "strengthInBits");
//                }

//                int strengthInBytes = strengthInBits / bitsPerByte;

//                byte[] data = new byte[strengthInBytes];
//                _random.GetBytes(data);
//                return HttpServerUtility.UrlTokenEncode(data);
//            }
//        }

//        #endregion Helpers
//    }
//}