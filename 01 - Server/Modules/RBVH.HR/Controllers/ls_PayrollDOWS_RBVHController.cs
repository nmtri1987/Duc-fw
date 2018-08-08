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
    public class ls_PayrollDOWS_RBVHController : ApiController
    {
        // GET api/<controller>
        /// <summary>
        /// Gets this instance.
        /// </summary>
        /// <returns></returns>
        //public ls_PayrollDOWS_RBVHCollection Get()
        //{
        //    return ls_PayrollDOWS_RBVHManager.GetAllItemByEntity;
        //}

		public ls_PayrollDOWS_RBVHCollection GetbyUser(string usr)
        {
            return ls_PayrollDOWS_RBVHManager.GetbyUser(usr);
        }

		 

		

        // GET api/<controller>/5
        /// <summary>
        /// Gets the specified COM group identifier.
        /// </summary>
        /// <param name="ls_PayrollDOWS_RBVHId">The COM group identifier.</param>
        /// <returns></returns>
        public ls_PayrollDOWS_RBVHCollection Get(int ENtityID)
        {
            return ls_PayrollDOWS_RBVHManager.GetAllItemByEntity(ENtityID);
        }
        public ls_PayrollDOWS_RBVH Get(int id,string action)
        {
            return ls_PayrollDOWS_RBVHManager.GetItemByID(id);
        }
        // POST api/<controller>
        /// <summary>
        /// Posts the specified value.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        public ls_PayrollDOWS_RBVH Post([FromBody]ls_PayrollDOWS_RBVH value)
        {
            return ls_PayrollDOWS_RBVHManager.AddItem(value);
        }

        // PUT api/<controller>/5
        /// <summary>
        /// Puts the specified identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        /// <exception cref="HttpResponseException"></exception>
        public ls_PayrollDOWS_RBVH Put(string id, [FromBody]ls_PayrollDOWS_RBVH value)
        {
            return ls_PayrollDOWS_RBVHManager.UpdateItem(value);
        }

		// GET api/<controller>
        /// <summary>
        /// Gets this instance.
        /// </summary>
        /// <returns></returns>
        public ls_PayrollDOWS_RBVHCollection Post(string method,[FromBody] SearchFilter value)
        {
            return ls_PayrollDOWS_RBVHManager.Search(value);
        }

        // DELETE api/<controller>/5
        /// <summary>
        /// Deletes the specified identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        public void Delete(int id)
        {
            ls_PayrollDOWS_RBVHManager.DeleteItem(id);
        }
    }
}