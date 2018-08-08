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
    public class EmpTransController : ApiController
    {
        // GET api/<controller>
        /// <summary>
        /// Gets this instance.
        /// </summary>
        /// <returns></returns>
        public EmpTransCollection Get()
        {
            return EmpTransManager.GetAllItem();
        }

		public EmpTransCollection GetbyUser(string usr)
        {
            return EmpTransManager.GetbyUser(usr);
        }

        // GET api/<controller>/5
        /// <summary>
        /// Gets the specified COM group identifier.
        /// </summary>
        /// <param name="EmpTransId">The COM group identifier.</param>
        /// <returns></returns>
        public EmpTrans Get(String TranCode)
        {
            return EmpTransManager.GetItemByID(TranCode);
        }

        // POST api/<controller>
        /// <summary>
        /// Posts the specified value.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        public EmpTrans Post([FromBody]EmpTrans value)
        {
            return EmpTransManager.AddItem(value);
        }

        // PUT api/<controller>/5
        /// <summary>
        /// Puts the specified identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        /// <exception cref="HttpResponseException"></exception>
        public EmpTrans Put(string id, [FromBody]EmpTrans value)
        {
            return EmpTransManager.UpdateItem(value);
        }

        // DELETE api/<controller>/5
        /// <summary>
        /// Deletes the specified identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        public void Delete(String id)
        {
            EmpTransManager.DeleteItem(id);
        }
    }
}