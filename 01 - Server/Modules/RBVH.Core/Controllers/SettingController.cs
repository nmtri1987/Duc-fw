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
	[AutoInvalidateCacheOutput]
    public class SettingController : ApiController
    {
        // GET api/<controller>
        /// <summary>
        /// Gets this instance.
        /// </summary>
        /// <returns></returns>
		[CacheOutput(ClientTimeSpan = 50, ServerTimeSpan = 50)]
        public SettingCollection Get(int CompanyID)
        {
            return SettingManager.GetAllItem(CompanyID);
        }

		public SettingCollection GetbyUser(string usr,int CompanyID)
        {
            return SettingManager.GetbyUser(usr,CompanyID);
        }

		 

		

        // GET api/<controller>/5
        /// <summary>
        /// Gets the specified COM group identifier.
        /// </summary>
        /// <param name="SettingId">The COM group identifier.</param>
        /// <returns></returns>
        public Setting Get(int SettingID,int CompanyID)
        {
            return SettingManager.GetItemByID(SettingID,CompanyID);
        }

        // POST api/<controller>
        /// <summary>
        /// Posts the specified value.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        public Setting Post([FromBody]Setting value)
        {
          Setting objItem = new Setting();
            try
            {
				objItem = SettingManager.AddItem(value);
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
        public Setting Put(string id, [FromBody]Setting value)
        {
            Setting objItem = new Setting();
            try
            {
				objItem= SettingManager.UpdateItem(value);
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
        public SettingCollection Post(string method,[FromBody] SearchFilter value)
        {
            return SettingManager.Search(value);
        }

        // DELETE api/<controller>/5
        /// <summary>
        /// Deletes the specified identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        public void Delete(int id,int CompanyID)
        {
            SettingManager.DeleteItem(id,CompanyID);
        }
    }
}