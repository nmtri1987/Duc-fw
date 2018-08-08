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
public class T_CMS_Master_EmploymentType :BaseEntity
{

public int EmpTypeID{ get; set; }

public string EmpTypeDescription{ get; set; }

public string EmpTypeCode{ get; set; }

public int EntityID{ get; set; }

public bool IsActive{ get; set; }

public int CreatedBy{ get; set; }

public DateTime CreatedDate{ get; set; }

public int ModifiedBy{ get; set; }
[DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}", ApplyFormatInEditMode = true)] 
public DateTime ModifiedDate{ get; set; }

}
public class T_CMS_Master_EmploymentTypeCollection : BaseEntityCollection<T_CMS_Master_EmploymentType> { }
    public enum EmpSubTypeEnum
    {
        Definite = 100,
        InDefinite=101,
        Seasonal=103
    }
}