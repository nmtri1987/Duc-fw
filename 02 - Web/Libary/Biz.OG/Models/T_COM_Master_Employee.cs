using System.Text;
using System.Data;
using System.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.ComponentModel.DataAnnotations;
//using Server.DAC;
//using Server.Helpers;
namespace Biz.OG.Models
{
    //[DataContract]
    public class T_COM_Master_Employee : BaseEntity
    {


        public int EmployeeCode { get; set; }


        public string EmployeeNo { get; set; }


        public string FirstName_EN { get; set; }


        public string MiddleName_EN { get; set; }


        public string LastName_EN { get; set; }


        public string FirstName_VN { get; set; }


        public string MiddleName_VN { get; set; }


        public string LastName_VN { get; set; }


        public string Email { get; set; }


        public string DomainId { get; set; }


        public string ESSPassword { get; set; }


        public int LocationId { get; set; }


        public int GroupCode { get; set; }


        public string SessionCode { get; set; }


        public int DirectManager { get; set; }


        public int IndirectManager { get; set; }


        public string Gender { get; set; }


        public int CategoryID { get; set; }


        public int WorkingHours { get; set; }


        public DateTime JoinedDate { get; set; }


        public int ProbationDays { get; set; }


        public string BloodGroup { get; set; }


        public int MaritalStatusID { get; set; }


        public string EmergencyContactName { get; set; }


        public string EmergencyContactNo { get; set; }


        public string EmergencyContactRelation { get; set; }


        public DateTime DOB { get; set; }


        public string POB { get; set; }


        public int POBID { get; set; }


        public string NativePlace { get; set; }


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


        public string PersonalEmail { get; set; }


        public string TaxCode { get; set; }


        public string SocialBookNo { get; set; }


        public DateTime SocialBookDOI { get; set; }


        public int SocialBookStatusID { get; set; }


        public int RegisteredHospitalID { get; set; }


        public string RelatedExperience { get; set; }


        public int Age { get; set; }


        public int EmpSourceID { get; set; }


        public int DistanceToOfficeID { get; set; }


        public string ForeignLanguage { get; set; }


        public string InternationalTravelTo { get; set; }


        public string OtherSkills { get; set; }


        public decimal SickLeaveDays { get; set; }


        public bool SIEligibility { get; set; }


        public DateTime SIStartDate { get; set; }


        public bool HIEligibility { get; set; }


        public DateTime HIStartDate { get; set; }


        public bool UIEligibility { get; set; }


        public DateTime UIStartDate { get; set; }


        public int PickupArea { get; set; }


        public string HomeCity { get; set; }


        public int HomeCountry { get; set; }


        public string TempCity { get; set; }


        public int TempCountry { get; set; }


        public string AwardsInBosch { get; set; }


        public int EmployeeStatusID { get; set; }


        public string TerminationDecisionNo { get; set; }


        public DateTime TerminationDate { get; set; }


        public int TerminationReasonID { get; set; }


        public DateTime ExitInterViewDate { get; set; }


        public int EmployeeTypeID { get; set; }


        public int EmployeeSubTypeID { get; set; }


        public string EmployeeTypeCode { get; set; }


        public string LabourBookNo { get; set; }


        public DateTime LabourBookDOI { get; set; }


        public string LabourBookPOI { get; set; }


        public decimal AnnualLeaveDays { get; set; }


        public int AssignmentID { get; set; }


        public bool IsActive { get; set; }


        public int CreatedBy { get; set; }


        public DateTime CreatedDate { get; set; }


        public int ModifiedBy { get; set; }

        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}", ApplyFormatInEditMode = true)]

        public DateTime ModifiedDate { get; set; }


        public int ContractManager { get; set; }


        public int SubGroupCode { get; set; }


        public bool RegForKiosk { get; set; }


        public bool IsPersonCapacity { get; set; }


        public decimal OverAllExperience1 { get; set; }


    }
    public class T_COM_Master_EmployeeCollection : BaseEntityCollection<T_COM_Master_Employee> { }
}