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
    public class T_COM_Master_PlaceOfIssueController : ApiController
    {
        // GET api/<controller>
        /// <summary>
        /// Gets this instance.
        /// </summary>
        /// <returns></returns>
        public T_COM_Master_PlaceOfIssueCollection Get()
        {
            return T_COM_Master_PlaceOfIssueManager.GetAllItem();
        }

		public T_COM_Master_PlaceOfIssueCollection GetbyUser(string usr)
        {
            return T_COM_Master_PlaceOfIssueManager.GetbyUser(usr);
        }

		 

		

        // GET api/<controller>/5
        /// <summary>
        /// Gets the specified COM group identifier.
        /// </summary>
        /// <param name="T_COM_Master_PlaceOfIssueId">The COM group identifier.</param>
        /// <returns></returns>
        public T_COM_Master_PlaceOfIssue Get(int POI_ID)
        {
            return T_COM_Master_PlaceOfIssueManager.GetItemByID(POI_ID);
        }

        // GET api/<controller>/5
        /// <summary>
        /// Gets the specified COM group identifier.
        /// </summary>
        /// <param name="T_COM_Master_PlaceOfIssueId">The COM group identifier.</param>
        /// <returns></returns>
        public T_COM_Master_PlaceOfIssue Get(string POI_Name_VN)
        {
            return T_COM_Master_PlaceOfIssueManager.GetItemByPOI_Name_VN(POI_Name_VN);
        }

        // POST api/<controller>
        /// <summary>
        /// Posts the specified value.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        public T_COM_Master_PlaceOfIssue Post([FromBody]T_COM_Master_PlaceOfIssue value)
        {
            return T_COM_Master_PlaceOfIssueManager.AddItem(value);
        }

        // PUT api/<controller>/5
        /// <summary>
        /// Puts the specified identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        /// <exception cref="HttpResponseException"></exception>
        public T_COM_Master_PlaceOfIssue Put(string id, [FromBody]T_COM_Master_PlaceOfIssue value)
        {
            return T_COM_Master_PlaceOfIssueManager.UpdateItem(value);
        }

		// GET api/<controller>
        /// <summary>
        /// Gets this instance.
        /// </summary>
        /// <returns></returns>
        public T_COM_Master_PlaceOfIssueCollection Post(string method,[FromBody] SearchFilter value)
        {
            return T_COM_Master_PlaceOfIssueManager.Search(value);
        }

        // DELETE api/<controller>/5
        /// <summary>
        /// Deletes the specified identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        public void Delete(int id)
        {
            T_COM_Master_PlaceOfIssueManager.DeleteItem(id);
        }
    }
}