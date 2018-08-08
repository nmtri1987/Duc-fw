using System;
using System.Web.Http;
using LV.TMS.Models;

namespace LV.TMS.Controllers
{
    /// <summary>
    ///
    /// </summary>
    /// <seealso cref="System.Web.Http.ApiController" />
    public class hrm_atd_ScanTimeController : ApiController
    {
        // GET api/<controller>
        /// <summary>
        /// Gets this instance.
        /// </summary>
        /// <returns></returns>
        public hrm_atd_ScanTimeCollection Get()
        {
            return hrm_atd_ScanTimeManager.GetAllItem();
        }

        public hrm_atd_ScanTimeCollection GetbyUser(string usr)
        {
            return hrm_atd_ScanTimeManager.GetbyUser(usr);
        }

        // GET api/<controller>/5
        /// <summary>
        /// Gets the specified COM group identifier.
        /// </summary>
        /// <param name="hrm_atd_ScanTimeId">The COM group identifier.</param>
        /// <returns></returns>
        public hrm_atd_ScanTime Get(Guid ID)
        {
            return hrm_atd_ScanTimeManager.GetItemByID(ID);
        }

        // POST api/<controller>
        /// <summary>
        /// Posts the specified value.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        public hrm_atd_ScanTime Post([FromBody]hrm_atd_ScanTime value, string action)
        {
            if (action == "submit")
            {
                return hrm_atd_ScanTimeManager.Submit(value);
            }
            return null;
        }

        // PUT api/<controller>/5
        /// <summary>
        /// Puts the specified identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        /// <exception cref="HttpResponseException"></exception>
        public hrm_atd_ScanTime Put(string id, [FromBody]hrm_atd_ScanTime value)
        {
            return hrm_atd_ScanTimeManager.UpdateItem(value);
        }

        // GET api/<controller>
        /// <summary>
        /// Gets this instance.
        /// </summary>
        /// <returns></returns>
        public hrm_atd_ScanTimeCollection Post(string method, [FromBody] ScanTimeFilter value)
        {
            return hrm_atd_ScanTimeManager.Search(value);
        }

        // DELETE api/<controller>/5
        /// <summary>
        /// Deletes the specified identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        public void Delete(Guid id)
        {
            hrm_atd_ScanTimeManager.DeleteItem(id);
        }
    }
}