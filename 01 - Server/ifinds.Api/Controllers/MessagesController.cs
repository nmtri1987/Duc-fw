using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
//using DTP.Data;
//using ifinds.Object.CS.Models;
using ifinds.Object.CS;
using System.Data;

namespace ifinds.Object.CS.Controllers
{
    /// <summary>
    ///
    /// </summary>
    /// <seealso cref="System.Web.Http.ApiController" />
    public class MessagesController : ApiController
    {
        // GET api/<controller>
        /// <summary>
        /// Gets this instance.
        /// </summary>
        /// <returns></returns>
        public MessagesCollection Get(int CompanyID)
        {
            return MessagesManager.GetAllItem(CompanyID);
        }

		public MessagesCollection GetbyUser(string usr,int CompanyID)
        {
            return MessagesManager.GetbyUser(usr,CompanyID);
        }

        public DataTable GetbyUserName(string UserName, int CompanyID,string action)
        {
            return MessagesManager.GetbyUserName(UserName, CompanyID);
        }

        public int Getbyupdate(string Fromuser, string Touser, int CompanyID)
        {
            return MessagesManager.UpdateStatus(Fromuser,Touser, CompanyID);
        }





        // GET api/<controller>/5
        /// <summary>
        /// Gets the specified COM group identifier.
        /// </summary>
        /// <param name="MessagesId">The COM group identifier.</param>
        /// <returns></returns>
        public Messages Get(Int64 MessageID,int CompanyID)
        {
            return MessagesManager.GetItemByID(MessageID,CompanyID);
        }

        // POST api/<controller>
        /// <summary>
        /// Posts the specified value.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        public Messages Post([FromBody]Messages value)
        {
          Messages objItem = new Messages();
            try
            {
				objItem = MessagesManager.AddItem(value);
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
        public Messages Put(string id, [FromBody]Messages value)
        {
            Messages objItem = new Messages();
            try
            {
				objItem= MessagesManager.UpdateItem(value);
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
        public MessagesCollection Post(string method,[FromBody] SearchFilter value)
        {
            return MessagesManager.Search(value);
        }

        // DELETE api/<controller>/5
        /// <summary>
        /// Deletes the specified identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        public void Delete(Int64 id,int CompanyID)
        {
            MessagesManager.DeleteItem(id,CompanyID);
        }
    }
}