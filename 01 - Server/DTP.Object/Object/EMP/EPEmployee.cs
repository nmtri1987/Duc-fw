//using System.Text;
//using System.Data;
//using System.Data.SqlClient;
//using System;
//using System.Collections.Generic;
//using System.Runtime.Serialization;
//using DTP.Object;
//namespace ifinds.Object.CS
//{
//    [DataContract]
//    public class EPEmployee : BaseDBEntity
//    {

//        [DataMember]
//        public string EmpCD { get; set; }

//        [DataMember]
//        public int CompanyID { get; set; }

//        [DataMember]
//        public string EmpName { get; set; }

//        [DataMember]
//        public string Firstname { get; set; }

//        [DataMember]
//        public string LastName { get; set; }

//        [DataMember]
//        public string Phone1 { get; set; }

//        [DataMember]
//        public string Phone2 { get; set; }

//        [DataMember]
//        public string EmpRefNo { get; set; }

//        [DataMember]
//        public string Branch { get; set; }

//        [DataMember]
//        public string EmpClass { get; set; }

//        [DataMember]
//        public string DepartmentID { get; set; }

//        [DataMember]
//        public string ReportTo { get; set; }

//        [DataMember]
//        public string CuryId { get; set; }

//        [DataMember]
//        public string CuryRateTypeID { get; set; }

//        [DataMember]
//        public DateTime DateofBirth { get; set; }

//        [DataMember]
//        public int CountryID { get; set; }

//        [DataMember]
//        public string Address1 { get; set; }

//        [DataMember]
//        public string Address2 { get; set; }

//        [DataMember]
//        public string City { get; set; }

//        [DataMember]
//        public string CreatedUser { get; set; }

//        [DataMember]
//        public DateTime CreatedDate { get; set; }

//        [DataMember]
//        public string UpdatedUser { get; set; }

//        [DataMember]
//        public DateTime UpdatedDate { get; set; }

//        [DataMember]
//        public string UserID { get; set; }

//        [DataMember]
//        public string ImgUrl { get; set; }
//        [DataMember]
//        public string Email { get; set; }

        
//    }
//    public class EPEmployeeCollection : BaseDBEntityCollection<EPEmployee> { }
//    public class EPEmployeeManager
//    {
//        private static EPEmployee GetItemFromReader(IDataReader dataReader)
//        {
//            EPEmployee objItem = new EPEmployee();

//            objItem.EmpCD = SqlHelper.GetString(dataReader, "EmpCD");

//            objItem.CompanyID = SqlHelper.GetInt(dataReader, "CompanyID");

//            objItem.EmpName = SqlHelper.GetString(dataReader, "EmpName");

//            objItem.Firstname = SqlHelper.GetString(dataReader, "Firstname");

//            objItem.LastName = SqlHelper.GetString(dataReader, "LastName");

//            objItem.Phone1 = SqlHelper.GetString(dataReader, "Phone1");

//            objItem.Phone2 = SqlHelper.GetString(dataReader, "Phone2");

//            objItem.EmpRefNo = SqlHelper.GetString(dataReader, "EmpRefNo");

//            objItem.Branch = SqlHelper.GetString(dataReader, "Branch");

//            objItem.EmpClass = SqlHelper.GetString(dataReader, "EmpClass");

//            objItem.DepartmentID = SqlHelper.GetString(dataReader, "DepartmentID");

//            objItem.ReportTo = SqlHelper.GetString(dataReader, "ReportTo");

//            objItem.CuryId = SqlHelper.GetString(dataReader, "CuryId");

//            objItem.CuryRateTypeID = SqlHelper.GetString(dataReader, "CuryRateTypeID");

//            objItem.DateofBirth = SqlHelper.GetDateTime(dataReader, "DateofBirth");

//            objItem.CountryID = SqlHelper.GetInt(dataReader, "CountryID");

//            objItem.Address1 = SqlHelper.GetString(dataReader, "Address1");

//            objItem.Address2 = SqlHelper.GetString(dataReader, "Address2");

//            objItem.City = SqlHelper.GetString(dataReader, "City");

//            objItem.CreatedUser = SqlHelper.GetString(dataReader, "CreatedUser");

//            objItem.CreatedDate = SqlHelper.GetDateTime(dataReader, "CreatedDate");

//            objItem.UpdatedUser = SqlHelper.GetString(dataReader, "UpdatedUser");

//            objItem.UpdatedDate = SqlHelper.GetDateTime(dataReader, "UpdatedDate");

//            objItem.UserID = SqlHelper.GetString(dataReader, "UserID");

//            objItem.ImgUrl = SqlHelper.GetString(dataReader, "ImgUrl");
//            objItem.Email = SqlHelper.GetString(dataReader, "Email");

