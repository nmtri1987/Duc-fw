using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
//using DTP.Data;
//using ifinds.Object.CS.Models;
using ifinds.Object.CS;
using DTP.Object;

namespace ifinds.Object.CS.Controllers
{
    /// <summary>
    ///
    /// </summary>
    /// <seealso cref="System.Web.Http.ApiController" />
    public class CompanyTreeMemberController : ApiController
    {
        // GET api/<controller>
        /// <summary>
        /// Gets this instance.
        /// </summary>
        /// <returns></returns>
        public CompanyTreeMemberCollection Get(int CompanyID,int WorkGroupID,string GetType, string usr)
        {
            switch (GetType)
            {
                case "getall":
                    return CompanyTreeMemberManager.GetAllItem(CompanyID, WorkGroupID);
                default:
                    return CompanyTreeMemberManager.GetbyUser(usr,CompanyID,WorkGroupID);
            }
            
        }




        // GET api/<controller>/5
        /// <summary>
        /// Gets the specified COM group identifier.
        /// </summary>
        /// <param name="CompanyID">The COM group identifier.</param>
        ///  <param name="UserID">The COM group identifier.</param>
        ///   <param name="WorkGroupID">The COM group identifier.</param>
        /// <returns></returns>
        public CompanyTreeMember Get(int WorkGroupID,int CompanyID, string EmployeeID)
        {
            return CompanyTreeMemberManager.GetItemByID(WorkGroupID,CompanyID, EmployeeID);
        }

        // POST api/<controller>
        /// <summary>
        /// Posts the specified value.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        public CompanyTreeMember Post([FromBody]CompanyTreeMember value)
        {
            return CompanyTreeMemberManager.AddItem(value);
        }

        // PUT api/<controller>/5
        /// <summary>
        /// Puts the specified identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        /// <exception cref="HttpResponseException"></exception>
        public CompanyTreeMember Put(string id, [FromBody]CompanyTreeMember value)
        {
            return CompanyTreeMemberManager.UpdateItem(value);
        }

		// GET api/<controller>
        /// <summary>
        /// Gets this instance.
        /// </summary>
        /// <returns></returns>
        public CompanyTreeMemberCollection PutSearch(string method,[FromBody] SearchFilter value)
        {
            return CompanyTreeMemberManager.Search(value);
        }

        // DELETE api/<controller>/5
        /// <summary>
        /// Deletes the specified identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        public void Delete(Int32 id,int CompanyID,string EmployeeID)
        {
            CompanyTreeMemberManager.DeleteItem(id,CompanyID, EmployeeID);
        }
    }
}