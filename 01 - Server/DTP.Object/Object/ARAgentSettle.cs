using System.Text;
using System.Data;
using System.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using DTP.Data;
namespace ifinds.Object.AR
{
    [DataContract]
    public class ARAgentSettle : BaseDBEntity
    {

        [DataMember]
        public int CompanyID { get; set; }

        [DataMember]
        public Int64 SettleID { get; set; }

        [DataMember]
        public string CustomerID { get; set; }

        [DataMember]
        public int ProviderID { get; set; }

        [DataMember]
        public string CuryID { get; set; }

        [DataMember]
        public decimal CuryAmount { get; set; }

        [DataMember]
        public decimal Amount { get; set; }

        [DataMember]
        public int CuryRateID { get; set; }

        [DataMember]
        public DateTime SettleDate { get; set; }

        [DataMember]
        public string CreatedUser { get; set; }

        [DataMember]
        public DateTime CreatedDate { get; set; }

        [DataMember]
        public string LastModifiedUser { get; set; }

        [DataMember]
        public DateTime LastModifiedDate { get; set; }

        [DataMember]        public int TotalRecord { get; set; }

    }
    public class ARAgentSettleCollection : BaseDBEntityCollection<ARAgentSettle> { }
    public class ARAgentSettleManager
    {
        private static ARAgentSettle GetItemFromReader(IDataReader dataReader)
        {
            ARAgentSettle objItem = new ARAgentSettle();

            objItem.CompanyID = SqlHelper.GetInt(dataReader, "CompanyID");

            objItem.SettleID = SqlHelper.GetInt(dataReader, "SettleID");

            objItem.CustomerID = SqlHelper.GetString(dataReader, "CustomerID");

            objItem.ProviderID = SqlHelper.GetInt(dataReader, "ProviderID");

            objItem.CuryID = SqlHelper.GetString(dataReader, "CuryID");

            objItem.CuryAmount = SqlHelper.GetDecimal(dataReader, "CuryAmount");

            objItem.Amount = SqlHelper.GetDecimal(dataReader, "Amount");

            objItem.CuryRateID = SqlHelper.GetInt(dataReader, "CuryRateID");

            objItem.SettleDate = SqlHelper.GetDateTime(dataReader, "SettleDate");

            objItem.CreatedUser = SqlHelper.GetString(dataReader, "CreatedUser");

            objItem.CreatedDate = SqlHelper.GetDateTime(dataReader, "CreatedDate");

            objItem.LastModifiedUser = SqlHelper.GetString(dataReader, "LastModifiedUser");

            objItem.LastModifiedDate = SqlHelper.GetDateTime(dataReader, "LastModifiedDate");



            if (SqlHelper.ColumnExists(dataReader, "TotalRecord"))
            {
                objItem.TotalRecord = SqlHelper.GetInt(dataReader, "TotalRecord");
            }

            return objItem;
        }
        public static ARAgentSettle GetItemByID(Int64 SettleID, int CompanyID)
        {
            ARAgentSettle item = new ARAgentSettle();
            var sqlParams = new SqlParameter[]
                    {
                            new SqlParameter("@SettleID", SettleID),
                            new SqlParameter("@CompanyID", CompanyID),
                    };
            using (var reader = SqlHelper.ExecuteReader("ARAgentSettle_GetByID", sqlParams))
            {
                while (reader.Read())
                {
                    item = GetItemFromReader(reader);
                }
            }
            return item;


        }
        public static ARAgentSettle AddItem(ARAgentSettle model)
        {
            int result = 0;
            try
            {
                using (var reader = SqlHelper.ExecuteReader(CommandType.StoredProcedure, "ARAgentSettle_Add", CreateSqlParameter(model)))
                {
                    while (reader.Read())
                    {
                        result = (int)reader[0];
                    }
                }
            }
            catch (Exception ObjEx)
            {

            }
            return GetItemByID(result, model.CompanyID);

        }
        public static ARAgentSettle UpdateItem(ARAgentSettle model)
        {
            String result = String.Empty;
            using (var reader = SqlHelper.ExecuteReader(CommandType.StoredProcedure, "ARAgentSettle_Update", CreateSqlParameter(model)))
            {
                while (reader.Read())
                {
                    result = (String)reader[0];
                }
            }
            return GetItemByID(model.SettleID, model.CompanyID);

        }
        public static ARAgentSettleCollection GetAllItem(int CompanyID)
        {
            ARAgentSettleCollection collection = new ARAgentSettleCollection();

            var sqlParams = new SqlParameter[]
                    {
                            new SqlParameter("@CompanyID", CompanyID),
                    };
            using (var reader = SqlHelper.ExecuteReader("ARAgentSettle_GetAll", sqlParams))
            {
                while (reader.Read())
                {
                    ARAgentSettle obj = new ARAgentSettle();
                    obj = GetItemFromReader(reader);
                    collection.Add(obj);
                }
            }
            return collection;
        }