//            return objItem;
//        }
//        public static EPEmployee GetItemByID(string EmpCD, int CompanyID)
//        {
//            EPEmployee item = new EPEmployee();
//            var sqlParams = new SqlParameter[]
//                    {
//                            new SqlParameter("@EmpCD", EmpCD),
//                            new SqlParameter("@CompanyID", CompanyID),
//                    };
//            using (var reader = SqlHelper.ExecuteReader("EPEmployee_GetByID", sqlParams))
//            {
//                while (reader.Read())
//                {
//                    item = GetItemFromReader(reader);
//                }
//            }
//            return item;


//        }
//        public static EPEmployee AddItem(EPEmployee model)
//        {
//            String result = String.Empty;
//            using (var reader = SqlHelper.ExecuteReader(CommandType.StoredProcedure, "EPEmployee_Add", CreateSqlParameter(model)))
//            {
//                while (reader.Read())
//                {
//                    result = (String)reader[0];
//                }
//            }
//            return GetItemByID(result);

//        }
//        public static EPEmployee UpdateItem(EPEmployee model)
//        {
//            String result = String.Empty;
//            using (var reader = SqlHelper.ExecuteReader(CommandType.StoredProcedure, "EPEmployee_Update", CreateSqlParameter(model)))
//            {
//                while (reader.Read())
//                {
//                    result = (String)reader[0];
//                }
//            }
//            return GetItemByID(result);

//        }
//        public static EPEmployeeCollection GetAllItem(int CompanyID)
//        {
//            EPEmployeeCollection collection = new EPEmployeeCollection();
//            var sqlParams = new SqlParameter[]
//              {
//                            new SqlParameter("@CompanyID", CompanyID),
//              };
//            using (var reader = SqlHelper.ExecuteReader("EPEmployee_GetAll", sqlParams))
//            {
//                while (reader.Read())
//                {
//                    EPEmployee obj = new EPEmployee();
//                    obj = GetItemFromReader(reader);
//                    collection.Add(obj);
//                }
//            }
//            return collection;
//        }

//        public static EPEmployeeCollection Search(SearchFilter SearchKey)
//        {
//            EPEmployeeCollection collection = new EPEmployeeCollection();
//            using (var reader = SqlHelper.ExecuteReader("EPEmployee_Search", SearchFilterManager.SqlSearchParam(SearchKey)))
//            {
//                while (reader.Read())
//                {
//                    EPEmployee obj = new EPEmployee();
//                    obj = GetItemFromReader(reader);
//                    collection.Add(obj);
//                }
//            }
//            return collection;
//        }

//        public static EPEmployeeCollection GetbyUser(string CreatedUser, int CompanyID)
//        {
//            EPEmployeeCollection collection = new EPEmployeeCollection();
//            EPEmployee obj;
//            var sqlParams = new SqlParameter[]
//              {
//                            new SqlParameter("@CreatedUser", CreatedUser),
//                            new SqlParameter("@CompanyID", CompanyID),
//              };
//            using (var reader = SqlHelper.ExecuteReader("EPEmployee_GetAll_byUser", sqlParams))
//            {
//                while (reader.Read())
//                {
//                    obj = GetItemFromReader(reader);
//                    collection.Add(obj);
//                }
//            }
//            return collection;
//        }

//        private static SqlParameter[] CreateSqlParameter(EPEmployee model)
//        {
//            return new SqlParameter[]
//                {
//                    new SqlParameter("@EmpCD", model.EmpCD),
//                    new SqlParameter("@CompanyID", model.CompanyID),
//                    new SqlParameter("@EmpName", model.EmpName),
//                    new SqlParameter("@Firstname", model.Firstname),
//                    new SqlParameter("@LastName", model.LastName),
//                    new SqlParameter("@Phone1", model.Phone1),
//                    new SqlParameter("@Phone2", model.Phone2),
//                    new SqlParameter("@EmpRefNo", model.EmpRefNo),
//                    new SqlParameter("@Branch", model.Branch),
//                    new SqlParameter("@EmpClass", model.EmpClass),
//                    new SqlParameter("@DepartmentID", model.DepartmentID),
//                    new SqlParameter("@ReportTo", model.ReportTo),
//                    new SqlParameter("@CuryId", model.CuryId),
//                    new SqlParameter("@CuryRateTypeID", model.CuryRateTypeID),
//                    new SqlParameter("@DateofBirth", model.DateofBirth),
//                    new SqlParameter("@CountryID", model.CountryID),
//                    new SqlParameter("@Address1", model.Address1),
//                    new SqlParameter("@Address2", model.Address2),
//                    new SqlParameter("@City", model.City),
//                    new SqlParameter("@CreatedUser", model.CreatedUser),
//                    new SqlParameter("@CreatedDate", model.CreatedDate),
//                    new SqlParameter("@UpdatedUser", model.UpdatedUser),
//                    new SqlParameter("@UpdatedDate", model.UpdatedDate),
//                    new SqlParameter("@UserID", model.UserID),
//                    new SqlParameter("@ImgUrl", model.ImgUrl),
//                    new SqlParameter("@Email", model.Email),
//        };
//        }

//        public static int DeleteItem(String itemID)
//        {
//            return SqlHelper.ExecuteNonQuery("EPEmployee_Delete", itemID);
//        }
//    }
//}