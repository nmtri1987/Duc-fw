
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
//using DTP.Data;
//using ifinds.Object.OG.Models;
using RBVH.HR.Models;
using DTP.Data.Models;
namespace RBVH.HR.Controllers
{
    /// <summary>
    ///
    /// </summary>
    /// <seealso cref="System.Web.Http.ApiController" />
    public class ContractController : ApiController
    {
        // GET api/<controller>
        /// <summary>
        /// Gets this instance.
        /// </summary>
        /// <returns></returns>
        public T_CMS_Master_ContractCollection Get(bool isActive)
        {

            return T_CMS_Master_ContractManager.GetAllItem(isActive);
        }

		public T_CMS_Master_ContractCollection GetbyUser(string usr)
        {
            return T_CMS_Master_ContractManager.GetbyUser(usr);
        }

        public QueuedEmailCollection getReminderContract(int CompanyID)
        {
            return T_CMS_Master_ContractManager.ReminderContractEmailList(CompanyID);
        }
        // GET api/<controller>/5
        /// <summary>
        /// Gets the specified COM group identifier.
        /// </summary>
        /// <param name="T_CMS_Master_ContractId">The COM group identifier.</param>
        /// <returns></returns>
        public T_CMS_Master_Contract Get(int ID)
        {
            return T_CMS_Master_ContractManager.GetItemByID(ID);
        }

        public T_CMS_Master_Contract Get(int EmployeeCode,string Type)
        {
            return T_CMS_Master_ContractManager.GetItemByEmployeeCode(EmployeeCode);
        }
        public System.Data.DataTable Get(int EntityID, DateTime? ExeDate, bool IsActiveEmp, string Type="")
        {
            string ReType = Type.ToLower();
            if (ReType == "apendix")
            {
                return T_CMS_Master_ContractManager.Report_AppendixHistory(EntityID, ExeDate, IsActiveEmp, null);
            }
            else if(ReType == "ctractpending")
            {
                return T_CMS_Master_ContractManager.Report_ContractLevelPending(EntityID, ExeDate, IsActiveEmp, null);
            }
            else
            {
                return T_CMS_Master_ContractManager.Report_ContractHistory(EntityID, ExeDate, IsActiveEmp, null);
            }
        }
        
        // POST api/<controller>
        /// <summary>
        /// Posts the specified value.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        public T_CMS_Master_Contract Post([FromBody]T_CMS_Master_Contract value)
        {
            return T_CMS_Master_ContractManager.AddItem(value);
        }

        // PUT api/<controller>/5
        /// <summary>
        /// Puts the specified identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        /// <exception cref="HttpResponseException"></exception>
        public T_CMS_Master_Contract Put(string id, [FromBody]T_CMS_Master_Contract value)
        {
            //if (value.LabourDOI.ToString().IndexOf("01/01/0001") == -1)
            //{
            //    value.LabourDOI = null;
            //}
            //if (value.PassportDOI.ToString().IndexOf("01/01/0001") == -1)
            //{
            //    value.PassportDOI = null;
            //}
            //if (value.OriginalDate.ToString().IndexOf("01/01/0001") == -1)
            //{
            //    value.OriginalDate = null;
            //}
            return T_CMS_Master_ContractManager.UpdateItem(value);
        }

		// GET api/<controller>
        /// <summary>
        /// Gets this instance.
        /// </summary>
        /// <returns></returns>
        public T_CMS_Master_ContractCollection Post(string method,[FromBody] SearchFilter value)
        {
            return T_CMS_Master_ContractManager.Search(value);
        }

        
        // DELETE api/<controller>/5
        /// <summary>
        /// Deletes the specified identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        public void Delete(int id)
        {
            T_CMS_Master_ContractManager.DeleteItem(id);
        }
    }
}