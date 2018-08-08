using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
//using DTP.Data;
//using ifinds.Object.OG.Models;
using ifinds.Object.OG;
namespace ifinds.Object.OG.Controllers
{
    /// <summary>
    ///
    /// </summary>
    /// <seealso cref="System.Web.Http.ApiController" />
    public class EPEmployeeProjectController : ApiController
    {
        // GET api/<controller>
        /// <summary>
        /// Gets this instance.
        /// </summary>
        /// <returns></returns>
        public EPEmployeeProjectCollection Get(int CompanyID)
        {
            return EPEmployeeProjectManager.GetAllItem(CompanyID);
        }
        public EPEmployeeProjectCollection Get(int CompanyID, string ProjectCD)
        {
            return EPEmployeeProjectManager.GetItemByProjectCD(ProjectCD, CompanyID);
        }
        public EPEmployeeProjectCollection GetbyUser(string usr)
        {
            return EPEmployeeProjectManager.GetbyUser(usr);
        }

		 

		

        // GET api/<controller>/5
        /// <summary>
        /// Gets the specified COM group identifier.
        /// </summary>
        /// <param name="EPEmployeeProjectId">The COM group identifier.</param>
        /// <returns></returns>
        public EPEmployeeProject Get(String ProjectCD,int CompanyID,string EmployeeCD)
        {
            return EPEmployeeProjectManager.GetItemByID(ProjectCD,CompanyID, EmployeeCD);
        }

        // POST api/<controller>
        /// <summary>
        /// Posts the specified value.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        public EPEmployeeProject Post([FromBody]EPEmployeeProject value)
        {
            return EPEmployeeProjectManager.AddItem(value);
        }

        // PUT api/<controller>/5
        /// <summary>
        /// Puts the specified identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        /// <exception cref="HttpResponseException"></exception>
        public EPEmployeeProject Put(string id, [FromBody]EPEmployeeProject value)
        {
            return EPEmployeeProjectManager.UpdateItem(value);
        }

		// GET api/<controller>
        /// <summary>
        /// Gets this instance.
        /// </summary>
        /// <returns></returns>
        public EPEmployeeProjectCollection PutSearch(string method,[FromBody] SearchFilter value)
        {
            return EPEmployeeProjectManager.Search(value);
        }

        // DELETE api/<controller>/5
        /// <summary>
        /// Deletes the specified identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        public void Delete(String id,int CompanyID)
        {
          //  EPEmployeeProjectManager.DeleteItem(id,CompanyID);
        }
    }
}