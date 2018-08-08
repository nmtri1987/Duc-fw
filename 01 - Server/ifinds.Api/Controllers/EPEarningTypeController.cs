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
    public class EPEarningTypeController : ApiController
    {
        // GET api/<controller>
        /// <summary>
        /// Gets this instance.
        /// </summary>
        /// <returns></returns>
        public EPEarningTypeCollection Get(int CompanyID)
        {
            return EPEarningTypeManager.GetAllItem(CompanyID);
        }

		public EPEarningTypeCollection GetbyUser(string usr,int CompanyID)
        {
            return EPEarningTypeManager.GetbyUser(usr,CompanyID);
        }

		 

		

        // GET api/<controller>/5
        /// <summary>
        /// Gets the specified COM group identifier.
        /// </summary>
        /// <param name="EPEarningTypeId">The COM group identifier.</param>
        /// <returns></returns>
        public EPEarningType Get(string TypeCD,int CompanyID)
        {
            return EPEarningTypeManager.GetItemByID(TypeCD,CompanyID);
        }

        // POST api/<controller>
        /// <summary>
        /// Posts the specified value.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        public EPEarningType Post([FromBody]EPEarningType value)
        {
            EPEarningType objItem = new EPEarningType();
            try
            {
				objItem = EPEarningTypeManager.AddItem(value);
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
        public EPEarningType Put(string id, [FromBody]EPEarningType value)
        {
            EPEarningType objItem = new EPEarningType();
            try
            {
				objItem= EPEarningTypeManager.UpdateItem(value);
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
        public EPEarningTypeCollection Post(string method,[FromBody] SearchFilter value)
        {
            return EPEarningTypeManager.Search(value);
        }

        // DELETE api/<controller>/5
        /// <summary>
        /// Deletes the specified identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        public void Delete(string id,int CompanyID)
        {
            EPEarningTypeManager.DeleteItem(id,CompanyID);
        }
    }
}