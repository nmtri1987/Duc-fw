using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
//using DTP.Data;
//using ifinds.Object.CS.Models;
using ifinds.Object.CS;
namespace ifinds.Api.Controllers
{
    /// <summary>
    ///
    /// </summary>
    /// <seealso cref="System.Web.Http.ApiController" />
    public class BranchController : ApiController
    {
        // GET api/<controller>
        /// <summary>
        /// Gets this instance.
        /// </summary>
        /// <returns></returns>
        public BranchCollection Get(int CompanyID)
        {
            return BranchManager.GetAllItem(CompanyID);
        }

		public BranchCollection GetbyUser(string usr,int CompanyID)
        {
            return BranchManager.GetbyUser(usr,CompanyID);
        }

		 

		

        // GET api/<controller>/5
        /// <summary>
        /// Gets the specified COM group identifier.
        /// </summary>
        /// <param name="BranchId">The COM group identifier.</param>
        /// <returns></returns>
        public Branch Get(int BranchID,int CompanyID)
        {
            return BranchManager.GetItemByID(BranchID,CompanyID);
        }

        // POST api/<controller>
        /// <summary>
        /// Posts the specified value.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        public Branch Post([FromBody]Branch value)
        {
            Branch objItem = new Branch();
            try
            {
                objItem = BranchManager.AddItem(value);
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
        public Branch Put(string id, [FromBody]Branch value)
        {
            Branch objItem = new Branch();
            try
            {
				objItem= BranchManager.UpdateItem(value);
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
        public BranchCollection Post(string method,[FromBody] SearchFilter value)
        {
            return BranchManager.Search(value);
        }

        // DELETE api/<controller>/5
        /// <summary>
        /// Deletes the specified identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        public void Delete(int id,int CompanyID)
        {
            BranchManager.DeleteItem(id,CompanyID);
        }
    }
}