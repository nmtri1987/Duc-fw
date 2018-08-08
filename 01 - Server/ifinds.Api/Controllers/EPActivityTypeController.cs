using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
//using DTP.Data;
//using ifinds.Object.OG.Models;
using ifinds.Object.OG;
namespace ifinds.Object.OG.Controllers
{
    /// <summary>
    ///
    /// </summary>
    /// <seealso cref="System.Web.Http.ApiController" />
    public class EPActivityTypeController : ApiController
    {
        // GET api/<controller>
        /// <summary>
        /// Gets this instance.
        /// </summary>
        /// <returns></returns>
        public EPActivityTypeCollection Get(int CompanyID)
        {
            return EPActivityTypeManager.GetAllItem(CompanyID);
        }

		public EPActivityTypeCollection GetbyUser(string usr,int CompanyID)
        {
            return EPActivityTypeManager.GetbyUser(usr,CompanyID);
        }

		 

		

        // GET api/<controller>/5
        /// <summary>
        /// Gets the specified COM group identifier.
        /// </summary>
        /// <param name="EPActivityTypeId">The COM group identifier.</param>
        /// <returns></returns>
        public EPActivityType Get(string Type,int CompanyID)
        {
            return EPActivityTypeManager.GetItemByID(Type,CompanyID);
        }

        // POST api/<controller>
        /// <summary>
        /// Posts the specified value.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        public EPActivityType Post([FromBody]EPActivityType value)
        {
            EPActivityType objItem = new EPActivityType();
            try
            {
				objItem = EPActivityTypeManager.AddItem(value);
			}
			catch (Exception ObjEx)
            {
                IfindLogManager.AddItem(new IfindLog(){LinkUrl = Request.RequestUri.AbsoluteUri,Exception = ObjEx.Message,Message = ObjEx.StackTrace});
            }
            return objItem;
        }

        // PUT api/<controller>/5
        /// <summary>
        /// Puts the specified identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        /// <exception cref="HttpResponseException"></exception>
        public EPActivityType Put(string id, [FromBody]EPActivityType value)
        {
            EPActivityType objItem = new EPActivityType();
            try
            {
				objItem= EPActivityTypeManager.UpdateItem(value);
			}
			catch (Exception ObjEx)
            {
                IfindLogManager.AddItem(new IfindLog(){LinkUrl = Request.RequestUri.AbsoluteUri,Exception = ObjEx.Message,Message = ObjEx.StackTrace});
            }
            return objItem;
        }

		// GET api/<controller>
        /// <summary>
        /// Gets this instance.
        /// </summary>
        /// <returns></returns>
        public EPActivityTypeCollection Post(string method,[FromBody] SearchFilter value)
        {
            return EPActivityTypeManager.Search(value);
        }

        // DELETE api/<controller>/5
        /// <summary>
        /// Deletes the specified identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        public void Delete(string id,int CompanyID)
        {
            EPActivityTypeManager.DeleteItem(id,CompanyID);
        }
    }
}