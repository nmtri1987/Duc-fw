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
    public class EmpBudgetScheduleController : ApiController
    {
        // GET api/<controller>
        /// <summary>
        /// Gets this instance.
        /// </summary>
        /// <returns></returns>
        public EmpBudgetScheduleCollection Get()
        {
            return EmpBudgetScheduleManager.GetAllItem();
        }


        // GET api/<controller>
        /// <summary>
        /// Gets this instance.
        /// </summary>
        /// <returns></returns>
        public EmpBudgetScheduleCollection Getmysch(string usr, string tp)
        {
            EmpBudgetScheduleCollection mycol = new DTP.Object.EmpBudgetScheduleCollection();
            switch (tp)
            {
                case "inc":
                    mycol = EmpBudgetScheduleManager.GetAllByUser(usr,1);
                    break;
                case "mypmnt":
                    mycol = EmpBudgetScheduleManager.GetAllByUser(usr,2);
                    break;
                default:
                    mycol = EmpBudgetScheduleManager.GetAllByUser(usr,-1);
                    break;
            }
            return mycol;
        }

        // GET api/<controller>/5
        /// <summary>
        /// Gets the specified COM group identifier.
        /// </summary>
        /// <param name="EmpBudgetScheduleId">The COM group identifier.</param>
        /// <returns></returns>
        public EmpBudgetSchedule Get(string Code)
        {
            return EmpBudgetScheduleManager.GetItemByID(Code);
        }

        // POST api/<controller>
        /// <summary>
        /// Posts the specified value.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        public EmpBudgetSchedule Post([FromBody]EmpBudgetSchedule value)
        {
            return EmpBudgetScheduleManager.AddItem(value);
        }

        // PUT api/<controller>/5
        /// <summary>
        /// Puts the specified identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        /// <exception cref="HttpResponseException"></exception>
        public EmpBudgetSchedule Put(string id, [FromBody]EmpBudgetSchedule value)
        {
            return EmpBudgetScheduleManager.UpdateItem(value);
        }

        // DELETE api/<controller>/5
        /// <summary>
        /// Deletes the specified identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        public void Delete(string id)
        {
            EmpBudgetScheduleManager.DeleteItem(id);
        }
    }
}