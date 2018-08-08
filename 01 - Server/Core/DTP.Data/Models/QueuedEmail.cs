using System;
using System.Runtime.Serialization;

namespace DTP.Data.Models
{
    [DataContract]
    public partial class QueuedEmail : BaseDBEntity
    {
        [DataMember]
        public int Id { get; set; }


        [DataMember]
        public int CompanyID { get; set; }

        /// <summary>
        /// Gets or sets the priority
        /// </summary>
        [DataMember]
        public int PriorityId { get; set; }

        /// <summary>
        /// Gets or sets the From property (email address)
        /// </summary>
        /// 
        [DataMember]
        public string From { get; set; }

        /// <summary>
        /// Gets or sets the FromName property
        /// </summary>
        /// 
        [DataMember]
        public string FromName { get; set; }

        /// <summary>
        /// Gets or sets the To property (email address)
        /// </summary>
        /// 
        [DataMember]
        public string To { get; set; }

        /// <summary>
        /// Gets or sets the ToName property
        /// </summary>
        /// 

        [DataMember]
        public string ToName { get; set; }

        /// <summary>
        /// Gets or sets the ReplyTo property (email address)
        /// </summary>
        /// 

        [DataMember]
        public string ReplyTo { get; set; }

        /// <summary>
        /// Gets or sets the ReplyToName property
        /// </summary>
        /// 
        [DataMember]
        public string ReplyToName { get; set; }

        /// <summary>
        /// Gets or sets the CC
        /// </summary>
        /// 
        [DataMember]
        public string CC { get; set; }

        /// <summary>
        /// Gets or sets the Bcc
        /// </summary>
        /// 
        [DataMember]
        public string Bcc { get; set; }

        /// <summary>
        /// Gets or sets the subject
        /// </summary>
        /// 
        [DataMember]
        public string Subject { get; set; }

        /// <summary>
        /// Gets or sets the body
        /// </summary>
        /// 
        [DataMember]
        public string Body { get; set; }

        /// <summary>
        /// Gets or sets the attachment file path (full file path)
        /// </summary>
        /// 
        [DataMember]
        public string AttachmentFilePath { get; set; }

        /// <summary>
        /// Gets or sets the attachment file name. If specified, then this file name will be sent to a recipient. Otherwise, "AttachmentFilePath" name will be used.
        /// </summary>
        /// 
        [DataMember]
        public string AttachmentFileName { get; set; }

        /// <summary>
        /// Gets or sets the download identifier of attached file
        /// </summary>
        /// 
        [DataMember]
        public int AttachedDownloadId { get; set; }

        /// <summary>
        /// Gets or sets the date and time of item creation in UTC
        /// </summary>
        /// 
        [DataMember]
        public DateTime? CreatedOnUtc { get; set; }

        /// <summary>
        /// Gets or sets the date and time in UTC before which this email should not be sent
        /// </summary>
        /// 
        [DataMember]
        public DateTime? DontSendBeforeDateUtc { get; set; }

        /// <summary>
        /// Gets or sets the send tries
        /// </summary>
        /// 
        [DataMember]
        public int SentTries { get; set; }

        /// <summary>
        /// Gets or sets the sent date and time
        /// </summary>
        /// 
        [DataMember]
        public DateTime? SentOnUtc { get; set; }

        /// <summary>
        /// Gets or sets the used email account identifier
        /// </summary>
        /// 
        [DataMember]
        public int EmailAccountId { get; set; }

        [DataMember]
        public int CreatedUser { get; set; }

        [DataMember]
        public DateTime? CreatedDate { get; set; }

        /// <summary>
        /// Gets the email account
        /// </summary>
        /// 
        //[DataMember]
        //public virtual EmailAccount EmailAccount { get; set; }


        ///// <summary>
        ///// Gets or sets the priority
        ///// </summary>
        //public QueuedEmailPriority Priority
        //{
        //    get
        //    {
        //        return (QueuedEmailPriority)this.PriorityId;
        //    }
        //    set
        //    {
        //        this.PriorityId = (int)value;
        //    }
        //}

    }
    public class QueuedEmailCollection : BaseDBEntityCollection<QueuedEmail> { }
}
