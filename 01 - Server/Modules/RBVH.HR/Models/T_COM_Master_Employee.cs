using System.Text;
using System.Data;
using System.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using DTP.Data;
namespace Biz.OG
{
    [DataContract]
    public class T_COM_Master_Employee : BaseDBEntity
    {

        [DataMember]
        public int EmployeeCode { get; set; }

        [DataMember]
        public string EmployeeNo { get; set; }

        [DataMember]
        public string FirstName_EN { get; set; }

        [DataMember]
        public string MiddleName_EN { get; set; }

        [DataMember]
        public string LastName_EN { get; set; }

        [DataMember]
        public string FirstName_VN { get; set; }

        [DataMember]
        public string MiddleName_VN { get; set; }

        [DataMember]
        public string LastName_VN { get; set; }

        [DataMember]
        public string Email { get; set; }

        [DataMember]
        public string DomainId { get; set; }

        [DataMember]
        public string ESSPassword { get; set; }

        [DataMember]
        public int LocationId { get; set; }

        [DataMember]
        public int GroupCode { get; set; }

        [DataMember]
        public string SessionCode { get; set; }

        [DataMember]
        public int DirectManager { get; set; }

        [DataMember]
        public int IndirectManager { get; set; }

        [DataMember]
        public string Gender { get; set; }

        [DataMember]
        public int CategoryID { get; set; }

        [DataMember]
        public int WorkingHours { get; set; }

        [DataMember]
        public DateTime JoinedDate { get; set; }

        [DataMember]
        public int ProbationDays { get; set; }

        [DataMember]
        public string BloodGroup { get; set; }

        [DataMember]
        public int MaritalStatusID { get; set; }

        [DataMember]
        public string EmergencyContactName { get; set; }

        [DataMember]
        public string EmergencyContactNo { get; set; }

        [DataMember]
        public string EmergencyContactRelation { get; set; }

        [DataMember]
        public DateTime DOB { get; set; }

        [DataMember]
        public string POB { get; set; }

        [DataMember]
        public int POBID { get; set; }

        [DataMember]
        public string NativePlace { get; set; }

        [DataMember]
        public int NationalityID { get; set; }

        [DataMember]
        public string IDCardNo { get; set; }

        [DataMember]
        public DateTime IDDOI { get; set; }

        [DataMember]
        public int IDPOI { get; set; }

        [DataMember]
        public string PassportNO { get; set; }

        [DataMember]
        public DateTime PassportDOI { get; set; }

        [DataMember]
        public string PassportPOI { get; set; }

        [DataMember]
        public DateTime PassportDOE { get; set; }

        [DataMember]
        public string PermanentAddress { get; set; }

        [DataMember]
        public string TempAddress { get; set; }

        [DataMember]
        public string HomePhone { get; set; }

        [DataMember]
        public string HandPhone { get; set; }

        [DataMember]
        public string PersonalEmail { get; set; }

        [DataMember]
        public string TaxCode { get; set; }

        [DataMember]
        public string SocialBookNo { get; set; }

        [DataMember]
        public DateTime SocialBookDOI { get; set; }

        [DataMember]
        public int SocialBookStatusID { get; set; }

        [DataMember]
        public int RegisteredHospitalID { get; set; }

        [DataMember]
        public string RelatedExperience { get; set; }

        [DataMember]
        public int Age { get; set; }

        [DataMember]
        public int EmpSourceID { get; set; }

        [DataMember]
        public int DistanceToOfficeID { get; set; }

        [DataMember]
        public string ForeignLanguage { get; set; }

        [DataMember]
        public string InternationalTravelTo { get; set; }

        [DataMember]
        public string OtherSkills { get; set; }

        [DataMember]
        public decimal SickLeaveDays { get; set; }

        [DataMember]
        public bool SIEligibility { get; set; }

        [DataMember]
        public DateTime SIStartDate { get; set; }

        [DataMember]
        public bool HIEligibility { get; set; }

        [DataMember]
        public DateTime HIStartDate { get; set; }

        [DataMember]
        public bool UIEligibility { get; set; }

        [DataMember]
        public DateTime UIStartDate { get; set; }

        [DataMember]
        public int PickupArea { get; set; }

