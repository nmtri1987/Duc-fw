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
public class T_LMS_Master_AnnualLeave :BaseEntity
{

public int Grade_Id{ get; set; }

public int NoOfDays{ get; set; }

public int CreatedBy{ get; set; }

public DateTime CreatedDate{ get; set; }

public int ModifiedBy{ get; set; }
[DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}", ApplyFormatInEditMode = true)] 
public DateTime ModifiedDate{ get; set; }

public string Description{ get; set; }

public bool IsManualEntry{ get; set; }

public bool IsAcitve{ get; set; }

}
public class T_LMS_Master_AnnualLeaveCollection : BaseEntityCollection<T_LMS_Master_AnnualLeave> { }
}