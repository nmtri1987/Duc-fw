using System.Text;
using System.Data;
using System.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using DTP.Data;
namespace RBVH.HR.Models
{
    [DataContract]

    public class T_COM_Master_University : BaseDBEntity
    {

        [DataMember]
        public int UniversityID { get; set; }

        [DataMember]
        public string UniversityName_EN { get; set; }

        [DataMember]
        public string UniversityName_VN { get; set; }

        [DataMember]
        public int EntityID { get; set; }

        [DataMember]
        public int CreatedBy { get; set; }

        [DataMember]
        public DateTime CreatedDate { get; set; }

        [DataMember]
        public int ModifiedBy { get; set; }

        [DataMember]
        public DateTime ModifiedDate { get; set; }

        [DataMember]
        public bool IsActive { get; set; }


    }
    
    public class T_COM_Master_UniversityCollection : BaseDBEntityCollection<T_COM_Master_University> { }
    public class T_COM_Master_UniversityManager
    {
        private static T_COM_Master_University GetItemFromReader(IDataReader dataReader)
        {
            T_COM_Master_University objItem = new T_COM_Master_University();

            objItem.UniversityID = SqlHelper.GetInt(dataReader, "UniversityID");

            objItem.UniversityName_EN = SqlHelper.GetString(dataReader, "UniversityName_EN");

            objItem.UniversityName_VN = SqlHelper.GetString(dataReader, "UniversityName_VN");

            objItem.EntityID = SqlHelper.GetInt(dataReader, "EntityID");

            objItem.CreatedBy = SqlHelper.GetInt(dataReader, "CreatedBy");

            objItem.CreatedDate = SqlHelper.GetDateTime(dataReader, "CreatedDate");

            objItem.ModifiedBy = SqlHelper.GetInt(dataReader, "ModifiedBy");

            objItem.ModifiedDate = SqlHelper.GetDateTime(dataReader, "ModifiedDate");

            objItem.IsActive = SqlHelper.GetBoolean(dataReader, "IsActive");



            if (SqlHelper.ColumnExists(dataReader, "TotalRecord"))
            {
                objItem.TotalRecord = SqlHelper.GetInt(dataReader, "TotalRecord");
            }
            return objItem;
        }
        public static T_COM_Master_University GetItemByID(int UniversityID)
        {
            T_COM_Master_University item = new T_COM_Master_University();
            var sqlParams = new SqlParameter[1];
            sqlParams[0] = new SqlParameter("@UniversityID", UniversityID);
            using (var reader = SqlHelper.ExecuteReaderService(ModuleConfig.MyConnection, "T_COM_Master_University_GetByID", sqlParams))
            {
                while (reader.Read())
                {
                    item = GetItemFromReader(reader);
                }
            }
            return item;


        }

        public static T_COM_Master_University GetbyUniversityName(string UniversityName)
        {
            T_COM_Master_University item = new T_COM_Master_University();
            T_COM_Master_University obj;
            using (var reader = SqlHelper.ExecuteReaderService(ModuleConfig.MyConnection, "T_COM_Master_University_GetByName", new SqlParameter("@UniversityName", UniversityName)))
            {
                while (reader.Read())
                {
                    item = GetItemFromReader(reader);
                }
            }
            return item;
        }
        public static T_COM_Master_University AddItem(T_COM_Master_University model)
        {
            int result = 0;
            using (var reader = SqlHelper.ExecuteReaderService(ModuleConfig.MyConnection, CommandType.StoredProcedure, "T_COM_Master_University_Add", CreateSqlParameter(model)))
            {
                while (reader.Read())
                {
                    result = (int)reader[0];
                }
            }
            return GetItemByID(result);

        }
        public static T_COM_Master_University UpdateItem(T_COM_Master_University model)
        {
            int result = 0;
            using (var reader = SqlHelper.ExecuteReaderService(ModuleConfig.MyConnection, CommandType.StoredProcedure, "T_COM_Master_University_Update", CreateSqlParameter(model)))
            {
                while (reader.Read())
                {
                    result = (int)reader[0];
                }
            }
            return GetItemByID(result);

        }
        public static T_COM_Master_UniversityCollection GetAllItem()
        {
            T_COM_Master_UniversityCollection collection = new T_COM_Master_UniversityCollection();
            using (var reader = SqlHelper.ExecuteReaderService(ModuleConfig.MyConnection, "T_COM_Master_University_GetAll", null))
            {
                while (reader.Read())
                {
                    T_COM_Master_University obj = new T_COM_Master_University();
                    obj = GetItemFromReader(reader);
                    collection.Add(obj);
                }
            }
            return collection;
        }

        public static T_COM_Master_UniversityCollection Search(SearchFilter SearchKey)
        {
            T_COM_Master_UniversityCollection collection = new T_COM_Master_UniversityCollection();
            using (var reader = SqlHelper.ExecuteReaderService(ModuleConfig.MyConnection, "T_COM_Master_University_Search", SearchFilterManager.SqlSearchParamNoCompany(SearchKey)))
            {
                while (reader.Read())
                {
                    T_COM_Master_University obj = new T_COM_Master_University();
                    obj = GetItemFromReader(reader);
                    collection.Add(obj);
                }
            }
            return collection;
        }
        

        private static SqlParameter[] CreateSqlParameter(T_COM_Master_University model)
        {
            return new SqlParameter[]
                {
                    new SqlParameter("@UniversityID", model.UniversityID),
                    new SqlParameter("@UniversityName_EN", model.UniversityName_EN),
                    new SqlParameter("@UniversityName_VN", model.UniversityName_VN),
                    new SqlParameter("@EntityID", model.EntityID),
                    new SqlParameter("@CreatedBy", model.CreatedBy),
                    new SqlParameter("@CreatedDate", model.CreatedDate),
                    new SqlParameter("@ModifiedBy", model.ModifiedBy),
                    new SqlParameter("@ModifiedDate", model.ModifiedDate),
                    new SqlParameter("@IsActive", model.IsActive),

                };
        }

        public static int DeleteItem(int itemID)
        {
            return SqlHelper.ExecuteNonQuery("T_COM_Master_University_Delete", itemID);
        }
    }
}