        [DataMember]
        public string HomeCity { get; set; }

        [DataMember]
        public int HomeCountry { get; set; }

        [DataMember]
        public string TempCity { get; set; }

        [DataMember]
        public int TempCountry { get; set; }

        [DataMember]
        public string AwardsInBosch { get; set; }

        [DataMember]
        public int EmployeeStatusID { get; set; }

        [DataMember]
        public string TerminationDecisionNo { get; set; }

        [DataMember]
        public DateTime TerminationDate { get; set; }

        [DataMember]
        public int TerminationReasonID { get; set; }

        [DataMember]
        public DateTime ExitInterViewDate { get; set; }

        [DataMember]
        public int EmployeeTypeID { get; set; }

        [DataMember]
        public int EmployeeSubTypeID { get; set; }

        [DataMember]
        public string EmployeeTypeCode { get; set; }

        [DataMember]
        public string LabourBookNo { get; set; }

        [DataMember]
        public DateTime LabourBookDOI { get; set; }

        [DataMember]
        public string LabourBookPOI { get; set; }

        [DataMember]
        public decimal AnnualLeaveDays { get; set; }

        [DataMember]
        public int AssignmentID { get; set; }

        [DataMember]
        public bool IsActive { get; set; }

        [DataMember]
        public int CreatedBy { get; set; }

        [DataMember]
        public DateTime CreatedDate { get; set; }

        [DataMember]
        public int ModifiedBy { get; set; }

        [DataMember]
        public DateTime ModifiedDate { get; set; }

        [DataMember]
        public int ContractManager { get; set; }

        [DataMember]
        public int SubGroupCode { get; set; }

        [DataMember]
        public bool RegForKiosk { get; set; }

        [DataMember]
        public bool IsPersonCapacity { get; set; }

