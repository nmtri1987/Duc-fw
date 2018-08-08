
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebApi.OutputCache.V2;
using DTP.Data.Models;
using RBVH.HR.Models;
namespace RBVH.Core.Controllers
{
    /// <summary>
    ///
    /// </summary>
    /// <seealso cref="System.Web.Http.ApiController" />
	[AutoInvalidateCacheOutput]
    public class QueuedEmailController : ApiController
    {
        // GET api/<controller>
        /// <summary>
        /// Gets this instance.
        /// </summary>
        /// <returns></returns>
		[CacheOutput(ClientTimeSpan = 50, ServerTimeSpan = 50)]
        public QueuedEmailCollection Get(int CompanyID)
        {
            return QueuedEmailManager.GetAllItem(CompanyID);
        }

        public QueuedEmailCollection GetbyUser(string usr, int CompanyID)
        {
            return QueuedEmailManager.GetbyUser(usr, CompanyID);
        }
        // GET api/<controller>/5
        /// <summary>
        /// Gets the specified COM group identifier.
        /// </summary>
        /// <param name="QueuedEmailId">The COM group identifier.</param>
        /// <returns></returns>
        public QueuedEmail Get(int Id, int CompanyID)
        {
            return QueuedEmailManager.GetItemByID(Id, CompanyID);
        }

        // POST api/<controller>
        /// <summary>
        /// Posts the specified value.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        
        public QueuedEmail Post([FromBody]QueuedEmail value)
        {
            QueuedEmail objItem = new QueuedEmail();
            try
            {
                objItem = QueuedEmailManager.AddItem(value);
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
        public QueuedEmail Put(string id, [FromBody]QueuedEmail value)
        {
            QueuedEmail objItem = new QueuedEmail();
            try
            {
                objItem = QueuedEmailManager.UpdateItem(value);
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
        public QueuedEmailCollection Post(string method, [FromBody] SearchFilter value)
        {
            return QueuedEmailManager.Search(value);
        }

        // DELETE api/<controller>/5
        /// <summary>
        /// Deletes the specified identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        public void Delete(int id, int CompanyID)
        {
            QueuedEmailManager.DeleteItem(id, CompanyID);
        }
    }
}