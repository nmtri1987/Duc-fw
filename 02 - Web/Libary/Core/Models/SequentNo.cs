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
public class SequentNo :BaseEntity
{















public string SequentNoKey { get; set; }
}
public class SequentNoCollection : BaseEntityCollection<SequentNo> { }
}