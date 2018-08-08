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
    public class Provider : BaseDBEntity
    {

        [DataMember]
        public int ProviderID { get; set; }

        [DataMember]
        public int CompanyID { get; set; }

        [DataMember]
        public string Name { get; set; }

        [DataMember]
        public string Description { get; set; }

        [DataMember]
        public string CreatedUser { get; set; }

        [DataMember]
        public DateTime CreatedDate { get; set; }

        [DataMember]
        public int TotalRecord { get; set; }

    }
    public class ProviderCollection : BaseDBEntityCollection<Provider> { }
    public class ProviderManager
    {
        private static Provider GetItemFromReader(IDataReader dataReader)
        {
            Provider objItem = new Provider();

            objItem.ProviderID = SqlHelper.GetInt(dataReader, "ProviderID");

            objItem.CompanyID = SqlHelper.GetInt(dataReader, "CompanyID");

            objItem.Name = SqlHelper.GetString(dataReader, "Name");

            objItem.Description = SqlHelper.GetString(dataReader, "Description");

            objItem.CreatedUser = SqlHelper.GetString(dataReader, "CreatedUser");

            objItem.CreatedDate = SqlHelper.GetDateTime(dataReader, "CreatedDate");



            if (SqlHelper.ColumnExists(dataReader, "TotalRecord"))
            {
                objItem.TotalRecord = SqlHelper.GetInt(dataReader, "TotalRecord");
            }

            return objItem;
        }
        public static Provider GetItemByID(int ProviderID, int CompanyID)
        {
            Provider item = new Provider();
            var sqlParams = new SqlParameter[]
                    {
                            new SqlParameter("@ProviderID", ProviderID),
                            new SqlParameter("@CompanyID", CompanyID),
                    };
            using (var reader = SqlHelper.ExecuteReader("ARProvider_GetByID", sqlParams))
            {
                while (reader.Read())
                {
                    item = GetItemFromReader(reader);
                }
            }
            return item;


        }
        public static Provider AddItem(Provider model)
        {
            int result = 0;
            try
            {
                using (var reader = SqlHelper.ExecuteReader(CommandType.StoredProcedure, "ARProvider_Add", CreateSqlParameter(model)))
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
        public static Provider UpdateItem(Provider model)
        {
            String result = String.Empty;
            using (var reader = SqlHelper.ExecuteReader(CommandType.StoredProcedure, "ARProvider_Update", CreateSqlParameter(model)))
            {
                while (reader.Read())
                {
                    result = (String)reader[0];
                }
            }
            return GetItemByID(model.ProviderID, model.CompanyID);

        }
        public static ProviderCollection GetAllItem(int CompanyID)
        {
            ProviderCollection collection = new ProviderCollection();

            var sqlParams = new SqlParameter[]
                    {
                            new SqlParameter("@CompanyID", CompanyID),
                    };
            using (var reader = SqlHelper.ExecuteReader("ARProvider_GetAll", sqlParams))
            {
                while (reader.Read())
                {
                    Provider obj = new Provider();
                    obj = GetItemFromReader(reader);
                    collection.Add(obj);
                }
            }
            return collection;
        }

        public static ProviderCollection Search(SearchFilter SearchKey)
        {
            ProviderCollection collection = new ProviderCollection();
            using (var reader = SqlHelper.ExecuteReader("ARProvider_Search", SearchFilterManager.SqlSearchParam(SearchKey)))
            {
                while (reader.Read())
                {
                    Provider obj = new Provider();
                    obj = GetItemFromReader(reader);
                    collection.Add(obj);
                }
            }
            return collection;
        }
        public static ProviderCollection GetbyUser(string CreatedUser, int CompanyID)
        {
            ProviderCollection collection = new ProviderCollection();
            Provider obj;
            var sqlParams = new SqlParameter[]
              {
                            new SqlParameter("@CreatedUser", CreatedUser),
                            new SqlParameter("@CompanyID", CompanyID),
              };
            using (var reader = SqlHelper.ExecuteReader("ARProvider_GetAll_byUser", sqlParams))
            {
                while (reader.Read())
                {
                    obj = GetItemFromReader(reader);
                    collection.Add(obj);
                }
            }
            return collection;
        }

        private static SqlParameter[] CreateSqlParameter(Provider model)
        {
            return new SqlParameter[]
                {
                    new SqlParameter("@ProviderID", model.ProviderID),
                    new SqlParameter("@CompanyID", model.CompanyID),
                    new SqlParameter("@Name", model.Name),
                    new SqlParameter("@Description", model.Description),
                    new SqlParameter("@CreatedUser", model.CreatedUser),
                    new SqlParameter("@CreatedDate", model.CreatedDate),

                };
        }

        public static int DeleteItem(int itemID, int CompanyID)
        {
            return SqlHelper.ExecuteNonQuery("ARProvider_Delete", new SqlParameter[]
            {
                new SqlParameter(@"ProviderID",itemID),
                    new SqlParameter("@CompanyID", CompanyID) });
        }
    }
}