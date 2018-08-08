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
public class UserActivity :BaseEntity
{

public int CompanyID{ get; set; }

public string UserActivityCD{ get; set; }

public int ToUser{ get; set; }
[DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}", ApplyFormatInEditMode = true)] 
public DateTime ActivityDate{ get; set; }

public string ActivityContent{ get; set; }

public int CreatedUser{ get; set; }

public DateTime CreatedDate{ get; set; }

public string UserActivityKey { get; set; }
}
public class UserActivityCollection : BaseEntityCollection<UserActivity> { }
}