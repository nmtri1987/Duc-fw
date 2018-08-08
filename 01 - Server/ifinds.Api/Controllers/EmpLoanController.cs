using DTP.Object;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using DTP.Object.Personal;
namespace ifinds.Api.Controllers
{
    /// <summary>
    ///
    /// </summary>
    /// <seealso cref="System.Web.Http.ApiController" />
    public class EmpLoanController : ApiController
    {
        // GET api/<controller>
        /// <summary>
        /// Gets this instance.
        /// </summary>
        /// <returns></returns>
        public EmpLoanCollection Get()
        {
            return EmpLoanManager.GetAllItem();
        }

        public LoanCalCollection GetCalEmpLoan(string CalCode)
        {
            return EmpLoanManager.CalEmpLoan(EmpLoanManager.GetItemByID(CalCode));
        }

        public EmpLoanCollection GetByUser(string UserName)
        {
            return EmpLoanManager.GetAllItembyCreatedUser(UserName);
        }
        // GET api/<controller>/5
        /// <summary>
        /// Gets the specified COM group identifier.
        /// </summary>
        /// <param name="EmpLoanId">The COM group identifier.</param>
        /// <returns></returns>
        public EmpLoan Get(string LoanCode)
        {
            return EmpLoanManager.GetItemByID(LoanCode);
        }

        // POST api/<controller>
        /// <summary>
        /// Posts the specified value.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        public EmpLoan Post([FromBody]EmpLoan value)
        {
            return EmpLoanManager.AddItem(value);
        }

        // PUT api/<controller>/5
        /// <summary>
        /// Puts the specified identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        /// <exception cref="HttpResponseException"></exception>
        public EmpLoan Put(string id, [FromBody]EmpLoan value)
        {
            return EmpLoanManager.UpdateItem(value);
        }

        // DELETE api/<controller>/5
        /// <summary>
        /// Deletes the specified identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        public void Delete(string id)
        {
            EmpLoanManager.DeleteItem(id);
        }
    }
}