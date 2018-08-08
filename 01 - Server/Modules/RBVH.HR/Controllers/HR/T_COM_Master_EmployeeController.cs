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
    public class T_COM_Master_EmployeeController  : ApiController// BaseApi<T_COM_Master_Employee>
    {
        // GET api/<controller>
        /// <summary>
        /// Gets this instance.
        /// </summary>
        /// <returns></returns>
        public T_COM_Master_EmployeeCollection Get()
        {
            return T_COM_Master_EmployeeManager.GetAllItem();
        }

        public T_COM_Master_EmployeeCollection GetbyUser(string usr)
        {
            return T_COM_Master_EmployeeManager.GetbyUser(usr);
        }



        public System.Data.DataTable GetReport(string EntityID,string Name, string RefID)
        {
            System.Data.DataTable dt = new System.Data.DataTable();
            DateTime date = DateTime.Now;
            if (!string.IsNullOrEmpty(RefID))
            {
                date = DateTime.Parse(RefID);
            }
            switch (Name)
            {
                case "emplist":
                    dt = T_COM_Master_EmployeeManager.GetbyEntity(EntityID, date);
                    break;

                        default:
                    break;
            }
            return dt;
        }

        // GET api/<controller>/5
        /// <summary>
        /// Gets the specified COM group identifier.
        /// </summary>
        /// <param name="T_COM_Master_EmployeeId">The COM group identifier.</param>
        /// <returns></returns>
        public T_COM_Master_Employee Get(int EmployeeCode)
        {
            return T_COM_Master_EmployeeManager.GetItemByID(EmployeeCode);
        }


        // GET api/<controller>/5
        /// <summary>
        /// Gets the specified COM group identifier.
        /// </summary>
        /// <param name="T_COM_Master_EmployeeId">The COM group identifier.</param>
        /// <returns></returns>
        public T_COM_Master_Employee Get(string DomainId)
        {
            return T_COM_Master_EmployeeManager.GetItemByDomainId(DomainId);
        }

        // POST api/<controller>
        /// <summary>
        /// Posts the specified value.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        public T_COM_Master_Employee Post([FromBody]T_COM_Master_Employee value)
        {
            return T_COM_Master_EmployeeManager.AddItem(value);
        }

        // PUT api/<controller>/5
        /// <summary>
        /// Puts the specified identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        /// <exception cref="HttpResponseException"></exception>
        public T_COM_Master_Employee Put(string id, [FromBody]T_COM_Master_Employee value)
        {
            return T_COM_Master_EmployeeManager.UpdateItem(value);
        }

        // GET api/<controller>
        /// <summary>
        /// Gets this instance.
        /// </summary>
        /// <returns></returns>
        public T_COM_Master_EmployeeCollection Post(string method, [FromBody] RBVHSearchFilter value)
        {
            return T_COM_Master_EmployeeManager.Search(value);
        }

        // DELETE api/<controller>/5
        /// <summary>
        /// Deletes the specified identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        public void Delete(int id)
        {
            T_COM_Master_EmployeeManager.DeleteItem(id);
        }
    }
}