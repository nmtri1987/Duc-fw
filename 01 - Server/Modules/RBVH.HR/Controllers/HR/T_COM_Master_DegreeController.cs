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
    public class T_COM_Master_DegreeController : ApiController
    {
        // GET api/<controller>
        /// <summary>
        /// Gets this instance.
        /// </summary>
        /// <returns></returns>
        public T_COM_Master_DegreeCollection Get()
        {
            return T_COM_Master_DegreeManager.GetAllItem();
        }

		public T_COM_Master_DegreeCollection GetbyUser(string usr)
        {
            return T_COM_Master_DegreeManager.GetbyUser(usr);
        }

		 

		

        // GET api/<controller>/5
        /// <summary>
        /// Gets the specified COM group identifier.
        /// </summary>
        /// <param name="T_COM_Master_DegreeId">The COM group identifier.</param>
        /// <returns></returns>
        public T_COM_Master_Degree Get(int DegreeID)
        {
            return T_COM_Master_DegreeManager.GetItemByID(DegreeID);
        }

        // POST api/<controller>
        /// <summary>
        /// Posts the specified value.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        public T_COM_Master_Degree Post([FromBody]T_COM_Master_Degree value)
        {
            return T_COM_Master_DegreeManager.AddItem(value);
        }

        // PUT api/<controller>/5
        /// <summary>
        /// Puts the specified identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        /// <exception cref="HttpResponseException"></exception>
        public T_COM_Master_Degree Put(string id, [FromBody]T_COM_Master_Degree value)
        {
            return T_COM_Master_DegreeManager.UpdateItem(value);
        }

		// GET api/<controller>
        /// <summary>
        /// Gets this instance.
        /// </summary>
        /// <returns></returns>
        public T_COM_Master_DegreeCollection Post(string method,[FromBody] SearchFilter value)
        {
            return T_COM_Master_DegreeManager.Search(value);
        }

        // DELETE api/<controller>/5
        /// <summary>
        /// Deletes the specified identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        public void Delete(int id)
        {
            T_COM_Master_DegreeManager.DeleteItem(id);
        }
    }
}