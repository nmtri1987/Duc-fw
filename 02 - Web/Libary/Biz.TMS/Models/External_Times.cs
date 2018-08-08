using System.Text;
using System.Data;
using System.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.ComponentModel.DataAnnotations;

namespace Biz.TMS.Models
{
    public class External_Times :BaseEntity
    {
        //public string AssociateNo { get; set; }
        //public string FirstName { get; set; }
        //public string LastName { get; set; }

        //[DisplayFormat(DataFormatString = "{0:0,0}", ApplyFormatInEditMode = true)]
        //public DateTime? Work_Date { get; set; }

        //[DisplayFormat(DataFormatString = "{0:0,0}", ApplyFormatInEditMode = true)]
        //public DateTime? RootIn { get; set; }

        
        //public DateTime? RootOut { get; set; }
        //public string WorkHour { get; set; }

        
        public string AssociateNo { get; set; }

        
        public string AssociateName { get; set; }

        public bool IsActive { get; set; }
        public string NT_ID { get; set; }
        public string AccessCardNo { get; set; }
        public string VendorName { get; set; }

        public string Role { get; set; }

        public string DeptName { get; set; }

        public string GroupName { get; set; }
        public string DirectManager { get; set; }
        public string ProjectCode { get; set; }
        public string PONumber { get; set; }

        [DisplayFormat(DataFormatString = "{0:0,0}", ApplyFormatInEditMode = true)]
        public DateTime JoinDate { get; set; }

        [DisplayFormat(DataFormatString = "{0:0,0}", ApplyFormatInEditMode = true)]
        public DateTime EndDate { get; set; }
        
        public decimal Standard { get; set; }
        
        public decimal Actual { get; set; }
      
    }
    public class External_TimesCollection : BaseEntityCollection<External_Times> { }
    public class ExternalDaily : BaseEntity
    {
        
        public string AssociateNo { get; set; }

        
        public string AssociateName { get; set; }

        [DisplayFormat(DataFormatString = "{0:0,0}", ApplyFormatInEditMode = true)]
        public DateTime? JoinDate { get; set; }

        [DisplayFormat(DataFormatString = "{0:0,0}", ApplyFormatInEditMode = true)]
        public DateTime? EndDate { get; set; }

        [DisplayFormat(DataFormatString = "{0:0,0}", ApplyFormatInEditMode = true)]
        public DateTime? Work_Date { get; set; }

        public DateTime? RootIn { get; set; }

        public DateTime? RootOut { get; set; }
        
        public decimal WorkHour { get; set; }

    }
    public class ExternalDailyCollection : BaseEntityCollection<ExternalDaily> { }
}
