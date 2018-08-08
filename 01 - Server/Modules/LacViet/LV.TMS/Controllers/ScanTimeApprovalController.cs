namespace LV.TMS.Controllers
{
    using System.Web.Http;
    using Models;

    public class ScanTimeApprovalController : ApiController
    {
        public ScanTimeApprovalCollection Post([FromBody] ScanTimeApprovalSqlParameters value)
        {
            return ScanTimeApprovalManager.GetAllItemByManager(value);
        }
        public int Put([FromBody]TimesheetApproveReject value)
        {
            return ScanTimeApprovalManager.UpdateItems(value);
        }
    }
}