        [DataMember]
        public decimal OverAllExperience1 { get; set; }
        [DataMember]        public int EntityID { get; set; }        

    }
    public class T_COM_Master_EmployeeCollection : BaseDBEntityCollection<T_COM_Master_Employee> { }
    public class T_COM_Master_EmployeeManager:IServiceManager<T_COM_Master_Employee>
    {
        private static T_COM_Master_Employee GetItemFromReader(IDataReader dataReader)
        {
            T_COM_Master_Employee objItem = new T_COM_Master_Employee();

            objItem.EmployeeCode = SqlHelper.GetInt(dataReader, "EmployeeCode");

            objItem.EmployeeNo = SqlHelper.GetString(dataReader, "EmployeeNo");

            objItem.FirstName_EN = SqlHelper.GetString(dataReader, "FirstName_EN");

            objItem.MiddleName_EN = SqlHelper.GetString(dataReader, "MiddleName_EN");

            objItem.LastName_EN = SqlHelper.GetString(dataReader, "LastName_EN");

            objItem.FirstName_VN = SqlHelper.GetString(dataReader, "FirstName_VN");

            objItem.MiddleName_VN = SqlHelper.GetString(dataReader, "MiddleName_VN");

            objItem.LastName_VN = SqlHelper.GetString(dataReader, "LastName_VN");

            objItem.Email = SqlHelper.GetString(dataReader, "Email");

            objItem.DomainId = SqlHelper.GetString(dataReader, "DomainId");

            objItem.ESSPassword = SqlHelper.GetString(dataReader, "ESSPassword");

            objItem.LocationId = SqlHelper.GetInt(dataReader, "LocationId");

            objItem.GroupCode = SqlHelper.GetInt(dataReader, "GroupCode");

            objItem.SessionCode = SqlHelper.GetString(dataReader, "SessionCode");

            objItem.DirectManager = SqlHelper.GetInt(dataReader, "DirectManager");

            objItem.IndirectManager = SqlHelper.GetInt(dataReader, "IndirectManager");

            objItem.Gender = SqlHelper.GetString(dataReader, "Gender");

            objItem.CategoryID = SqlHelper.GetInt(dataReader, "CategoryID");

            objItem.WorkingHours = SqlHelper.GetInt(dataReader, "WorkingHours");

            objItem.JoinedDate = SqlHelper.GetDateTime(dataReader, "JoinedDate");

            objItem.ProbationDays = SqlHelper.GetInt(dataReader, "ProbationDays");

            objItem.BloodGroup = SqlHelper.GetString(dataReader, "BloodGroup");

            objItem.MaritalStatusID = SqlHelper.GetInt(dataReader, "MaritalStatusID");

            objItem.EmergencyContactName = SqlHelper.GetString(dataReader, "EmergencyContactName");

            objItem.EmergencyContactNo = SqlHelper.GetString(dataReader, "EmergencyContactNo");

            objItem.EmergencyContactRelation = SqlHelper.GetString(dataReader, "EmergencyContactRelation");

            objItem.DOB = SqlHelper.GetDateTime(dataReader, "DOB");

            objItem.POB = SqlHelper.GetString(dataReader, "POB");

            objItem.POBID = SqlHelper.GetInt(dataReader, "POBID");

            objItem.NativePlace = SqlHelper.GetString(dataReader, "NativePlace");

            objItem.NationalityID = SqlHelper.GetInt(dataReader, "NationalityID");

            objItem.IDCardNo = SqlHelper.GetString(dataReader, "IDCardNo");

            objItem.IDDOI = SqlHelper.GetDateTime(dataReader, "IDDOI");

            objItem.IDPOI = SqlHelper.GetInt(dataReader, "IDPOI");

            objItem.PassportNO = SqlHelper.GetString(dataReader, "PassportNO");

            objItem.PassportDOI = SqlHelper.GetDateTime(dataReader, "PassportDOI");

            objItem.PassportPOI = SqlHelper.GetString(dataReader, "PassportPOI");

            objItem.PassportDOE = SqlHelper.GetDateTime(dataReader, "PassportDOE");

            objItem.PermanentAddress = SqlHelper.GetString(dataReader, "PermanentAddress");

            objItem.TempAddress = SqlHelper.GetString(dataReader, "TempAddress");

            objItem.HomePhone = SqlHelper.GetString(dataReader, "HomePhone");

            objItem.HandPhone = SqlHelper.GetString(dataReader, "HandPhone");

            objItem.PersonalEmail = SqlHelper.GetString(dataReader, "PersonalEmail");

            objItem.TaxCode = SqlHelper.GetString(dataReader, "TaxCode");

            objItem.SocialBookNo = SqlHelper.GetString(dataReader, "SocialBookNo");

            objItem.SocialBookDOI = SqlHelper.GetDateTime(dataReader, "SocialBookDOI");

            objItem.SocialBookStatusID = SqlHelper.GetInt(dataReader, "SocialBookStatusID");

            objItem.RegisteredHospitalID = SqlHelper.GetInt(dataReader, "RegisteredHospitalID");

            objItem.RelatedExperience = SqlHelper.GetString(dataReader, "RelatedExperience");

            objItem.Age = SqlHelper.GetInt(dataReader, "Age");

            objItem.EmpSourceID = SqlHelper.GetInt(dataReader, "EmpSourceID");

            objItem.DistanceToOfficeID = SqlHelper.GetInt(dataReader, "DistanceToOfficeID");

            objItem.ForeignLanguage = SqlHelper.GetString(dataReader, "ForeignLanguage");

            objItem.InternationalTravelTo = SqlHelper.GetString(dataReader, "InternationalTravelTo");

            objItem.OtherSkills = SqlHelper.GetString(dataReader, "OtherSkills");

            objItem.SickLeaveDays = SqlHelper.GetDecimal(dataReader, "SickLeaveDays");

            objItem.SIEligibility = SqlHelper.GetBoolean(dataReader, "SIEligibility");

            objItem.SIStartDate = SqlHelper.GetDateTime(dataReader, "SIStartDate");

            objItem.HIEligibility = SqlHelper.GetBoolean(dataReader, "HIEligibility");

            objItem.HIStartDate = SqlHelper.GetDateTime(dataReader, "HIStartDate");

            objItem.UIEligibility = SqlHelper.GetBoolean(dataReader, "UIEligibility");

            objItem.UIStartDate = SqlHelper.GetDateTime(dataReader, "UIStartDate");

            objItem.PickupArea = SqlHelper.GetInt(dataReader, "PickupArea");

            objItem.HomeCity = SqlHelper.GetString(dataReader, "HomeCity");

            objItem.HomeCountry = SqlHelper.GetInt(dataReader, "HomeCountry");

            objItem.TempCity = SqlHelper.GetString(dataReader, "TempCity");

            objItem.TempCountry = SqlHelper.GetInt(dataReader, "TempCountry");

            objItem.AwardsInBosch = SqlHelper.GetString(dataReader, "AwardsInBosch");

            objItem.EmployeeStatusID = SqlHelper.GetInt(dataReader, "EmployeeStatusID");

            objItem.TerminationDecisionNo = SqlHelper.GetString(dataReader, "TerminationDecisionNo");

            objItem.TerminationDate = SqlHelper.GetDateTime(dataReader, "TerminationDate");

            objItem.TerminationReasonID = SqlHelper.GetInt(dataReader, "TerminationReasonID");

            objItem.ExitInterViewDate = SqlHelper.GetDateTime(dataReader, "ExitInterViewDate");

            objItem.EmployeeTypeID = SqlHelper.GetInt(dataReader, "EmployeeTypeID");

            objItem.EmployeeSubTypeID = SqlHelper.GetInt(dataReader, "EmployeeSubTypeID");

            objItem.EmployeeTypeCode = SqlHelper.GetString(dataReader, "EmployeeTypeCode");

            objItem.LabourBookNo = SqlHelper.GetString(dataReader, "LabourBookNo");

            objItem.LabourBookDOI = SqlHelper.GetDateTime(dataReader, "LabourBookDOI");

            objItem.LabourBookPOI = SqlHelper.GetString(dataReader, "LabourBookPOI");

            objItem.AnnualLeaveDays = SqlHelper.GetDecimal(dataReader, "AnnualLeaveDays");

            objItem.AssignmentID = SqlHelper.GetInt(dataReader, "AssignmentID");

            objItem.IsActive = SqlHelper.GetBoolean(dataReader, "IsActive");

            objItem.CreatedBy = SqlHelper.GetInt(dataReader, "CreatedBy");

            objItem.CreatedDate = SqlHelper.GetDateTime(dataReader, "CreatedDate");

            objItem.ModifiedBy = SqlHelper.GetInt(dataReader, "ModifiedBy");

            objItem.ModifiedDate = SqlHelper.GetDateTime(dataReader, "ModifiedDate");

            objItem.ContractManager = SqlHelper.GetInt(dataReader, "ContractManager");

            objItem.SubGroupCode = SqlHelper.GetInt(dataReader, "SubGroupCode");

            objItem.RegForKiosk = SqlHelper.GetBoolean(dataReader, "RegForKiosk");

            objItem.IsPersonCapacity = SqlHelper.GetBoolean(dataReader, "IsPersonCapacity");

            objItem.OverAllExperience1 = SqlHelper.GetDecimal(dataReader, "OverAllExperience1");

            if (SqlHelper.ColumnExists(dataReader, "EntityID"))
            {
                objItem.EntityID = SqlHelper.GetInt(dataReader, "EntityID");
            }

            if (SqlHelper.ColumnExists(dataReader, "TotalRecord"))
            {
                objItem.TotalRecord = SqlHelper.GetInt(dataReader, "TotalRecord");
            }
            return objItem;
        }
        public static T_COM_Master_Employee GetItemByID(int EmployeeCode)
        {
            T_COM_Master_Employee item = new T_COM_Master_Employee();
            var sqlParams = new SqlParameter[1];
            sqlParams[0] = new SqlParameter("@EmployeeCode", EmployeeCode);
            using (var reader = SqlHelper.ExecuteReaderService(ModuleConfig.MyConnection, "T_COM_Master_Employee_GetByID", sqlParams))
            {
                while (reader.Read())
                {
                    item = GetItemFromReader(reader);
                }
            }
            return item;


        }
        public static T_COM_Master_Employee GetItemByDomainId(string DomainID)
        {
            T_COM_Master_Employee item = new T_COM_Master_Employee();
            var sqlParams = new SqlParameter[1];
            sqlParams[0] = new SqlParameter("@DomainID", DomainID);
            using (var reader = SqlHelper.ExecuteReaderService(ModuleConfig.MyConnection, "T_COM_Master_Employee_GetByDomainId", sqlParams))
            {
                while (reader.Read())
                {
                    item = GetItemFromReader(reader);
                }
            }
            return item;


        }
        public static T_COM_Master_Employee AddItem(T_COM_Master_Employee model)
        {
            int result = 0;
            using (var reader = SqlHelper.ExecuteReaderService(ModuleConfig.MyConnection, CommandType.StoredProcedure, "T_COM_Master_Employee_Add", CreateSqlParameter(model)))
            {
                while (reader.Read())
                {
                    result = (int)reader[0];
                }
            }
            return GetItemByID(result);

        }
        public static T_COM_Master_Employee UpdateItem(T_COM_Master_Employee model)
        {
            int result = 0;
            using (var reader = SqlHelper.ExecuteReaderService(ModuleConfig.MyConnection, CommandType.StoredProcedure, "T_COM_Master_Employee_Update", CreateSqlParameter(model)))
            {
                while (reader.Read())
                {
                    result = (int)reader[0];
                }
            }
            return GetItemByID(result);

        }
        public static T_COM_Master_EmployeeCollection GetAllItem()
        {
            T_COM_Master_EmployeeCollection collection = new T_COM_Master_EmployeeCollection();
            using (var reader = SqlHelper.ExecuteReaderService(ModuleConfig.MyConnection, "T_COM_Master_Employee_GetAll", null))
            {
                while (reader.Read())
                {
                    T_COM_Master_Employee obj = new T_COM_Master_Employee();
                    obj = GetItemFromReader(reader);
                    collection.Add(obj);
                }
            }
            return collection;
        }
        public static DataTable GetbyEntity(string EntityID, DateTime? ReportDate)
        {
            DataTable dt= new DataTable();

            DataSet ds = SqlHelper.ExecuteDataset(ModuleConfig.MyConnection, "USP_CMS_ListEmployee_Get", 90, new SqlParameter[]
                {
                    new SqlParameter("@EntityID", EntityID),
                    new SqlParameter("@ReportDate", ReportDate.Value.ToShortTimeString()
                    ),
                });
            if (ds.Tables.Count > 0)
            {
                dt = ds.Tables[0];
            }
          
            return dt;
        }
        public static T_COM_Master_EmployeeCollection Search(RBVHSearchFilter SearchKey)
        {
            T_COM_Master_EmployeeCollection collection = new T_COM_Master_EmployeeCollection();
            using (var reader = SqlHelper.ExecuteReaderService(ModuleConfig.MyConnection, "T_COM_Master_Employee_Search", SearchFilterManager.SqlSearchConditionNoCompany(SearchKey)))
            {
                while (reader.Read())
                {
                    T_COM_Master_Employee obj = new T_COM_Master_Employee();
                    obj = GetItemFromReader(reader);
                    collection.Add(obj);
                }
            }
            return collection;
        }
        public static T_COM_Master_EmployeeCollection Search(SearchFilter SearchKey)
        {
            T_COM_Master_EmployeeCollection collection = new T_COM_Master_EmployeeCollection();
            using (var reader = SqlHelper.ExecuteReaderService(ModuleConfig.MyConnection, "T_COM_Master_Employee_Search", SearchFilterManager.SqlSearchConditionNoCompany(SearchKey)))
            {
                while (reader.Read())
                {
                    T_COM_Master_Employee obj = new T_COM_Master_Employee();
                    obj = GetItemFromReader(reader);
                    collection.Add(obj);
                }
            }
            return collection;
        }
        public static T_COM_Master_EmployeeCollection GetbyUser(string CreatedUser)
        {
            T_COM_Master_EmployeeCollection collection = new T_COM_Master_EmployeeCollection();
            T_COM_Master_Employee obj;
            using (var reader = SqlHelper.ExecuteReaderService(ModuleConfig.MyConnection, "T_COM_Master_Employee_GetAll_byUser", new SqlParameter("@CreatedUser", CreatedUser)))
            {
                while (reader.Read())
                {
                    obj = GetItemFromReader(reader);
                    collection.Add(obj);
                }
            }
            return collection;
        }

