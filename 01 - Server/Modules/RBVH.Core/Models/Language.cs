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
    public class Language : BaseDBEntity
    {

        [DataMember]
        public int CompanyID { get; set; }

        [DataMember]
        public int LanguageId { get; set; }

        [DataMember]
        public string Name { get; set; }

        [DataMember]
        public string LanguageCulture { get; set; }

        [DataMember]
        public bool Published { get; set; }

        [DataMember]
        public int DisplayOrder { get; set; }

        [DataMember]
        public int CreatedUser { get; set; }

        [DataMember]
        public DateTime CreatedDate { get; set; }


    }
    public class LanguageCollection : BaseDBEntityCollection<Language> { }
    public class LanguageManager
    {
        private static Language GetItemFromReader(IDataReader dataReader)
        {
            Language objItem = new Language();

            objItem.CompanyID = SqlHelper.GetInt(dataReader, "CompanyID");

            objItem.LanguageId = SqlHelper.GetInt(dataReader, "LanguageId");

            objItem.Name = SqlHelper.GetString(dataReader, "Name");

            objItem.LanguageCulture = SqlHelper.GetString(dataReader, "LanguageCulture");

            objItem.Published = SqlHelper.GetBoolean(dataReader, "Published");

            objItem.DisplayOrder = SqlHelper.GetInt(dataReader, "DisplayOrder");

            objItem.CreatedUser = SqlHelper.GetInt(dataReader, "CreatedUser");

            objItem.CreatedDate = SqlHelper.GetDateTime(dataReader, "CreatedDate");



            if (SqlHelper.ColumnExists(dataReader, "TotalRecord"))
            {
                objItem.TotalRecord = SqlHelper.GetInt(dataReader, "TotalRecord");
            }

            return objItem;
        }
        public static Language GetItemByID(int LanguageId, int CompanyID)
        {
            Language item = new Language();
            var sqlParams = new SqlParameter[]
                    {
                            new SqlParameter("@LanguageId", LanguageId),
                            new SqlParameter("@CompanyID", CompanyID),
                    };
            using (var reader = SqlHelper.ExecuteReader("tblLanguage_GetByID", sqlParams))
            {
                while (reader.Read())
                {
                    item = GetItemFromReader(reader);
                }
            }
            return item;


        }
        public static Language AddItem(Language model)
        {
            int result = 0;
            try
            {
                using (var reader = SqlHelper.ExecuteReader(CommandType.StoredProcedure, "tblLanguage_Add", CreateSqlParameter(model)))
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
        public static Language UpdateItem(Language model)
        {
            String result = String.Empty;
            using (var reader = SqlHelper.ExecuteReader(CommandType.StoredProcedure, "tblLanguage_Update", CreateSqlParameter(model)))
            {
                while (reader.Read())
                {
                    result = (String)reader[0];
                }
            }
            return GetItemByID(model.LanguageId, model.CompanyID);

        }
        public static LanguageCollection GetAllItem(int CompanyID)
        {
            LanguageCollection collection = new LanguageCollection();

            var sqlParams = new SqlParameter[]
                    {
                            new SqlParameter("@CompanyID", CompanyID),
                    };
            using (var reader = SqlHelper.ExecuteReader("tblLanguage_GetAll", sqlParams))
            {
                while (reader.Read())
                {
                    Language obj = new Language();
                    obj = GetItemFromReader(reader);
                    collection.Add(obj);
                }
            }
            return collection;
        }

        public static LanguageCollection Search(SearchFilter SearchKey)
        {
            LanguageCollection collection = new LanguageCollection();
            using (var reader = SqlHelper.ExecuteReader("tblLanguage_Search", SearchFilterManager.SqlSearchParam(SearchKey)))
            {
                while (reader.Read())
                {
                    Language obj = new Language();
                    obj = GetItemFromReader(reader);
                    collection.Add(obj);
                }
            }
            return collection;
        }
        public static LanguageCollection GetbyUser(string CreatedUser, int CompanyID)
        {
            LanguageCollection collection = new LanguageCollection();
            Language obj;
            var sqlParams = new SqlParameter[]
              {
                            new SqlParameter("@CreatedUser", CreatedUser),
                            new SqlParameter("@CompanyID", CompanyID),
              };
            using (var reader = SqlHelper.ExecuteReader("tblLanguage_GetAll_byUser", sqlParams))
            {
                while (reader.Read())
                {
                    obj = GetItemFromReader(reader);
                    collection.Add(obj);
                }
            }
            return collection;
        }

        private static SqlParameter[] CreateSqlParameter(Language model)
        {
            return new SqlParameter[]
                {
                    new SqlParameter("@CompanyID", model.CompanyID),
                    new SqlParameter("@LanguageId", model.LanguageId),
                    new SqlParameter("@Name", model.Name),
                    new SqlParameter("@LanguageCulture", model.LanguageCulture),
                    new SqlParameter("@Published", model.Published),
                    new SqlParameter("@DisplayOrder", model.DisplayOrder),
                    new SqlParameter("@CreatedUser", model.CreatedUser),
                    new SqlParameter("@CreatedDate", model.CreatedDate),

                };
        }

        public static int DeleteItem(int itemID, int CompanyID)
        {
            return SqlHelper.ExecuteNonQuery("tblLanguage_Delete", new SqlParameter[]
            {
                new SqlParameter(@"LanguageId",itemID),
                    new SqlParameter("@CompanyID", CompanyID) });
        }
    }
}