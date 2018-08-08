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
    public class T_CMS_Master_Internship : BaseDBEntity
    {

        [DataMember]
        public int ID { get; set; }

        [DataMember]
        public int EmployeeCode { get; set; }

        [DataMember]
        public string InternNo { get; set; }

        [DataMember]
        public int StatusID { get; set; }

        [DataMember]
        public int ApproverLevel { get; set; }

        [DataMember]
        public int InternLevel { get; set; }

        [DataMember]
        public string IDCardNo { get; set; }

        [DataMember]
        public string PassportNo { get; set; }

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
        public int SalutationID { get; set; }

        [DataMember]
        public int UniversityID { get; set; }

        [DataMember]
        public int DeptID { get; set; }

        [DataMember]
        public string Dept { get; set; }

        [DataMember]
        public int LocationID { get; set; }

        [DataMember]
        public int WorkHours { get; set; }

        [DataMember]
        public decimal Grossoffer { get; set; }

        [DataMember]
        public int HousingAllowance { get; set; }

        [DataMember]
        public int PeriodofInternship { get; set; }

        [DataMember]
        public DateTime Joiningdate { get; set; }

        [DataMember]
        public DateTime Enddate { get; set; }

        [DataMember]
        public int EmpTypeID { get; set; }

        [DataMember]
        public int EmpSubTypeID { get; set; }

        [DataMember]
        public string Remarks { get; set; }

        [DataMember]
        public bool IsActive { get; set; }

        [DataMember]
        public int CreatedBy { get; set; }

        [DataMember]
        public DateTime CreatedDate { get; set; }

        [DataMember]
        public int ModifiedBy { get; set; }

        [DataMember]
        public DateTime? ModifiedDate { get; set; }


    }
    public class T_CMS_Master_InternshipCollection : BaseDBEntityCollection<T_CMS_Master_Internship> { }
    public class T_CMS_Master_InternshipManager
    {
        private static T_CMS_Master_Internship GetItemFromReader(IDataReader dataReader)
        {
            T_CMS_Master_Internship objItem = new T_CMS_Master_Internship();

            objItem.ID = SqlHelper.GetInt(dataReader, "ID");

            objItem.EmployeeCode = SqlHelper.GetInt(dataReader, "EmployeeCode");

            objItem.InternNo = SqlHelper.GetString(dataReader, "InternNo");

            objItem.StatusID = SqlHelper.GetInt(dataReader, "StatusID");

            objItem.ApproverLevel = SqlHelper.GetInt(dataReader, "ApproverLevel");

            objItem.InternLevel = SqlHelper.GetInt(dataReader, "InternLevel");

            objItem.IDCardNo = SqlHelper.GetString(dataReader, "IDCardNo");

            objItem.PassportNo = SqlHelper.GetString(dataReader, "PassportNo");

            objItem.FirstName_EN = SqlHelper.GetString(dataReader, "FirstName_EN");

            objItem.MiddleName_EN = SqlHelper.GetString(dataReader, "MiddleName_EN");

            objItem.LastName_EN = SqlHelper.GetString(dataReader, "LastName_EN");

            objItem.FirstName_VN = SqlHelper.GetString(dataReader, "FirstName_VN");

            objItem.MiddleName_VN = SqlHelper.GetString(dataReader, "MiddleName_VN");

            objItem.LastName_VN = SqlHelper.GetString(dataReader, "LastName_VN");

            objItem.SalutationID = SqlHelper.GetInt(dataReader, "SalutationID");

            objItem.UniversityID = SqlHelper.GetInt(dataReader, "UniversityID");

            objItem.DeptID = SqlHelper.GetInt(dataReader, "DeptID");

            objItem.Dept = SqlHelper.GetString(dataReader, "Dept");

            objItem.LocationID = SqlHelper.GetInt(dataReader, "LocationID");

            objItem.WorkHours = SqlHelper.GetInt(dataReader, "WorkHours");

            objItem.Grossoffer = SqlHelper.GetDecimal(dataReader, "Grossoffer");

            objItem.HousingAllowance = SqlHelper.GetInt(dataReader, "HousingAllowance");

            objItem.PeriodofInternship = SqlHelper.GetInt(dataReader, "PeriodofInternship");

            objItem.Joiningdate = SqlHelper.GetDateTime(dataReader, "Joiningdate");

            objItem.Enddate = SqlHelper.GetDateTime(dataReader, "Enddate");

            objItem.EmpTypeID = SqlHelper.GetInt(dataReader, "EmpTypeID");

            objItem.EmpSubTypeID = SqlHelper.GetInt(dataReader, "EmpSubTypeID");

            objItem.Remarks = SqlHelper.GetString(dataReader, "Remarks");

            objItem.IsActive = SqlHelper.GetBoolean(dataReader, "IsActive");

            objItem.CreatedBy = SqlHelper.GetInt(dataReader, "CreatedBy");

            objItem.CreatedDate = SqlHelper.GetDateTime(dataReader, "CreatedDate");

            objItem.ModifiedBy = SqlHelper.GetInt(dataReader, "ModifiedBy");

            objItem.ModifiedDate = SqlHelper.GetDateTime(dataReader, "ModifiedDate");



            if (SqlHelper.ColumnExists(dataReader, "TotalRecord"))
            {
                objItem.TotalRecord = SqlHelper.GetInt(dataReader, "TotalRecord");
            }
            return objItem;
        }
        public static T_CMS_Master_Internship GetItemByID(int ID)
        {
            T_CMS_Master_Internship item = new T_CMS_Master_Internship();
            var sqlParams = new SqlParameter[1];
            sqlParams[0] = new SqlParameter("@ID", ID);
            using (var reader = SqlHelper.ExecuteReaderService(ModuleConfig.MyConnection, "T_CMS_Master_Internship_GetByID", sqlParams))
            {
                while (reader.Read())
                {
                    item = GetItemFromReader(reader);
                }
            }
            return item;


        }
        /// <summary>
        /* EXEC dbo.USP_CMS_OfferGeneration_Insert @Mode = 'INTERN_INSERT'
	,@EmployeeCode = 11536
	,@IDCardNo = ''
	,@FirstName_EN = 'aa'
	,@MiddleName_EN = 'aa'
	,@LastName_EN = 'aa'
	,@FirstName_VN = 'aa'
	,@MiddleName_VN = 'Thị Bảo'
	,@LastName_VN = 'Ngọc'
	,@SalutationID = 1
	--,@PositionID = 12
	--,@GradeID = null
	,@DeptID = 1031
	,@LocationID = 6
	,@WorkHours = 1
	,@Grossoffer = 15000
	--,@AnnualLeave = 15
	,@PeriodofProbation = 60
	,@Joiningdate = '11-11-2013'
	,@Enddate = '01-10-2014'
	,@EmpTypeID = 3
	,@EmpSubTypeID = 105
	,@PassportNo=null
	,@UniversityID=1
	,@HousingAllowance=null
	,@PeriodofInternship=1
	,@CreatedBy=103	*/
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static T_CMS_Master_Internship InsertNew(T_CMS_Master_Internship model)
        {
            int result = 0;
            using (var reader = SqlHelper.ExecuteReaderService(ModuleConfig.MyConnection, CommandType.StoredProcedure, "USP_CMS_OfferGeneration_Insert", CreateNewSqlParameter(model)))
            {
                while (reader.Read())
                {
                    result = (int)reader[0];
                }
            }
            return GetItemByID(result);
        }
            
        public static T_CMS_Master_Internship AddItem(T_CMS_Master_Internship model)
        {
            int result = 0;
            using (var reader = SqlHelper.ExecuteReaderService(ModuleConfig.MyConnection, CommandType.StoredProcedure, "T_CMS_Master_Internship_Add", CreateSqlParameter(model)))
            {
                while (reader.Read())
                {
                    result = (int)reader[0];
                }
            }
            return GetItemByID(result);

        }
        public static T_CMS_Master_Internship UpdateItem(T_CMS_Master_Internship model)
        {
            int result = 0;
            using (var reader = SqlHelper.ExecuteReaderService(ModuleConfig.MyConnection, CommandType.StoredProcedure, "T_CMS_Master_Internship_Update", CreateSqlParameter(model)))
            {
                while (reader.Read())
                {
                    result = (int)reader[0];
                }
            }
            return GetItemByID(result);

        }
        public static T_CMS_Master_InternshipCollection GetAllItem()
        {
            T_CMS_Master_InternshipCollection collection = new T_CMS_Master_InternshipCollection();
            using (var reader = SqlHelper.ExecuteReaderService(ModuleConfig.MyConnection, "T_CMS_Master_Internship_GetAll", null))
            {
                while (reader.Read())
                {
                    T_CMS_Master_Internship obj = new T_CMS_Master_Internship();
                    obj = GetItemFromReader(reader);
                    collection.Add(obj);
                }
            }
            return collection;
        }

        public static T_CMS_Master_InternshipCollection Search(SearchFilter SearchKey)
        {
            T_CMS_Master_InternshipCollection collection = new T_CMS_Master_InternshipCollection();
            using (var reader = SqlHelper.ExecuteReaderService(ModuleConfig.MyConnection, "T_CMS_Master_Internship_Search", SearchFilterManager.SqlSearchConditionNoCompany(SearchKey)))
            {
                while (reader.Read())
                {
                    T_CMS_Master_Internship obj = new T_CMS_Master_Internship();
                    obj = GetItemFromReader(reader);
                    collection.Add(obj);
                }
            }
            return collection;
        }
        public static T_CMS_Master_InternshipCollection GetbyUser(string CreatedUser)
        {
            T_CMS_Master_InternshipCollection collection = new T_CMS_Master_InternshipCollection();
            T_CMS_Master_Internship obj;
            using (var reader = SqlHelper.ExecuteReaderService(ModuleConfig.MyConnection, "T_CMS_Master_Internship_GetAll_byUser", new SqlParameter("@CreatedUser", CreatedUser)))
            {
                while (reader.Read())
                {
                    obj = GetItemFromReader(reader);
                    collection.Add(obj);
                }
            }
            return collection;
        }

        private static SqlParameter[] CreateSqlParameter(T_CMS_Master_Internship model)
        {
            return new SqlParameter[]
                {
                    new SqlParameter("@ID", model.ID),
                    new SqlParameter("@EmployeeCode", model.EmployeeCode),
                    new SqlParameter("@InternNo", model.InternNo),
                    new SqlParameter("@StatusID", model.StatusID),
                    new SqlParameter("@ApproverLevel", model.ApproverLevel),
                    new SqlParameter("@InternLevel", model.InternLevel),
                    new SqlParameter("@IDCardNo", model.IDCardNo),
                    new SqlParameter("@PassportNo", model.PassportNo),
                    new SqlParameter("@FirstName_EN", model.FirstName_EN),
                    new SqlParameter("@MiddleName_EN", model.MiddleName_EN),
                    new SqlParameter("@LastName_EN", model.LastName_EN),
                    new SqlParameter("@FirstName_VN", model.FirstName_VN),
                    new SqlParameter("@MiddleName_VN", model.MiddleName_VN),
                    new SqlParameter("@LastName_VN", model.LastName_VN),
                    new SqlParameter("@SalutationID", model.SalutationID),
                    new SqlParameter("@UniversityID", model.UniversityID),
                    new SqlParameter("@DeptID", model.DeptID),
                    new SqlParameter("@Dept", model.Dept),
                    new SqlParameter("@LocationID", model.LocationID),
                    new SqlParameter("@WorkHours", model.WorkHours),
                    new SqlParameter("@Grossoffer", model.Grossoffer),
                    new SqlParameter("@HousingAllowance", model.HousingAllowance),
                    new SqlParameter("@PeriodofInternship", model.PeriodofInternship),
                    new SqlParameter("@Joiningdate", model.Joiningdate),
                    new SqlParameter("@Enddate", model.Enddate),
                    new SqlParameter("@EmpTypeID", model.EmpTypeID),
                    new SqlParameter("@EmpSubTypeID", model.EmpSubTypeID),
                    new SqlParameter("@Remarks", model.Remarks),
                    new SqlParameter("@IsActive", model.IsActive),
                    new SqlParameter("@CreatedBy", model.CreatedBy),
                    new SqlParameter("@CreatedDate", model.CreatedDate),
                    new SqlParameter("@ModifiedBy", model.ModifiedBy),
                    new SqlParameter("@ModifiedDate", model.ModifiedDate),

                };
        }
        /// <summary>

        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        private static SqlParameter[] CreateNewSqlParameter(T_CMS_Master_Internship model)
        {
            return new SqlParameter[]
                {
                    new SqlParameter("@Mode", "INTERN_INSERT"),
                    //new SqlParameter("@ID", model.ID),
                    new SqlParameter("@EmployeeCode", model.EmployeeCode),
                    new SqlParameter("@IDCardNo", model.IDCardNo),
                    new SqlParameter("@FirstName_EN", model.FirstName_EN),
                    new SqlParameter("@MiddleName_EN", model.MiddleName_EN),
                    new SqlParameter("@LastName_EN", model.LastName_EN),
                    new SqlParameter("@FirstName_VN", model.FirstName_VN),
                    new SqlParameter("@MiddleName_VN", model.MiddleName_VN),
                    new SqlParameter("@LastName_VN", model.LastName_VN),
                    new SqlParameter("@SalutationID", model.SalutationID),
                    new SqlParameter("@DeptID", model.DeptID),
                    new SqlParameter("@LocationID", model.LocationID),
                    new SqlParameter("@WorkHours", model.WorkHours),
                    new SqlParameter("@Grossoffer", model.Grossoffer),
                    new SqlParameter("@PeriodofProbation", 60),
                    new SqlParameter("@Joiningdate", model.Joiningdate),
                    new SqlParameter("@Enddate", model.Enddate),
                    new SqlParameter("@EmpTypeID", model.EmpTypeID),
                    new SqlParameter("@EmpSubTypeID", model.EmpSubTypeID),
                    new SqlParameter("@PassportNo", model.PassportNo),
                    new SqlParameter("@UniversityID", model.UniversityID),
                    new SqlParameter("@HousingAllowance", model.HousingAllowance),
                    new SqlParameter("@PeriodofInternship", model.PeriodofInternship),
                    new SqlParameter("@CreatedBy", model.CreatedBy),
                    new SqlParameter("@Remarks", model.Remarks),
                    //not need to insert

                    //new SqlParameter("@InternNo", model.InternNo),
                    //new SqlParameter("@StatusID", model.StatusID),
                    //new SqlParameter("@ApproverLevel", model.ApproverLevel),
                    //new SqlParameter("@InternLevel", model.InternLevel),
                    
                    
                    
                    
                    
                    
                    //new SqlParameter("@Dept", model.Dept),
                    
                    
                    
                    
                    
                    
                    
                    //new SqlParameter("@Remarks", model.Remarks),
                    //new SqlParameter("@IsActive", model.IsActive),
                    
                    //new SqlParameter("@CreatedDate", model.CreatedDate),
                    //new SqlParameter("@ModifiedBy", model.ModifiedBy),
                    //new SqlParameter("@ModifiedDate", model.ModifiedDate),

                };
        }

        public static int DeleteItem(int itemID)
        {
            return SqlHelper.ExecuteNonQuery("T_CMS_Master_Internship_Delete", itemID);
        }
    }
}