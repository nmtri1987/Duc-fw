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

public int CompanyID{ get; set; }

public string NumberingID{ get; set; }

public string NumberingPrefix{ get; set; }

public int LastSequent{ get; set; }

public int MaxLength{ get; set; }

public int CreatedUser{ get; set; }

public DateTime CreatedDate{ get; set; }

public string SequentNoKey { get; set; }
}
public class SequentNoCollection : BaseEntityCollection<SequentNo> { }
}