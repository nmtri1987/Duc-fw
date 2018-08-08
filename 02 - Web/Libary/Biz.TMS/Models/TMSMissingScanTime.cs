using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Biz.TMS.Models
{
    public class TMSMissingScanTime : BaseEntity
    {
        public int EntityID { get; set; }
        public string EmployeeNo { get; set; }
        public int EmployeeCode { get; set; }
        public string FirstName_EN { get; set; }
        public string MiddleName_EN { get; set; }
        public string LastName_EN { get; set; }
        public DateTime Work_Date { get; set; }
        public DateTime InTime { get; set; }
        public DateTime OutTime { get; set; }
        public DateTime ST_InTime { get; set; }
        public DateTime ST_OutTime { get; set; }
        public int DirectManager { get; set; }
        public string DomainId { get; set; }

    }
    public class TMSMissingScanTimeCollection : BaseEntityCollection<TMSMissingScanTime> { }
}
