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



















}
public class T_CMS_Master_EmploymentTypeCollection : BaseEntityCollection<T_CMS_Master_EmploymentType> { }
    public enum EmpSubTypeEnum
    {
        Definite = 100,
        InDefinite=101,
        Seasonal=103
    }
}