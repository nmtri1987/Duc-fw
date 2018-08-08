
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
    public class T_CMS_InterfaceLacviet_WorkingHoursController : ApiController
    {
        // GET api/<controller>
        /// <summary>
        /// Gets this instance.
        /// </summary>
        /// <returns></returns>
        public T_CMS_InterfaceLacviet_WorkingHoursCollection Get()
        {
            return T_CMS_InterfaceLacviet_WorkingHoursManager.GetAllItem();
        }

		public T_CMS_InterfaceLacviet_WorkingHoursCollection GetbyUser(string usr)
        {
            return T_CMS_InterfaceLacviet_WorkingHoursManager.GetbyUser(usr);
        }

		 

		

        // GET api/<controller>/5
        /// <summary>
        /// Gets the specified COM group identifier.
        /// </summary>
        /// <param name="T_CMS_InterfaceLacviet_WorkingHoursId">The COM group identifier.</param>
        /// <returns></returns>
        public T_CMS_InterfaceLacviet_WorkingHours Get(int WorkingId)
        {
            return T_CMS_InterfaceLacviet_WorkingHoursManager.GetItemByID(WorkingId);
        }

        // POST api/<controller>
        /// <summary>
        /// Posts the specified value.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        public T_CMS_InterfaceLacviet_WorkingHours Post([FromBody]T_CMS_InterfaceLacviet_WorkingHours value)
        {
            return T_CMS_InterfaceLacviet_WorkingHoursManager.AddItem(value);
        }

        // PUT api/<controller>/5
        /// <summary>
        /// Puts the specified identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        /// <exception cref="HttpResponseException"></exception>
        public T_CMS_InterfaceLacviet_WorkingHours Put(string id, [FromBody]T_CMS_InterfaceLacviet_WorkingHours value)
        {
            return T_CMS_InterfaceLacviet_WorkingHoursManager.UpdateItem(value);
        }

		// GET api/<controller>
        /// <summary>
        /// Gets this instance.
        /// </summary>
        /// <returns></returns>
        public T_CMS_InterfaceLacviet_WorkingHoursCollection Post(string method,[FromBody] SearchFilter value)
        {
            return T_CMS_InterfaceLacviet_WorkingHoursManager.Search(value);
        }

        // DELETE api/<controller>/5
        /// <summary>
        /// Deletes the specified identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        public void Delete(int id)
        {
            T_CMS_InterfaceLacviet_WorkingHoursManager.DeleteItem(id);
        }
    }
}