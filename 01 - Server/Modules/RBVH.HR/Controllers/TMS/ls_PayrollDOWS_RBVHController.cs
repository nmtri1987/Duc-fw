using System.Net.Http;
using System.Web.Http;
using RBVH.HR.Models.TMS;
using System.Collections.Generic;
namespace RBVH.HR.Controllers.TMS
{
    public class ls_PayrollDOWS_RBVHController : ApiController
    {
        public IEnumerable<ls_PayrollDOWS_RBVH> Post(string method, [FromBody] SearchFilter value)
        {
            return ls_PayrollDOWS_RBVHManager.GetSearch(value);
        }
    }
}
