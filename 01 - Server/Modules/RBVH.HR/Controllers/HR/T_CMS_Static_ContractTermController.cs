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
    public class T_CMS_Static_ContractTermController : ApiController
    {
        // GET api/<controller>
        /// <summary>
        /// Gets this instance.
        /// </summary>
        /// <returns></returns>
        public T_CMS_Static_ContractTermCollection Get()
        {
            return T_CMS_Static_ContractTermManager.GetAllItem();
        }

		public T_CMS_Static_ContractTermCollection GetbyUser(string usr)
        {
            return T_CMS_Static_ContractTermManager.GetbyUser(usr);
        }

		 

		

        // GET api/<controller>/5
        /// <summary>
        /// Gets the specified COM group identifier.
        /// </summary>
        /// <param name="T_CMS_Static_ContractTermId">The COM group identifier.</param>
        /// <returns></returns>
        public T_CMS_Static_ContractTerm Get(int ID)
        {
            return T_CMS_Static_ContractTermManager.GetItemByID(ID);
        }

        // POST api/<controller>
        /// <summary>
        /// Posts the specified value.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        public T_CMS_Static_ContractTerm Post([FromBody]T_CMS_Static_ContractTerm value)
        {
            return T_CMS_Static_ContractTermManager.AddItem(value);
        }

        // PUT api/<controller>/5
        /// <summary>
        /// Puts the specified identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        /// <exception cref="HttpResponseException"></exception>
        public T_CMS_Static_ContractTerm Put(string id, [FromBody]T_CMS_Static_ContractTerm value)
        {
            return T_CMS_Static_ContractTermManager.UpdateItem(value);
        }

		// GET api/<controller>
        /// <summary>
        /// Gets this instance.
        /// </summary>
        /// <returns></returns>
        public T_CMS_Static_ContractTermCollection Post(string method,[FromBody] SearchFilter value)
        {
            return T_CMS_Static_ContractTermManager.Search(value);
        }

        // DELETE api/<controller>/5
        /// <summary>
        /// Deletes the specified identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        public void Delete(int id)
        {
            T_CMS_Static_ContractTermManager.DeleteItem(id);
        }
    }
}