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
public class T_COM_Master_Location :BaseEntity
{


        public string LocationName { get; set; }
        public string LocationCD { get
            {
                return LocationID + "-" + LocationName;
            }
        }

        



















}
public class T_COM_Master_LocationCollection : BaseEntityCollection<T_COM_Master_Location> { }
}