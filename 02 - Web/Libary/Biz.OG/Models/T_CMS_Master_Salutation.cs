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
public class T_CMS_Master_Salutation :BaseEntity
{



        public string SalutationCD
        {
            get
            {
                return Salutation_EN;
            } 
        }
















}
public class T_CMS_Master_SalutationCollection : BaseEntityCollection<T_CMS_Master_Salutation> { }
}