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
public class EmployeeInRoles :BaseEntity
{


public int CompanyID{ get; set; }


public string Rolename{ get; set; }


public int EmployeeCode{ get; set; }


public string ApplicationName{ get; set; }


public int CreatedUser{ get; set; }


public DateTime CreatedDate{ get; set; }


public int UpdateUser{ get; set; }

[DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}", ApplyFormatInEditMode = true)] 

public DateTime UpdateDate{ get; set; }


public string EmployeeInRolesKey { get; set; }
}
public class EmployeeInRolesCollection : BaseEntityCollection<EmployeeInRoles> { }
}