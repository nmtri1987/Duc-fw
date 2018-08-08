
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
    public class T_COM_Master_LocationController : ApiController
    {
        // GET api/<controller>
        /// <summary>
        /// Gets this instance.
        /// </summary>
        /// <returns></returns>
        public T_COM_Master_LocationCollection Get()
        {
            return T_COM_Master_LocationManager.GetAllItem();
        }
        public T_COM_Master_LocationCollection Get(int EntityID,string method)
        {
            return T_COM_Master_LocationManager.GetAllItemByEnity(EntityID);
        }
        

        public T_COM_Master_LocationCollection GetbyUser(string usr)
        {
            return T_COM_Master_LocationManager.GetbyUser(usr);
        }

		 

		

        // GET api/<controller>/5
        /// <summary>
        /// Gets the specified COM group identifier.
        /// </summary>
        /// <param name="T_COM_Master_LocationId">The COM group identifier.</param>
        /// <returns></returns>
        public T_COM_Master_Location Get(int LocationID)
        {
            return T_COM_Master_LocationManager.GetItemByID(LocationID);
        }

        // POST api/<controller>
        /// <summary>
        /// Posts the specified value.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        public T_COM_Master_Location Post([FromBody]T_COM_Master_Location value)
        {
            return T_COM_Master_LocationManager.AddItem(value);
        }

        // PUT api/<controller>/5
        /// <summary>
        /// Puts the specified identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        /// <exception cref="HttpResponseException"></exception>
        public T_COM_Master_Location Put(string id, [FromBody]T_COM_Master_Location value)
        {
            return T_COM_Master_LocationManager.UpdateItem(value);
        }

		// GET api/<controller>
        /// <summary>
        /// Gets this instance.
        /// </summary>
        /// <returns></returns>
        public T_COM_Master_LocationCollection Post(string method,[FromBody] SearchFilter value)
        {
            return T_COM_Master_LocationManager.Search(value);
        }

        // DELETE api/<controller>/5
        /// <summary>
        /// Deletes the specified identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        public void Delete(int id)
        {
            T_COM_Master_LocationManager.DeleteItem(id);
        }
    }
}