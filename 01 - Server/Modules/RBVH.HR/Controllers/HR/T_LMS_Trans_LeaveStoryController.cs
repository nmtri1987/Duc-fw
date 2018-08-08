using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

using Biz.TMS.Models;
namespace Biz.TMS.Controllers
{
    /// <summary>
    ///
    /// </summary>
    /// <seealso cref="System.Web.Http.ApiController" />
    public class T_LMS_Trans_LeaveStoryController : ApiController
    {
        // GET api/<controller>
        /// <summary>
        /// Gets this instance.
        /// </summary>
        /// <returns></returns>
        public T_LMS_Trans_LeaveStoryCollection Get()
        {
            return T_LMS_Trans_LeaveStoryManager.GetAllItem();
        }

		public T_LMS_Trans_LeaveStoryCollection GetbyUser(string usr)
        {
            return T_LMS_Trans_LeaveStoryManager.GetbyUser(usr);
        }

        public LeaveWFCollection Get(int EntityID, int EmployeeCode, DateTime WorkDate)
        {
            LeaveWFPara Filter = new LeaveWFPara()
            {
                EntityID = EntityID,
                EmployeeCode = EmployeeCode,
                WorkDate = WorkDate
            };
            return T_LMS_Trans_LeaveStoryManager.GetLeaveReason(Filter);
        }

        public LeaveWFCollection Get(LeaveWFPara Filter)
        {
            return T_LMS_Trans_LeaveStoryManager.GetLeaveReason(Filter);
        }

        // GET api/<controller>/5
        /// <summary>
        /// Gets the specified COM group identifier.
        /// </summary>
        /// <param name="T_LMS_Trans_LeaveStoryId">The COM group identifier.</param>
        /// <returns></returns>
        public T_LMS_Trans_LeaveStory Get(int Id)
        {
            return T_LMS_Trans_LeaveStoryManager.GetItemByID(Id);
        }

        // POST api/<controller>
        /// <summary>
        /// Posts the specified value.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        public T_LMS_Trans_LeaveStory Post([FromBody]T_LMS_Trans_LeaveStory value)
        {
            return T_LMS_Trans_LeaveStoryManager.AddItem(value);
        }

        // PUT api/<controller>/5
        /// <summary>
        /// Puts the specified identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        /// <exception cref="HttpResponseException"></exception>
        public T_LMS_Trans_LeaveStory Put(string id, [FromBody]T_LMS_Trans_LeaveStory value)
        {
            return T_LMS_Trans_LeaveStoryManager.UpdateItem(value);
        }

		// GET api/<controller>
        /// <summary>
        /// Gets this instance.
        /// </summary>
        /// <returns></returns>
        public T_LMS_Trans_LeaveStoryCollection Post(string method,[FromBody] SearchFilter value)
        {
            return T_LMS_Trans_LeaveStoryManager.Search(value);
        }

        // DELETE api/<controller>/5
        /// <summary>
        /// Deletes the specified identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        public void Delete(int id)
        {
            T_LMS_Trans_LeaveStoryManager.DeleteItem(id);
        }
    }
}