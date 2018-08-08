using System.Text;
using System.Data;
using System.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.ComponentModel.DataAnnotations;
using Biz.Core.Attribute;
using Biz.Core.Services;
//using Server.DAC;
//using Server.Helpers;
namespace Biz.Core.Models
{
    //[DataContract]
    public class RBVHEmployee : BaseEntity
    {

        //[ColumnAttribute(DataType = "key-new",ActionLink ="Info")]
        public int EmployeeCode { get; set; }


        public string EmployeeNo { get; set; }
        public string DomainId { get; set; }

        public string FullName
        {
            get
            {
                return LastName_EN +" " + MiddleName_EN + " " + FirstName_EN ;
            }
        }
        [ColumnAttribute(Hide = true)]
        public string EmpNoFullName { get
            {
                return EmployeeNo + " - " + FullName;
            }
        }

        [ColumnAttribute(Hide = true)]
        public string FirstName_EN { get; set; }

        [ColumnAttribute(Hide = true)]
        public string MiddleName_EN { get; set; }

        [ColumnAttribute(Hide = true)]
        public string LastName_EN { get; set; }

        [ColumnAttribute(Hide = true)]
        public string FirstName_VN { get; set; }

        [ColumnAttribute(Hide = true)]
        public string MiddleName_VN { get; set; }

        [ColumnAttribute(Hide = true)]
        public string LastName_VN { get; set; }

        [ColumnAttribute(Hide = true)]
        public string ESSPassword { get; set; }

        [ColumnAttribute(Hide = true)]
        public int LocationId { get; set; }

        [ColumnAttribute(Hide = true)]
        public int GroupCode { get; set; }

        [ColumnAttribute(Hide = true)]
        public string SessionCode { get; set; }

        public DateTime JoinedDate { get; set; }


        [ColumnAttribute(Hide = true)]
        public int DirectManager { get; set; }

        [ColumnAttribute(Hide = true)]
        private RBVHEmployee DirectManagerInfo
        {
            get
            {
                if (DirectManager != 0)
                {
                    RBVHEmployee obj = RBVHEmployeeManager.GetById(DirectManager);
                    if (obj != null)
                    {
                        return obj;
                    }

                }
                return null;
            }
        }

        public string DirectmanagerNo
        {
            get
            {
                if (DirectManagerInfo != null)
                {
                    return DirectManagerInfo.EmployeeNo;
                }
                return "";
            }
        }

        public string DirectmanagerName
        {
            get
            {
                if (DirectManagerInfo != null)
                {
                    return DirectManagerInfo.FullName;
                }
                return "";
            }
        }

        [ColumnAttribute(Hide = true)]
        public int IndirectManager { get; set; }

        [ColumnAttribute(Hide = true)]
        private RBVHEmployee IndirectManagerInfo
        {
            get
            {
                if (DirectManager != 0)
                {
                    RBVHEmployee obj = RBVHEmployeeManager.GetById(DirectManager);
                    if (obj != null)
                    {
                        return obj;
                    }

                }
                return null;
            }
        }

        public string IndirectManagerNo
        {
            get
            {
                if (IndirectManagerInfo != null)
                {
                    return IndirectManagerInfo.EmployeeNo;

                }
                return "";
            }
        }

        public string IndirectManagerName
        {
            get
            {
                if (IndirectManagerInfo != null)
                {
                    return IndirectManagerInfo.FullName;

                }
                return "";
            }
        }

        public string Gender { get; set; }

        [ColumnAttribute(Hide = true)]
        public int CategoryID { get; set; }



        public int ProbationDays { get; set; }

        [ColumnAttribute(Hide = true)]
        public string BloodGroup { get; set; }

        [ColumnAttribute(Hide = true)]
        public int MaritalStatusID { get; set; }

        [ColumnAttribute(Hide = true)]
        public string EmergencyContactName { get; set; }

        [ColumnAttribute(Hide = true)]
        public string EmergencyContactNo { get; set; }

        [ColumnAttribute(Hide = true)]
        public string EmergencyContactRelation { get; set; }


        public DateTime DOB { get; set; }


        public string POB { get; set; }

        [ColumnAttribute(Hide = true)]
        public int POBID { get; set; }

        [ColumnAttribute(Hide = true)]
        public string NativePlace { get; set; }

        [ColumnAttribute(Hide = true)]
        public int NationalityID { get; set; }


        public string IDCardNo { get; set; }


        public DateTime IDDOI { get; set; }


        public int IDPOI { get; set; }


        public string PassportNO { get; set; }


        public DateTime PassportDOI { get; set; }


        public string PassportPOI { get; set; }


        public DateTime PassportDOE { get; set; }


        public string PermanentAddress { get; set; }

        
        public string TempAddress { get; set; }


        public string HomePhone { get; set; }


        public string HandPhone { get; set; }

        [ColumnAttribute(Hide = true)]
        public string PersonalEmail { get; set; }


        public string TaxCode { get; set; }


        public string SocialBookNo { get; set; }


        public DateTime SocialBookDOI { get; set; }


        public int SocialBookStatusID { get; set; }

