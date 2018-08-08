using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebApi.OutputCache.V2;
using RBVH.Core;
namespace RBVH.Core.Controllers
{
    /// <summary>
    ///
    /// </summary>
    /// <seealso cref="System.Web.Http.ApiController" />
	[AutoInvalidateCacheOutput]
    public class LocaleStringResourceController : ApiController
    {
        // GET api/<controller>
        /// <summary>
        /// Gets this instance.
        /// </summary>
        /// <returns></returns>
		[CacheOutput(ClientTimeSpan = 50, ServerTimeSpan = 50)]
        public LocaleStringResourceCollection Get(int CompanyID)
        {
            return LocaleStringResourceManager.GetAllItem(CompanyID);
        }

		public LocaleStringResourceCollection GetbyUser(string usr,int CompanyID)
        {
            return LocaleStringResourceManager.GetbyUser(usr,CompanyID);
        }

		 

		

        // GET api/<controller>/5
        /// <summary>
        /// Gets the specified COM group identifier.
        /// </summary>
        /// <param name="LocaleStringResourceId">The COM group identifier.</param>
        /// <returns></returns>
        public LocaleStringResource Get(int LocaleStringResourceID,int CompanyID)
        {
            return LocaleStringResourceManager.GetItemByID(LocaleStringResourceID,CompanyID);
        }

        // POST api/<controller>
        /// <summary>
        /// Posts the specified value.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        public LocaleStringResource Post([FromBody]LocaleStringResource value)
        {
          LocaleStringResource objItem = new LocaleStringResource();
            try
            {
				objItem = LocaleStringResourceManager.AddItem(value);
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
        public LocaleStringResource Put(string id, [FromBody]LocaleStringResource value)
        {
            LocaleStringResource objItem = new LocaleStringResource();
            try
            {
				objItem= LocaleStringResourceManager.UpdateItem(value);
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
        public LocaleStringResourceCollection Post(string method,[FromBody] SearchFilter value)
        {
            return LocaleStringResourceManager.Search(value);
        }

        // DELETE api/<controller>/5
        /// <summary>
        /// Deletes the specified identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        public void Delete(int id,int CompanyID)
        {
            LocaleStringResourceManager.DeleteItem(id,CompanyID);
        }
    }
}