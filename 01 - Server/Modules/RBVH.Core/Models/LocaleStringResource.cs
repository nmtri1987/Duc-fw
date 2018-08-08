using System.Text;
using System.Data;
using System.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using DTP.Data;
namespace RBVH.Core
{
    [DataContract]
    public class LocaleStringResource : BaseDBEntity
    {

        [DataMember]
        public int LocaleStringResourceID { get; set; }

        [DataMember]
        public int LanguageID { get; set; }

        [DataMember]
        public string ResourceName { get; set; }

        [DataMember]
        public string ResourceValue { get; set; }

        [DataMember]
        public int CompanyID { get; set; }

        [DataMember]
        public int CreatedUser { get; set; }

        [DataMember]
        public DateTime CreatedDate { get; set; }


    }
    public class LocaleStringResourceCollection : BaseDBEntityCollection<LocaleStringResource> { }
    public class LocaleStringResourceManager
    {
        private static LocaleStringResource GetItemFromReader(IDataReader dataReader)
        {
            LocaleStringResource objItem = new LocaleStringResource();

            objItem.LocaleStringResourceID = SqlHelper.GetInt(dataReader, "LocaleStringResourceID");

            objItem.LanguageID = SqlHelper.GetInt(dataReader, "LanguageID");

            objItem.ResourceName = SqlHelper.GetString(dataReader, "ResourceName");

            objItem.ResourceValue = SqlHelper.GetString(dataReader, "ResourceValue");

            objItem.CompanyID = SqlHelper.GetInt(dataReader, "CompanyID");

            objItem.CreatedUser = SqlHelper.GetInt(dataReader, "CreatedUser");

            objItem.CreatedDate = SqlHelper.GetDateTime(dataReader, "CreatedDate");



            if (SqlHelper.ColumnExists(dataReader, "TotalRecord"))
            {
                objItem.TotalRecord = SqlHelper.GetInt(dataReader, "TotalRecord");
            }

            return objItem;
        }
        public static LocaleStringResource GetItemByID(int LocaleStringResourceID, int CompanyID)
        {
            LocaleStringResource item = new LocaleStringResource();
            var sqlParams = new SqlParameter[]
                    {
                            new SqlParameter("@LocaleStringResourceID", LocaleStringResourceID),
                            new SqlParameter("@CompanyID", CompanyID),
                    };
            using (var reader = SqlHelper.ExecuteReader("LocaleStringResource_GetByID", sqlParams))
            {
                while (reader.Read())
                {
                    item = GetItemFromReader(reader);
                }
            }
            return item;


        }
        public static LocaleStringResource AddItem(LocaleStringResource model)
        {
            int result = 0;
            try
            {
                using (var reader = SqlHelper.ExecuteReader(CommandType.StoredProcedure, "LocaleStringResource_Add", CreateSqlParameter(model)))
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
        public static LocaleStringResource UpdateItem(LocaleStringResource model)
        {
            String result = String.Empty;
            using (var reader = SqlHelper.ExecuteReader(CommandType.StoredProcedure, "LocaleStringResource_Update", CreateSqlParameter(model)))
            {
                while (reader.Read())
                {
                    result = (String)reader[0];
                }
            }
            return GetItemByID(model.LocaleStringResourceID, model.CompanyID);

        }
        public static LocaleStringResourceCollection GetAllItem(int CompanyID)
        {
            LocaleStringResourceCollection collection = new LocaleStringResourceCollection();

            var sqlParams = new SqlParameter[]
                    {
                            new SqlParameter("@CompanyID", CompanyID),
                    };
            using (var reader = SqlHelper.ExecuteReader("LocaleStringResource_GetAll", sqlParams))
            {
                while (reader.Read())
                {
                    LocaleStringResource obj = new LocaleStringResource();
                    obj = GetItemFromReader(reader);
                    collection.Add(obj);
                }
            }
            return collection;
        }

        public static LocaleStringResourceCollection Search(SearchFilter SearchKey)
        {
            LocaleStringResourceCollection collection = new LocaleStringResourceCollection();
            using (var reader = SqlHelper.ExecuteReader("DNHLocaleStringResource_Search", SearchFilterManager.SqlSearchParam(SearchKey)))
            {
                while (reader.Read())
                {
                    LocaleStringResource obj = new LocaleStringResource();
                    obj = GetItemFromReader(reader);
                    collection.Add(obj);
                }
            }
            return collection;
        }
        public static LocaleStringResourceCollection GetbyUser(string CreatedUser, int CompanyID)
        {
            LocaleStringResourceCollection collection = new LocaleStringResourceCollection();
            LocaleStringResource obj;
            var sqlParams = new SqlParameter[]
              {
                            new SqlParameter("@CreatedUser", CreatedUser),
                            new SqlParameter("@CompanyID", CompanyID),
              };
            using (var reader = SqlHelper.ExecuteReader("LocaleStringResource_GetAll_byUser", sqlParams))
            {
                while (reader.Read())
                {
                    obj = GetItemFromReader(reader);
                    collection.Add(obj);
                }
            }
            return collection;
        }

        private static SqlParameter[] CreateSqlParameter(LocaleStringResource model)
        {
            return new SqlParameter[]
                {
                    new SqlParameter("@LocaleStringResourceID", model.LocaleStringResourceID),
                    new SqlParameter("@LanguageID", model.LanguageID),
                    new SqlParameter("@ResourceName", model.ResourceName),
                    new SqlParameter("@ResourceValue", model.ResourceValue),
                    new SqlParameter("@CompanyID", model.CompanyID),
                    new SqlParameter("@CreatedUser", model.CreatedUser),
                    new SqlParameter("@CreatedDate", model.CreatedDate),

                };
        }

        public static int DeleteItem(int itemID, int CompanyID)
        {
            return SqlHelper.ExecuteNonQuery("LocaleStringResource_Delete", new SqlParameter[]
            {
                new SqlParameter(@"LocaleStringResourceID",itemID),
                    new SqlParameter("@CompanyID", CompanyID) });
        }
    }
}