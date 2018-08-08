using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
//using DTP.Data;
//using ifinds.Object.OG.Models;
using ifinds.Object;
using ifinds.Data;

namespace ifinds.Api.Controllers
{
    /// <summary>
    ///
    /// </summary>
    /// <seealso cref="System.Web.Http.ApiController" />
    public class AttachmentsController : ApiController
    {
        // GET api/<controller>
        /// <summary>
        /// Gets this instance.
        /// </summary>
        /// <returns></returns>
        public AttachmentsCollection Get(int CompanyID)
        {
            return AttachmentsManager.GetAllItem(CompanyID);
        }

		public AttachmentsCollection GetbyUser(string usr, int CompanyID)
        {
            return AttachmentsManager.GetbyUser(usr,CompanyID);
        }

		 

		

        // GET api/<controller>/5
        /// <summary>
        /// Gets the specified COM group identifier.
        /// </summary>
        /// <param name="AttachmentsId">The COM group identifier.</param>
        /// <returns></returns>
        public Attachments Get(Guid AttachID,int CompanyID)
        {
            return AttachmentsManager.GetItemByID(AttachID,CompanyID);
        }

        // POST api/<controller>
        /// <summary>
        /// Posts the specified value.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        public Attachments Post([FromBody]Attachments value)
        {
            return AttachmentsManager.AddItem(value);
        }

        // PUT api/<controller>/5
        /// <summary>
        /// Puts the specified identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        /// <exception cref="HttpResponseException"></exception>
        public Attachments Put(string id, [FromBody]Attachments value)
        {
            return AttachmentsManager.UpdateItem(value);
        }

		// GET api/<controller>
        /// <summary>
        /// Gets this instance.
        /// </summary>
        /// <returns></returns>
        //public AttachmentsCollection PutSearch(string method,[FromBody] SearchFilter value)
        //{
        //    return AttachmentsManager.Search(value);
        //}

        // DELETE api/<controller>/5
        /// <summary>
        /// Deletes the specified identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        public void Delete(Guid id,int CompanyID)
        {
            AttachmentsManager.DeleteItem(id,CompanyID);
        }
    }
}