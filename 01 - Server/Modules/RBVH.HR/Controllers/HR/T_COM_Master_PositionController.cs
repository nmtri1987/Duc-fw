//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Net;
//using System.Net.Http;
using System.Web.Http;
using Biz.OG.Models;
namespace Biz.OG.Controllers
{
    /// <summary>
    ///
    /// </summary>
    /// <seealso cref="System.Web.Http.ApiController" />
    public class T_COM_Master_PositionController : ApiController
    {
        // GET api/<controller>
        /// <summary>
        /// Gets this instance.
        /// </summary>
        /// <returns></returns>
        public T_COM_Master_PositionCollection Get()
        {
            return T_COM_Master_PositionManager.GetAllItem();
        }

		public T_COM_Master_PositionCollection GetbyUser(string usr)
        {
            return T_COM_Master_PositionManager.GetbyUser(usr);
        }

		 

		

        // GET api/<controller>/5
        /// <summary>
        /// Gets the specified COM group identifier.
        /// </summary>
        /// <param name="T_COM_Master_PositionId">The COM group identifier.</param>
        /// <returns></returns>
        public T_COM_Master_Position Get(int PositionID)
        {
            return T_COM_Master_PositionManager.GetItemByID(PositionID);
        }

        // GET api/<controller>/5
        /// <summary>
        /// Gets the specified COM group identifier.
        /// </summary>
        /// <param name="T_COM_Master_PositionId">The COM group identifier.</param>
        /// <returns></returns>
        public PositionRCollection GetRange(int EntityID,string Mode)
        {
            return T_COM_Master_PositionManager.GetPostionRangesalary(EntityID);
        }


        // GET api/<controller>/5
        /// <summary>
        /// Gets the specified COM group identifier.
        /// </summary>
        /// <param name="T_COM_Master_PositionId">The COM group identifier.</param>
        /// <returns></returns>
        public T_COM_Master_Position Get(string PositionName_EN)
        {
            return T_COM_Master_PositionManager.GetItemByName(PositionName_EN);
        }

        // POST api/<controller>
        /// <summary>
        /// Posts the specified value.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        public T_COM_Master_Position Post([FromBody]T_COM_Master_Position value)
        {
            return T_COM_Master_PositionManager.AddItem(value);
        }

        // PUT api/<controller>/5
        /// <summary>
        /// Puts the specified identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        /// <exception cref="HttpResponseException"></exception>
        public T_COM_Master_Position Put(string id, [FromBody]T_COM_Master_Position value)
        {
            return T_COM_Master_PositionManager.UpdateItem(value);
        }

		// GET api/<controller>
        /// <summary>
        /// Gets this instance.
        /// </summary>
        /// <returns></returns>
        public T_COM_Master_PositionCollection Post(string method,[FromBody] SearchFilter value)
        {
            return T_COM_Master_PositionManager.Search(value);
        }

        // DELETE api/<controller>/5
        /// <summary>
        /// Deletes the specified identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        public void Delete(int id)
        {
            T_COM_Master_PositionManager.DeleteItem(id);
        }
    }
}