        private static SqlParameter[] CreateSqlParameter(T_COM_Master_Employee model)
        {
            return new SqlParameter[]
                {
                    new SqlParameter("@EmployeeCode", model.EmployeeCode),
                    new SqlParameter("@EmployeeNo", model.EmployeeNo),
                    new SqlParameter("@FirstName_EN", model.FirstName_EN),
                    new SqlParameter("@MiddleName_EN", model.MiddleName_EN),
                    new SqlParameter("@LastName_EN", model.LastName_EN),
                    new SqlParameter("@FirstName_VN", model.FirstName_VN),
                    new SqlParameter("@MiddleName_VN", model.MiddleName_VN),
                    new SqlParameter("@LastName_VN", model.LastName_VN),
                    new SqlParameter("@Email", model.Email),
                    new SqlParameter("@DomainId", model.DomainId),
                    new SqlParameter("@ESSPassword", model.ESSPassword),
                    new SqlParameter("@LocationId", model.LocationId),
                    new SqlParameter("@GroupCode", model.GroupCode),
                    new SqlParameter("@SessionCode", model.SessionCode),
                    new SqlParameter("@DirectManager", model.DirectManager),
                    new SqlParameter("@IndirectManager", model.IndirectManager),
                    new SqlParameter("@Gender", model.Gender),
                    new SqlParameter("@CategoryID", model.CategoryID),
                    new SqlParameter("@WorkingHours", model.WorkingHours),
                    new SqlParameter("@JoinedDate", model.JoinedDate),
                    new SqlParameter("@ProbationDays", model.ProbationDays),
                    new SqlParameter("@BloodGroup", model.BloodGroup),
                    new SqlParameter("@MaritalStatusID", model.MaritalStatusID),
                    new SqlParameter("@EmergencyContactName", model.EmergencyContactName),
                    new SqlParameter("@EmergencyContactNo", model.EmergencyContactNo),
                    new SqlParameter("@EmergencyContactRelation", model.EmergencyContactRelation),
                    new SqlParameter("@DOB", model.DOB),
                    new SqlParameter("@POB", model.POB),
                    new SqlParameter("@POBID", model.POBID),
                    new SqlParameter("@NativePlace", model.NativePlace),
                    new SqlParameter("@NationalityID", model.NationalityID),
                    new SqlParameter("@IDCardNo", model.IDCardNo),
                    new SqlParameter("@IDDOI", model.IDDOI),
                    new SqlParameter("@IDPOI", model.IDPOI),
                    new SqlParameter("@PassportNO", model.PassportNO),
                    new SqlParameter("@PassportDOI", model.PassportDOI),
                    new SqlParameter("@PassportPOI", model.PassportPOI),
                    new SqlParameter("@PassportDOE", model.PassportDOE),
                    new SqlParameter("@PermanentAddress", model.PermanentAddress),
                    new SqlParameter("@TempAddress", model.TempAddress),
                    new SqlParameter("@HomePhone", model.HomePhone),
                    new SqlParameter("@HandPhone", model.HandPhone),
                    new SqlParameter("@PersonalEmail", model.PersonalEmail),
                    new SqlParameter("@TaxCode", model.TaxCode),
                    new SqlParameter("@SocialBookNo", model.SocialBookNo),
                    new SqlParameter("@SocialBookDOI", model.SocialBookDOI),
                    new SqlParameter("@SocialBookStatusID", model.SocialBookStatusID),
                    new SqlParameter("@RegisteredHospitalID", model.RegisteredHospitalID),
                    new SqlParameter("@RelatedExperience", model.RelatedExperience),
                    new SqlParameter("@Age", model.Age),
                    new SqlParameter("@EmpSourceID", model.EmpSourceID),
                    new SqlParameter("@DistanceToOfficeID", model.DistanceToOfficeID),
                    new SqlParameter("@ForeignLanguage", model.ForeignLanguage),
                    new SqlParameter("@InternationalTravelTo", model.InternationalTravelTo),
                    new SqlParameter("@OtherSkills", model.OtherSkills),
                    new SqlParameter("@SickLeaveDays", model.SickLeaveDays),
                    new SqlParameter("@SIEligibility", model.SIEligibility),
                    new SqlParameter("@SIStartDate", model.SIStartDate),
                    new SqlParameter("@HIEligibility", model.HIEligibility),
                    new SqlParameter("@HIStartDate", model.HIStartDate),
                    new SqlParameter("@UIEligibility", model.UIEligibility),
                    new SqlParameter("@UIStartDate", model.UIStartDate),
                    new SqlParameter("@PickupArea", model.PickupArea),
                    new SqlParameter("@HomeCity", model.HomeCity),
                    new SqlParameter("@HomeCountry", model.HomeCountry),
                    new SqlParameter("@TempCity", model.TempCity),
                    new SqlParameter("@TempCountry", model.TempCountry),
                    new SqlParameter("@AwardsInBosch", model.AwardsInBosch),
                    new SqlParameter("@EmployeeStatusID", model.EmployeeStatusID),
                    new SqlParameter("@TerminationDecisionNo", model.TerminationDecisionNo),
                    new SqlParameter("@TerminationDate", model.TerminationDate),
                    new SqlParameter("@TerminationReasonID", model.TerminationReasonID),
                    new SqlParameter("@ExitInterViewDate", model.ExitInterViewDate),
                    new SqlParameter("@EmployeeTypeID", model.EmployeeTypeID),
                    new SqlParameter("@EmployeeSubTypeID", model.EmployeeSubTypeID),
                    new SqlParameter("@EmployeeTypeCode", model.EmployeeTypeCode),
                    new SqlParameter("@LabourBookNo", model.LabourBookNo),
                    new SqlParameter("@LabourBookDOI", model.LabourBookDOI),
                    new SqlParameter("@LabourBookPOI", model.LabourBookPOI),
                    new SqlParameter("@AnnualLeaveDays", model.AnnualLeaveDays),
                    new SqlParameter("@AssignmentID", model.AssignmentID),
                    new SqlParameter("@IsActive", model.IsActive),
                    new SqlParameter("@CreatedBy", model.CreatedBy),
                    new SqlParameter("@CreatedDate", model.CreatedDate),
                    new SqlParameter("@ModifiedBy", model.ModifiedBy),
                    new SqlParameter("@ModifiedDate", model.ModifiedDate),
                    new SqlParameter("@ContractManager", model.ContractManager),
                    new SqlParameter("@SubGroupCode", model.SubGroupCode),
                    new SqlParameter("@RegForKiosk", model.RegForKiosk),
                    new SqlParameter("@IsPersonCapacity", model.IsPersonCapacity),
                    new SqlParameter("@OverAllExperience1", model.OverAllExperience1),

                };
        }

