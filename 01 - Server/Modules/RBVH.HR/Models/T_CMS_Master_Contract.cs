using System.Text;
using System.Data;
using System.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using DTP.Data;
using DTP.Data.Models;
//using DTP.Object;
namespace RBVH.HR.Models
{
    [DataContract]
    public class T_CMS_Master_Contract : BaseDBEntity
    {

        [DataMember]
        public int ID { get; set; }

        [DataMember]
        public string Mode { get; set; }

        [DataMember]
        public int EmployeeCode { get; set; }

        [DataMember]
        public string ContractNo { get; set; }

        [DataMember]
        public int StatusID { get; set; }

        [DataMember]
        public int ApproverLevel { get; set; }

        [DataMember]
        public int ContractLevel { get; set; }

        [DataMember]
        public int SalutationID { get; set; }

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
        public DateTime DOB { get; set; }

        [DataMember]
        public string POB { get; set; }

        [DataMember]
        public string HighestDegree { get; set; }

        [DataMember]
        public string PerAddress { get; set; }

        [DataMember]
        public string IDCardNo { get; set; }

        [DataMember]
        public DateTime IDDOI { get; set; }

        [DataMember]
        public int IDPOI { get; set; }

        [DataMember]
        public string PassportNo { get; set; }

        [DataMember]
        public DateTime? PassportDOI { get; set; }

        [DataMember]
        public string PassportPOI { get; set; }

        [DataMember]
        public string LabourBookNo { get; set; }

        [DataMember]
        public DateTime? LabourDOI { get; set; }

        [DataMember]
        public string LabourPOI { get; set; }

        [DataMember]
        public int ContractTerm { get; set; }

        [DataMember]
        public DateTime Joiningdate { get; set; }

        [DataMember]
        public DateTime Enddate { get; set; }

        [DataMember]
        public int PositionID { get; set; }

        [DataMember]
        public int LocationID { get; set; }

        [DataMember]
        public int GradeID { get; set; }

        [DataMember]
        public int DeptID { get; set; }

        [DataMember]
        public int WorkHoursID { get; set; }

        [DataMember]
        public decimal Grossoffer { get; set; }

        [DataMember]
        public int AnnualLeave { get; set; }

        [DataMember]
        public int EmpTypeID { get; set; }

        [DataMember]
        public int EmpSubTypeID { get; set; }

        [DataMember]
        public string WorkPermitNo { get; set; }

        [DataMember]
        public DateTime WorkPermitFrom { get; set; }

        [DataMember]
        public DateTime WorkPermitTo { get; set; }

        [DataMember]
        public decimal HomeGrossOffer { get; set; }

        [DataMember]
        public string HomeCountryCurrency { get; set; }

        [DataMember]
        public DateTime HomeGrossOfferEffectiveFrom { get; set; }

        [DataMember]
        public DateTime HomeGrossOfferEffectiveTo { get; set; }

        [DataMember]
        public string RelocationallowanceCurrency { get; set; }

        [DataMember]
        public decimal Relocationallowance { get; set; }

        [DataMember]
        public string GoabroadallowanceCurrency { get; set; }

        [DataMember]
        public decimal Goabroadallowance { get; set; }

        [DataMember]
        public string WaivingallowanceCurrency { get; set; }

        [DataMember]
        public decimal Waivingallowance { get; set; }

        [DataMember]
        public string HostCountryCurrency { get; set; }

        [DataMember]
        public DateTime HostGrossOfferEffectiveFrom { get; set; }

        [DataMember]
        public DateTime HostGrossOfferEffectiveTo { get; set; }

        [DataMember]
        public string Remarks { get; set; }

        [DataMember]
        public int CreatedBy { get; set; }

        [DataMember]
        public DateTime CreatedDate { get; set; }

        [DataMember]
        public int ModifiedBy { get; set; }

        [DataMember]
        public DateTime ModifiedDate { get; set; }

        [DataMember]
        public bool IsActive { get; set; }

        [DataMember]
        public int ProbationsPeriod { get; set; }

        [DataMember]
        public DateTime? OriginalDate { get; set; }

        [DataMember]
        public int WorkFlowStatus { get; set; }

        [DataMember]
        public string HandPhone { get; set; }

        [DataMember]
        public string TempAddress { get; set; }

