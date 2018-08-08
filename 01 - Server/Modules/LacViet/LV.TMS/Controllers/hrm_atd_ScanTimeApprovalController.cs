namespace LV.TMS.Controllers
{
    using System.Web.Http;
    using Models;

    public class hrm_atd_ScanTimeApprovalController : ApiController
    {

        public hrm_atd_ScanTimeApprovalCollection Get([FromBody] ScanTimeApprovalSqlParameters value)
        {
            return hrm_atd_ScanTimeApprovalManager.GetAllItem(value);
        }
        public hrm_atd_ScanTimeApproval Put(string id, [FromBody]hrm_atd_ScanTimeApproval value)
        {
            //return hrm_atd_ScanTimeApprovalManager.UpdateItem(value);
            return null;
        }

        // GET api/<controller>
        /// <summary>
        /// Gets this instance.
        /// </summary>
        /// <returns></returns>
        //public hrm_atd_ScanTimeApprovalCollection Post(string method, [FromBody] SearchFilter value)
        //{
        //    return hrm_atd_ScanTimeApprovalManager.Search(value);
        //}
    }
}
