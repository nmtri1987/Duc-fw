using System.Text;
using System.Data;
using System.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
//using Server.DAC;
//using Server.Helpers;
namespace DTP.Object
{
    [DataContract]
    public class ItemClassAttribute : BaseDBEntity
    {
        [DataMember]
        public string AttID { get; set; }

        [DataMember]
        public string ClassCode { get; set; }

        [DataMember]
        public string AttName { get; set; }

        [DataMember]
        public string AttDesc { get; set; }

        [DataMember]
        public bool Active { get; set; }

        [DataMember]
        public string CreatedUser { get; set; }

        [DataMember]
        public DateTime CreatedDate { get; set; }


    }
    public class ItemClassAttributeCollection : BaseDBEntityCollection<ItemClassAttribute> { }
    public class ItemClassAttributeManager
    {
        private static ItemClassAttribute GetItemFromReader(IDataReader dataReader)
        {
            ItemClassAttribute objItem = new ItemClassAttribute();
            objItem.AttID = SqlHelper.GetString(dataReader, "AttID");

            objItem.ClassCode = SqlHelper.GetString(dataReader, "ClassCode");

            objItem.AttName = SqlHelper.GetString(dataReader, "AttName");

            objItem.AttDesc = SqlHelper.GetString(dataReader, "AttDesc");

            objItem.Active = SqlHelper.GetBoolean(dataReader, "Active");

            objItem.CreatedUser = SqlHelper.GetString(dataReader, "CreatedUser");

            objItem.CreatedDate = SqlHelper.GetDateTime(dataReader, "CreatedDate");


            return objItem;
        }
        public static ItemClassAttribute GetItemByID(String attID)
        {
            ItemClassAttribute item = new ItemClassAttribute();
            var sqlParams = new SqlParameter[1];
            sqlParams[0] = new SqlParameter("@AttID", attID);
            using (var reader = SqlHelper.ExecuteReader("tblItemClassAttribute_GetByID", sqlParams))
            {
                while (reader.Read())
                {
                    item = GetItemFromReader(reader);
                }
            }
            return item;


        }
        public static ItemClassAttribute AddItem(ItemClassAttribute model)
        {
            String result = String.Empty;
            using (var reader = SqlHelper.ExecuteReader(CommandType.StoredProcedure, "tblItemClassAttribute_Add", CreateSqlParameter(model)))
            {
                while (reader.Read())
                {
                    result = (String)reader[0];
                }
            }
            return GetItemByID(result);

        }
        public static ItemClassAttribute UpdateItem(ItemClassAttribute model)
        {
            String result = String.Empty;
            using (var reader = SqlHelper.ExecuteReader(CommandType.StoredProcedure, "tblItemClassAttribute_Update", CreateSqlParameter(model)))
            {
                while (reader.Read())
                {
                    result = (String)reader[0];
                }
            }
            return GetItemByID(result);

        }
        public static ItemClassAttributeCollection GetAllItem()
        {
            ItemClassAttributeCollection collection = new ItemClassAttributeCollection();
            using (var reader = SqlHelper.ExecuteReader("tblItemClassAttribute_GetAll", null))
            {
                while (reader.Read())
                {
                    ItemClassAttribute obj = new ItemClassAttribute();
                    obj = GetItemFromReader(reader);
                    collection.Add(obj);
                }
            }
            return collection;
        }
        public static ItemClassAttributeCollection GetbyUser(string CreatedUser)
        {
            ItemClassAttributeCollection collection = new ItemClassAttributeCollection();
            ItemClassAttribute obj;
            using (var reader = SqlHelper.ExecuteReader("tblItemClassAttribute_GetAll_byUser", new SqlParameter("@CreatedUser", CreatedUser)))
            {
                while (reader.Read())
                {
                    obj = GetItemFromReader(reader);
                    collection.Add(obj);
                }
            }
            return collection;
        }
        public static ItemClassAttributeCollection GetAllByClassCode(string ClassCode)
        {
            ItemClassAttributeCollection collection = new ItemClassAttributeCollection();
            ItemClassAttribute obj;
            using (var reader = SqlHelper.ExecuteReader("tblItemClassAttribute_GetAll_byClassCode", new SqlParameter("@ClassCode", ClassCode)))
            {
                while (reader.Read())
                {
                    obj = GetItemFromReader(reader);
                    collection.Add(obj);
                }
            }
            return collection;
        }
        private static SqlParameter[] CreateSqlParameter(ItemClassAttribute model)
        {
            return new SqlParameter[]
                {
                new SqlParameter("@AttID", model.AttID),
					new SqlParameter("@ClassCode", model.ClassCode),
					new SqlParameter("@AttName", model.AttName),
					new SqlParameter("@AttDesc", model.AttDesc),
					new SqlParameter("@Active", model.Active),
					new SqlParameter("@CreatedUser", model.CreatedUser),
					new SqlParameter("@CreatedDate", model.CreatedDate),
					
                };
        }

        public static int DeleteItem(String itemID)
        {
            return SqlHelper.ExecuteNonQuery("tblItemClassAttribute_Delete", itemID);
        }
    }
}