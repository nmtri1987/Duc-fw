using System.Text;
using System.Data;
using System.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using DTP.Data;

namespace RBVH.TMS.Ext.Models
{
    [DataContract]
    public class External_Times :BaseDBEntity
    {
        [DataMember]
        public string AssociateNo { get; set; }

        [DataMember]
        public string AssociateName { get; set; }

        
        [DataMember]
        public DateTime JoinDate { get; set; }
        [DataMember]
        public DateTime EndDate { get; set; }
        [DataMember]
        public decimal Standard { get; set; }
        [DataMember]
        public decimal Actual { get; set; }
        [DataMember]
        public bool IsActive { get; set; }
        [DataMember]
        public string NT_ID { get; set; }

        [DataMember]
        public int? AccessCardNo { get; set; }

        [DataMember]
        public string VendorName { get; set; }

        [DataMember]
        public string Role { get; set; }

        [DataMember]
        public string DeptName { get; set; }

        [DataMember]
        public string GroupName { get; set; }
        [DataMember]
        public string ProjectCode { get; set; }
        [DataMember]
        public string PONumber { get; set; }

        [DataMember]
        public string DirectManager { get; set; }

    }
    public class External_TimesCollection : BaseDBEntityCollection<External_Times> { }


    public class ExternalDaily : BaseDBEntity
    {
        [DataMember]
        public string AssociateNo { get; set; }

        [DataMember]
        public string AssociateName { get; set; }
        [DataMember]
        
        public DateTime JoinDate { get; set; }

        [DataMember]
        public DateTime EndDate { get; set; }

        [DataMember]
        public DateTime Work_Date { get; set; }
        [DataMember]
        public string RootIn { get; set; }
        [DataMember]
        public string RootOut { get; set; }
        [DataMember]
        public decimal WorkHour { get; set; }

    }
    public class ExternalDailyCollection : BaseDBEntityCollection<ExternalDaily> { }
}
