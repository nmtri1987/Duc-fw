using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

using Biz.OG.Models;
namespace Biz.OG.Controllers
{
    /// <summary>
    ///
    /// </summary>
    /// <seealso cref="System.Web.Http.ApiController" />
    public class T_LMS_Master_AnnualLeaveController : ApiController
    {
        // GET api/<controller>
        /// <summary>
        /// Gets this instance.
        /// </summary>
        /// <returns></returns>
        public T_LMS_Master_AnnualLeaveCollection Get()
        {
            return T_LMS_Master_AnnualLeaveManager.GetAllItem();
        }

		public T_LMS_Master_AnnualLeaveCollection GetbyUser(string usr)
        {
            return T_LMS_Master_AnnualLeaveManager.GetbyUser(usr);
        }

		 

		

        // GET api/<controller>/5
        /// <summary>
        /// Gets the specified COM group identifier.
        /// </summary>
        /// <param name="T_LMS_Master_AnnualLeaveId">The COM group identifier.</param>
        /// <returns></returns>
        public T_LMS_Master_AnnualLeave Get(int ID)
        {
            return T_LMS_Master_AnnualLeaveManager.GetItemByID(ID);
        }

        // POST api/<controller>
        /// <summary>
        /// Posts the specified value.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        public T_LMS_Master_AnnualLeave Post([FromBody]T_LMS_Master_AnnualLeave value)
        {
            return T_LMS_Master_AnnualLeaveManager.AddItem(value);
        }

        // PUT api/<controller>/5
        /// <summary>
        /// Puts the specified identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        /// <exception cref="HttpResponseException"></exception>
        public T_LMS_Master_AnnualLeave Put(string id, [FromBody]T_LMS_Master_AnnualLeave value)
        {
            return T_LMS_Master_AnnualLeaveManager.UpdateItem(value);
        }

		// GET api/<controller>
        /// <summary>
        /// Gets this instance.
        /// </summary>
        /// <returns></returns>
        public T_LMS_Master_AnnualLeaveCollection Post(string method,[FromBody] SearchFilter value)
        {
            return T_LMS_Master_AnnualLeaveManager.Search(value);
        }

        // DELETE api/<controller>/5
        /// <summary>
        /// Deletes the specified identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        public void Delete(int id)
        {
            T_LMS_Master_AnnualLeaveManager.DeleteItem(id);
        }
    }
}