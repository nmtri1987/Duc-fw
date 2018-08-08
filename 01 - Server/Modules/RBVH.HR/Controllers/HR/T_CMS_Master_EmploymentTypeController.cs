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
    public class T_CMS_Master_EmploymentTypeController : ApiController
    {
        // GET api/<controller>
        /// <summary>
        /// Gets this instance.
        /// </summary>
        /// <returns></returns>
        public T_CMS_Master_EmploymentTypeCollection Get()
        {
            return T_CMS_Master_EmploymentTypeManager.GetAllItem();
        }

        public T_CMS_Master_EmploymentTypeCollection Get(int EntityID, string method)
        {
            return T_CMS_Master_EmploymentTypeManager.GetAllItemByEnity(EntityID);
        }

        public T_CMS_Master_EmploymentTypeCollection GetbyUser(string usr)
        {
            return T_CMS_Master_EmploymentTypeManager.GetbyUser(usr);
        }

		 

		

        // GET api/<controller>/5
        /// <summary>
        /// Gets the specified COM group identifier.
        /// </summary>
        /// <param name="T_CMS_Master_EmploymentTypeId">The COM group identifier.</param>
        /// <returns></returns>
        public T_CMS_Master_EmploymentType Get(int EmpTypeID)
        {
            return T_CMS_Master_EmploymentTypeManager.GetItemByID(EmpTypeID);
        }

        // POST api/<controller>
        /// <summary>
        /// Posts the specified value.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        public T_CMS_Master_EmploymentType Post([FromBody]T_CMS_Master_EmploymentType value)
        {
            return T_CMS_Master_EmploymentTypeManager.AddItem(value);
        }

        // PUT api/<controller>/5
        /// <summary>
        /// Puts the specified identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        /// <exception cref="HttpResponseException"></exception>
        public T_CMS_Master_EmploymentType Put(string id, [FromBody]T_CMS_Master_EmploymentType value)
        {
            return T_CMS_Master_EmploymentTypeManager.UpdateItem(value);
        }

		// GET api/<controller>
        /// <summary>
        /// Gets this instance.
        /// </summary>
        /// <returns></returns>
        public T_CMS_Master_EmploymentTypeCollection Post(string method,[FromBody] SearchFilter value)
        {
            return T_CMS_Master_EmploymentTypeManager.Search(value);
        }

        // DELETE api/<controller>/5
        /// <summary>
        /// Deletes the specified identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        public void Delete(int id)
        {
            T_CMS_Master_EmploymentTypeManager.DeleteItem(id);
        }
    }
}