        [DataMember]
        public string JobDesc { get; set; }


    }
    public class T_CMS_Master_ContractCollection : BaseDBEntityCollection<T_CMS_Master_Contract> { }
    public class T_CMS_Master_ContractManager
    {
        private static T_CMS_Master_Contract GetItemFromReader(IDataReader dataReader)
        {
            T_CMS_Master_Contract objItem = new T_CMS_Master_Contract();

            objItem.ID = SqlHelper.GetInt(dataReader, "ID");

            objItem.EmployeeCode = SqlHelper.GetInt(dataReader, "EmployeeCode");

            objItem.ContractNo = SqlHelper.GetString(dataReader, "ContractNo");

            objItem.StatusID = SqlHelper.GetInt(dataReader, "StatusID");

            objItem.ApproverLevel = SqlHelper.GetInt(dataReader, "ApproverLevel");

            objItem.ContractLevel = SqlHelper.GetInt(dataReader, "ContractLevel");

            objItem.SalutationID = SqlHelper.GetInt(dataReader, "SalutationID");

            objItem.FirstName_EN = SqlHelper.GetString(dataReader, "FirstName_EN");

            objItem.MiddleName_EN = SqlHelper.GetString(dataReader, "MiddleName_EN");

            objItem.LastName_EN = SqlHelper.GetString(dataReader, "LastName_EN");

            objItem.FirstName_VN = SqlHelper.GetString(dataReader, "FirstName_VN");

            objItem.MiddleName_VN = SqlHelper.GetString(dataReader, "MiddleName_VN");

            objItem.LastName_VN = SqlHelper.GetString(dataReader, "LastName_VN");

            objItem.DOB = SqlHelper.GetDateTime(dataReader, "DOB");

            objItem.POB = SqlHelper.GetString(dataReader, "POB");

            objItem.HighestDegree = SqlHelper.GetString(dataReader, "HighestDegree");

            objItem.PerAddress = SqlHelper.GetString(dataReader, "EmpAddress");

            objItem.IDCardNo = SqlHelper.GetString(dataReader, "IDCardNo");

            objItem.IDDOI = SqlHelper.GetDateTime(dataReader, "IDDOI");

            objItem.IDPOI = SqlHelper.GetInt(dataReader, "IDPOI");

            objItem.PassportNo = SqlHelper.GetString(dataReader, "PassportNo");

            objItem.PassportDOI = SqlHelper.GetNDateTime(dataReader, "PassportDOI");

            objItem.PassportPOI = SqlHelper.GetString(dataReader, "PassportPOI");

            objItem.LabourBookNo = SqlHelper.GetString(dataReader, "LabourBookNo");

            objItem.LabourDOI = SqlHelper.GetNDateTime(dataReader, "LabourDOI");

            objItem.LabourPOI = SqlHelper.GetString(dataReader, "LabourPOI");

            objItem.ContractTerm = SqlHelper.GetInt(dataReader, "ContractTerm");

            objItem.Joiningdate = SqlHelper.GetDateTime(dataReader, "Joiningdate");

            objItem.Enddate = SqlHelper.GetDateTime(dataReader, "Enddate");

            objItem.PositionID = SqlHelper.GetInt(dataReader, "PositionID");

            objItem.LocationID = SqlHelper.GetInt(dataReader, "LocationID");

            objItem.GradeID = SqlHelper.GetInt(dataReader, "GradeID");

            objItem.DeptID = SqlHelper.GetInt(dataReader, "DeptID");

            objItem.WorkHoursID = SqlHelper.GetInt(dataReader, "WorkHoursID");

            objItem.Grossoffer = SqlHelper.GetDecimal(dataReader, "Grossoffer");

            objItem.AnnualLeave = SqlHelper.GetInt(dataReader, "AnnualLeave");

            objItem.EmpTypeID = SqlHelper.GetInt(dataReader, "EmpTypeID");

            objItem.EmpSubTypeID = SqlHelper.GetInt(dataReader, "EmpSubTypeID");

            objItem.WorkPermitNo = SqlHelper.GetString(dataReader, "WorkPermitNo");

            objItem.WorkPermitFrom = SqlHelper.GetDateTime(dataReader, "WorkPermitFrom");

            objItem.WorkPermitTo = SqlHelper.GetDateTime(dataReader, "WorkPermitTo");

            objItem.HomeGrossOffer = SqlHelper.GetDecimal(dataReader, "HomeGrossOffer");

            objItem.HomeCountryCurrency = SqlHelper.GetString(dataReader, "HomeCountryCurrency");

            objItem.HomeGrossOfferEffectiveFrom = SqlHelper.GetDateTime(dataReader, "HomeGrossOfferEffectiveFrom");

            objItem.HomeGrossOfferEffectiveTo = SqlHelper.GetDateTime(dataReader, "HomeGrossOfferEffectiveTo");

            objItem.RelocationallowanceCurrency = SqlHelper.GetString(dataReader, "RelocationallowanceCurrency");

            objItem.Relocationallowance = SqlHelper.GetDecimal(dataReader, "Relocationallowance");

            objItem.GoabroadallowanceCurrency = SqlHelper.GetString(dataReader, "GoabroadallowanceCurrency");

            objItem.Goabroadallowance = SqlHelper.GetDecimal(dataReader, "Goabroadallowance");

            objItem.WaivingallowanceCurrency = SqlHelper.GetString(dataReader, "WaivingallowanceCurrency");

            objItem.Waivingallowance = SqlHelper.GetDecimal(dataReader, "Waivingallowance");

            objItem.HostCountryCurrency = SqlHelper.GetString(dataReader, "HostCountryCurrency");

            objItem.HostGrossOfferEffectiveFrom = SqlHelper.GetDateTime(dataReader, "HostGrossOfferEffectiveFrom");

            objItem.HostGrossOfferEffectiveTo = SqlHelper.GetDateTime(dataReader, "HostGrossOfferEffectiveTo");

            objItem.Remarks = SqlHelper.GetString(dataReader, "Remarks");

            objItem.CreatedBy = SqlHelper.GetInt(dataReader, "CreatedBy");

            objItem.CreatedDate = SqlHelper.GetDateTime(dataReader, "CreatedDate");

            objItem.ModifiedBy = SqlHelper.GetInt(dataReader, "ModifiedBy");

            objItem.ModifiedDate = SqlHelper.GetDateTime(dataReader, "ModifiedDate");

            objItem.IsActive = SqlHelper.GetBoolean(dataReader, "IsActive");

            objItem.ProbationsPeriod = SqlHelper.GetInt(dataReader, "ProbationsPeriod");

            objItem.OriginalDate = SqlHelper.GetDateTime(dataReader, "OriginalDate");

            objItem.WorkFlowStatus = SqlHelper.GetInt(dataReader, "WorkFlowStatus");

            objItem.HandPhone = SqlHelper.GetString(dataReader, "HandPhone");

            objItem.TempAddress = SqlHelper.GetString(dataReader, "TempAddress");

            if (SqlHelper.ColumnExists(dataReader, "JobDesc"))
            {
                objItem.JobDesc = SqlHelper.GetString(dataReader, "JobDesc");
            }
            if (SqlHelper.ColumnExists(dataReader, "TotalRecord"))
            {
                objItem.TotalRecord = SqlHelper.GetInt(dataReader, "TotalRecord");
            }

            return objItem;
        }
        public static T_CMS_Master_Contract GetItemByID(int ID)
        {
            T_CMS_Master_Contract item = new T_CMS_Master_Contract();
            var sqlParams = new SqlParameter[1];
            sqlParams[0] = new SqlParameter("@ID", ID);
            using (var reader = SqlHelper.ExecuteReaderService(ModuleConfig.MyConnection, "T_CMS_Master_Contract_GetByID", sqlParams))
            {
                while (reader.Read())
                {
                    item = GetItemFromReader(reader);
                }
            }
            return item;


        }
        private static QueuedEmail GetQueMessageFromReader(IDataReader dataReader,int CompanyID)
        {
            QueuedEmail objItem = new QueuedEmail();

            objItem.CompanyID = CompanyID;
            objItem.Id = 0;
            objItem.PriorityId = SqlHelper.GetInt(dataReader, "MailPriority"); ;
            objItem.From = SqlHelper.GetString(dataReader, "FromId");
            objItem.To = SqlHelper.GetString(dataReader, "ToId");
            objItem.CC = SqlHelper.GetString(dataReader, "CcId");
            objItem.Bcc = SqlHelper.GetString(dataReader, "BccId");
            objItem.Subject = SqlHelper.GetString(dataReader, "MailSubject");
            objItem.Body = SqlHelper.GetString(dataReader, "MailContent");
			objItem.FromName = SqlHelper.GetString(dataReader, "SentBy");
			//,Remarks
            return objItem;
        }
        public static QueuedEmailCollection ReminderContractEmailList(int CompanyID)
        {
            QueuedEmailCollection collection = new QueuedEmailCollection();
            //var sqlParams = new SqlParameter[1];
            //sqlParams[0] = new SqlParameter("@isActive", isActive);
            using (var reader = SqlHelper.ExecuteLongReaderService(ModuleConfig.MyConnection,600, "USP_CMS_JOB_Reminder_Fetch_V1", null))
            {
                QueuedEmail obj = new QueuedEmail();
                while (reader.Read())
                {
                    obj = GetQueMessageFromReader(reader, CompanyID);
                    collection.Add(obj);
                }
            }
            return collection;
        }
        public static T_CMS_Master_Contract GetItemByEmployeeCode(int EmployeeCode)
        {
            T_CMS_Master_Contract item = new T_CMS_Master_Contract();
            var sqlParams = new SqlParameter[1];
            sqlParams[0] = new SqlParameter("@EmployeeCode", EmployeeCode);
            using (var reader = SqlHelper.ExecuteReaderService(ModuleConfig.MyConnection, "T_CMS_Master_Contract_GetByEmployeeCode", sqlParams))
            {
                while (reader.Read())
                {
                    item = GetItemFromReader(reader);
                }
            }
            return item;


        }
        public static T_CMS_Master_Contract AddItem(T_CMS_Master_Contract model)
        {
            int result = 0;
            
                //using (var reader = SqlHelper.ExecuteReaderService(ModuleConfig.MyConnection, CommandType.StoredProcedure, "T_CMS_Master_Contract_Add", CreateSqlParameter(model)))
            using (var reader = SqlHelper.ExecuteReaderService(ModuleConfig.MyConnection, CommandType.StoredProcedure, "DNH_CMS_ContractGeneration_Insert", AddNewSqlParameter(model)))
            {
                while (reader.Read())
                {
                    result = (int)reader[0];
                }
            }
            
            return GetItemByEmployeeCode(result);

        }


