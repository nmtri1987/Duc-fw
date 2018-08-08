using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Biz.TMS.Models
{
    public class EmpTaskFilter
    {
        public string TaskType { get; set; }
        public int EntityID { get; set; }
        public int? WorkingTimeGroupID { get; set; }
        public int EmployeeCode { get; set; }
        public DateTime DateID { get; set; }
    }
}
