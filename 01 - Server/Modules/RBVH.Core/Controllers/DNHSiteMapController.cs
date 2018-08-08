using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebApi.OutputCache.V2;
using RBVH.Core.Models;
namespace RBVH.Core.Controllers
{
    /// <summary>
    ///
    /// </summary>
    /// <seealso cref="System.Web.Http.ApiController" />
	//[AutoInvalidateCacheOutput]
    public class DNHSiteMapController : ApiController
    {
        // GET api/<controller>
        /// <summary>
        /// Gets this instance.
        /// </summary>
        /// <returns></returns>
		[CacheOutput(ClientTimeSpan = 50, ServerTimeSpan = 50)]
        public DNHSiteMapCollection Get(int CompanyID)
        {
            return DNHSiteMapManager.GetAllItem(CompanyID);
        }

		public DNHSiteMapCollection GetbyUser(string usr,int CompanyID,Guid? NodeID)
        {
            return DNHSiteMapManager.GetbyUser(usr,CompanyID, NodeID);
        }

		 

		

        // GET api/<controller>/5
        /// <summary>
        /// Gets the specified COM group identifier.
        /// </summary>
        /// <param name="DNHSiteMapId">The COM group identifier.</param>
        /// <returns></returns>
        public DNHSiteMap Get(Guid NodeID,int CompanyID)
        {
            return DNHSiteMapManager.GetItemByID(NodeID,CompanyID);
        }

        // POST api/<controller>
        /// <summary>
        /// Posts the specified value.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        public DNHSiteMap Post([FromBody]DNHSiteMap value)
        {
          DNHSiteMap objItem = new DNHSiteMap();
            try
            {
				objItem = DNHSiteMapManager.AddItem(value);
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
        public DNHSiteMap Put(string id, [FromBody]DNHSiteMap value)
        {
            DNHSiteMap objItem = new DNHSiteMap();
            try
            {
				objItem= DNHSiteMapManager.UpdateItem(value);
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
        public DNHSiteMapCollection Post(string method,[FromBody] SearchFilter value)
        {
            return DNHSiteMapManager.Search(value);
        }

        // DELETE api/<controller>/5
        /// <summary>
        /// Deletes the specified identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        public void Delete(Guid id,int CompanyID)
        {
            DNHSiteMapManager.DeleteItem(id,CompanyID);
        }
    }
}