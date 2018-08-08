using System.Text;
using System.Data;
using System.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.ComponentModel.DataAnnotations;
//using Server.DAC;
//using Server.Helpers;
namespace Biz.TMS.Models
{
//[DataContract]
public class T_LMS_Trans_LeaveStory :BaseEntity
{

































































}
public class T_LMS_Trans_LeaveStoryCollection : BaseEntityCollection<T_LMS_Trans_LeaveStory> { }

    public class LeaveWF : BaseEntity
    {
        
        public int ID { get; set; }
        
        public int EmployeeCode { get; set; }
        
        public string EmployeeNo { get; set; }
        
        public string EmployeeName_EN { get; set; }
        

        public DateTime StartDate { get; set; }
        
        public DateTime EndDate { get; set; }
        
        public string FullName_EN { get; set; }
        
        public string StatusName { get; set; }
        
        public int ApprovalLevel { get; set; }
        
        public int DirectManagerCode { get; set; }
        
        public string DirectManagerNo { get; set; }
        
        public string DirectManagerName { get; set; }
        
        public int InDirectManagerCode { get; set; }
        
        public string InDirectManagerNo { get; set; }
        
        public string InDirectManagerName { get; set; }
        
        public string RoleFullName { get; set; }
        
        public string Description { get; set; }
        
        public DateTime CreatedDate { get; set; }
    }
    public class LeaveWFCollection : BaseEntityCollection<LeaveWF> { }
    public class LeaveWFPara
    {
        public int EntityID { get; set; }
        public int EmployeeCode { get; set; }
        public DateTime WorkDate { get; set; }
    }
}