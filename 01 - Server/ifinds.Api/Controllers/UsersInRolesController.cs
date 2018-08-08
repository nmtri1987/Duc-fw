using DTP.Object;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
//using DTP.Object;
//using ifinds.Object.CS.Models;
namespace ifinds.Api.Controllers
{
    /// <summary>
    ///
    /// </summary>
    /// <seealso cref="System.Web.Http.ApiController" />
    public class UsersInRolesController : ApiController
    {
        // GET api/<controller>
        /// <summary>
        /// Gets this instance.
        /// </summary>
        /// <returns></returns>
        public UsersInRolesCollection Get(int CompanyID)
        {
            return UsersInRolesManager.GetAllItem(CompanyID);
        }

		public UsersInRolesCollection GetbyUser(string usr, int CompanyID)
        {
            return UsersInRolesManager.GetbyUser(usr, CompanyID);
        }


        public UsersInRolesCollection Get(string usr,int companyID,string getBy)
        {
            switch (getBy)
            {
                case "username":
                    return UsersInRolesManager.GetRolesbyUserName(usr, companyID);
                default:
                    return UsersInRolesManager.GetbyUser(usr,companyID);
            }
        }


        // GET api/<controller>/5
        /// <summary>
        /// Gets the specified COM group identifier.
        /// </summary>
        /// <param name="UsersInRolesId">The COM group identifier.</param>
        /// <returns></returns>
        public UsersInRoles Get(String Username, int CompanyID)
        {
            return UsersInRolesManager.GetItemByID(Username,CompanyID);
        }

        // POST api/<controller>
        /// <summary>
        /// Posts the specified value.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        public UsersInRoles Post([FromBody]UsersInRoles value)
        {
            return UsersInRolesManager.AddItem(value);
        }

        // PUT api/<controller>/5
        /// <summary>
        /// Puts the specified identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        /// <exception cref="HttpResponseException"></exception>
        public UsersInRoles Put(string id, [FromBody]UsersInRoles value)
        {
            return UsersInRolesManager.UpdateItem(value);
        }

		// GET api/<controller>
        /// <summary>
        /// Gets this instance.
        /// </summary>
        /// <returns></returns>
        public UsersInRolesCollection PutSearch(string method,[FromBody] SearchFilter value)
        {
            return UsersInRolesManager.Search(value);
        }

        // DELETE api/<controller>/5
        /// <summary>
        /// Deletes the specified identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        public void Delete(String id)
        {
            UsersInRolesManager.DeleteItem(id);
        }

        public void Delete(string userName, int CompanyID, string deleteby)
        {
            UsersInRolesManager.DeleteItemByUsername(userName,CompanyID);
        }
    }
}