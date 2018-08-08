using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DTP.Data;
using DTP.Core;
namespace RBVH.HR.Models.TMS
{
    public class ls_PayrollDOWS_RBVH:BaseDBEntity
    {
        public int Dow_ID { get; set; }
        public string Dow_Code{ get; set; }
        public DateTime Beg_Day { get; set; }
        public DateTime End_Day { get; set; }
        public int Dow_Num { get; set; }
        public int ENtityID { get; set; }
        public DateTime? TranLeaveOT_Day { get; set; }
        public DateTime? FillUnNoReg_Day { get; set; }
    }
    public class ls_PayrollDOWS_RBVHCollection : BaseDBEntityCollection<ls_PayrollDOWS_RBVH> { }
    public class ls_PayrollDOWS_RBVHManager
    {
        public static  IEnumerable<ls_PayrollDOWS_RBVH> GetSearch(SearchFilter value)
        {
            using (var reader = SqlHelper.ExecuteReader("ls_PayrollDOWS_RBVH", SearchFilterManager.SqlSearchDynParam(value)))
            {
                return CommonHelper.DataReaderToList<ls_PayrollDOWS_RBVH>(reader);
            }
        }
    }
}