        [ColumnAttribute(Hide = true)]
        public int RegisteredHospitalID { get; set; }

        [ColumnAttribute(Hide = true)]
        public string RelatedExperience { get; set; }


        public int Age { get; set; }

        [ColumnAttribute(Hide = true)]
        public int EmpSourceID { get; set; }

        [ColumnAttribute(Hide = true)]
        public int DistanceToOfficeID { get; set; }

        [ColumnAttribute(Hide = true)]
        public string ForeignLanguage { get; set; }

        [ColumnAttribute(Hide = true)]
        public string InternationalTravelTo { get; set; }

        [ColumnAttribute(Hide = true)]
        public string OtherSkills { get; set; }


        public decimal SickLeaveDays { get; set; }

        [ColumnAttribute(Hide = true)]
        public bool SIEligibility { get; set; }

        [ColumnAttribute(Hide = true)]
        public DateTime SIStartDate { get; set; }

        [ColumnAttribute(Hide = true)]
        public bool HIEligibility { get; set; }

        [ColumnAttribute(Hide = true)]
        public DateTime HIStartDate { get; set; }

        [ColumnAttribute(Hide = true)]
        public bool UIEligibility { get; set; }

        [ColumnAttribute(Hide = true)]
        public DateTime UIStartDate { get; set; }

        [ColumnAttribute(Hide = true)]
        public int PickupArea { get; set; }


        public string HomeCity { get; set; }

        [ColumnAttribute(Hide = true)]
        public int HomeCountry { get; set; }

        [ColumnAttribute(Hide = true)]
        public string TempCity { get; set; }

        [ColumnAttribute(Hide = true)]
        public int TempCountry { get; set; }

        [ColumnAttribute(Hide = true)]
        public string AwardsInBosch { get; set; }

        [ColumnAttribute(Hide = true)]
        public int EmployeeStatusID { get; set; }

        [ColumnAttribute(Hide = true)]
        public string TerminationDecisionNo { get; set; }


        public DateTime TerminationDate { get; set; }

        [ColumnAttribute(Hide = true)]
        public int TerminationReasonID { get; set; }

        [ColumnAttribute(Hide = true)]
        public DateTime ExitInterViewDate { get; set; }

        [ColumnAttribute(Hide = true)]
        public int EmployeeTypeID { get; set; }

        [ColumnAttribute(Hide = true)]
        public int EmployeeSubTypeID { get; set; }

        [ColumnAttribute(Hide = true)]
        public string EmployeeTypeCode { get; set; }


        public string LabourBookNo { get; set; }


        public DateTime LabourBookDOI { get; set; }


        public string LabourBookPOI { get; set; }


        public decimal AnnualLeaveDays { get; set; }

        [ColumnAttribute(Hide = true)]
        public int AssignmentID { get; set; }

        
        public bool IsActive { get; set; }

        [ColumnAttribute(Hide = true)]
        public int CreatedBy { get; set; }


        public DateTime CreatedDate { get; set; }

        [ColumnAttribute(Hide = true)]
        public int ModifiedBy { get; set; }

        
        [DisplayFormat(DataFormatString = SystemConst.DateFM, ApplyFormatInEditMode = true)]
        public DateTime ModifiedDate { get; set; }


        public int ContractManager { get; set; }

        //[ColumnAttribute(Hide = true)]
        //private RBVHEmployee ContractManagerInfo
        //{
        //    get
        //    {
        //        if (ContractManager != 0)
        //        {
        //            RBVHEmployee obj = RBVHEmployeeManager.GetById(ContractManager);
        //            if (obj != null)
        //            {
        //                return obj;
        //            }

        //        }
        //        return null;
        //    }
        //}

        //public string ContractManagerNo
        //{
        //    get
        //    {
        //        if (ContractManagerInfo != null)
        //        {
        //            return ContractManagerInfo.EmployeeNo;

        //        }
        //        return "";
        //    }
        //}

        //public string ContractManagerName
        //{
        //    get
        //    {
        //        if (ContractManagerInfo != null)
        //        {
        //            return ContractManagerInfo.FullName;

        //        }
        //        return "";
        //    }
        //}

        [ColumnAttribute(Hide = true)]
        public int SubGroupCode { get; set; }

        [ColumnAttribute(Hide = true)]
        public bool RegForKiosk { get; set; }

        [ColumnAttribute(Hide = true)]
        public bool IsPersonCapacity { get; set; }

        [ColumnAttribute(Hide = true)]
        public decimal OverAllExperience1 { get; set; }

        [ColumnAttribute(Hide = true)]
        public Contract EmpContract
        {
            get
            {
                return Biz.Core.Services.ContractManager.GetbyEmpCode(EmployeeCode);
            }
        }

        //public string 
        public string Email { get; set; }

        public int EntityID { get; set; }
    }
    public class RBVHEmployeeCollection : BaseEntityCollection<RBVHEmployee> { }
    public class RBVHEmployeeSearchpara
    {
        public string EmployeeNo { get; set; }
        public string Entity { get; set; }
        public string EntityId { get; set; }
        public string EmployeeName { get; set; }
        public string OrgId { get; set; }

    }

}