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
    public class ProviderRate : BaseDBEntity
    {

        [DataMember]
        public int ProviderID { get; set; }

        [DataMember]
        public int CompanyID { get; set; }

        [DataMember]
        public int RateID { get; set; }

        [DataMember]
        public int SharingPer { get; set; }

        [DataMember]
        public decimal Rate { get; set; }

        [DataMember]
        public string CreatedUser { get; set; }

        [DataMember]
        public DateTime CreatedDate { get; set; }
        [DataMember]
        public int TotalRecord { get; set; }


    }
    public class ProviderRateCollection : BaseDBEntityCollection<ProviderRate> { }
    public class ProviderRateManager
    {
        private static ProviderRate GetItemFromReader(IDataReader dataReader)
        {
            ProviderRate objItem = new ProviderRate();

            objItem.ProviderID = SqlHelper.GetInt(dataReader, "ProviderID");

            objItem.CompanyID = SqlHelper.GetInt(dataReader, "CompanyID");

            objItem.RateID = SqlHelper.GetInt(dataReader, "RateID");

            objItem.SharingPer = SqlHelper.GetInt(dataReader, "SharingPer");

            objItem.Rate = SqlHelper.GetDecimal(dataReader, "Rate");

            objItem.CreatedUser = SqlHelper.GetString(dataReader, "CreatedUser");

            objItem.CreatedDate = SqlHelper.GetDateTime(dataReader, "CreatedDate");



            if (SqlHelper.ColumnExists(dataReader, "TotalRecord"))
            {
                objItem.TotalRecord = SqlHelper.GetInt(dataReader, "TotalRecord");
            }

            return objItem;
        }
        public static ProviderRate GetItemByID(int RateID, int CompanyID)
        {
            ProviderRate item = new ProviderRate();
            var sqlParams = new SqlParameter[]
                    {
                            new SqlParameter("@RateID", RateID),
                            new SqlParameter("@CompanyID", CompanyID),
                    };
            using (var reader = SqlHelper.ExecuteReader("ARProviderRate_GetByID", sqlParams))
            {
                while (reader.Read())
                {
                    item = GetItemFromReader(reader);
                }
            }
            return item;


        }
        public static ProviderRate AddItem(ProviderRate model)
        {
            int result = 0;
            try
            {
                using (var reader = SqlHelper.ExecuteReader(CommandType.StoredProcedure, "ARProviderRate_Add", CreateSqlParameter(model)))
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
        public static ProviderRate UpdateItem(ProviderRate model)
        {
            String result = String.Empty;
            using (var reader = SqlHelper.ExecuteReader(CommandType.StoredProcedure, "ARProviderRate_Update", CreateSqlParameter(model)))
            {
                while (reader.Read())
                {
                    result = (String)reader[0];
                }
            }
            return GetItemByID(model.RateID, model.CompanyID);

        }
        public static ProviderRateCollection GetAllItem(int CompanyID)
        {
            ProviderRateCollection collection = new ProviderRateCollection();

            var sqlParams = new SqlParameter[]
                    {
                            new SqlParameter("@CompanyID", CompanyID),
                    };
            using (var reader = SqlHelper.ExecuteReader("ARProviderRate_GetAll", sqlParams))
            {
                while (reader.Read())
                {
                    ProviderRate obj = new ProviderRate();
                    obj = GetItemFromReader(reader);
                    collection.Add(obj);
                }
            }
            return collection;
        }

        public static ProviderRateCollection Search(SearchFilter SearchKey)
        {
            ProviderRateCollection collection = new ProviderRateCollection();
            using (var reader = SqlHelper.ExecuteReader("ARProviderRate_Search", SearchFilterManager.SqlSearchParam(SearchKey)))
            {
                while (reader.Read())
                {
                    ProviderRate obj = new ProviderRate();
                    obj = GetItemFromReader(reader);
                    collection.Add(obj);
                }
            }
            return collection;
        }
        public static ProviderRateCollection GetbyUser(string CreatedUser, int CompanyID)
        {
            ProviderRateCollection collection = new ProviderRateCollection();
            ProviderRate obj;
            var sqlParams = new SqlParameter[]
              {
                            new SqlParameter("@CreatedUser", CreatedUser),
                            new SqlParameter("@CompanyID", CompanyID),
              };
            using (var reader = SqlHelper.ExecuteReader("ARProviderRate_GetAll_byUser", sqlParams))
            {
                while (reader.Read())
                {
                    obj = GetItemFromReader(reader);
                    collection.Add(obj);
                }
            }
            return collection;
        }

        private static SqlParameter[] CreateSqlParameter(ProviderRate model)
        {
            return new SqlParameter[]
                {
                    new SqlParameter("@ProviderID", model.ProviderID),
                    new SqlParameter("@CompanyID", model.CompanyID),
                    new SqlParameter("@RateID", model.RateID),
                    new SqlParameter("@SharingPer", model.SharingPer),
                    new SqlParameter("@Rate", model.Rate),
                    new SqlParameter("@CreatedUser", model.CreatedUser),
                    new SqlParameter("@CreatedDate", model.CreatedDate),

                };
        }

        public static int DeleteItem(int itemID, int CompanyID)
        {
            return SqlHelper.ExecuteNonQuery("ARProviderRate_Delete", new SqlParameter[]
            {
                new SqlParameter(@"RateID",itemID),
                    new SqlParameter("@CompanyID", CompanyID) });
        }
    }
}