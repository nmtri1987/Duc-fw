using System;

namespace LV.TMS.Models
{
    using System.Runtime.Serialization;
    /// <summary>
    /// Use to map to hrm_atd_Scantime_ApprovalDetails entities
    /// </summary>
    [DataContract]
    public class ScanTimeApprovalReceiver : BaseDBEntity
    {
        [DataMember]
        public int EmployeeCode { get; set; }
        [DataMember]
        public string EmployeeNo { get; set; }
        [DataMember]
        public string EmployeeName { get; set; }
        [DataMember]
        public string EmployeeNameVN { get; set; }
        [DataMember]
        public DateTime WorkDate { get; set; }
        [DataMember]
        public string RootIn { get; set; }
        [DataMember]
        public string RootOut { get; set; }
        [DataMember]
        public string ManualIn { get; set; }
        [DataMember]
        public string ManualOut { get; set; }
        [DataMember]
        public bool ScanIn { get; set; }
        [DataMember]
        public bool ScanOut { get; set; }
        [DataMember]
        public string ShiftCode { get; set; }
        [DataMember]
        public string Leave { get; set; }
        [DataMember]
        public string OverTime { get; set; }
        [DataMember]
        public int IsApproved { get; set; }
        [DataMember]
        public int IsSubmited { get; set; }
        [DataMember]
        public bool IsRejected { get; set; }
        [DataMember]
        public string StatusName { get; set; }
        [DataMember]
        public string RequestorNote { get; set; }
        [DataMember]
        public string ApproverNote { get; set; }
        [DataMember]
        public int IsHoliday { get; set; }
        [DataMember]
        public bool IsWeekend { get; set; }
        [DataMember]
        public int OverDue { get; set; }
        [DataMember]
        public int RowNum { get; set; }
        [DataMember]
        public string TimeIn { get; set; }
        [DataMember]
        public string TimeOut { get; set; }
    }

    [DataContract]
    public class TimesheetEntryReceiver : BaseDBEntity
    {
        [DataMember]
        public int EmployeeCode { get; set; }
        [DataMember]
        public DateTime WorkDate { get; set; }
        [DataMember]
        public string ManualIn { get; set; }
        [DataMember]
        public string ManualOut { get; set; }
        [DataMember]
        public string RequestorNote { get; set; }
        [DataMember]
        public string ApproverNote { get; set; }

    }

    public class TimesheetCollection : BaseDBEntityCollection<TimesheetEntryReceiver>
    {
        
        //public int UserLoggedIn { get; set; }
        //public bool IsApproved { get; set; }
    }
    public class TimesheetApproveReject
    {
        public TimesheetApproveReject()
        {
            this.TimesheetEntryCollection = new TimesheetCollection();
        }
        public TimesheetCollection TimesheetEntryCollection { get; set; }
        public int UserLoggedIn { get; set; }
        public bool IsApproved { get; set; }
    }
}
