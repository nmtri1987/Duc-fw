using System.Text;
using System.Data;
using System.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.ComponentModel.DataAnnotations;
//using Server.DAC;
//using Server.Helpers;
namespace Biz.OG.Models
{
//[DataContract]
public class T_COM_Master_Entity :BaseEntity
{

public int EntityId{ get; set; }

public string ShortName_EN{ get; set; }

public string ShortName_VT{ get; set; }

public string LongName_EN{ get; set; }

public string LongName_VT{ get; set; }

public string Description{ get; set; }

public string Address_EN{ get; set; }

public string Address_VT{ get; set; }

public decimal PerDayWorkingHour{ get; set; }

public int CreatedBy{ get; set; }

public DateTime CreatedDate{ get; set; }

public int ModifiedBy{ get; set; }
[DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}", ApplyFormatInEditMode = true)] 
public DateTime ModifiedDate{ get; set; }

public bool IsAcitve{ get; set; }

}
public class T_COM_Master_EntityCollection : BaseEntityCollection<T_COM_Master_Entity> { }
}