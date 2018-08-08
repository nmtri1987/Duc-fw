using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using WebApi.OutputCache.V2;
using RBVH.Core;
using System.Web.Http;
namespace RBVH.Core.Controllers
{
    /// <summary>
    ///
    /// </summary>
    /// <seealso cref="System.Web.Http.ApiController" />
	//[AutoInvalidateCacheOutput]
    public class LanguageController : ApiController
    {
        // GET api/<controller>
        /// <summary>
        /// Gets this instance.
        /// </summary>
        /// <returns></returns>
		[CacheOutput(ClientTimeSpan = 50, ServerTimeSpan = 50)]
        public LanguageCollection Get(int CompanyID)
        {
            return LanguageManager.GetAllItem(CompanyID);
        }

		public LanguageCollection GetbyUser(string usr,int CompanyID)
        {
            return LanguageManager.GetbyUser(usr,CompanyID);
        }

		 

		

        // GET api/<controller>/5
        /// <summary>
        /// Gets the specified COM group identifier.
        /// </summary>
        /// <param name="LanguageId">The COM group identifier.</param>
        /// <returns></returns>
        public Language Get(int LanguageId,int CompanyID)
        {
            return LanguageManager.GetItemByID(LanguageId,CompanyID);
        }

        // POST api/<controller>
        /// <summary>
        /// Posts the specified value.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        public Language Post([FromBody]Language value)
        {
          Language objItem = new Language();
            try
            {
				objItem = LanguageManager.AddItem(value);
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
        public Language Put(string id, [FromBody]Language value)
        {
            Language objItem = new Language();
            try
            {
				objItem= LanguageManager.UpdateItem(value);
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
        public LanguageCollection Post(string method,[FromBody] SearchFilter value)
        {
            return LanguageManager.Search(value);
        }

        // DELETE api/<controller>/5
        /// <summary>
        /// Deletes the specified identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        public void Delete(int id,int CompanyID)
        {
            LanguageManager.DeleteItem(id,CompanyID);
        }
    }
}