using System.Text;
using System.Data;
using System.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.ComponentModel.DataAnnotations;
using Biz.OG.Services;
using System.ComponentModel;
using Biz.Core.Models;
using Biz.Core.Services;
using Newtonsoft.Json;
//using Server.DAC;
//using Server.Helpers;
namespace Biz.OG.Models
{
    //[DataContract]

    public class ContractReport : BaseEntity
    {
        public string ID { get; set; }
        public string EntityId { get; set; }
        public string EmployeeCode { get; set; }
        public string filepath { get; set; }
        public string Mode { get; set; }
        public string filetype { get; set; }
    }
    public class ContractReportCollection : BaseEntityCollection<ContractReport> { }

    [Serializable]
    [JsonObject]
    public class T_CMS_Master_Contract : BaseEntity
    {

        [DefaultValue(0)]
        public int ID { get; set; }


        private string _DepartCD = "";
        public string DepartCD { get {
                if (string.IsNullOrEmpty(_DepartCD))
                {
                    return T_COm_Master_OrgManager.GetById(DeptID).OrgName;
                }
                else
                {
                    return _DepartCD;
                }
            }
            set { _DepartCD = value; } }

        public int SalutationID { get; set; }


        private string _SalutationCD = "";
        public string SalutationCD
        {
            get
            {
                if (string.IsNullOrEmpty(_SalutationCD))
                {
                    return T_CMS_Master_SalutationManager.GetById(SalutationID).SalutationCD;
                }
                else
                {
                    return _SalutationCD;
                }

            }
            set
            {
                _SalutationCD = value;
            }
        }
        public string LastName_EN { get; set; }
        public string MiddleName_EN { get; set; }
        public string FirstName_EN { get; set; }

        public string LastName_VN { get; set; }
        public string MiddleName_VN { get; set; }

        public string FirstName_VN { get; set; }

        public int GradeID { get; set; }

        private string _GradeName = "";
        public string GradeName
        {
            get
            {
                if (string.IsNullOrEmpty(_GradeName))
                {
                    return T_COM_Master_GradeManager.GetById(GradeID).GradeName;
                } else
                {
                    return _GradeName;
                }
            }
            set
            {
                _GradeName = value;
            }
        }


        public int PositionID { get; set; }

        private string _PostionName = "";
        public string PostionName
        {
            get
            {
                if (string.IsNullOrEmpty(_GradeName))
                {
                    return T_COM_Master_PositionManager.GetById(PositionID).PositionName_EN;
                }
                else
                {
                    return _PostionName;
                }

            } set
            {
                _PostionName = value;
            }
        }
        [DefaultValue("VND")]
        public string HomeCountryCurrency { get; set; }

        [DisplayFormat(DataFormatString = "{0:0,0}", ApplyFormatInEditMode = true)]
        public Decimal Grossoffer { get; set; }

        [DisplayFormat(DataFormatString = "dd/mm/yyyy", ApplyFormatInEditMode = true)]
        [DataType(DataType.Date)]
        public DateTime Joiningdate { get; set; }

        [DisplayFormat(DataFormatString = SystemConst.DateFM, ApplyFormatInEditMode = true)]
        [DataType(DataType.Date)]
        public DateTime Enddate { get; set; }

        public int LocationID { get; set; }

        public string LocationCD
        {
            get
            {
                return T_COM_Master_LocationManager.GetById(LocationID).LocationName;
            }
        }

        public int WorkHoursID { get; set; }

        private string _WorkHoursCD = "";
        public string WorkHoursCD
        {
            get
            {
                if (string.IsNullOrEmpty(_WorkHoursCD))
                {
                    return T_CMS_InterfaceLacviet_WorkingHoursManager.GetById(WorkHoursID).WorkingHours.ToString();
                }
                else
                {
                    return _WorkHoursCD;
                }
                    
            }
            set
            {
                _WorkHoursCD = value;
            }
        }

        
        public int ContractTerm { get; set; }

        private string _ContractTermCD = "";
        public string ContractTermCD {
            get
            {
                if (string.IsNullOrEmpty(_ContractTermCD))
                {
                    if (ContractTerm == 0)
                    {
                        return "Indefinite contract";
                    }else
                    {
                        return ContractTerm + " months";
                    }
                 //   return ContractTerm;
                    //return T_CMS_Static_ContractTermManager.GetById(ContractTerm).ContractTerm;
                }
                else
                {
                    return _ContractTermCD;
                }
                return _ContractTermCD;
            }
            set
            {
                _ContractTermCD = value;
            }
        }

        public int? ProbationsPeriod { get; set; }

        private string _ProbationsPeriodCD = "";
        public string ProbationsPeriodCD
        {
            get
            {
                if (string.IsNullOrEmpty(_ProbationsPeriodCD))
                {
                    if (ProbationsPeriod.HasValue)
                    {
                        T_CMS_Static_PeriodOfProbation b = T_CMS_Static_PeriodOfProbationManager.GetById(ProbationsPeriod.Value);
                        if (b != null)
                        {
                            return b.CD;
                        }
                    }
                    return "2";
                }
                else
                {
                    return _ProbationsPeriodCD;
                }
                    
            }
            set
            {
                _ProbationsPeriodCD = value;
            }
        }

        [DisplayFormat(DataFormatString = "dd/mm/yyyy", ApplyFormatInEditMode = true)]
        [DataType(DataType.Date)]
        public DateTime DOB { get; set; }

        [Required]
        public string POB { get; set; }


        public string IDCardNo { get; set; }

        [DisplayFormat(DataFormatString = "dd/mm/yyyy", ApplyFormatInEditMode = true)]
        [DataType(DataType.Date)]
        public DateTime IDDOI { get; set; }


        public int IDPOI { get; set; }


