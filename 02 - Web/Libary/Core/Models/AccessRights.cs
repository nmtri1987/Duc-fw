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
public class AccessRights :BaseEntity
{

public int CompanyID{ get; set; }

public string RoleName{ get; set; }

public Guid NodeID{ get; set; }

public int Access{ get; set; }

public int CreatedUser{ get; set; }
[DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}", ApplyFormatInEditMode = true)] 
public DateTime CreateDate{ get; set; }

public string AccessRightsKey { get; set; }
}
public class AccessRightsCollection : BaseEntityCollection<AccessRights> { }
}