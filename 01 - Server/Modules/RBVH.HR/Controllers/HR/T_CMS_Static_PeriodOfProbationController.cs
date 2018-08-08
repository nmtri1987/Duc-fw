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
    public class T_CMS_Static_PeriodOfProbationController : ApiController
    {
        // GET api/<controller>
        /// <summary>
        /// Gets this instance.
        /// </summary>
        /// <returns></returns>
        public T_CMS_Static_PeriodOfProbationCollection Get()
        {
            return T_CMS_Static_PeriodOfProbationManager.GetAllItem();
        }

		public T_CMS_Static_PeriodOfProbationCollection GetbyUser(string usr)
        {
            return T_CMS_Static_PeriodOfProbationManager.GetbyUser(usr);
        }

		 

		

        // GET api/<controller>/5
        /// <summary>
        /// Gets the specified COM group identifier.
        /// </summary>
        /// <param name="T_CMS_Static_PeriodOfProbationId">The COM group identifier.</param>
        /// <returns></returns>
        public T_CMS_Static_PeriodOfProbation Get(int ID)
        {
            return T_CMS_Static_PeriodOfProbationManager.GetItemByID(ID);
        }

        // POST api/<controller>
        /// <summary>
        /// Posts the specified value.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        public T_CMS_Static_PeriodOfProbation Post([FromBody]T_CMS_Static_PeriodOfProbation value)
        {
            return T_CMS_Static_PeriodOfProbationManager.AddItem(value);
        }

        // PUT api/<controller>/5
        /// <summary>
        /// Puts the specified identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        /// <exception cref="HttpResponseException"></exception>
        public T_CMS_Static_PeriodOfProbation Put(string id, [FromBody]T_CMS_Static_PeriodOfProbation value)
        {
            return T_CMS_Static_PeriodOfProbationManager.UpdateItem(value);
        }

		// GET api/<controller>
        /// <summary>
        /// Gets this instance.
        /// </summary>
        /// <returns></returns>
        public T_CMS_Static_PeriodOfProbationCollection Post(string method,[FromBody] SearchFilter value)
        {
            return T_CMS_Static_PeriodOfProbationManager.Search(value);
        }

        // DELETE api/<controller>/5
        /// <summary>
        /// Deletes the specified identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        public void Delete(int id)
        {
            T_CMS_Static_PeriodOfProbationManager.DeleteItem(id);
        }
    }
}