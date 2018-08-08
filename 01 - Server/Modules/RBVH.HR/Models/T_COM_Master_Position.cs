using System.Text;
using System.Data;
using System.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using DTP.Data;
namespace Biz.OG.Models
{
    [DataContract]
    public class T_COM_Master_Position : BaseDBEntity
    {

        [DataMember]
        public int PositionID { get; set; }

        [DataMember]
        public string PositionName_EN { get; set; }

        [DataMember]
        public string PositionName_VN { get; set; }

        [DataMember]
        public int EntityID { get; set; }

        [DataMember]
        public string EmployeeType { get; set; }

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
    public class T_COM_Master_PositionCollection : BaseDBEntityCollection<T_COM_Master_Position> { }
    public class PositionR : BaseDBEntity
    {
        [DataMember]
        public int PositionID { get; set; }

        [DataMember]
        public string PostionName { get; set; }

        [DataMember]
        public decimal MinSalary { get; set; }

        [DataMember]
        public decimal MaxSalary { get; set; }

    }
    public class PositionRCollection : BaseDBEntityCollection<PositionR> { }
    public class T_COM_Master_PositionManager
    {
        private static T_COM_Master_Position GetItemFromReader(IDataReader dataReader)
        {
            T_COM_Master_Position objItem = new T_COM_Master_Position();

            objItem.PositionID = SqlHelper.GetInt(dataReader, "PositionID");

            objItem.PositionName_EN = SqlHelper.GetString(dataReader, "PositionName_EN");

            objItem.PositionName_VN = SqlHelper.GetString(dataReader, "PositionName_VN");

            objItem.EntityID = SqlHelper.GetInt(dataReader, "EntityID");

            objItem.EmployeeType = SqlHelper.GetString(dataReader, "EmployeeType");

            objItem.CreatedBy = SqlHelper.GetInt(dataReader, "CreatedBy");

            objItem.CreatedDate = SqlHelper.GetDateTime(dataReader, "CreatedDate");

            objItem.ModifiedBy = SqlHelper.GetInt(dataReader, "ModifiedBy");

            objItem.ModifiedDate = SqlHelper.GetDateTime(dataReader, "ModifiedDate");

            objItem.IsActive = SqlHelper.GetBoolean(dataReader, "IsActive");



            return objItem;
        }

        private static PositionR GetPositionRFromReader(IDataReader dataReader)
        {
            PositionR objItem = new PositionR();
            objItem.PositionID = SqlHelper.GetInt(dataReader, "PositionID");
            objItem.PostionName = SqlHelper.GetString(dataReader, "PostionName");
            objItem.MinSalary = SqlHelper.GetDecimal(dataReader, "MinSalary");
            objItem.MaxSalary = SqlHelper.GetDecimal(dataReader, "MaxSalary");
            return objItem;
        }
        public static PositionRCollection GetPostionRangesalary(int EntityID)
        {
            PositionRCollection collection = new PositionRCollection();
            var sqlParams = new SqlParameter[1];
            sqlParams[0] = new SqlParameter("@EntityID", EntityID);
            using (var reader = SqlHelper.ExecuteReaderService(ModuleConfig.MyConnection, "T_COM_Master_Position_Range", sqlParams))
            {
                while (reader.Read())
                {
                    PositionR obj = new PositionR();
                    obj = GetPositionRFromReader(reader);
                    collection.Add(obj);
                }
            }
            return collection;
        }
        public static T_COM_Master_Position GetItemByID(int PositionID)
        {
            T_COM_Master_Position item = new T_COM_Master_Position();
            var sqlParams = new SqlParameter[1];
            sqlParams[0] = new SqlParameter("@PositionID", PositionID);
            using (var reader = SqlHelper.ExecuteReaderService(ModuleConfig.MyConnection, "T_COM_Master_Position_GetByID", sqlParams))
            {
                while (reader.Read())
                {
                    item = GetItemFromReader(reader);
                }
            }
            return item;


        }

