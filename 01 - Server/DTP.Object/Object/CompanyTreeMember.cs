using System.Text;
using System.Data;
using System.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
namespace DTP.Object
{
    [DataContract]
    public class CompanyTreeMember : BaseDBEntity
    {

        [DataMember]
        public int CompanyID { get; set; }

        [DataMember]
        public int WorkGroupID { get; set; }

        [DataMember]
        public string EmployeeID { get; set; }

        [DataMember]
        public int WaitTime { get; set; }

        [DataMember]
        public bool IsOwner { get; set; }

        [DataMember]
        public bool Active { get; set; }

        [DataMember]
        public string CreatedUser { get; set; }

        [DataMember]
        public DateTime CreatedDate { get; set; }

        [DataMember]
        public string ModifiedUser { get; set; }

        [DataMember]
        public DateTime ModifiedDate { get; set; }

        [DataMember]
        public string UserName { get; set; }

        //[DataMember]
        //public string EmployeeID { get; set; }

        [DataMember]
        public string EmployeeName { get; set; }
        [DataMember]
        public string DepartmentID { get; set; }
        [DataMember]
        public string DepartmentName { get; set; }
        [DataMember]
        public string Position { get; set; }


    }
    public class CompanyTreeMemberCollection : BaseDBEntityCollection<CompanyTreeMember> { }
    public class CompanyTreeMemberManager
    {
        private static CompanyTreeMember GetItemFromReader(IDataReader dataReader)
        {
            CompanyTreeMember objItem = new CompanyTreeMember();

            objItem.CompanyID = SqlHelper.GetInt(dataReader, "CompanyID");

            objItem.WorkGroupID = SqlHelper.GetInt(dataReader, "WorkGroupID");

            objItem.EmployeeID = SqlHelper.GetString(dataReader, "EmployeeID");

            objItem.WaitTime = SqlHelper.GetInt(dataReader, "WaitTime");

            objItem.IsOwner = SqlHelper.GetBoolean(dataReader, "IsOwner");

            objItem.Active = SqlHelper.GetBoolean(dataReader, "Active");

            objItem.CreatedUser = SqlHelper.GetString(dataReader, "CreatedUser");

            objItem.CreatedDate = SqlHelper.GetDateTime(dataReader, "CreatedDate");

            objItem.ModifiedUser = SqlHelper.GetString(dataReader, "ModifiedUser");

            objItem.ModifiedDate = SqlHelper.GetDateTime(dataReader, "ModifiedDate");

            try { objItem.UserName = SqlHelper.GetString(dataReader, "UserName"); } catch { }
            //try { objItem.EmployeeID = SqlHelper.GetString(dataReader, "EmpCD"); } catch { }
            try { objItem.EmployeeName = SqlHelper.GetString(dataReader, "EmpName"); } catch { }
            try { objItem.DepartmentID = SqlHelper.GetString(dataReader, "DepartmentID"); } catch { }
            try { objItem.DepartmentName = SqlHelper.GetString(dataReader, "Description"); } catch { }
            try { objItem.Position = SqlHelper.GetString(dataReader, "Position"); } catch { }
            return objItem;
        }
        public static CompanyTreeMember GetItemByID(Int32 WorkGroupID, int CompanyID, string EmployeeID)
        {
            CompanyTreeMember item = new CompanyTreeMember();
            var sqlParams = new SqlParameter[3];
            sqlParams[0] = new SqlParameter("@WorkGroupID", WorkGroupID);
            sqlParams[1] = new SqlParameter("@CompanyID", CompanyID);
            sqlParams[2] = new SqlParameter("@EmployeeID", EmployeeID);
            using (var reader = SqlHelper.ExecuteReader("CompanyTreeMember_GetByID", sqlParams))
            {
                while (reader.Read())
                {
                    item = GetItemFromReader(reader);
                }
            }
            return item;


        }
        public static CompanyTreeMember AddItem(CompanyTreeMember model)
        {
            int result = 0;
            using (var reader = SqlHelper.ExecuteReader(CommandType.StoredProcedure, "CompanyTreeMember_Add", CreateSqlParameter(model)))
            {
                while (reader.Read())
                {
                    result = (int)reader[0];
                }
            }
            return GetItemByID(result, model.CompanyID, model.EmployeeID);

        }
        public static CompanyTreeMember UpdateItem(CompanyTreeMember model)
        {
            String result = String.Empty;
            using (var reader = SqlHelper.ExecuteReader(CommandType.StoredProcedure, "CompanyTreeMember_Update", CreateSqlParameter(model)))
            {
                while (reader.Read())
                {
                    result = (String)reader[0];
                }
            }
            return GetItemByID(model.WorkGroupID, model.CompanyID, model.EmployeeID);

        }
        public static CompanyTreeMemberCollection GetAllItem(int CompanyID, int WorkGroupID)
        {
            CompanyTreeMemberCollection collection = new CompanyTreeMemberCollection();

            var sqlParams = new SqlParameter[2];
            sqlParams[0] = new SqlParameter("@CompanyID", CompanyID);
            sqlParams[1] = new SqlParameter("@WorkGroupID", WorkGroupID);
            using (var reader = SqlHelper.ExecuteReader("CompanyTreeMember_GetAll", sqlParams))
            {
                while (reader.Read())
                {
                    CompanyTreeMember obj = new CompanyTreeMember();
                    obj = GetItemFromReader(reader);
                    collection.Add(obj);
                }
            }
            return collection;
        }