        public static T_CMS_Master_Contract UpdateItem(T_CMS_Master_Contract model)
        {
            int result = 0;
            using (var reader = SqlHelper.ExecuteReaderService(ModuleConfig.MyConnection, CommandType.StoredProcedure, "T_CMS_Master_Contract_Update_Mini", CreateSqlParameter(model)))
            {
                while (reader.Read())
                {
                    result = (int)reader[0];
                }
            }
            return GetItemByID(model.ID);

        }
        public static T_CMS_Master_ContractCollection GetAllItem(bool isActive)
        {
            T_CMS_Master_ContractCollection collection = new T_CMS_Master_ContractCollection();
            var sqlParams = new SqlParameter[1];
            sqlParams[0] = new SqlParameter("@isActive", isActive);
            using (var reader = SqlHelper.ExecuteReaderService(ModuleConfig.MyConnection, "T_CMS_Master_Contract_GetAll", sqlParams))
            {
                while (reader.Read())
                {
                    T_CMS_Master_Contract obj = new T_CMS_Master_Contract();
                    obj = GetItemFromReader(reader);
                    collection.Add(obj);
                }
            }
            return collection;
        }

        public static T_CMS_Master_ContractCollection Search(SearchFilter SearchKey)
        {
            T_CMS_Master_ContractCollection collection = new T_CMS_Master_ContractCollection();
            using (var reader = SqlHelper.ExecuteReaderService(ModuleConfig.MyConnection, "T_CMS_Master_Contract_Search", SearchFilterManager.SqlSearchConditionNoCompany(SearchKey)))
            {
                while (reader.Read())
                {
                    T_CMS_Master_Contract obj = new T_CMS_Master_Contract();
                    obj = GetItemFromReader(reader);
                    collection.Add(obj);
                }
            }
            return collection;
        }
        public static T_CMS_Master_ContractCollection GetbyUser(string CreatedUser)
        {
            T_CMS_Master_ContractCollection collection = new T_CMS_Master_ContractCollection();
            T_CMS_Master_Contract obj;
            using (var reader = SqlHelper.ExecuteReaderService(ModuleConfig.MyConnection, "T_CMS_Master_Contract_GetAll_byUser", new SqlParameter("@CreatedUser", CreatedUser)))
            {
                while (reader.Read())
                {
                    obj = GetItemFromReader(reader);
                    collection.Add(obj);
                }
            }
            return collection;
        }
        #region New Report 
        public static DataTable Report_ContractHistory(int EntityID, DateTime? ExeDate,bool IsActiveEmp,int? DeptID)
        {
            DataTable dt = new DataTable();
            //DataTable data = new DataTable();
            var pars = new SqlParameter[] {
                    new SqlParameter("@EntityID", EntityID),
                    new SqlParameter("@ExeDate", ExeDate),
                    new SqlParameter("@IsActiveEmp",IsActiveEmp),
                    new SqlParameter("@DeptID",DeptID),
           
                    //new SqlParameter("@CompanyID",value.CompanyID),
            };
            //@EntityID = 10002, @FromDate = '2017-05-16', @ToDate = '2017-06-14', @OrderBy = 'EmployeeNo', @OrderDirection = 'DESC'
            DataSet ds = SqlHelper.ExecuteDataset(QTConfig.MyConnection, "USP_CMS_ReportContractHistory", 90, pars);
            if (ds.Tables.Count > 0)
            {
                dt = ds.Tables[0];
            }
            return dt;
        }
        public static DataTable Report_AppendixHistory(int EntityID, DateTime? ExeDate, bool IsActiveEmp, int? DeptID)
        {
            DataTable dt = new DataTable();
            //DataTable data = new DataTable();
            var pars = new SqlParameter[] {
                    new SqlParameter("@EntityID", EntityID),
                    new SqlParameter("@ExeDate", ExeDate),
                    new SqlParameter("@IsActiveEmp",IsActiveEmp),
                    new SqlParameter("@DeptID",DeptID),
           
                    //new SqlParameter("@CompanyID",value.CompanyID),
            };
            //@EntityID = 10002, @FromDate = '2017-05-16', @ToDate = '2017-06-14', @OrderBy = 'EmployeeNo', @OrderDirection = 'DESC'
            DataSet ds = SqlHelper.ExecuteDataset(QTConfig.MyConnection, "USP_CMS_ReportContractHistory_Appendix", 90, pars);
            if (ds.Tables.Count > 0)
            {
                dt = ds.Tables[0];
            }
            return dt;
        }

