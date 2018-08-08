
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

using RBVH.TMS.Ext.Models;

namespace RBVH.TMS.Ext.Controllers
{
    public class ExScanTimeController : ApiController
    {
        // GET api/<controller>
        /// <summary>
        /// Gets this instance.
        /// </summary>
        /// <returns></returns>
        public T_TMS_External_Atd_ScanTimeCollection Get()
        {
            return T_TMS_External_Atd_ScanTimeManager.GetAllItem();
        }

        public T_TMS_External_Atd_ScanTimeCollection GetbyUser(string usr)
        {
            return T_TMS_External_Atd_ScanTimeManager.GetbyUser(usr);
        }





        // GET api/<controller>/5
        /// <summary>
        /// Gets the specified COM group identifier.
        /// </summary>
        /// <param name="T_TMS_External_Atd_ScanTimeId">The COM group identifier.</param>
        /// <returns></returns>
        public T_TMS_External_Atd_ScanTime Get(Guid ID)
        {
            return T_TMS_External_Atd_ScanTimeManager.GetItemByID(ID);
        }

        // POST api/<controller>
        /// <summary>
        /// Posts the specified value.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        public T_TMS_External_Atd_ScanTime Post([FromBody]T_TMS_External_Atd_ScanTime value)
        {
            return T_TMS_External_Atd_ScanTimeManager.AddItem(value);
        }

        // PUT api/<controller>/5
        /// <summary>
        /// Puts the specified identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        /// <exception cref="HttpResponseException"></exception>
        public T_TMS_External_Atd_ScanTime Put(string id, [FromBody]T_TMS_External_Atd_ScanTime value)
        {
            return T_TMS_External_Atd_ScanTimeManager.UpdateItem(value);
        }

        //// GET api/<controller>
        ///// <summary>
        ///// Gets this instance.
        ///// </summary>
        ///// <returns></returns>
        //public T_TMS_External_Atd_ScanTimeCollection Post(string method, [FromBody] SearchFilter value)
        //{
        //    return T_TMS_External_Atd_ScanTimeManager.Search(value);
        //}

        public External_TimesCollection Get(int UserID,string MonthID, string FilterBy, string FilterValue)
        {
            string[] b = MonthID.Split('-');
            DateTime currentdate = DateTime.Now;
            DateTime fromDate = DateTime.Now;
            DateTime toDate = DateTime.Now;
            try
            {
                if (b.Length > 0)
                {
                    currentdate = new DateTime(int.Parse(b[1]), int.Parse(b[0]), 1);
                     fromDate = DTP.Core.DTimeHelper.FirstDayOfMonth(currentdate);
                     toDate = DTP.Core.DTimeHelper.LastDayOfMonth(currentdate);// monthPayrollCirle.EndDate;
                }
            }
            catch { }
            return T_TMS_External_Atd_ScanTimeManager.GetAllItemByMonth(fromDate, toDate, UserID,  FilterBy,  FilterValue);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="UserID">ManagerID</param>
        /// <param name="FD">FromDate</param>
        /// <param name="TD">ToDate</param>
        /// <param name="AID">AssociatateID</param>
        /// <returns></returns>
        public ExternalDailyCollection Get(int UserID, string FD, string TD, int AID)
        {
            return T_TMS_External_Atd_ScanTimeManager.GetExDailyTMS(UserID, FD, TD, AID);
            
        }

        // DELETE api/<controller>/5
        /// <summary>
        /// Deletes the specified identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        public void Delete(Guid id)
        {
            T_TMS_External_Atd_ScanTimeManager.DeleteItem(id);
        }

    }
}
