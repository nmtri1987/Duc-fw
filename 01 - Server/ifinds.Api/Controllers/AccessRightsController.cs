using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using DTP.Object;
namespace ifinds.Api.Controllers
{
    /// <summary>
    ///
    /// </summary>
    /// <seealso cref="System.Web.Http.ApiController" />
    public class AccessRightsController : ApiController
    {
        // GET api/<controller>
        /// <summary>
        /// Gets this instance.
        /// </summary>
        /// <returns></returns>
        public AccessRightsCollection Get(int CompanyID)
        {
            return AccessRightsManager.GetAllItem(CompanyID);
        }

		public AccessRightsCollection GetbyUser(string usr)
        {
            return AccessRightsManager.GetbyUser(usr);
        }

		 

		

        // GET api/<controller>/5
        /// <summary>
        /// Gets the specified COM group identifier.
        /// </summary>
        /// <param name="AccessRightsId">The COM group identifier.</param>
        /// <returns></returns>
        public AccessRights Get(String RoleName,int CompanyID)
        {
            return new AccessRights();
        }

     

        // POST api/<controller>
        /// <summary>
        /// Posts the specified value.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        public AccessRights Post([FromBody]AccessRights value)
        {
            return AccessRightsManager.InsertOrUpdateItem(value);
        }

        // PUT api/<controller>/5
        /// <summary>
        /// Puts the specified identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        /// <exception cref="HttpResponseException"></exception>
        public AccessRights Put(string id, [FromBody]AccessRights value)
        {
            return AccessRightsManager.UpdateItem(value);
        }

		// GET api/<controller>
        /// <summary>
        /// Gets this instance.
        /// </summary>
        /// <returns></returns>
        public AccessRightsCollection PutSearch(string method,[FromBody] SearchFilter value)
        {
            return AccessRightsManager.Search(value);
        }

        public AccessRightsCollection Get(string roleName, string applicationName,string nodeParentID,int companyID)
        {
            return AccessRightsManager.GetbyNoteParent(roleName,applicationName,nodeParentID,companyID);
        }
        // DELETE api/<controller>/5
        /// <summary>
        /// Deletes the specified identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        public void Delete(String id,int CompanyID)
        {
            AccessRightsManager.DeleteItem(id,CompanyID);
        }
    }
}