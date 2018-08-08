using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Biz.TMS.Models
{
    public class REmployee : BaseEntity
    {
        public int EntityID { get; set; }
        public int EmployeeCode { get; set; }
        public string EmployeeNo { get; set; }
        public string EmployeeName_EN { get; set; }
        public string Email { get; set; }
        public int DirectManager { get; set; }
        public string DirectManagerNo { get; set; }
        public string DirectManagerName { get; set; }
        public int IndirectManager { get; set; }
        public string InDirectManagerNo { get; set; }
        public string InDirectManagerName { get; set; }
        public int GroupCode { get; set; }
        public string GroupName { get; set; }
        public int DeptCode { get; set; }
        public string DeptName { get; set; }
        public int PositionID { get; set; }
        public string PositionName { get; set; }
        public bool IsScan { get; set; }
        public string RangeDate { get; set; }
    }
    public class REmployeeCollections : BaseEntityCollection<REmployee> { }

    public class REmployeePara
    {
        public int EntityID { get; set; }
        public DateTime FromDate { get; set; }
        public int NumOfDay { get; set; }
    }
    public class EmpReminderModel
    {
        public string ToName { get; set; }
        public REmployeeCollections EmployeeList { get; set; }
    }
}
