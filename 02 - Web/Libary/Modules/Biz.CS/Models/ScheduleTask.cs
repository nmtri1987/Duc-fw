using System;
using System.ComponentModel.DataAnnotations;
using Biz.Core.Attribute;
using Newtonsoft.Json;
namespace Biz.CS.Models
{
    [Serializable]
    [JsonObject]
    public class ScheduleTask : BaseEntity
    {
        [LocalizedDisplayName("CompanyID")]
        [ColumnAttribute(Hide =true)]
        public int CompanyID { get; set; }

        [LocalizedDisplayName("Id")]
        [ColumnAttribute(Hide = true)]
        public int Id { get; set; }

        string _Name = "";
        [LocalizedDisplayName("Name")]
        [ColumnAttribute(DataType = "key-ref", ActionLink = "Update")]
        public string Name
        {
            get
            {
                return Id + "-" + _Name;
            }
            set
            {
                _Name = value;
            }
        }

        [LocalizedDisplayName("Seconds")]

        public int Seconds { get; set; }

        [LocalizedDisplayName("Type")]

        public string Type { get; set; }

        [LocalizedDisplayName("Enabled")]

        public bool Enabled { get; set; }

        [LocalizedDisplayName("StopOnError")]

        public bool StopOnError { get; set; }

        [LocalizedDisplayName("LeasedByMachineName")]

        public string LeasedByMachineName { get; set; }

        [LocalizedDisplayName("LeasedUntilUtc")]
        [DisplayFormat(DataFormatString = SystemConst.DateFM, ApplyFormatInEditMode = true)]
        [DataType(DataType.Date)]

        public DateTime LeasedUntilUtc { get; set; }

        [LocalizedDisplayName("LastStartUtc")]
        [DisplayFormat(DataFormatString = SystemConst.DateFM, ApplyFormatInEditMode = true)]
        [DataType(DataType.Date)]

        public DateTime LastStartUtc { get; set; }

        [LocalizedDisplayName("LastEndUtc")]
        [DisplayFormat(DataFormatString = SystemConst.DateFM, ApplyFormatInEditMode = true)]
        [DataType(DataType.Date)]

        public DateTime LastEndUtc { get; set; }

        [LocalizedDisplayName("LastSuccessUtc")]
        [DisplayFormat(DataFormatString = SystemConst.DateFM, ApplyFormatInEditMode = true)]
        [DataType(DataType.Date)]
        public DateTime LastSuccessUtc { get; set; }

        [LocalizedDisplayName("CreatedDate")]
        public DateTime CreatedDate { get; set; }

        [LocalizedDisplayName("CreatedUser")]
        public int CreatedUser { get; set; }

        [LocalizedDisplayName("ScreenID")]
        public string ScreenID { get; set; }

    }
    [Serializable]
    public class ScheduleTaskCollection : BaseEntityCollection<ScheduleTask> { }
}

