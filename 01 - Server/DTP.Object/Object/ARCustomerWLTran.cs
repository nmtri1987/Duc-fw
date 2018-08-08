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
    public class ARCustomerWLTran : BaseDBEntity
    {

        [DataMember]
        public int CompanyID { get; set; }

        [DataMember]
        public Int64 TranID { get; set; }

        [DataMember]
        public string CustomerID { get; set; }

        [DataMember]
        public int ProviderID { get; set; }

        [DataMember]
        public decimal CuryWinlossAmt { get; set; }

        [DataMember]
        public decimal WinlossAmt { get; set; }

        [DataMember]
        public decimal CuryPaymentAmt { get; set; }

        [DataMember]
        public decimal PaymentAmt { get; set; }

        [DataMember]
        public DateTime TranDate { get; set; }

        [DataMember]
        public decimal Rate { get; set; }

        [DataMember]
        public int CuryRateID { get; set; }

        [DataMember]
        public string CreatedUser { get; set; }

        [DataMember]
        public DateTime CreatedDate { get; set; }

        [DataMember]
        public string LastModifiedUser { get; set; }

        [DataMember]
        public DateTime LastModifiedDate { get; set; }

        [DataMember]
        public int TotalRecord { get; set; }

        [DataMember]
        public decimal SettleAmount { get; set; }

        [DataMember]
        public bool IsPayment { get; set; }
    }
    public class ARCustomerWLTranCollection : BaseDBEntityCollection<ARCustomerWLTran> { }
    public class ARCustomerWLTranManager
    {
        private static ARCustomerWLTran GetItemFromReader(IDataReader dataReader)
        {
            ARCustomerWLTran objItem = new ARCustomerWLTran();

            objItem.CompanyID = SqlHelper.GetInt(dataReader, "CompanyID");

            objItem.TranID = SqlHelper.GetInt64(dataReader, "TranID");

            objItem.CustomerID = SqlHelper.GetString(dataReader, "CustomerID");

            objItem.ProviderID = SqlHelper.GetInt(dataReader, "ProviderID");

            objItem.CuryWinlossAmt = SqlHelper.GetDecimal(dataReader, "CuryWinlossAmt");

            objItem.WinlossAmt = SqlHelper.GetDecimal(dataReader, "WinlossAmt");

            objItem.CuryPaymentAmt = SqlHelper.GetDecimal(dataReader, "CuryPaymentAmt");

            objItem.PaymentAmt = SqlHelper.GetDecimal(dataReader, "PaymentAmt");

            objItem.TranDate = SqlHelper.GetDateTime(dataReader, "TranDate");

            objItem.Rate = SqlHelper.GetDecimal(dataReader, "Rate");

            objItem.CreatedUser = SqlHelper.GetString(dataReader, "CreatedUser");

            objItem.CreatedDate = SqlHelper.GetDateTime(dataReader, "CreatedDate");

            objItem.LastModifiedUser = SqlHelper.GetString(dataReader, "LastModifiedUser");

            objItem.LastModifiedDate = SqlHelper.GetDateTime(dataReader, "LastModifiedDate");

            objItem.SettleAmount = SqlHelper.GetDecimal(dataReader, "SettleAmount");

            objItem.IsPayment = SqlHelper.GetBoolean(dataReader, "IsPayment");

            if (SqlHelper.ColumnExists(dataReader, "CuryRateID"))
            {
                objItem.CuryRateID = SqlHelper.GetInt(dataReader, "CuryRateID");
            }

            if (SqlHelper.ColumnExists(dataReader, "TotalRecord"))
            {
                objItem.TotalRecord = SqlHelper.GetInt(dataReader, "TotalRecord");
            }

            return objItem;
        }
        public static ARCustomerWLTran GetItemByID(Int64 TranID, int CompanyID)
        {
            ARCustomerWLTran item = new ARCustomerWLTran();
            var sqlParams = new SqlParameter[]
                    {
                            new SqlParameter("@TranID", TranID),
                            new SqlParameter("@CompanyID", CompanyID),
                    };
            using (var reader = SqlHelper.ExecuteReader("ARCustomerWLTran_GetByID", sqlParams))
            {
                while (reader.Read())
                {
                    item = GetItemFromReader(reader);
                }
            }
            return item;


        }
        public static ARCustomerWLTran AddItem(ARCustomerWLTran model)
        {
            Int64 result = 0;
            try
            {
                using (var reader = SqlHelper.ExecuteReader(CommandType.StoredProcedure, "ARCustomerWLTran_Add", CreateSqlParameter(model)))
                {
                    while (reader.Read())
                    {
                        result = (Int64)reader[0];
                    }
                }
            }
            catch (Exception ObjEx)
            {

            }
            return GetItemByID(result, model.CompanyID);

        }
        public static ARCustomerWLTran UpdateItem(ARCustomerWLTran model)
        {
            String result = String.Empty;
            using (var reader = SqlHelper.ExecuteReader(CommandType.StoredProcedure, "ARCustomerWLTran_Update", CreateSqlParameter(model)))
            {
                while (reader.Read())
                {
                    result = (String)reader[0];
                }
            }
            return GetItemByID(model.TranID, model.CompanyID);

        }
        public static ARCustomerWLTranCollection GetAllItem(int CompanyID)
        {
            ARCustomerWLTranCollection collection = new ARCustomerWLTranCollection();

            var sqlParams = new SqlParameter[]
                    {
                            new SqlParameter("@CompanyID", CompanyID),
                    };
            using (var reader = SqlHelper.ExecuteReader("ARCustomerWLTran_GetAll", sqlParams))
            {
                while (reader.Read())
                {
                    ARCustomerWLTran obj = new ARCustomerWLTran();
                    obj = GetItemFromReader(reader);
                    collection.Add(obj);
                }
            }
            return collection;
        }



        public static ARCustomerWLTranCollection Search(SearchFilter SearchKey)
        {
            ARCustomerWLTranCollection collection = new ARCustomerWLTranCollection();
            using (var reader = SqlHelper.ExecuteReader("ARCustomerWLTran_Search", SearchFilterManager.SqlSearchDynParam(SearchKey)))
            {
                while (reader.Read())
                {
                    ARCustomerWLTran obj = new ARCustomerWLTran();
                    obj = GetItemFromReader(reader);
                    collection.Add(obj);
                }
            }
            return collection;
        }
        public static ARCustomerWLTranCollection GetbyUser(string CreatedUser, int CompanyID)
        {
            ARCustomerWLTranCollection collection = new ARCustomerWLTranCollection();
            ARCustomerWLTran obj;
            var sqlParams = new SqlParameter[]
              {
                            new SqlParameter("@CreatedUser", CreatedUser),
                            new SqlParameter("@CompanyID", CompanyID),
              };
            using (var reader = SqlHelper.ExecuteReader("ARCustomerWLTran_GetAll_byUser", sqlParams))
            {
                while (reader.Read())
                {
                    obj = GetItemFromReader(reader);
                    collection.Add(obj);
                }
            }
            return collection;
        }

        private static SqlParameter[] CreateSqlParameter(ARCustomerWLTran model)
        {
            return new SqlParameter[]
                {
                    new SqlParameter("@CompanyID", model.CompanyID),
                    new SqlParameter("@TranID", model.TranID),
                    new SqlParameter("@CustomerID", model.CustomerID),
                    new SqlParameter("@ProviderID", model.ProviderID),
                    new SqlParameter("@CuryWinlossAmt", model.CuryWinlossAmt),
                    new SqlParameter("@WinlossAmt", model.WinlossAmt),
                    new SqlParameter("@CuryPaymentAmt", model.CuryPaymentAmt),
                    new SqlParameter("@PaymentAmt", model.PaymentAmt),
                    new SqlParameter("@TranDate", model.TranDate),
                    new SqlParameter("@Rate", model.Rate),
                    new SqlParameter("@CuryRateID", model.CuryRateID),
                    new SqlParameter("@SettleAmount", model.SettleAmount),
                    new SqlParameter("@IsPayment", model.IsPayment),
                    new SqlParameter("@CreatedUser", model.CreatedUser),
                    new SqlParameter("@CreatedDate", model.CreatedDate),
                    new SqlParameter("@LastModifiedUser", model.LastModifiedUser),
                    new SqlParameter("@LastModifiedDate", model.LastModifiedDate),

                };
        }

        public static int DeleteItem(Int64 itemID, int CompanyID)
        {
            return SqlHelper.ExecuteNonQuery("ARCustomerWLTran_Delete", new SqlParameter[]
            {
                new SqlParameter("@TranID",itemID),
                    new SqlParameter("@CompanyID", CompanyID) });
        }
        public static DataTable WinLossTranReport(SearchFilter value, string CustomerID, int? ProviderID, DateTime? FromDate, DateTime? ToDate)
        {
            DataTable dt = new DataTable();
            //DataTable data = new DataTable();
            var pars = new SqlParameter[]
            {
                    new SqlParameter("@CustomerID",CustomerID),
                    new SqlParameter("@ProviderID",ProviderID),
                    new SqlParameter("@FromDate",FromDate),
                    new SqlParameter("@ToDate",ToDate),
                    new SqlParameter("@OrderBy", value.OrderBy),
                    new SqlParameter("@OrderDirection", value.OrderDirection),
                    new SqlParameter("@Page", value.Page),
                    new SqlParameter("@PageSize",value.PageSize),
                    new SqlParameter("@CompanyID",value.CompanyID),

            };
            DataSet ds = SqlHelper.ExecuteDataset("ARCustomerWL_Report", pars);
            if (ds.Tables.Count > 0)
            {
               
                dt = ds.Tables[0];
                
                //foreach (DataColumn col in dt.Columns)
                //{
                //    if (col.DataType.Name == "Decimal")
                //    {
                //        foreach (DataRow dr in dt.Rows)
                //        {
                //            if (dr[col.ColumnName] == DBNull.Value)
                //            {
                //                dr[col.ColumnName] = 0;
                //            }
                //        }
                //    }
                   
                //}

            }
            return dt;
        }
        public static DataTable WinLossTranReportByCountry(int CompanyID, DateTime? FromDate, DateTime? ToDate)
        {
            DataTable dt = new DataTable();
            var pars = new SqlParameter[]
            {
                    new SqlParameter("@CompanyID",CompanyID),
                    new SqlParameter("@FromDate",FromDate),
                    new SqlParameter("@ToDate",ToDate),

            };
            DataSet ds = SqlHelper.ExecuteDataset("ARCustomerWL_ReportByCountry", pars);
            if (ds.Tables.Count > 0)
            {
                dt = ds.Tables[0];
            }
            return dt;
        }
    }
}