        public static CompanyTreeMemberCollection Search(SearchFilter SearchKey)
        {
            CompanyTreeMemberCollection collection = new CompanyTreeMemberCollection();
            using (var reader = SqlHelper.ExecuteReader("CompanyTreeMember_Search", SearchFilterManager.SqlSearchParam(SearchKey)))
            {
                while (reader.Read())
                {
                    CompanyTreeMember obj = new CompanyTreeMember();
                    obj = GetItemFromReader(reader);
                    collection.Add(obj);
                }
            }
            return collection;
        }
        public static CompanyTreeMemberCollection GetbyUser(string CreatedUser, int CompanyID, int WorkGroupID)
        {
            CompanyTreeMemberCollection collection = new CompanyTreeMemberCollection();
            CompanyTreeMember obj;
            using (var reader = SqlHelper.ExecuteReader("CompanyTreeMember_GetAll_byUser", new SqlParameter[] {
                new SqlParameter("@CreatedUser", CreatedUser) ,
                new SqlParameter("@CompanyID", CompanyID) ,
                new SqlParameter("@WorkGroupID", WorkGroupID) ,
            }))
            {
                while (reader.Read())
                {
                    obj = GetItemFromReader(reader);
                    collection.Add(obj);
                }
            }
            return collection;
        }

        private static SqlParameter[] CreateSqlParameter(CompanyTreeMember model)
        {
            return new SqlParameter[]
                {
                    new SqlParameter("@CompanyID", model.CompanyID),
                    new SqlParameter("@WorkGroupID", model.WorkGroupID),
                    new SqlParameter("@EmployeeID", model.EmployeeID),
                    new SqlParameter("@WaitTime", model.WaitTime),
                    new SqlParameter("@IsOwner", model.IsOwner),
                    new SqlParameter("@Active", model.Active),
                    new SqlParameter("@CreatedUser", model.CreatedUser),
                    new SqlParameter("@CreatedDate", model.CreatedDate),
                    new SqlParameter("@ModifiedUser", model.ModifiedUser),
                    new SqlParameter("@ModifiedDate", model.ModifiedDate),

                };
        }

        public static int DeleteItem(Int32 itemID, int CompanyID, string EmployeeID)
        {
            return SqlHelper.ExecuteNonQuery("CompanyTreeMember_Delete", new SqlParameter[]
            {
                new SqlParameter(@"WorkGroupID",itemID),
                    new SqlParameter("@CompanyID", CompanyID),
                    new SqlParameter("@EmployeeID", EmployeeID)
            }
            );
        }
    }
}