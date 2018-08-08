using System;
using System.Web.Http;
using DTP.Object;

namespace ifinds.Object.CS.Controllers
{
    /// <summary>
    ///
    /// </summary>
    /// <seealso cref="System.Web.Http.ApiController" />
    public class CompanyTreeController : ApiController
    {
        // GET api/<controller>
        /// <summary>
        /// Gets this instance.
        /// </summary>
        /// <returns></returns>
        public CompanyTreeCollection Get(int CompanyID)
        {
            return CompanyTreeManager.GetAllItem(CompanyID);
        }

		public CompanyTreeCollection GetbyUser(string usr)
        {
            return CompanyTreeManager.GetbyUser(usr);
        }

		 

		

        // GET api/<controller>/5
        /// <summary>
        /// Gets the specified COM group identifier.
        /// </summary>
        /// <param name="CompanyTreeId">The COM group identifier.</param>
        /// <returns></returns>
        public CompanyTree Get(Int32 WorkGroupID,int CompanyID)
        {
            return CompanyTreeManager.GetItemByID(WorkGroupID,CompanyID);
        }

        // POST api/<controller>
        /// <summary>
        /// Posts the specified value.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        public CompanyTree Post([FromBody]CompanyTree value)
        {
            return CompanyTreeManager.AddItem(value);
        }

        public int Post([FromBody]CompanyTreeList value,string method)
        {
            return CompanyTreeManager.CreateOrUpdate(value);
        }

        // PUT api/<controller>/5
        /// <summary>
        /// Puts the specified identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        /// <exception cref="HttpResponseException"></exception>
        public CompanyTree Put(string id, [FromBody]CompanyTree value)
        {
            return CompanyTreeManager.UpdateItem(value);
        }

		// GET api/<controller>
        /// <summary>
        /// Gets this instance.
        /// </summary>
        /// <returns></returns>
        public CompanyTreeCollection PutSearch(string method,[FromBody] SearchFilter value)
        {
            return CompanyTreeManager.Search(value);
        }

        // DELETE api/<controller>/5
        /// <summary>
        /// Deletes the specified identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        public void Delete(Int32 id,int CompanyID)
        {
            CompanyTreeManager.DeleteItem(id,CompanyID);
        }
    }
}