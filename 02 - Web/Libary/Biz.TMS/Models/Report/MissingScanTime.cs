using System.Text;
using System.Data;
using System.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.ComponentModel.DataAnnotations;
using Biz.Core.Attribute;
namespace Biz.TMS.Models
{
    public class MissingScanTime : BaseEntity
    {

        [ColumnAttribute(DataType = "key-new", ActionLink = "Info")]
        public int EmployeeCode { get; set; }
        public string DomainId { get; set; }
        public string EmployeeNo { get; set; }
        public string FirstName_EN { get; set; }
        public string MiddleName_EN { get; set; }
        public string LastName_EN { get; set; }

        [DisplayFormat(DataFormatString = SystemConst.DateFM, ApplyFormatInEditMode = true)]
        public DateTime Work_Date { get; set; }
        public string InTime { get; set; }
        public string OutTime { get; set; }
        public string ST_InTime { get; set; }
        public string ST_OutTime { get; set; }
        public int DirectManager { get; set; }
        public int EntityID { get; set; }


    }
    public class MissingScanTimeCollection : BaseEntityCollection<MissingScanTime> { }
    public class MissCanTimePara
    {
        public int EmployeeCode { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int EntityID { get; set; }
    }
    public class EMPTMSPara
    {
        public int EntityID { get; set; }
        public int DeptID { get; set; }
        public DateTime? FromDate { get; set; }
        public DateTime? ToDate { get; set; }
        public string OrderBy { get; set; }
        public string OrderDirection { get; set; }
        public int Page { get; set; }

        public int PageSize { get; set; }
    }
    
}
