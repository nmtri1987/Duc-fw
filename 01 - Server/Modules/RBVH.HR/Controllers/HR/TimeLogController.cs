
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;

using System.Web.Http;

namespace BRVH.HR.OG.Controllers
{
    /// <summary>
    ///
    /// </summary>
    /// <seealso cref="System.Web.Http.ApiController" />
    public class TimeLogController : ApiController
    {
        // GET api/<controller>
        /// <summary>
        /// Gets this instance.
        /// </summary>
        /// <returns></returns>
        //public TimeLogCollection Get()
        //{
        //    return TimeLogManager.GetAllItem();
        //}

		public TimeLogCollection GetbyUser(string usr)
        {
            return TimeLogManager.GetbyUser(usr);
        }

		 

		

        // GET api/<controller>/5
        /// <summary>
        /// Gets the specified COM group identifier.
        /// </summary>
        /// <param name="TimeLogId">The COM group identifier.</param>
        /// <returns></returns>
        public TimeLog Get(int TimeLogId)
        {
            return TimeLogManager.GetItemByID(TimeLogId);
        }

        // POST api/<controller>
        /// <summary>
        /// Posts the specified value.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        public TimeLog Post([FromBody]TimeLog value)
        {
            return TimeLogManager.AddItem(value);
        }

        // PUT api/<controller>/5
        /// <summary>
        /// Puts the specified identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        /// <exception cref="HttpResponseException"></exception>
        public TimeLog Put(string id, [FromBody]TimeLog value)
        {
            return TimeLogManager.UpdateItem(value);
        }

		// GET api/<controller>
        /// <summary>
        /// Gets this instance.
        /// </summary>
        /// <returns></returns>
        public TimeLogCollection Post(string method,[FromBody] SearchFilter value)
        {
            return TimeLogManager.Search(value);
        }

        // DELETE api/<controller>/5
        /// <summary>
        /// Deletes the specified identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        public void Delete(int id)
        {
            TimeLogManager.DeleteItem(id);
        }
    }
}