        public static ARAgentSettleCollection Search(SearchFilter SearchKey)
        {
            ARAgentSettleCollection collection = new ARAgentSettleCollection();
            using (var reader = SqlHelper.ExecuteReader("ARAgentSettle_Search", SearchFilterManager.SqlSearchParam(SearchKey)))
            {
                while (reader.Read())
                {
                    ARAgentSettle obj = new ARAgentSettle();
                    obj = GetItemFromReader(reader);
                    collection.Add(obj);
                }
            }
            return collection;
        }
        public static ARAgentSettleCollection GetbyUser(string CreatedUser, int CompanyID)
        {
            ARAgentSettleCollection collection = new ARAgentSettleCollection();
            ARAgentSettle obj;
            var sqlParams = new SqlParameter[]
              {
                            new SqlParameter("@CreatedUser", CreatedUser),
                            new SqlParameter("@CompanyID", CompanyID),
              };
            using (var reader = SqlHelper.ExecuteReader("ARAgentSettle_GetAll_byUser", sqlParams))
            {
                while (reader.Read())
                {
                    obj = GetItemFromReader(reader);
                    collection.Add(obj);
                }
            }
            return collection;
        }

        private static SqlParameter[] CreateSqlParameter(ARAgentSettle model)
        {
            return new SqlParameter[]
                {
                    new SqlParameter("@CompanyID", model.CompanyID),
                    new SqlParameter("@SettleID", model.SettleID),
                    new SqlParameter("@CustomerID", model.CustomerID),
                    new SqlParameter("@ProviderID", model.ProviderID),
                    new SqlParameter("@CuryID", model.CuryID),
                    new SqlParameter("@CuryAmount", model.CuryAmount),
                    new SqlParameter("@Amount", model.Amount),
                    new SqlParameter("@CuryRateID", model.CuryRateID),
                    new SqlParameter("@SettleDate", model.SettleDate),
                    new SqlParameter("@CreatedUser", model.CreatedUser),
                    new SqlParameter("@CreatedDate", model.CreatedDate),
                    new SqlParameter("@LastModifiedUser", model.LastModifiedUser),
                    new SqlParameter("@LastModifiedDate", model.LastModifiedDate),

                };
        }

        public static int DeleteItem(Int64 itemID, int CompanyID)
        {
            return SqlHelper.ExecuteNonQuery("ARAgentSettle_Delete", new SqlParameter[]
            {
                new SqlParameter(@"SettleID",itemID),
                    new SqlParameter("@CompanyID", CompanyID) });
        }
        public static DataTable AgentSettleSearch(SearchFilter value,string CustomerID, DateTime? FromDate, DateTime? ToDate)
        {
            DataTable dt = new DataTable();
            var pars = new SqlParameter[]
           {
                    new SqlParameter("@CustomerID",CustomerID),
                    new SqlParameter("@FromDate",FromDate),
                    new SqlParameter("@ToDate",ToDate),
                    new SqlParameter("@OrderBy", value.OrderBy),
                    new SqlParameter("@OrderDirection", value.OrderDirection),
                    new SqlParameter("@Page", value.Page),
                    new SqlParameter("@PageSize",value.PageSize),
                    new SqlParameter("@CompanyID",value.CompanyID),

           };
            DataSet ds = SqlHelper.ExecuteDataset("ARAgentSettle_Search", pars);
            if (ds.Tables.Count > 0)
            {
                dt = ds.Tables[0];
            }
            return dt;
        }
        public static DataTable AgentSettleDetail(SearchFilter value, string CustomerID, DateTime? FromDate, DateTime? ToDate)
        {
            DataTable dt = new DataTable();
            var pars = new SqlParameter[]
           {
                    new SqlParameter("@CustomerID",CustomerID),
                    new SqlParameter("@FromDate",FromDate),
                    new SqlParameter("@ToDate",ToDate),
                    new SqlParameter("@OrderBy", value.OrderBy),
                    new SqlParameter("@OrderDirection", value.OrderDirection),
                    new SqlParameter("@Page", value.Page),
                    new SqlParameter("@PageSize",value.PageSize),
                    new SqlParameter("@CompanyID",value.CompanyID),

           };
            DataSet ds = SqlHelper.ExecuteDataset("ARAgentSettle_Search_Detail", pars);
            if (ds.Tables.Count > 0)
            {
                dt = ds.Tables[0];
            }
            return dt;
        }
    }
}