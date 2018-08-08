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
    public class ScheduleTaskController : ApiController
    {
        // GET api/<controller>
        /// <summary>
        /// Gets this instance.
        /// </summary>
        /// <returns></returns>
		[CacheOutput(ClientTimeSpan = 50, ServerTimeSpan = 50)]
        public ScheduleTaskCollection Get(int CompanyID)
        {
            return ScheduleTaskManager.GetAllItem(CompanyID);
        }

        public ScheduleTask GetbyType(string Type, int CompanyID)
        {
            return ScheduleTaskManager.GetbyType(Type, CompanyID);
        }





        // GET api/<controller>/5
        /// <summary>
        /// Gets the specified COM group identifier.
        /// </summary>
        /// <param name="ScheduleTaskId">The COM group identifier.</param>
        /// <returns></returns>
        public ScheduleTask Get(int Id, int CompanyID)
        {
            return ScheduleTaskManager.GetItemByID(Id, CompanyID);
        }

        // POST api/<controller>
        /// <summary>
        /// Posts the specified value.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        public ScheduleTask Post([FromBody]ScheduleTask value)
        {
            ScheduleTask objItem = new ScheduleTask();
            try
            {
                objItem = ScheduleTaskManager.AddItem(value);
            }
            catch (Exception ObjEx)
            {
                IfindLogManager.AddItem(new IfindLog() { LinkUrl = Request.RequestUri.AbsoluteUri, Exception = ObjEx.Message, Message = ObjEx.StackTrace });
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
        public ScheduleTask Put(string id, [FromBody]ScheduleTask value)
        {
            ScheduleTask objItem = new ScheduleTask();
            try
            {
                objItem = ScheduleTaskManager.UpdateItem(value);
            }
            catch (Exception ObjEx)
            {
                IfindLogManager.AddItem(new IfindLog() { LinkUrl = Request.RequestUri.AbsoluteUri, Exception = ObjEx.Message, Message = ObjEx.StackTrace });
            }
            return objItem;
        }

        // GET api/<controller>
        /// <summary>
        /// Gets this instance.
        /// </summary>
        /// <returns></returns>
        public ScheduleTaskCollection Post(string method, [FromBody] SearchFilter value)
        {
            return ScheduleTaskManager.Search(value);
        }

        // DELETE api/<controller>/5
        /// <summary>
        /// Deletes the specified identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        public void Delete(int id, int CompanyID)
        {
            ScheduleTaskManager.DeleteItem(id, CompanyID);
        }
    }
}