        public static T_COM_Master_Position GetItemByName(string PositionName_EN)
        {
            T_COM_Master_Position item = new T_COM_Master_Position();
            var sqlParams = new SqlParameter[1];
            sqlParams[0] = new SqlParameter("@PositionName_EN", PositionName_EN);
            using (var reader = SqlHelper.ExecuteReaderService(ModuleConfig.MyConnection, "T_COM_Master_Position_GetByName", sqlParams))
            {
                while (reader.Read())
                {
                    item = GetItemFromReader(reader);
                }
            }
            return item;


        }
        public static T_COM_Master_Position AddItem(T_COM_Master_Position model)
        {
            int result = 0;
            using (var reader = SqlHelper.ExecuteReaderService(ModuleConfig.MyConnection, CommandType.StoredProcedure, "T_COM_Master_Position_Add", CreateSqlParameter(model)))
            {
                while (reader.Read())
                {
                    result = (int)reader[0];
                }
            }
            return GetItemByID(result);

        }
        public static T_COM_Master_Position UpdateItem(T_COM_Master_Position model)
        {
            int result = 0;
            using (var reader = SqlHelper.ExecuteReaderService(ModuleConfig.MyConnection, CommandType.StoredProcedure, "T_COM_Master_Position_Update", CreateSqlParameter(model)))
            {
                while (reader.Read())
                {
                    result = (int)reader[0];
                }
            }
            return GetItemByID(result);

        }
        public static T_COM_Master_PositionCollection GetAllItem()
        {
            T_COM_Master_PositionCollection collection = new T_COM_Master_PositionCollection();
            using (var reader = SqlHelper.ExecuteReaderService(ModuleConfig.MyConnection, "T_COM_Master_Position_GetAll", null))
            {
                while (reader.Read())
                {
                    T_COM_Master_Position obj = new T_COM_Master_Position();
                    obj = GetItemFromReader(reader);
                    collection.Add(obj);
                }
            }
            return collection;
        }

        public static T_COM_Master_PositionCollection Search(SearchFilter SearchKey)
        {
            T_COM_Master_PositionCollection collection = new T_COM_Master_PositionCollection();
            using (var reader = SqlHelper.ExecuteReaderService(ModuleConfig.MyConnection,"T_COM_Master_Position_Search", SearchFilterManager.SqlSearchParamNoCompany(SearchKey)))
            {
                while (reader.Read())
                {
                    T_COM_Master_Position obj = new T_COM_Master_Position();
                    obj = GetItemFromReader(reader);
                    collection.Add(obj);
                }
            }
            return collection;
        }
        public static T_COM_Master_PositionCollection GetbyUser(string CreatedUser)
        {
            T_COM_Master_PositionCollection collection = new T_COM_Master_PositionCollection();
            T_COM_Master_Position obj;
            using (var reader = SqlHelper.ExecuteReaderService(ModuleConfig.MyConnection, "T_COM_Master_Position_GetAll_byUser", new SqlParameter("@CreatedUser", CreatedUser)))
            {
                while (reader.Read())
                {
                    obj = GetItemFromReader(reader);
                    collection.Add(obj);
                }
            }
            return collection;
        }

        private static SqlParameter[] CreateSqlParameter(T_COM_Master_Position model)
        {
            return new SqlParameter[]
                {
                    new SqlParameter("@PositionID", model.PositionID),
                    new SqlParameter("@PositionName_EN", model.PositionName_EN),
                    new SqlParameter("@PositionName_VN", model.PositionName_VN),
                    new SqlParameter("@EntityID", model.EntityID),
                    new SqlParameter("@EmployeeType", model.EmployeeType),
                    new SqlParameter("@CreatedBy", model.CreatedBy),
                    new SqlParameter("@CreatedDate", model.CreatedDate),
                    new SqlParameter("@ModifiedBy", model.ModifiedBy),
                    new SqlParameter("@ModifiedDate", model.ModifiedDate),
                    new SqlParameter("@IsActive", model.IsActive),

                };
        }

        public static int DeleteItem(int itemID)
        {
            return SqlHelper.ExecuteNonQuery("T_COM_Master_Position_Delete", itemID);
        }
    }
}