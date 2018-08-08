using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

using Biz.OG.Models;
namespace Biz.OG.Controllers
{
    /// <summary>
    ///
    /// </summary>
    /// <seealso cref="System.Web.Http.ApiController" />
    public class T_CMS_Master_EmploymentSubTypeController : ApiController
    {
        // GET api/<controller>
        /// <summary>
        /// Gets this instance.
        /// </summary>
        /// <returns></returns>
        public T_CMS_Master_EmploymentSubTypeCollection Get()
        {
            return T_CMS_Master_EmploymentSubTypeManager.GetAllItem();
        }

		public T_CMS_Master_EmploymentSubTypeCollection GetAllByEmpTypeID(int EmpTypeID, string ev)
        {
            return T_CMS_Master_EmploymentSubTypeManager.GetAllByEmpTypeID(EmpTypeID);
        }

		 

		

        // GET api/<controller>/5
        /// <summary>
        /// Gets the specified COM group identifier.
        /// </summary>
        /// <param name="T_CMS_Master_EmploymentSubTypeId">The COM group identifier.</param>
        /// <returns></returns>
        public T_CMS_Master_EmploymentSubType Get(int EmpSubTypeID)
        {
            return T_CMS_Master_EmploymentSubTypeManager.GetItemByID(EmpSubTypeID);
        }

        // POST api/<controller>
        /// <summary>
        /// Posts the specified value.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        public T_CMS_Master_EmploymentSubType Post([FromBody]T_CMS_Master_EmploymentSubType value)
        {
            return T_CMS_Master_EmploymentSubTypeManager.AddItem(value);
        }

        // PUT api/<controller>/5
        /// <summary>
        /// Puts the specified identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        /// <exception cref="HttpResponseException"></exception>
        public T_CMS_Master_EmploymentSubType Put(string id, [FromBody]T_CMS_Master_EmploymentSubType value)
        {
            return T_CMS_Master_EmploymentSubTypeManager.UpdateItem(value);
        }

		// GET api/<controller>
        /// <summary>
        /// Gets this instance.
        /// </summary>
        /// <returns></returns>
        public T_CMS_Master_EmploymentSubTypeCollection Post(string method,[FromBody] SearchFilter value)
        {
            return T_CMS_Master_EmploymentSubTypeManager.Search(value);
        }

        // DELETE api/<controller>/5
        /// <summary>
        /// Deletes the specified identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        public void Delete(int id)
        {
            T_CMS_Master_EmploymentSubTypeManager.DeleteItem(id);
        }
    }
}