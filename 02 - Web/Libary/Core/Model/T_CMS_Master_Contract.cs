using System.Text;
using System.Data;
using System.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace Biz.Core.Models
{
    //[DataContract]
    public class Contract : BaseEntity
    {

        [DefaultValue(0)]
        public int ID { get; set; }

        public int DeptID { get; set; }
        public string DepartCD { get; set; }

        public int SalutationID { get; set; }
        public string Mode { get; set; }

        private string _SalutationCD = "";

        public string FirstName_EN { get; set; }


        public string MiddleName_EN { get; set; }


        public string LastName_EN { get; set; }


        public string FirstName_VN { get; set; }


        public string MiddleName_VN { get; set; }


        public string LastName_VN { get; set; }

        public int GradeID { get; set; }

        private string _GradeName = "";
       


        public int PositionID { get; set; }

        private string _PostionName = "";
       
        public string HomeCountryCurrency { get; set; }

        public Decimal Grossoffer { get; set; }

        [DisplayFormat(DataFormatString = SystemConst.DateFM, ApplyFormatInEditMode = true)]

        public DateTime Joiningdate { get; set; }

        [DisplayFormat(DataFormatString = SystemConst.DateFM, ApplyFormatInEditMode = true)]

        public DateTime Enddate { get; set; }

        public int LocationID { get; set; }

       

        public int WorkHoursID { get; set; }

        private string _WorkHoursCD = "";
     

        
        public int ContractTerm { get; set; }

        private string _ContractTermCD = "";
     

        public int ProbationsPeriod { get; set; }

        private string _ProbationsPeriodCD = "";
       

        [DisplayFormat(DataFormatString = SystemConst.DateFM, ApplyFormatInEditMode = true)]
        public DateTime DOB { get; set; }


        public string POB { get; set; }


        public string IDCardNo { get; set; }

        [DisplayFormat(DataFormatString = SystemConst.DateFM, ApplyFormatInEditMode = true)]

        public DateTime IDDOI { get; set; }


        public int IDPOI { get; set; }


        private string _IDPOICD = "";

      

        public string HighestDegree { get; set; }
      
        public string EmpAddress { get; set; }

        //end of report


        public int EmployeeCode { get; set; }

     

      


        public string ContractNo { get; set; }


        public int StatusID { get; set; }


        public int ApproverLevel { get; set; }


        public int ContractLevel { get; set; }

        public string PassportNo { get; set; }

        [DisplayFormat(DataFormatString = SystemConst.DateFM, ApplyFormatInEditMode = true)]

        public DateTime PassportDOI { get; set; }


        public string PassportPOI { get; set; }


        public string LabourBookNo { get; set; }

        [DisplayFormat(DataFormatString = SystemConst.DateFM, ApplyFormatInEditMode = true)]

        public DateTime LabourDOI { get; set; }


        public string LabourPOI { get; set; }


        

        


        


        


        


        


        


        


        public int AnnualLeave { get; set; }


        public int EmpTypeID { get; set; }


        public int EmpSubTypeID { get; set; }


        public string WorkPermitNo { get; set; }

        [DisplayFormat(DataFormatString = SystemConst.DateFM, ApplyFormatInEditMode = true)]

        public DateTime WorkPermitFrom { get; set; }

        [DisplayFormat(DataFormatString = SystemConst.DateFM, ApplyFormatInEditMode = true)]

        public DateTime WorkPermitTo { get; set; }


        public Decimal HomeGrossOffer { get; set; }


        

        [DisplayFormat(DataFormatString = SystemConst.DateFM, ApplyFormatInEditMode = true)]

        public DateTime HomeGrossOfferEffectiveFrom { get; set; }

        [DisplayFormat(DataFormatString = SystemConst.DateFM, ApplyFormatInEditMode = true)]

        public DateTime HomeGrossOfferEffectiveTo { get; set; }


        public string RelocationallowanceCurrency { get; set; }


        public Decimal Relocationallowance { get; set; }


        public string GoabroadallowanceCurrency { get; set; }


        public Decimal Goabroadallowance { get; set; }


        public string WaivingallowanceCurrency { get; set; }


        public Decimal Waivingallowance { get; set; }


        public string HostCountryCurrency { get; set; }

        [DisplayFormat(DataFormatString = SystemConst.DateFM, ApplyFormatInEditMode = true)]

        public DateTime HostGrossOfferEffectiveFrom { get; set; }

        [DisplayFormat(DataFormatString = SystemConst.DateFM, ApplyFormatInEditMode = true)]

        public DateTime HostGrossOfferEffectiveTo { get; set; }


        public string Remarks { get; set; }


        public int CreatedBy { get; set; }


        public DateTime CreatedDate { get; set; }


        public int ModifiedBy { get; set; }

        [DisplayFormat(DataFormatString = SystemConst.DateFM, ApplyFormatInEditMode = true)]

        public DateTime ModifiedDate { get; set; }


        public bool IsActive { get; set; }


        

        [DisplayFormat(DataFormatString = SystemConst.DateFM, ApplyFormatInEditMode = true)]

        public DateTime OriginalDate { get; set; }


        public int WorkFlowStatus { get; set; }


        public string HandPhone { get; set; }


        public string TempAddress { get; set; }

       


    }
    public class ContractCollection : BaseEntityCollection<Contract> { }
}