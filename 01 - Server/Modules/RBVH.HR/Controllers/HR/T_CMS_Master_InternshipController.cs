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
    public class T_CMS_Master_InternshipController : ApiController
    {
        // GET api/<controller>
        /// <summary>
        /// Gets this instance.
        /// </summary>
        /// <returns></returns>
        public T_CMS_Master_InternshipCollection Get()
        {
            return T_CMS_Master_InternshipManager.GetAllItem();
        }

		public T_CMS_Master_InternshipCollection GetbyUser(string usr)
        {
            return T_CMS_Master_InternshipManager.GetbyUser(usr);
        }

		 

		

        // GET api/<controller>/5
        /// <summary>
        /// Gets the specified COM group identifier.
        /// </summary>
        /// <param name="T_CMS_Master_InternshipId">The COM group identifier.</param>
        /// <returns></returns>
        public T_CMS_Master_Internship Get(int ID)
        {
            return T_CMS_Master_InternshipManager.GetItemByID(ID);
        }

        // POST api/<controller>
        /// <summary>
        /// Posts the specified value.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        public T_CMS_Master_Internship Post([FromBody]T_CMS_Master_Internship value)
        {
            return T_CMS_Master_InternshipManager.InsertNew(value);
        }

        // PUT api/<controller>/5
        /// <summary>
        /// Puts the specified identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        /// <exception cref="HttpResponseException"></exception>
        public T_CMS_Master_Internship Put(string id, [FromBody]T_CMS_Master_Internship value)
        {
            return T_CMS_Master_InternshipManager.UpdateItem(value);
        }

		// GET api/<controller>
        /// <summary>
        /// Gets this instance.
        /// </summary>
        /// <returns></returns>
        public T_CMS_Master_InternshipCollection Post(string method,[FromBody] SearchFilter value)
        {
            return T_CMS_Master_InternshipManager.Search(value);
        }

        // DELETE api/<controller>/5
        /// <summary>
        /// Deletes the specified identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        public void Delete(int id)
        {
            T_CMS_Master_InternshipManager.DeleteItem(id);
        }
    }
}