
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
    public class T_CMS_Master_SalutationController : ApiController
    {
        // GET api/<controller>
        /// <summary>
        /// Gets this instance.
        /// </summary>
        /// <returns></returns>
        public T_CMS_Master_SalutationCollection Get()
        {
            return T_CMS_Master_SalutationManager.GetAllItem();
        }

		public T_CMS_Master_SalutationCollection GetbyUser(string usr)
        {
            return T_CMS_Master_SalutationManager.GetbyUser(usr);
        }

		 

		

        // GET api/<controller>/5
        /// <summary>
        /// Gets the specified COM group identifier.
        /// </summary>
        /// <param name="T_CMS_Master_SalutationId">The COM group identifier.</param>
        /// <returns></returns>
        public T_CMS_Master_Salutation Get(int SalutationID)
        {
            return T_CMS_Master_SalutationManager.GetItemByID(SalutationID);
        }

        // POST api/<controller>
        /// <summary>
        /// Posts the specified value.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        public T_CMS_Master_Salutation Post([FromBody]T_CMS_Master_Salutation value)
        {
            return T_CMS_Master_SalutationManager.AddItem(value);
        }

        // PUT api/<controller>/5
        /// <summary>
        /// Puts the specified identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        /// <exception cref="HttpResponseException"></exception>
        public T_CMS_Master_Salutation Put(string id, [FromBody]T_CMS_Master_Salutation value)
        {
            return T_CMS_Master_SalutationManager.UpdateItem(value);
        }

		// GET api/<controller>
        /// <summary>
        /// Gets this instance.
        /// </summary>
        /// <returns></returns>
        public T_CMS_Master_SalutationCollection Post(string method,[FromBody] SearchFilter value)
        {
            return T_CMS_Master_SalutationManager.Search(value);
        }

        // DELETE api/<controller>/5
        /// <summary>
        /// Deletes the specified identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        public void Delete(int id)
        {
            T_CMS_Master_SalutationManager.DeleteItem(id);
        }
    }
}