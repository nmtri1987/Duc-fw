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
    public class T_COM_Master_EntityController : ApiController
    {
        // GET api/<controller>
        /// <summary>
        /// Gets this instance.
        /// </summary>
        /// <returns></returns>
        public T_COM_Master_EntityCollection Get()
        {
            return T_COM_Master_EntityManager.GetAllItem();
        }

		public T_COM_Master_EntityCollection GetbyEmployeeCode(int EmployeeCode)
        {
            return T_COM_Master_EntityManager.GetbyEmployeeCode(EmployeeCode);
        }

        // GET api/<controller>/5
        /// <summary>
        /// Gets the specified COM group identifier.
        /// </summary>
        /// <param name="T_COM_Master_EntityId">The COM group identifier.</param>
        /// <returns></returns>
        public T_COM_Master_Entity Get(int EntityId)
        {
            return T_COM_Master_EntityManager.GetItemByID(EntityId);
        }

        // POST api/<controller>
        /// <summary>
        /// Posts the specified value.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        public T_COM_Master_Entity Post([FromBody]T_COM_Master_Entity value)
        {
            return T_COM_Master_EntityManager.AddItem(value);
        }

        // PUT api/<controller>/5
        /// <summary>
        /// Puts the specified identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        /// <exception cref="HttpResponseException"></exception>
        public T_COM_Master_Entity Put(string id, [FromBody]T_COM_Master_Entity value)
        {
            return T_COM_Master_EntityManager.UpdateItem(value);
        }

		// GET api/<controller>
        /// <summary>
        /// Gets this instance.
        /// </summary>
        /// <returns></returns>
        public T_COM_Master_EntityCollection Post(string method,[FromBody] SearchFilter value)
        {
            return T_COM_Master_EntityManager.Search(value);
        }

        // DELETE api/<controller>/5
        /// <summary>
        /// Deletes the specified identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        public void Delete(int id)
        {
            T_COM_Master_EntityManager.DeleteItem(id);
        }
    }
}