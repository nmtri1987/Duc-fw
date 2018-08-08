using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
//using DTP.Data;
//using ifinds.Object.CS.Models;
using ifinds.Object.CS;
namespace ifinds.Object.CS.Controllers
{
    /// <summary>
    ///
    /// </summary>
    /// <seealso cref="System.Web.Http.ApiController" />
    public class RolesController : ApiController
    {
        // GET api/<controller>
        /// <summary>
        /// Gets this instance.
        /// </summary>
        /// <returns></returns>
        public RolesCollection Get(int CompanyID)
        {
            return RolesManager.GetAllItem(CompanyID);
        }

		public RolesCollection GetbyUser(string usr,int CompanyID)
        {
            return RolesManager.GetbyUser(usr,CompanyID);
        }

		 

		

        // GET api/<controller>/5
        /// <summary>
        /// Gets the specified COM group identifier.
        /// </summary>
        /// <param name="RolesId">The COM group identifier.</param>
        /// <returns></returns>
        public Roles Get(string Rolename,int CompanyID)
        {
            return RolesManager.GetItemByID(Rolename,CompanyID);
        }

        // POST api/<controller>
        /// <summary>
        /// Posts the specified value.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        public Roles Post([FromBody]Roles value)
        {
            Roles objItem = new Roles();
            try
            {
				objItem = RolesManager.AddItem(value);
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
        public Roles Put(string id, [FromBody]Roles value)
        {
            Roles objItem = new Roles();
            try
            {
				objItem= RolesManager.UpdateItem(value);
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
        public RolesCollection Post(string method,[FromBody] SearchFilter value)
        {
            return RolesManager.Search(value);
        }

        // DELETE api/<controller>/5
        /// <summary>
        /// Deletes the specified identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        public void Delete(string id,int CompanyID)
        {
            RolesManager.DeleteItem(id,CompanyID);
        }
    }
}