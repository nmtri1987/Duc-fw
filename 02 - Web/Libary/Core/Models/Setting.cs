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
public class Setting :BaseEntity
{

public int CompanyID{ get; set; }

public int SettingID{ get; set; }

public string Name{ get; set; }

public string Value{ get; set; }

public String Description{ get; set; }

public DateTime CreatedDate{ get; set; }

public int CreatedUser{ get; set; }

}
public class SettingCollection : BaseEntityCollection<Setting> { }
}