        public static DataTable Report_ContractLevelPending(int EntityID, DateTime? ExeDate, bool IsActiveEmp, int? DeptID)
        {
            DataTable dt = new DataTable();
            
            //DataTable data = new DataTable();
            var pars = new SqlParameter[] {
                    new SqlParameter("@EntityID", EntityID),
                    new SqlParameter("@ExeDate", ExeDate),
                    new SqlParameter("@IsActiveEmp",IsActiveEmp),
                    new SqlParameter("@DeptID",DeptID),
           
                    //new SqlParameter("@CompanyID",value.CompanyID),
            };
            //@EntityID = 10002, @FromDate = '2017-05-16', @ToDate = '2017-06-14', @OrderBy = 'EmployeeNo', @OrderDirection = 'DESC'
            DataSet ds = SqlHelper.ExecuteDataset(QTConfig.MyConnection, "USP_CMS_ContractLevelPending", 90, pars);
            if (ds.Tables.Count > 0)
            {
                dt = ds.Tables[0];
            }
            return dt;
        }
        #endregion
        private static SqlParameter[] CreateSqlParameter(T_CMS_Master_Contract model)
        {
            return new SqlParameter[]
                {
                    new SqlParameter("@ID", model.ID),
                    new SqlParameter("@EmployeeCode", model.EmployeeCode),
                    new SqlParameter("@ContractNo", model.ContractNo),
                    new SqlParameter("@StatusID", model.StatusID),
                    new SqlParameter("@ApproverLevel", model.ApproverLevel),
                    new SqlParameter("@ContractLevel", model.ContractLevel),
                    new SqlParameter("@SalutationID", model.SalutationID),
                    new SqlParameter("@FirstName_EN", model.FirstName_EN),
                    new SqlParameter("@MiddleName_EN", model.MiddleName_EN),
                    new SqlParameter("@LastName_EN", model.LastName_EN),
                    new SqlParameter("@FirstName_VN", model.FirstName_VN),
                    new SqlParameter("@MiddleName_VN", model.MiddleName_VN),
                    new SqlParameter("@LastName_VN", model.LastName_VN),
                    new SqlParameter("@DOB", model.DOB),
                    new SqlParameter("@POB", model.POB),
                    new SqlParameter("@HighestDegree", model.HighestDegree),
                    new SqlParameter("@EmpAddress", model.PerAddress),
                    new SqlParameter("@IDCardNo", model.IDCardNo),
                    new SqlParameter("@IDDOI", model.IDDOI),
                    new SqlParameter("@IDPOI", model.IDPOI),
                    new SqlParameter("@PassportNo", model.PassportNo),
                    new SqlParameter("@PassportDOI", model.PassportDOI),
                    new SqlParameter("@PassportPOI", model.PassportPOI),
                    new SqlParameter("@LabourBookNo", model.LabourBookNo),
                    new SqlParameter("@LabourDOI", model.LabourDOI),
                    new SqlParameter("@LabourPOI", model.LabourPOI),
                    new SqlParameter("@ContractTerm", model.ContractTerm),
                    new SqlParameter("@Joiningdate", model.Joiningdate),
                    new SqlParameter("@Enddate", model.Enddate),
                    new SqlParameter("@PositionID", model.PositionID),
                    new SqlParameter("@LocationID", model.LocationID),
                    new SqlParameter("@GradeID", model.GradeID),
                    new SqlParameter("@DeptID", model.DeptID),
                    new SqlParameter("@WorkHoursID", model.WorkHoursID),
                    new SqlParameter("@Grossoffer", model.Grossoffer),
                    new SqlParameter("@AnnualLeave", model.AnnualLeave),
                    new SqlParameter("@EmpTypeID", model.EmpTypeID),
                    new SqlParameter("@EmpSubTypeID", model.EmpSubTypeID),
                    new SqlParameter("@WorkPermitNo", model.WorkPermitNo),
                    new SqlParameter("@WorkPermitFrom", model.WorkPermitFrom),
                    new SqlParameter("@WorkPermitTo", model.WorkPermitTo),
                    new SqlParameter("@HomeGrossOffer", model.HomeGrossOffer),
                    new SqlParameter("@HomeCountryCurrency", model.HomeCountryCurrency),
                    new SqlParameter("@HomeGrossOfferEffectiveFrom", model.HomeGrossOfferEffectiveFrom),
                    new SqlParameter("@HomeGrossOfferEffectiveTo", model.HomeGrossOfferEffectiveTo),
                    new SqlParameter("@RelocationallowanceCurrency", model.RelocationallowanceCurrency),
                    new SqlParameter("@Relocationallowance", model.Relocationallowance),
                    new SqlParameter("@GoabroadallowanceCurrency", model.GoabroadallowanceCurrency),
                    new SqlParameter("@Goabroadallowance", model.Goabroadallowance),
                    new SqlParameter("@WaivingallowanceCurrency", model.WaivingallowanceCurrency),
                    new SqlParameter("@Waivingallowance", model.Waivingallowance),
                    new SqlParameter("@HostCountryCurrency", model.HostCountryCurrency),
                    new SqlParameter("@HostGrossOfferEffectiveFrom", model.HostGrossOfferEffectiveFrom),
                    new SqlParameter("@HostGrossOfferEffectiveTo", model.HostGrossOfferEffectiveTo),
                    new SqlParameter("@Remarks", model.Remarks),
                    new SqlParameter("@CreatedBy", model.CreatedBy),
                    new SqlParameter("@CreatedDate", model.CreatedDate),
                    new SqlParameter("@ModifiedBy", model.ModifiedBy),
                    new SqlParameter("@ModifiedDate", model.ModifiedDate),
                    new SqlParameter("@IsActive", model.IsActive),
                    new SqlParameter("@ProbationsPeriod", model.ProbationsPeriod),
                    new SqlParameter("@OriginalDate", model.OriginalDate),
                    new SqlParameter("@WorkFlowStatus", model.WorkFlowStatus),
                    new SqlParameter("@HandPhone", model.HandPhone),
                    new SqlParameter("@TempAddress", model.TempAddress),

                };
        }
        private static SqlParameter[] AddNewSqlParameter(T_CMS_Master_Contract model)
        {
            return new SqlParameter[]
                {
                    new SqlParameter("@Mode", model.Mode),
                    new SqlParameter("@Type", "NEWCONTRACT"),//DNH - Add only for the new Contract
                    new SqlParameter("@RowID", model.ID),
                    new SqlParameter("@EmployeeCode", model.EmployeeCode),
                    new SqlParameter("@SalutationID", model.SalutationID),
                    new SqlParameter("@FirstName_EN", model.FirstName_EN),
                    new SqlParameter("@MiddleName_EN", model.MiddleName_EN),
                    new SqlParameter("@LastName_EN", model.LastName_EN),
                    new SqlParameter("@FirstName_VN", model.FirstName_VN),
                    new SqlParameter("@MiddleName_VN", model.MiddleName_VN),
                    new SqlParameter("@LastName_VN", model.LastName_VN),
                    new SqlParameter("@DOB", model.DOB),
                    new SqlParameter("@POB", model.POB),
                    new SqlParameter("@HighestDegree", model.HighestDegree),
                    new SqlParameter("@EmpAddress", model.PerAddress),
                    new SqlParameter("@IDCardNo", model.IDCardNo),
                    new SqlParameter("@IDDOI", model.IDDOI),
                    new SqlParameter("@IDPOI", model.IDPOI),
                    new SqlParameter("@PassportNo", model.PassportNo),
                    new SqlParameter("@PassportDOI", model.PassportDOI),
                    new SqlParameter("@PassportPOI", model.PassportPOI),
                    new SqlParameter("@LabourBookNo", model.LabourBookNo),
                    new SqlParameter("@LabourDOI", model.LabourDOI),
                    new SqlParameter("@LabourPOI", model.LabourPOI),
                    new SqlParameter("@ContractTerm", model.ContractTerm),
                    new SqlParameter("@Joiningdate", model.Joiningdate),
                    new SqlParameter("@Enddate", model.Enddate),
                    new SqlParameter("@LocationID", model.LocationID),
                    new SqlParameter("@PositionID", model.PositionID),
                    new SqlParameter("@GradeID", model.GradeID),
                    new SqlParameter("@DeptID", model.DeptID),
                    new SqlParameter("@WorkHoursID", model.WorkHoursID),
                    new SqlParameter("@Grossoffer", model.Grossoffer),
                    new SqlParameter("@AnnualLeave", model.AnnualLeave),
                    new SqlParameter("@EmpTypeID", model.EmpTypeID),
                    new SqlParameter("@EmpSubTypeID", model.EmpSubTypeID),
                    new SqlParameter("@WorkPermitNo", model.WorkPermitNo),
                    new SqlParameter("@WorkPermitFrom", model.WorkPermitFrom),
                    new SqlParameter("@WorkPermitTo", model.WorkPermitTo),
                    new SqlParameter("@RoleId", 0),//DNH - Default Role = 0 
                    new SqlParameter("@CreatedBy", model.CreatedBy),
                    new SqlParameter("@HostCountryCurrency", model.HostCountryCurrency),
                    new SqlParameter("@HostGrossOfferEffectiveFrom", model.HostGrossOfferEffectiveFrom),
                    new SqlParameter("@HostGrossOfferEffectiveTo", model.HostGrossOfferEffectiveTo),
                    new SqlParameter("@HomeGrossOffer", model.HomeGrossOffer),
                    new SqlParameter("@HomeCountryCurrency", model.HomeCountryCurrency),
                    new SqlParameter("@HomeGrossOfferEffectiveFrom", model.HomeGrossOfferEffectiveFrom),
                    new SqlParameter("@HomeGrossOfferEffectiveTo", model.HomeGrossOfferEffectiveTo),
                    new SqlParameter("@RelocationallowanceCurrency", model.RelocationallowanceCurrency),
                    new SqlParameter("@Relocationallowance", model.Relocationallowance),
                    new SqlParameter("@GoabroadallowanceCurrency", model.GoabroadallowanceCurrency),
                    new SqlParameter("@Goabroadallowance", model.Goabroadallowance),
                    new SqlParameter("@WaivingallowanceCurrency", model.WaivingallowanceCurrency),
                    new SqlParameter("@Waivingallowance", model.Waivingallowance),
                    new SqlParameter("@Remarks", model.Remarks),
                    new SqlParameter("@ProbationsPeriod", model.ProbationsPeriod),
                    new SqlParameter("@HandPhone", model.HandPhone),
                    new SqlParameter("@TempAddress", model.TempAddress),
                    new SqlParameter("@JobDesc", model.JobDesc),



                };
        }
        public static int DeleteItem(int itemID)
        {
            return SqlHelper.ExecuteNonQuery("T_CMS_Master_Contract_Delete", itemID);
        }
    }
}