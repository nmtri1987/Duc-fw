namespace LV.TMS.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using Biz.Core.Attribute;

    public class ScanTimeApproval : BaseEntity
    {
        #region Hidden columns
        [Column(DataType = "hiddencol")]
        public int EmployeeCode { get; set; }
        [Column(Hide = true)]
        public string EmployeeName { get; set; }
        [Column(DataType = "hiddencol")]//[Column(Hide = true)]
        public string ManualIn { get; set; }
        [Column(DataType = "hiddencol")]
        //[Column(Hide = true)]
        public string ManualOut { get; set; }
        [Column(Hide = true)]
        public bool ScanIn { get; set; }
        [Column(Hide = true)]
        public bool ScanOut { get; set; }
        [Column(Hide = true)]
        public string ShiftCode { get; set; }
        [Column(Hide = true)]
        public int IsApproved { get; set; }
        [Column(Hide = true)]
        public int IsSubmited { get; set; }
        [Column(Hide = true)]
        public bool IsRejected { get; set; }
        [Column(Hide = true)]
        public int IsHoliday { get; set; }
        [Column(Hide = true)]
        public bool IsWeekend { get; set; }
        [Column(Hide = true)]
        public int OverDue { get; set; }
        [Column(Hide = true)]
        public int RowNum { get; set; }
        #endregion
        [Column(Hide = true)]
        private string _select;
        [ColumnAttribute(DataType = "alboolfnc", ActionLink = "selectRow")]
        public string Select
        {
            get {
                if (string.IsNullOrEmpty(_select))
                {
                    bool allowEdit = false;

                    if (!string.IsNullOrEmpty(StatusName))
                    {
                        if (StatusName.ToLower() == "submitted")
                        {
                            allowEdit = true;
                        }
                    }

                    string data = @"[{""EmployeeNo"":""" + EmployeeNo + @""",""AllowEdit"":""" + allowEdit + @""",""isWK"":""" + IsWeekend + @""",""isHLD"":""" + IsHoliday + @"""}]";
                    //data ="[{"+ string.Format(data, EmployeeNo, true)"}]";
                    return data;
                }
                return _select;
            }
            set { _select = value; }
        }
        [Column(DataType = "string")]
        public string EmployeeNo { get; set; }
        public string EmployeeNameVN { get; set; }
        public DateTime WorkDate { get; set; }
        //[Column(DataType = "string")]
        public string TimeIn { get; set; }
        //[Column(DataType = "string")]
        public string TimeOut { get; set; }
        [Column(DataType = "hiddencol")]
        public string RootIn { get; set; }
        [Column(DataType = "hiddencol")]
        public string RootOut { get; set; }
        [Column(DataType = "hiddencol")]
        public string Manual_in { get; set; }
        [Column(DataType = "hiddencol")]
        public string Manual_out { get; set; }
        public string Leave { get; set; }
        public string OverTime { get; set; }

        public string StatusName { get; set; }
        public string RequestorNote { get; set; }
        [Column(DataType = "ipt", ClassName = "aprNote")]//iptNoDisabled
        public string ApproverNote { get; set; }

    }

    public class ScanTimeApprovalCollection : BaseEntityCollection<ScanTimeApproval> { }

    public class ScanTimeApprovalSqlParameters : SearchFilter
    {
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }
        public int UserLoggedIn { get; set; }
        public bool ShowWaiting { get; set; }
        public int StartRow { get; set; }
        public int EndRow { get; set; }
        public new int OrderDirection { get; set; }
        public string FilterBy { get; set; }
        public bool ShowUnNoReg { get; set; }
    }
    public class SearchParams
    {
        public string EntityId { get; set; }
        public string EmpCode { get; set; }
        public string EmpName { get; set; }
        [LocalizedDisplayName("From Date")]
        [DataType(DataType.Date)]
        //[DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? StartDate { get; set; }
        [LocalizedDisplayName("To Date")]
        [DataType(DataType.Date)]
        //[DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? EndDate { get; set; }
        public string EmpTypeId { get; set; }
        public string LocationId { get; set; }
        [LocalizedDisplayName("Only show waiting for approval")]
        public string ShowMissing { get; set; }
    }

    public class TimesheetEntry : BaseEntity
    {
        public int EmployeeCode { get; set; }
        public DateTime WorkDate { get; set; }
        public string ManualIn { get; set; }
        public string ManualOut { get; set; }
        public string RequestorNote { get; set; }
        public string ApproverNote { get; set; }
        //public int UserLoggedIn { get; set; }
        //public bool IsApproved { get; set; }
    }

    public class TimesheetEntryCollection : BaseEntityCollection<TimesheetEntry>
    {
        //public int UserLoggedIn { get; set; }
        //public bool IsApproved { get; set; }
    }

    public class TimesheetApproveReject
    {
        public TimesheetApproveReject()
        {
            this.TimesheetEntryCollection = new TimesheetEntryCollection();
        }
        public TimesheetEntryCollection TimesheetEntryCollection { get; set; }
        public int UserLoggedIn { get; set; }
        public bool IsApproved { get; set; }
    }
}
