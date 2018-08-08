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
    public class DNHMessageTemplate : BaseEntity
    {


        public int CompanyID { get; set; }


        public int ID { get; set; }


        public string Name { get; set; }


        public string BccEmailAddresses { get; set; }


        public string Subject { get; set; }


        public string Body { get; set; }


        public bool IsActive { get; set; }


        public int? DelayBeforeSend { get; set; }


        public int DelayPeriodId { get; set; }


        public int AttachedDownloadId { get; set; }


        public int EmailAccountId { get; set; }


        public string CreatedUser { get; set; }


        public DateTime CreatedDate { get; set; }

        public string FromEmail { get; set; }

        /// <summary>
        /// Gets or sets the period of message delay
        /// </summary>
        public MessageDelayPeriod DelayPeriod
        {
            get { return (MessageDelayPeriod)this.DelayPeriodId; }
            set { this.DelayPeriodId = (int)value; }
        }
    }
    public class DNHMessageTemplateCollection : BaseEntityCollection<DNHMessageTemplate> { }
}