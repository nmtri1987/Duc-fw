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
    public class T_CMS_Master_WorkHoursController : ApiController
    {
        // GET api/<controller>
        /// <summary>
        /// Gets this instance.
        /// </summary>
        /// <returns></returns>
        public T_CMS_Master_WorkHoursCollection Get()
        {
            return T_CMS_Master_WorkHoursManager.GetAllItem();
        }

		public T_CMS_Master_WorkHoursCollection GetbyUser(string usr)
        {
            return T_CMS_Master_WorkHoursManager.GetbyUser(usr);
        }

		 

		

        // GET api/<controller>/5
        /// <summary>
        /// Gets the specified COM group identifier.
        /// </summary>
        /// <param name="T_CMS_Master_WorkHoursId">The COM group identifier.</param>
        /// <returns></returns>
        public T_CMS_Master_WorkHours Get(int WorkHoursID)
        {
            return T_CMS_Master_WorkHoursManager.GetItemByID(WorkHoursID);
        }


        // GET api/<controller>/5
        /// <summary>
        /// Gets the specified COM group identifier.
        /// </summary>
        /// <param name="T_CMS_Master_WorkHoursId">The COM group identifier.</param>
        /// <returns></returns>
        public T_CMS_Master_WorkHours Get(string WorkHours)
        {
            return T_CMS_Master_WorkHoursManager.GetItemByWorkHour(WorkHours);
        }
        // POST api/<controller>
        /// <summary>
        /// Posts the specified value.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        public T_CMS_Master_WorkHours Post([FromBody]T_CMS_Master_WorkHours value)
        {
            return T_CMS_Master_WorkHoursManager.AddItem(value);
        }


        [System.Web.Http.Authorize]
        public IHttpActionResult Post(SearchRequest requestModel)
        {
            //SearchFilter SearchKey = SearchFilter.SearchData(CompanyID, requestModel, "AccountID,Type", "AccountID");
            T_CMS_Master_WorkHoursCollection collection = T_CMS_Master_WorkHoursManager.Search(new SearchFilter()
            {
                CompanyID = 1,
                Keyword = requestModel.Search.Value,
                Page = (requestModel.Start / requestModel.Length) + 1,
                PageSize = requestModel.Length,
                ColumnsName = "WorkHoursID",
                OrderBy = "WorkHoursID",
                OrderDirection = "ASC",
            });
            int TotalRecord = 0;
            if (collection.Count > 0)
            {
                TotalRecord = collection[0].TotalRecord;
            }
            var response = new T_CMS_Master_WorkHoursResponse
            {
                data = collection,
                draw = requestModel.Draw,
                recordsFiltered = TotalRecord,
                recordsTotal = TotalRecord
            };
            return Ok(response);
        }

        // PUT api/<controller>/5
        /// <summary>
        /// Puts the specified identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        /// <exception cref="HttpResponseException"></exception>
        public T_CMS_Master_WorkHours Put(string id, [FromBody]T_CMS_Master_WorkHours value)
        {
            return T_CMS_Master_WorkHoursManager.UpdateItem(value);
        }

		// GET api/<controller>
        /// <summary>
        /// Gets this instance.
        /// </summary>
        /// <returns></returns>
        public T_CMS_Master_WorkHoursCollection Post(string method,[FromBody] SearchFilter value)
        {
            return T_CMS_Master_WorkHoursManager.Search(value);
        }

        // DELETE api/<controller>/5
        /// <summary>
        /// Deletes the specified identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        public void Delete(int id)
        {
            T_CMS_Master_WorkHoursManager.DeleteItem(id);
        }
    }
}