using Biz.Core.Attribute;
using System;
using System.ComponentModel.DataAnnotations;

//using Server.DAC;
//using Server.Helpers;
namespace LV.TMS.Models
{
    //[DataContract]
    public class hrm_atd_ScanTime : BaseEntity
    {
        //[LocalizedDisplayName("UserName")]
        //[Display(Name = "UserName")]
        //[ColumnAttribute(DataType = "albool")]
        //public bool Select { set; get; }
        private string _select;
        [ColumnAttribute(DataType = "alboolfnc", ActionLink ="selectRow")]
        public string Select { get
            {
                if (string.IsNullOrEmpty(_select) )
                {
                    bool allowEdit = true;

                    if (!string.IsNullOrEmpty(Status))
                    {
                        if (Status.ToLower() == "approved" || Status.ToLower() == "submitted")
                        {
                            allowEdit = false;
                        }
                    }

                    string data = @"[{""EmployeeNo"":"""+ EmployeeNo + @""",""AllowEdit"":"""+ allowEdit + @""",""isWK"":"""+isWeekend+ @""",""isHLD"":""" + IsHoliday + @"""}]";
                    //data ="[{"+ string.Format(data, EmployeeNo, true)"}]";
                    return  data;
                }
                return _select;
            }
            set { _select = value; }
        }
        [ColumnAttribute(Hide = true)]
        public int EmployeeCode { get; set; }
        //[ColumnAttribute(DataType = "albool")]
        //public bool Cancel { set; get; }
        public string EmployeeNo { get; set; }

        public string EmployeeName { get; set; }

        [DisplayFormat(DataFormatString = SystemConst.DateFM, ApplyFormatInEditMode = true)]
        [ColumnAttribute(DataType = "dateF")]
        [DataType(DataType.Date)]
        //[ColumnAttribute(DataType = "dateF")]
        public DateTime WorkDate { get; set; }

        public string Leave_OT { set; get; }

        //[ColumnAttribute(Hide = true)]
        [LocalizedDisplayName("Raw_In")]
        public string Raw_In { set; get; }

        //[ColumnAttribute(Hide = true)]
        [LocalizedDisplayName("Raw_Out")]
        public string Raw_Out { set; get; }

       
        [ColumnAttribute(DataType = "timetext")]
        public string Manual_In { set; get; }

        [ColumnAttribute(DataType = "timetext")]
        public string Manual_Out { set; get; }

        public string Hour { set; get; }

        public string Status { set; get; }

        

     

        [ColumnAttribute(Hide = true)]
        public Guid ID { get; set; }

     
        //[LocalizedDisplayName("Submit")]

        [ColumnAttribute(Hide = true)]
        public bool isWeekend { get;set;}

        [ColumnAttribute(Hide = true)]
        public bool IsHoliday { get; set; }

        [ColumnAttribute(DataType = "ipt", ClassName = "rqtNote")]
        public string Requestor_Note { set; get; }

        [ColumnAttribute(DataType = "ipt")]
        public string Approver_Note { set; get; }

    }

    public class hrm_atd_ScanTimeCollection : BaseEntityCollection<hrm_atd_ScanTime> { }

    public class ScanTimeFilter
    {
        public string Keyword { get; set; }
        public string ColumnsName { get; set; }
        public string OrderBy { get; set; }
        public string OrderDirection { get; set; }
        public int Page { get; set; }
        public int PageSize { get; set; }

        public int CompanyID { get; set; }

        public int EmployeeCode { get; set; }
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }

        public string Condition { get; set; }
    }

    public class TimeSheetearchpara
    {
        public string EntityId { get; set; }
        public string EmpCode { get; set; }
        public string EmpName { get; set; }

        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}", ApplyFormatInEditMode = true)]
        [LocalizedDisplayName("FromDate")]
        public DateTime FromDate { get; set; }

        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}", ApplyFormatInEditMode = true)]
        [LocalizedDisplayName("ToDate")]
        public DateTime ToDate { get; set; }

        public string EmpTypeID { get; set; }
        public string LocationID { get; set; }

        [LocalizedDisplayName("OnlyshowMissing")]
        public bool ShowMissing { get; set; }
    }
}