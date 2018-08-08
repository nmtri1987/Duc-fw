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
    public class ARAgentRate : BaseDBEntity
    {

        [DataMember]
        public int CompanyID { get; set; }

        [DataMember]
        public int ProviderID { get; set; }

        [DataMember]
        public int RateID { get; set; }

        [DataMember]
        public string CustomerID { get; set; }

        [DataMember]
        public int TotalRecord { get; set; }
        [DataMember]
        public string Name { get; set; }
        [DataMember]
        public int Sharing { get; set; }
        [DataMember]
        public decimal Rate { get; set; }

        [DataMember]
        public decimal BalanceAmt { get; set; }
        [DataMember]
        public decimal CuryBalanceAmt { get; set; }

        [DataMember]
        public decimal WinLossSumAmt { get; set; }

    }
    public class ARAgentRateCollection : BaseDBEntityCollection<ARAgentRate> { }
    public class ARAgentRateManager
    {
        private static ARAgentRate GetItemFromReader(IDataReader dataReader)
        {
            ARAgentRate objItem = new ARAgentRate();

            objItem.CompanyID = SqlHelper.GetInt(dataReader, "CompanyID");

            objItem.ProviderID = SqlHelper.GetInt(dataReader, "ProviderID");

            objItem.RateID = SqlHelper.GetInt(dataReader, "RateID");

            objItem.CustomerID = SqlHelper.GetString(dataReader, "CustomerID");



            if (SqlHelper.ColumnExists(dataReader, "TotalRecord"))
            {
                objItem.TotalRecord = SqlHelper.GetInt(dataReader, "TotalRecord");
            }

            if (SqlHelper.ColumnExists(dataReader, "SharingPer"))
            {
                objItem.Sharing = SqlHelper.GetInt(dataReader, "SharingPer");
            }
            if (SqlHelper.ColumnExists(dataReader, "Name"))
            {
                objItem.Name = SqlHelper.GetString(dataReader, "Name");
            }
            if (SqlHelper.ColumnExists(dataReader, "Rate"))
            {
                objItem.Rate = SqlHelper.GetDecimal(dataReader, "Rate");
            }
            if (SqlHelper.ColumnExists(dataReader, "BalanceAmt"))
            {
                objItem.BalanceAmt = SqlHelper.GetDecimal(dataReader, "BalanceAmt");
            }
            if (SqlHelper.ColumnExists(dataReader, "CuryBalanceAmt"))
            {
                objItem.CuryBalanceAmt = SqlHelper.GetDecimal(dataReader, "CuryBalanceAmt");
            }
            if (SqlHelper.ColumnExists(dataReader, "WinLossSumAmt"))
            {
                objItem.WinLossSumAmt = SqlHelper.GetDecimal(dataReader, "WinLossSumAmt");
            }
            return objItem;
        }
        public static ARAgentRate GetItemByID(string CustomerID, int CompanyID, int ProviderID)
        {
            ARAgentRate item = new ARAgentRate();
            var sqlParams = new SqlParameter[]
                    {
                            new SqlParameter("@CustomerID", CustomerID),
                            new SqlParameter("@CompanyID", CompanyID),
                            new SqlParameter("@ProviderID", ProviderID),
                    };
            using (var reader = SqlHelper.ExecuteReader("ARAgentRate_GetByID", sqlParams))
            {
                while (reader.Read())
                {
                    item = GetItemFromReader(reader);
                }
            }
            return item;


        }
        public static ARAgentRate AddItem(ARAgentRate model)
        {
            String result = String.Empty;
            try
            {
                using (var reader = SqlHelper.ExecuteReader(CommandType.StoredProcedure, "ARAgentRate_Add", CreateSqlParameter(model)))
                {
                    while (reader.Read())
                    {
                        result = (String)reader[0];
                    }
                }
            }
            catch (Exception ObjEx)
            {

            }
            return GetItemByID(result, model.CompanyID, model.ProviderID);

        }
        public static ARAgentRate UpdateItem(ARAgentRate model)
        {
            String result = String.Empty;
            using (var reader = SqlHelper.ExecuteReader(CommandType.StoredProcedure, "ARAgentRate_Update", CreateSqlParameter(model)))
            {
                while (reader.Read())
                {
                    result = (String)reader[0];
                }
            }
            return GetItemByID(model.CustomerID, model.CompanyID, model.ProviderID);

        }
        public static ARAgentRateCollection GetAllItem(int CompanyID, string CustomerID)
        {
            ARAgentRateCollection collection = new ARAgentRateCollection();

            var sqlParams = new SqlParameter[]
                    {
                            new SqlParameter("@CompanyID", CompanyID),
                            new SqlParameter("@CustomerID", CustomerID),
                    };
            using (var reader = SqlHelper.ExecuteReader("ARAgentRate_GetAll", sqlParams))
            {
                while (reader.Read())
                {
                    ARAgentRate obj = new ARAgentRate();
                    obj = GetItemFromReader(reader);
                    collection.Add(obj);
                }
            }
            return collection;
        }

        public static ARAgentRateCollection Search(SearchFilter SearchKey)
        {
            ARAgentRateCollection collection = new ARAgentRateCollection();
            using (var reader = SqlHelper.ExecuteReader("ARAgentRate_Search", SearchFilterManager.SqlSearchParam(SearchKey)))
            {
                while (reader.Read())
                {
                    ARAgentRate obj = new ARAgentRate();
                    obj = GetItemFromReader(reader);
                    collection.Add(obj);
                }
            }
            return collection;
        }


        private static SqlParameter[] CreateSqlParameter(ARAgentRate model)
        {
            return new SqlParameter[]
                {
                    new SqlParameter("@CompanyID", model.CompanyID),
                    new SqlParameter("@ProviderID", model.ProviderID),
                    new SqlParameter("@RateID", model.RateID),
                    new SqlParameter("@CustomerID", model.CustomerID),
                     new SqlParameter("@BalanceAmt", model.BalanceAmt),
                     new SqlParameter("@CuryBalanceAmt", model.CuryBalanceAmt),
                      new SqlParameter("@WinLossSumAmt", model.WinLossSumAmt),
                };
        }

        public static int DeleteItem(string itemID, int CompanyID, int RateID)
        {
            return SqlHelper.ExecuteNonQuery("ARAgentRate_Delete", new SqlParameter[]
            {
                new SqlParameter(@"CustomerID",itemID),
                    new SqlParameter("@CompanyID", CompanyID) ,
            new SqlParameter("@RateID", RateID)
            });
        }
    }
}