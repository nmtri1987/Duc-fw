using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

using RBVH.HR.Models;
namespace RBVH.HR.Controllers
{
    /// <summary>
    ///
    /// </summary>
    /// <seealso cref="System.Web.Http.ApiController" />
    public class T_COM_Master_UniversityController : ApiController
    {
        // GET api/<controller>
        /// <summary>
        /// Gets this instance.
        /// </summary>
        /// <returns></returns>
        public T_COM_Master_UniversityCollection Get()
        {
            return T_COM_Master_UniversityManager.GetAllItem();
        }

		public T_COM_Master_University GetbyUniversityName(string UniversityName,string ev)
        {
            return T_COM_Master_UniversityManager.GetbyUniversityName(UniversityName);
        }

		 

		

        // GET api/<controller>/5
        /// <summary>
        /// Gets the specified COM group identifier.
        /// </summary>
        /// <param name="T_COM_Master_UniversityId">The COM group identifier.</param>
        /// <returns></returns>
        public T_COM_Master_University Get(int UniversityID)
        {
            return T_COM_Master_UniversityManager.GetItemByID(UniversityID);
        }

        // POST api/<controller>
        /// <summary>
        /// Posts the specified value.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        public T_COM_Master_University Post([FromBody]T_COM_Master_University value)
        {
            return T_COM_Master_UniversityManager.AddItem(value);
        }

        // PUT api/<controller>/5
        /// <summary>
        /// Puts the specified identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        /// <exception cref="HttpResponseException"></exception>
        public T_COM_Master_University Put(string id, [FromBody]T_COM_Master_University value)
        {
            return T_COM_Master_UniversityManager.UpdateItem(value);
        }

		// GET api/<controller>
        /// <summary>
        /// Gets this instance.
        /// </summary>
        /// <returns></returns>
        public T_COM_Master_UniversityCollection Post(string method,[FromBody] SearchFilter value)
        {
            return T_COM_Master_UniversityManager.Search(value);
        }

        // DELETE api/<controller>/5
        /// <summary>
        /// Deletes the specified identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        public void Delete(int id)
        {
            T_COM_Master_UniversityManager.DeleteItem(id);
        }
    }
}