using daitiphu.common.Directory;

using System.Web.Http;

namespace ifinds.Api.Controllers
{
    public class CurrencyController : ApiController
    {
        // GET: api/Currency
        public tblCurrencyCollection Get()
        {
            return tblCurrencyManager.GetAllItem();
        }

        // GET: api/Currency/5
        public tblCurrency Get(int id)
        {
            return tblCurrencyManager.GetItemByID(id);
        }

        // POST: api/Currency
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/Currency/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/Currency/5
        public void Delete(int id)
        {
        }
    }
}
