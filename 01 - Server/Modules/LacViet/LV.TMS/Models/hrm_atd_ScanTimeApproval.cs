using System;

namespace LV.TMS.Models
{
    using System.Runtime.Serialization;
    /// <summary>
    /// Use to map to hrm_atd_Scantime_ApprovalDetails entities
    /// </summary>
    [DataContract]
    public class hrm_atd_ScanTimeApproval : BaseDBEntity
    {
        [DataMember]
        public int ID { get; set; }
        [DataMember]
        public int EmployeeID { get; set; }
        [DataMember]
        public DateTime Work_Date { get; set; }

        [DataMember]
        public int User_Level { get; set; }

        [DataMember]
        public int Approve_Status { get; set; }

        [DataMember]
        public bool Submit { get; set; }

        [DataMember]
        public int Approve_Late_Early { get; set; }

        [DataMember]
        public int User_Group_ID { get; set; }

        [DataMember]
        public DateTime Approval_Date { get; set; }

        [DataMember]
        public bool Reject_In_Out { get; set; }

        [DataMember]
        public bool Reject_Late_Early { get; set; }

        [DataMember]
        public int User_Group_ID_Level1 { get; set; }

        [DataMember]
        public bool Approval_OT { get; set; }

        [DataMember]
        public bool Accept_OT { get; set; }

        [DataMember]
        public string Note_Level_1 { get; set; }

        [DataMember]
        public string Note_Level_2 { get; set; }

        [DataMember]
        public int Is_Auto_Cal_Kow { get; set; }
    }
}
