using DTP.Object;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using DTP.Object;
namespace ifinds.Api.Controllers
{
    /// <summary>
    ///
    /// </summary>
    /// <seealso cref="System.Web.Http.ApiController" />
    public class SiteMapController : ApiController
    {
        // GET api/<controller>
        /// <summary>
        /// Gets this instance.
        /// </summary>
        /// <returns></returns>
        public SiteMapCollection Get(int CompanyID)
        {
            return SiteMapManager.GetAllItem(CompanyID);
        }

		public SiteMapCollection GetbyUser(string usr, int CompanyID)
        {
            return SiteMapManager.GetbyUser(usr,CompanyID);
        }

        // GET api/<controller>/5
        /// <summary>
        /// Gets the specified COM group identifier.
        /// </summary>
        /// <param name="SiteMapId">The COM group identifier.</param>
        /// <returns></returns>
        public SiteMap Get(Guid NodeID, int CompanyID)
        {
            return SiteMapManager.GetItemByID(NodeID,CompanyID);
        }

        // POST api/<controller>
        /// <summary>
        /// Posts the specified value.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        public SiteMap Post([FromBody]SiteMap value)
        {
            return SiteMapManager.AddItem(value);
        }

        // PUT api/<controller>/5
        /// <summary>
        /// Puts the specified identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        /// <exception cref="HttpResponseException"></exception>
        public SiteMap Put(string id, [FromBody]SiteMap value)
        {
            return SiteMapManager.UpdateItem(value);
        }

        // DELETE api/<controller>/5
        /// <summary>
        /// Deletes the specified identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        public void Delete(Guid id, int CompanyID)
        {
            SiteMapManager.DeleteItem(id,CompanyID);
        }
    }
}