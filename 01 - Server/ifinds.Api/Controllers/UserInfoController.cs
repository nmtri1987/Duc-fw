using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using DTP.Object;
using ifinds.Api.Models;
using Microsoft.AspNet.Identity;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity.Owin;

namespace ifinds.Api.Controllers
{
    public class UserInfoController : ApiController
    {
        private ApplicationUserManager _userManager;
        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? Request.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }
        // GET api/<controller>/
        public UserInfoCollection Get(int CompanyID)
        {
            return UserInfoManager.GetAllItem(CompanyID);
        }

        public UserInfoCollection GetbyEmpCD(int CompanyID,string EmpCD)
        {
            return UserInfoManager.GetItembyEmpCD(CompanyID, EmpCD);
        }

        // GET api/<controller>/5
        public UserInfo Get(string UserId, int CompanyID)
        {
            return UserInfoManager.GetItemByID(UserId,CompanyID);
        }

        // GET api/<controller>/5
        public UserInfo Get(string username, int CompanyID, string action)
        {
            return UserInfoManager.GetItemByUserName(username,CompanyID);
        }

        public async Task<bool> Get(string userName,string password, int CompanyID, string action)
        {
            var user = new ApplicationUser();
            user =  UserManager.FindByName(userName+"_"+CompanyID);
           //UserManager.RemovePassword(user.Id);
            user.PasswordHash = UserManager.PasswordHasher.HashPassword(password);
            var result = await UserManager.UpdateAsync(user);
            //IdentityResult result  =  await UserManager.AddPasswordAsync(user.Id,password);
        
            if (result.Succeeded)
            {
                return true;
            }

            return false;
        }

        public async Task<UserInfo> Post([FromBody]UserInfo value)
        {
            var user = new ApplicationUser() { UserName = value.UserName+"_"+value.CompanyID, Email = value.userLogin.Email + "_" + value.CompanyID };
            IdentityResult result = await UserManager.CreateAsync(user, value.userLogin.Password);
            //user = await UserManager.FindByNameAsync(value.UserName);
            if (result.Succeeded)
            {
                return UserInfoManager.AddItem(value);
            }

            return value;
        }
        // PUT api/<controller>/5
        /// <summary>
        /// Puts the specified identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        /// <exception cref="HttpResponseException"></exception>
        public UserInfo Put(string id, [FromBody]UserInfo value)
        {
            return UserInfoManager.UpdateItem(value);
        }

        // DELETE api/<controller>/5
        /// <summary>
        /// Deletes the specified identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        public void Delete(String id,int CompanyID)
        {
            UserInfoManager.DeleteItem(id, CompanyID);
        }

        // GET api/<controller>
        /// <summary>
        /// Gets this instance.
        /// </summary>
        /// <returns></returns>
        public UserInfoCollection Post(string method, [FromBody]SearchFilter value)
        {
            return UserInfoManager.Search(value);
        }

    }
}