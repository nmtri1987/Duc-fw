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

public int Id{ get; set; }

public int EmployeeCode{ get; set; }

public int LeaveTypeId{ get; set; }
[DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}", ApplyFormatInEditMode = true)] 
public DateTime StartDate{ get; set; }
[DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}", ApplyFormatInEditMode = true)] 
public DateTime EndDate{ get; set; }

public DateTime FromTime { get; set; }

public DateTime ToTime { get; set; }

public decimal NoOfDays{ get; set; }

public decimal NoOfHours{ get; set; }

public int HalfDayPart{ get; set; }

public string Remarks{ get; set; }

public string ApproverComments{ get; set; }

public int StatusId{ get; set; }

public int ApprovalLevel{ get; set; }

public bool IsUpdatedByAdmin{ get; set; }

public bool IsOverriddenLeave{ get; set; }

public int OverriddenLeaveId{ get; set; }

public bool IsNC{ get; set; }

public int OldStatusId{ get; set; }

public int OldApprovalLevelId{ get; set; }

public bool IsAcitve{ get; set; }

public int CreatedBy{ get; set; }

public DateTime CreatedDate{ get; set; }

public int ModifiedBy{ get; set; }
[DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}", ApplyFormatInEditMode = true)] 
public DateTime ModifiedDate{ get; set; }

public int Ref_RequestId{ get; set; }

public int UploadStatus{ get; set; }
[DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}", ApplyFormatInEditMode = true)] 
public DateTime LastUploadDate{ get; set; }
[DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}", ApplyFormatInEditMode = true)] 
public DateTime LastUploadedToDate{ get; set; }

public string NextLevelId{ get; set; }

public string NextApproverRoleId{ get; set; }
[DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}", ApplyFormatInEditMode = true)] 
public DateTime CancelRequestCreatedOn{ get; set; }

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