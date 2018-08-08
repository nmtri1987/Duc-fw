using System.Text;
using System.Data;
using System.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.ComponentModel.DataAnnotations;
//using Server.DAC;
//using Server.Helpers;
namespace BRVH.HR.OG.Models
{
//[DataContract]
public class TimeLog :BaseEntity
{


public int TimeLogId{ get; set; }


public string LAC{ get; set; }


public string ReaderType{ get; set; }


public string Door{ get; set; }


public string Name{ get; set; }


public string AssignID{ get; set; }


public string Department{ get; set; }


public string AccessType{ get; set; }

[DisplayFormat(DataFormatString = SystemConst.DateFM, ApplyFormatInEditMode = true)] 

public string DateLog{ get; set; }


public string TimeLogs{ get; set; }


}
public class TimeLogCollection : BaseEntityCollection<TimeLog> { }
}