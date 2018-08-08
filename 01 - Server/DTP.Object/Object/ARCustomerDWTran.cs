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
    public class ARCustomerDWTran : BaseDBEntity
    {

        [DataMember]
        public int CompanyID { get; set; }

        [DataMember]
        public Int64 TranID { get; set; }

        [DataMember]
        public string TranType { get; set; }

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
        public decimal CreditAmount { get; set; }

        [DataMember]
        public int CuryRateID { get; set; }

        [DataMember]
        public decimal AgentRate { get; set; }

        [DataMember]
        public DateTime TranDate { get; set; }

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
        public bool Released { get; set; }

        [DataMember]
        public string PaymentMethodID { get; set; }

        [DataMember]
        public string BankAccountID { get; set; }

        [DataMember]
        public int CashAccountID { get; set; }

        [DataMember]
        public string Description { get; set; }

        [DataMember]
        public string ReferenceNumber { get; set; }
        

    }
    public class ARCustomerDWTranCollection : BaseDBEntityCollection<ARCustomerDWTran> { }
    public class ARCustomerDWTranManager
    {
        private static ARCustomerDWTran GetItemFromReader(IDataReader dataReader)
        {
            ARCustomerDWTran objItem = new ARCustomerDWTran();

            objItem.CompanyID = SqlHelper.GetInt(dataReader, "CompanyID");

            objItem.TranID = SqlHelper.GetInt(dataReader, "TranID");

            objItem.TranType = SqlHelper.GetString(dataReader, "TranType");

            objItem.CustomerID = SqlHelper.GetString(dataReader, "CustomerID");

            objItem.ProviderID = SqlHelper.GetInt(dataReader, "ProviderID");

            objItem.CuryID = SqlHelper.GetString(dataReader, "CuryID");

            objItem.CuryAmount = SqlHelper.GetDecimal(dataReader, "CuryAmount");

            objItem.Amount = SqlHelper.GetDecimal(dataReader, "Amount");

            objItem.CreditAmount = SqlHelper.GetDecimal(dataReader, "CreditAmount");

            objItem.CuryRateID = SqlHelper.GetInt(dataReader, "CuryRateID");

            objItem.AgentRate = SqlHelper.GetDecimal(dataReader, "AgentRate");

            objItem.TranDate = SqlHelper.GetDateTime(dataReader, "TranDate");

            objItem.PaymentMethodID = SqlHelper.GetString(dataReader, "PaymentMethodID");

            objItem.BankAccountID = SqlHelper.GetString(dataReader, "BankAccountID");

            objItem.CashAccountID = SqlHelper.GetInt(dataReader, "CashAccountID");

            objItem.CreatedUser = SqlHelper.GetString(dataReader, "CreatedUser");

            objItem.CreatedDate = SqlHelper.GetDateTime(dataReader, "CreatedDate");

            objItem.LastModifiedUser = SqlHelper.GetString(dataReader, "LastModifiedUser");

            objItem.LastModifiedDate = SqlHelper.GetDateTime(dataReader, "LastModifiedDate");

            objItem.Description = SqlHelper.GetString(dataReader, "Description");

            objItem.ReferenceNumber = SqlHelper.GetString(dataReader, "ReferenceNumber");

            if (SqlHelper.ColumnExists(dataReader, "TotalRecord"))
            {
                objItem.TotalRecord = SqlHelper.GetInt(dataReader, "TotalRecord");
            }
            if (SqlHelper.ColumnExists(dataReader, "Released"))
            {
                objItem.Released = SqlHelper.GetBoolean(dataReader, "Released");
            }

            return objItem;
        }
        public static ARCustomerDWTran GetItemByID(Int64 TranID, int CompanyID)
        {
            ARCustomerDWTran item = new ARCustomerDWTran();
            var sqlParams = new SqlParameter[]
                    {
                            new SqlParameter("@TranID", TranID),
                            new SqlParameter("@CompanyID", CompanyID),
                    };
            using (var reader = SqlHelper.ExecuteReader("ARCustomerDWTran_GetByID", sqlParams))
            {
                while (reader.Read())
                {
                    item = GetItemFromReader(reader);
                }
            }
            return item;


        }
        public static ARCustomerDWTran AddItem(ARCustomerDWTran model)
        {
            Int64 result = 0;
            try
            {
                using (var reader = SqlHelper.ExecuteReader(CommandType.StoredProcedure, "ARCustomerDWTran_Add", CreateSqlParameter(model)))
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
        public static ARCustomerDWTran UpdateItem(ARCustomerDWTran model)
        {
            String result = String.Empty;
            using (var reader = SqlHelper.ExecuteReader(CommandType.StoredProcedure, "ARCustomerDWTran_Update", CreateSqlParameter(model)))
            {
                while (reader.Read())
                {
                    result = (String)reader[0];
                }
            }
            return GetItemByID(model.TranID, model.CompanyID);

        }
        public static ARCustomerDWTranCollection GetAllItem(int CompanyID)
        {
            ARCustomerDWTranCollection collection = new ARCustomerDWTranCollection();

            var sqlParams = new SqlParameter[]
                    {
                            new SqlParameter("@CompanyID", CompanyID),
                    };
            using (var reader = SqlHelper.ExecuteReader("ARCustomerDWTran_GetAll", sqlParams))
            {
                while (reader.Read())
                {
                    ARCustomerDWTran obj = new ARCustomerDWTran();
                    obj = GetItemFromReader(reader);
                    collection.Add(obj);
                }
            }
            return collection;
        }

        public static ARCustomerDWTranCollection Search(SearchFilter SearchKey)
        {
            ARCustomerDWTranCollection collection = new ARCustomerDWTranCollection();
            using (var reader = SqlHelper.ExecuteReader("ARCustomerDWTran_Search", SearchFilterManager.SqlSearchDynParam(SearchKey)))
            {
                while (reader.Read())
                {
                    ARCustomerDWTran obj = new ARCustomerDWTran();
                    obj = GetItemFromReader(reader);
                    collection.Add(obj);
                }
            }
            return collection;
        }
        public static ARCustomerDWTranCollection GetbyUser(string CreatedUser, int CompanyID)
        {
            ARCustomerDWTranCollection collection = new ARCustomerDWTranCollection();
            ARCustomerDWTran obj;
            var sqlParams = new SqlParameter[]
              {
                            new SqlParameter("@CreatedUser", CreatedUser),
                            new SqlParameter("@CompanyID", CompanyID),
              };
            using (var reader = SqlHelper.ExecuteReader("ARCustomerDWTran_GetAll_byUser", sqlParams))
            {
                while (reader.Read())
                {
                    obj = GetItemFromReader(reader);
                    collection.Add(obj);
                }
            }
            return collection;
        }

        private static SqlParameter[] CreateSqlParameter(ARCustomerDWTran model)
        {
            return new SqlParameter[]
                {
                    new SqlParameter("@CompanyID", model.CompanyID),
                    new SqlParameter("@TranID", model.TranID),
                    new SqlParameter("@TranType", model.TranType),
                    new SqlParameter("@CustomerID", model.CustomerID),
                    new SqlParameter("@ProviderID", model.ProviderID),
                    new SqlParameter("@CuryID", model.CuryID),
                    new SqlParameter("@CuryAmount", model.CuryAmount),
                    new SqlParameter("@Amount", model.Amount),
                    new SqlParameter("@CreditAmount", model.CreditAmount),
                    new SqlParameter("@CuryRateID", model.CuryRateID),
                    new SqlParameter("@AgentRate", model.AgentRate),
                    new SqlParameter("@TranDate", model.TranDate),
                    new SqlParameter("@Released", model.Released),
                    new SqlParameter("@PaymentMethodID", model.PaymentMethodID),
                    new SqlParameter("@BankAccountID", model.BankAccountID),
                     new SqlParameter("@CashAccountID", model.CashAccountID),
                     new SqlParameter("@Description", model.Description),
                     new SqlParameter("@ReferenceNumber", model.ReferenceNumber),
                    new SqlParameter("@CreatedUser", model.CreatedUser),
                    new SqlParameter("@CreatedDate", model.CreatedDate),
                    new SqlParameter("@LastModifiedUser", model.LastModifiedUser),
                    new SqlParameter("@LastModifiedDate", model.LastModifiedDate),

                };
        }

        public static int DeleteItem(Int64 itemID, int CompanyID)
        {
            return SqlHelper.ExecuteNonQuery("ARCustomerDWTran_Delete", new SqlParameter[]
            {
                new SqlParameter(@"TranID",itemID),
                    new SqlParameter("@CompanyID", CompanyID) });
        }
    }
}