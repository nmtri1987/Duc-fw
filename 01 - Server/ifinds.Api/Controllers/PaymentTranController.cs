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
    public class PaymentTranController : ApiController
    {
        // GET api/<controller>
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<controller>/5
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<controller>
        /// <summary>
        /// Posts the specified value.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        public PaymentTranCollection PostFilter([FromBody]UserTranFilter model, string filter)
        {
            return PaymentTranManager.GetbyUserPaymentTran(model);
        }


        public PaymentTran GetRefNbr(String RefNbr)
        {
            return PaymentTranManager.GetItemByRefNbr(RefNbr);
        }
        // POST api/<controller>
        public void Post([FromBody]string value)
        {
        }

        // PUT api/<controller>/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/<controller>/5
        public void Delete(int id)
        {
        }
    }
}