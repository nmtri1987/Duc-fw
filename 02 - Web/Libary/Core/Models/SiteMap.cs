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
public class SiteMap :BaseEntity
{

public int CompanyID{ get; set; }

public Guid NodeID{ get; set; }

public int Position{ get; set; }

public string Title{ get; set; }

public string Description{ get; set; }

public string Url{ get; set; }

public bool Expanded{ get; set; }

public bool IsFolder{ get; set; }

public string ScreenID{ get; set; }

public Guid ParentID{ get; set; }

public int CreatedUser{ get; set; }

public DateTime CreatedDate{ get; set; }

public string IconImage{ get; set; }

}
public class SiteMapCollection : BaseEntityCollection<SiteMap> { }
}