        private string _IDPOICD = "";

        public string IDPOICD
        {
            get
            {
                if (string.IsNullOrEmpty(_IDPOICD))
                {
                    return T_COM_Master_PlaceOfIssueManager.GetById(IDPOI).POI_Name_VN;
                }
                else
                {
                    return _IDPOICD;
                }

            }set
            {
                _IDPOICD = value;
            }
        }

        [Required]
        public string HighestDegree { get; set; }
        //public string HighestDegreeCD
        //{
        //    get
        //    {
        //        return T_COM_Master_DegreeManager.GetById(HighestDegree).LocationCD;
        //    }
        //}

        [DisplayName("EmpAddress")]
        public string PerAddress { get; set; }

        //end of report

        [LocalizedDisplayName("EmployeeCode")]
        public int EmployeeCode { get; set; }

        public string ContractNo { get; set; }


        public int StatusID { get; set; }


        public int ApproverLevel { get; set; }


        public int ContractLevel { get; set; }

        public string PassportNo { get; set; }

        [DisplayFormat(DataFormatString = SystemConst.DateFM, ApplyFormatInEditMode = true)]
        [DataType(DataType.Date)]
        public DateTime? PassportDOI { get; set; }


        public string PassportPOI { get; set; }


        public string LabourBookNo { get; set; }

        [DisplayFormat(DataFormatString = SystemConst.DateFM, ApplyFormatInEditMode = true)]
        [DataType(DataType.Date)]
        public DateTime? LabourDOI { get; set; }


        public string LabourPOI { get; set; }

        public int AnnualLeave { get; set; }


        public int EmpTypeID { get; set; }


        public int EmpSubTypeID { get; set; }


        public string WorkPermitNo { get; set; }

        //[DisplayFormat(DataFormatString = SystemConst.DateFM, ApplyFormatInEditMode = true)]

        public DateTime WorkPermitFrom { get; set; }

        //[DisplayFormat(DataFormatString = SystemConst.DateFM, ApplyFormatInEditMode = true)]

        public DateTime WorkPermitTo { get; set; }


        public Decimal HomeGrossOffer { get; set; }


        

        //[DisplayFormat(DataFormatString = SystemConst.DateFM, ApplyFormatInEditMode = true)]

        public DateTime HomeGrossOfferEffectiveFrom { get; set; }

        //[DisplayFormat(DataFormatString = SystemConst.DateFM, ApplyFormatInEditMode = true)]

        public DateTime HomeGrossOfferEffectiveTo { get; set; }


        public string RelocationallowanceCurrency { get; set; }


        public Decimal Relocationallowance { get; set; }


        public string GoabroadallowanceCurrency { get; set; }


        public Decimal Goabroadallowance { get; set; }


        public string WaivingallowanceCurrency { get; set; }


        public Decimal Waivingallowance { get; set; }


        public string HostCountryCurrency { get; set; }

        //[DisplayFormat(DataFormatString = SystemConst.DateFM, ApplyFormatInEditMode = true)]

        public DateTime HostGrossOfferEffectiveFrom { get; set; }

        //[DisplayFormat(DataFormatString = SystemConst.DateFM, ApplyFormatInEditMode = true)]

        public DateTime HostGrossOfferEffectiveTo { get; set; }


        public string Remarks { get; set; }


        public int CreatedBy { get; set; }


        public DateTime CreatedDate { get; set; }


        public int ModifiedBy { get; set; }

        //[DisplayFormat(DataFormatString = SystemConst.DateFM, ApplyFormatInEditMode = true)]

        public DateTime ModifiedDate { get; set; }


        public bool IsActive { get; set; }


        

        //[DisplayFormat(DataFormatString = SystemConst.DateFM, ApplyFormatInEditMode = true)]

        public DateTime OriginalDate { get; set; }


        public int WorkFlowStatus { get; set; }


        public string HandPhone { get; set; }


        public string TempAddress { get; set; }

       public T_COm_Master_Org CMSOrg
        {
            get{
                return T_COm_Master_OrgManager.GetById(DeptID);

            }
        }
        public string Mode { get; set; }
        public int DeptID { get; set; }
        public string Fullname
        {
            get
            {

                return  LastName_EN + " " + MiddleName_EN + " " + FirstName_EN;
            }
        }
        public string EmployeeNO
        {
            get
            {
                if (ContractNo == null) return "";
                string[] b = ContractNo.Split('/');

                if (b.Length == 0)
                {
                    return "";
                }
                return b[0];
            }
        }
        public RBVHEmployee EmployeeInfo
        {
            get
            {
                return Biz.Core.Services.RBVHEmployeeManager.GetById(EmployeeCode);
            }
        }
        public string DirectCode
        {
            get
            {
                if (EmployeeInfo != null)
                {
                    return Biz.Core.Services.RBVHEmployeeManager.GetById(EmployeeInfo.DirectManager).EmployeeCode.ToString();
                    //return 
                }
                return ""; 
            }
        }
        public string INDirectCode
        {
            get
            {
                if (EmployeeInfo != null)
                {
                    return Biz.Core.Services.RBVHEmployeeManager.GetById(EmployeeInfo.IndirectManager).EmployeeCode.ToString();
                    //return 
                }
                return "";
            }
        }
        public string EmpTypeCD { get; set; }
        public string JobDesc { get; set; }
    }
    [Serializable]
    public class T_CMS_Master_ContractCollection : BaseEntityCollection<T_CMS_Master_Contract> { }
    public class ContractSearchpara
    {
        public string EntityId { get; set; }
        public string EmpCode { get; set; }
        public string EmpName { get; set; }
        public string start { get; set; }
        public string end { get; set; }
        public string EmpTypeID { get; set; }
        public string LocationID { get; set; }
        public string IsActive { get; set; }
    }
}