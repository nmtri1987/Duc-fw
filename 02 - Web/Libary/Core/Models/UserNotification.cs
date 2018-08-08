using System.Text;
using System.Data;
using System.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.ComponentModel.DataAnnotations;
//using Server.DAC;
//using Server.Helpers;
namespace Biz.Core.Models
{
//[DataContract]
public class UserNotification :BaseEntity
{

public int CompanyID{ get; set; }

public int NotificationID{ get; set; }

public int ToEmployeeCode{ get; set; }
[DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}", ApplyFormatInEditMode = true)] 
public DateTime NFrom{ get; set; }
[DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}", ApplyFormatInEditMode = true)] 
public DateTime NTo{ get; set; }

public string Subject{ get; set; }

public String Body{ get; set; }
[DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}", ApplyFormatInEditMode = true)] 
public DateTime PublishedDateTime{ get; set; }

public bool IsRead{ get; set; }

public bool IsPublished{ get; set; }

public int CreatedUser{ get; set; }

public DateTime CreatedDate{ get; set; }

public string LastModifiedUser{ get; set; }
[DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}", ApplyFormatInEditMode = true)] 
public DateTime LastModifiedDate{ get; set; }

}
public class UserNotificationCollection : BaseEntityCollection<UserNotification> { }
}