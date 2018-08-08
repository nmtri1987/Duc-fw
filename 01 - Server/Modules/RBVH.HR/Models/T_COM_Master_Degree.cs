using System.Text;
using System.Data;
using System.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using DTP.Data;
namespace Biz.OG
{
    [DataContract]
    public class T_COM_Master_Degree : BaseDBEntity
    {

        [DataMember]
        public int DegreeID { get; set; }

        [DataMember]
        public string Degree { get; set; }

        [DataMember]
        public string DegreeVN { get; set; }

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
    public class T_COM_Master_DegreeCollection : BaseDBEntityCollection<T_COM_Master_Degree> { }
    public class T_COM_Master_DegreeManager
    {
        private static T_COM_Master_Degree GetItemFromReader(IDataReader dataReader)
        {
            T_COM_Master_Degree objItem = new T_COM_Master_Degree();

            objItem.DegreeID = SqlHelper.GetInt(dataReader, "DegreeID");

            objItem.Degree = SqlHelper.GetString(dataReader, "Degree");

            objItem.DegreeVN = SqlHelper.GetString(dataReader, "DegreeVN");

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
        public static T_COM_Master_Degree GetItemByID(int DegreeID)
        {
            T_COM_Master_Degree item = new T_COM_Master_Degree();
            var sqlParams = new SqlParameter[1];
            sqlParams[0] = new SqlParameter("@DegreeID", DegreeID);
            using (var reader = SqlHelper.ExecuteReaderService(ModuleConfig.MyConnection, "T_COM_Master_Degree_GetByID", sqlParams))
            {
                while (reader.Read())
                {
                    item = GetItemFromReader(reader);
                }
            }
            return item;


        }
        public static T_COM_Master_Degree AddItem(T_COM_Master_Degree model)
        {
            int result = 0;
            using (var reader = SqlHelper.ExecuteReaderService(ModuleConfig.MyConnection, CommandType.StoredProcedure, "T_COM_Master_Degree_Add", CreateSqlParameter(model)))
            {
                while (reader.Read())
                {
                    result = (int)reader[0];
                }
            }
            return GetItemByID(result);

        }
        public static T_COM_Master_Degree UpdateItem(T_COM_Master_Degree model)
        {
            int result = 0;
            using (var reader = SqlHelper.ExecuteReaderService(ModuleConfig.MyConnection, CommandType.StoredProcedure, "T_COM_Master_Degree_Update", CreateSqlParameter(model)))
            {
                while (reader.Read())
                {
                    result = (int)reader[0];
                }
            }
            return GetItemByID(result);

        }
        public static T_COM_Master_DegreeCollection GetAllItem()
        {
            T_COM_Master_DegreeCollection collection = new T_COM_Master_DegreeCollection();
            using (var reader = SqlHelper.ExecuteReaderService(ModuleConfig.MyConnection, "T_COM_Master_Degree_GetAll", null))
            {
                while (reader.Read())
                {
                    T_COM_Master_Degree obj = new T_COM_Master_Degree();
                    obj = GetItemFromReader(reader);
                    collection.Add(obj);
                }
            }
            return collection;
        }

        public static T_COM_Master_DegreeCollection Search(SearchFilter SearchKey)
        {
            T_COM_Master_DegreeCollection collection = new T_COM_Master_DegreeCollection();
            using (var reader = SqlHelper.ExecuteReaderService(ModuleConfig.MyConnection, "T_COM_Master_Degree_Search", SearchFilterManager.SqlSearchParamNoCompany(SearchKey)))
            {
                while (reader.Read())
                {
                    T_COM_Master_Degree obj = new T_COM_Master_Degree();
                    obj = GetItemFromReader(reader);
                    collection.Add(obj);
                }
            }
            return collection;
        }
        public static T_COM_Master_DegreeCollection GetbyUser(string CreatedUser)
        {
            T_COM_Master_DegreeCollection collection = new T_COM_Master_DegreeCollection();
            T_COM_Master_Degree obj;
            using (var reader = SqlHelper.ExecuteReaderService(ModuleConfig.MyConnection, "T_COM_Master_Degree_GetAll_byUser", new SqlParameter("@CreatedUser", CreatedUser)))
            {
                while (reader.Read())
                {
                    obj = GetItemFromReader(reader);
                    collection.Add(obj);
                }
            }
            return collection;
        }

        private static SqlParameter[] CreateSqlParameter(T_COM_Master_Degree model)
        {
            return new SqlParameter[]
                {
                    new SqlParameter("@DegreeID", model.DegreeID),
                    new SqlParameter("@Degree", model.Degree),
                    new SqlParameter("@DegreeVN", model.DegreeVN),
                    new SqlParameter("@CreatedBy", model.CreatedBy),
                    new SqlParameter("@CreatedDate", model.CreatedDate),
                    new SqlParameter("@ModifiedBy", model.ModifiedBy),
                    new SqlParameter("@ModifiedDate", model.ModifiedDate),
                    new SqlParameter("@IsActive", model.IsActive),

                };
        }

        public static int DeleteItem(int itemID)
        {
            return SqlHelper.ExecuteNonQuery("T_COM_Master_Degree_Delete", itemID);
        }
    }
}