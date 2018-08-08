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
public class Roles :BaseEntity
{


public int CompanyID{ get; set; }


public string Rolename{ get; set; }


public string ApplicationName{ get; set; }


public string Descr{ get; set; }


public bool isGuest{ get; set; }


public int Createduser{ get; set; }


public DateTime CreatedDate{ get; set; }


public string RolesKey { get; set; }
}
public class RolesCollection : BaseEntityCollection<Roles> { }
}