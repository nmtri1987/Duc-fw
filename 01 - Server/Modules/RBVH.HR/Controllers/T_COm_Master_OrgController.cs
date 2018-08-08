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
    public class T_COm_Master_OrgController : ApiController
    {
        // GET api/<controller>
        /// <summary>
        /// Gets this instance.
        /// </summary>
        /// <returns></returns>
        public T_COm_Master_OrgCollection Get()
        {
            return T_COm_Master_OrgManager.GetAllItem();
        }

		public T_COm_Master_Org GetbyUser(string usr)
        {
            return T_COm_Master_OrgManager.GetItemByOrgName(usr);
        }

        // GET api/<controller>/5
        /// <summary>
        /// Gets the specified COM group identifier.
        /// </summary>
        /// <param name="T_COm_Master_OrgId">The COM group identifier.</param>
        /// <returns></returns>
        public T_COm_Master_Org Get(int OrgId)
        {
            return T_COm_Master_OrgManager.GetItemByID(OrgId);
        }

        // POST api/<controller>
        /// <summary>
        /// Posts the specified value.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        public T_COm_Master_Org Post([FromBody]T_COm_Master_Org value)
        {
            return T_COm_Master_OrgManager.AddItem(value);
        }

        // PUT api/<controller>/5
        /// <summary>
        /// Puts the specified identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        /// <exception cref="HttpResponseException"></exception>
        public T_COm_Master_Org Put(string id, [FromBody]T_COm_Master_Org value)
        {
            return T_COm_Master_OrgManager.UpdateItem(value);
        }

		// GET api/<controller>
        /// <summary>
        /// Gets this instance.
        /// </summary>
        /// <returns></returns>
        public T_COm_Master_OrgCollection Post(string method,[FromBody] SearchFilter value)
        {
            return T_COm_Master_OrgManager.Search(value);
        }

        // DELETE api/<controller>/5
        /// <summary>
        /// Deletes the specified identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        public void Delete(int id)
        {
            T_COm_Master_OrgManager.DeleteItem(id);
        }
    }
}