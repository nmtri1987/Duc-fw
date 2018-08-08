using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
//using DTP.Data;
//using ifinds.Object.OG.Models;
using ifinds.Object.OG;
namespace ifinds.Object.OG.Controllers
{
    /// <summary>
    ///
    /// </summary>
    /// <seealso cref="System.Web.Http.ApiController" />
    public class EPDepartmentController : ApiController
    {
        // GET api/<controller>
        /// <summary>
        /// Gets this instance.
        /// </summary>
        /// <returns></returns>
        public EPDepartmentCollection Get(int CompanyID)
        {
            return EPDepartmentManager.GetAllItem(CompanyID);
        }

		public EPDepartmentCollection GetbyUser(string usr,int CompanyID)
        {
            return EPDepartmentManager.GetbyUser(usr,CompanyID);
        }

		 

		

        // GET api/<controller>/5
        /// <summary>
        /// Gets the specified COM group identifier.
        /// </summary>
        /// <param name="EPDepartmentId">The COM group identifier.</param>
        /// <returns></returns>
        public EPDepartment Get(string DepartmentID,int CompanyID)
        {
            return EPDepartmentManager.GetItemByID(DepartmentID,CompanyID);
        }

        // POST api/<controller>
        /// <summary>
        /// Posts the specified value.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        public EPDepartment Post([FromBody]EPDepartment value)
        {
            EPDepartment objItem = new EPDepartment();
            try
            {
				objItem = EPDepartmentManager.AddItem(value);
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
        public EPDepartment Put(string id, [FromBody]EPDepartment value)
        {
            EPDepartment objItem = new EPDepartment();
            try
            {
				objItem= EPDepartmentManager.UpdateItem(value);
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
        public EPDepartmentCollection Post(string method,[FromBody] SearchFilter value)
        {
            return EPDepartmentManager.Search(value);
        }

        // DELETE api/<controller>/5
        /// <summary>
        /// Deletes the specified identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        public void Delete(string id,int CompanyID)
        {
            EPDepartmentManager.DeleteItem(id,CompanyID);
        }
    }
}