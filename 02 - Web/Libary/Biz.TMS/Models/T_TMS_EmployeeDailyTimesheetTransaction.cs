using System;
using System.ComponentModel.DataAnnotations;
using Biz.Core.Attribute;
namespace Biz.TMS
{
    public class T_TMS_EmployeeDailyTimesheetTransaction : BaseEntity
    {
        [LocalizedDisplayNameAttribute("EmployeeCode")]
        public int EmployeeCode { get; set; }

        [LocalizedDisplayNameAttribute("DateID")]
        [ColumnAttribute(DataType= "dateF")]
        public DateTime DateID { get; set; }

       

        [LocalizedDisplayNameAttribute("CompanyID")]
        [ColumnAttribute(Hide =true)]
        public int CompanyID { get; set; }

        [LocalizedDisplayNameAttribute("ID")]
        [ColumnAttribute(Hide = true)]
        public Guid ID { get; set; }

        [LocalizedDisplayNameAttribute("RefNo")]
        [ColumnAttribute(Hide = true)]
        public Guid RefNo { get; set; }

        


        [LocalizedDisplayNameAttribute("IsWeekend")]
        [ColumnAttribute(Hide = true)]
        public bool IsWeekend { get; set; }

        [LocalizedDisplayNameAttribute("IsHoliday")]
        [ColumnAttribute(Hide = true)]
        public bool IsHoliday { get; set; }

      

        [LocalizedDisplayNameAttribute("IsScan")]
        [ColumnAttribute(Hide = true)]
        public bool IsScan { get; set; }

        [LocalizedDisplayNameAttribute("RawIn")]
        [DisplayFormat(DataFormatString = SystemConst.DateFM, ApplyFormatInEditMode = true)]
        [DataType(DataType.Date)]
        [ColumnAttribute(DataType = "time")]
        public DateTime? RawIn { get; set; }


        [LocalizedDisplayNameAttribute("RawOut")]
        [DisplayFormat(DataFormatString = SystemConst.DateFM, ApplyFormatInEditMode = true)]
        [DataType(DataType.Date)]
        [ColumnAttribute(DataType = "time")]
        public DateTime? RawOut { get; set; }

        [LocalizedDisplayNameAttribute("ManualIn")]
        [DisplayFormat(DataFormatString = SystemConst.DateFM, ApplyFormatInEditMode = true)]
        [DataType(DataType.Date)]
        [ColumnAttribute(DataType = "timetext")]
        public DateTime? ManualIn { get; set; }

        [LocalizedDisplayNameAttribute("ManualOut")]
        [DisplayFormat(DataFormatString = SystemConst.DateFM, ApplyFormatInEditMode = true)]
        [DataType(DataType.Date)]
        [ColumnAttribute(DataType = "timetext")]
        public DateTime? ManualOut { get; set; }

        [LocalizedDisplayNameAttribute("TotalWorkedHour")]

        public float TotalWorkedHour { get; set; }

        [LocalizedDisplayNameAttribute("KowID")]
        [ColumnAttribute(Hide = true)]
        public int KowID { get; set; }

        [LocalizedDisplayNameAttribute("DayNum")]
        [ColumnAttribute(Hide = true)]
        public float DayNum { get; set; }

        [LocalizedDisplayNameAttribute("DowID")]
        [ColumnAttribute(Hide = true)]
        public int DowID { get; set; }

        [LocalizedDisplayNameAttribute("ScreenID")]
        [ColumnAttribute(Hide = true)]
        public string ScreenID { get; set; }

        [LocalizedDisplayNameAttribute("CreatedDate")]
        [ColumnAttribute(Hide = true)]
        public DateTime CreatedDate { get; set; }

        [LocalizedDisplayNameAttribute("CreatedUser")]
        [ColumnAttribute(Hide = true)]
        public string CreatedUser { get; set; }

        [LocalizedDisplayNameAttribute("ModifiedDate")]
        [DisplayFormat(DataFormatString = SystemConst.DateFM, ApplyFormatInEditMode = true)]
        [ColumnAttribute(Hide = true)]
        [DataType(DataType.Date)]
        public DateTime ModifiedDate { get; set; }


        [LocalizedDisplayNameAttribute("ModifiedUser")]
        [ColumnAttribute(Hide = true)]
        public string ModifiedUser { get; set; }
        
        [ColumnAttribute(DataType = "albool")]
        public bool Submit { get; set; }


        [ColumnAttribute(DataType = "albool")]
        public bool Cancel { get; set; }


        
        public string  RequestNote { get; set; }        public string ApproverNote { get; set; }

    }
    public class T_TMS_EmployeeDailyTimesheetTransactionCollection : BaseEntityCollection<T_TMS_EmployeeDailyTimesheetTransaction> { }
}

//namespace Biz.TMS.Controllers
//{
//    public class T_TMS_EmployeeDailyTimesheetTransactionController : BaseController<T_TMS_EmployeeDailyTimesheetTransaction> {}
//}