        public static int DeleteItem(int itemID)
        {
            return SqlHelper.ExecuteNonQuery("T_COM_Master_Employee_Delete", itemID);
        }

        #region IservicerBase
        public virtual T_COM_Master_Employee Get(GetParam value)
        {
            T_COM_Master_Employee item = new T_COM_Master_Employee();
            var sqlParams = new SqlParameter[1];
            sqlParams[0] = new SqlParameter("@CompanyID", value.CompanyID);
            using (var reader = SqlHelper.ExecuteReader("T_COM_Master_Employee_GetByID", sqlParams))
            {
                while (reader.Read())
                {
                    item = GetItemFromReader(reader);
                }
            }
            return item;
        }
        public virtual IEnumerable<T_COM_Master_Employee> GetSearch(SearchFilter value)
        {
            return Search(value);
        }

        public virtual T_COM_Master_Employee Add(T_COM_Master_Employee model)
        {
            Int32 result = 0;
            using (var reader = SqlHelper.ExecuteReader(CommandType.StoredProcedure, "T_COM_Master_Employee_Add", CreateSqlParameter(model)))
            {
                while (reader.Read())
                {
                    result = (Int32)reader[0];
                }
            }
            return GetItemByID(result);
        }

        public virtual T_COM_Master_Employee Update(T_COM_Master_Employee model)
        {
            Int32 result = 0;
            using (var reader = SqlHelper.ExecuteReader(CommandType.StoredProcedure, "T_COM_Master_Employee_Update", CreateSqlParameter(model)))
            {
                while (reader.Read())
                {
                    result = (Int32)reader[0];
                }
            }
            return GetItemByID(result);

        }

        public virtual int Del(GetParam value)
        {
            return SqlHelper.ExecuteNonQuery("T_COM_Master_Employee_Delete", value.ID);
        }
        #endregion
    }
}