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
    public class ItemAttribute : BaseDBEntity
    {
        [DataMember]
        public string MapCode { get; set; }

        [DataMember]
        public string ItemCode { get; set; }

        [DataMember]
        public string AttributeCode { get; set; }

        [DataMember]
        public string ItemValue { get; set; }

        [DataMember]
        public string CreatedUser { get; set; }

        [DataMember]
        public DateTime CreatedDate { get; set; }
    }
    public class ItemAttributeCollection : BaseDBEntityCollection<ItemAttribute> { }
    public class ItemAttributeManager
    {
        private static ItemAttribute GetItemFromReader(IDataReader dataReader)
        {
            ItemAttribute objItem = new ItemAttribute();
            objItem.MapCode = SqlHelper.GetString(dataReader, "MapCode");

            objItem.ItemCode = SqlHelper.GetString(dataReader, "ItemCode");

            objItem.AttributeCode = SqlHelper.GetString(dataReader, "AttributeCode");

            objItem.ItemValue = SqlHelper.GetString(dataReader, "ItemValue");

            objItem.CreatedUser = SqlHelper.GetString(dataReader, "CreatedUser");

            objItem.CreatedDate = SqlHelper.GetDateTime(dataReader, "CreatedDate");
            return objItem;
        }
        public static ItemAttribute GetItemByID(String mapCode)
        {
            ItemAttribute item = new ItemAttribute();
            var sqlParams = new SqlParameter[1];
            sqlParams[0] = new SqlParameter("@MapCode", mapCode);
            using (var reader = SqlHelper.ExecuteReader("tblItemAttribute_GetByID", sqlParams))
            {
                while (reader.Read())
                {
                    item = GetItemFromReader(reader);
                }
            }
            return item;


        }
        public static ItemAttribute AddItem(ItemAttribute model)
        {
            String result = String.Empty;
            using (var reader = SqlHelper.ExecuteReader(CommandType.StoredProcedure, "tblItemAttribute_Add", CreateSqlParameter(model)))
            {
                while (reader.Read())
                {
                    result = (String)reader[0];
                }
            }
            return GetItemByID(result);

        }
        public static ItemAttribute UpdateItem(ItemAttribute model)
        {
            String result = String.Empty;
            using (var reader = SqlHelper.ExecuteReader(CommandType.StoredProcedure, "tblItemAttribute_Update", CreateSqlParameter(model)))
            {
                while (reader.Read())
                {
                    result = (String)reader[0];
                }
            }
            return GetItemByID(result);

        }
        public static ItemAttributeCollection GetAllItem()
        {
            ItemAttributeCollection collection = new ItemAttributeCollection();
            using (var reader = SqlHelper.ExecuteReader("tblItemAttribute_GetAll", null))
            {
                while (reader.Read())
                {
                    ItemAttribute obj = new ItemAttribute();
                    obj = GetItemFromReader(reader);
                    collection.Add(obj);
                }
            }
            return collection;
        }
        public static ItemAttributeCollection GetbyUser(string CreatedUser)
        {
            ItemAttributeCollection collection = new ItemAttributeCollection();
            ItemAttribute obj;
            using (var reader = SqlHelper.ExecuteReader("tblItemAttribute_GetAll_byUser", new SqlParameter("@CreatedUser", CreatedUser)))
            {
                while (reader.Read())
                {
                    obj = GetItemFromReader(reader);
                    collection.Add(obj);
                }
            }
            return collection;
        }

        public static ItemAttributeCollection GetbyItem(string ItemCode)
        {
            ItemAttributeCollection collection = new ItemAttributeCollection();
            ItemAttribute obj;
            using (var reader = SqlHelper.ExecuteReader("tblItemAttribute_GetAll_byItem", new SqlParameter("@ItemCode", ItemCode)))
            {
                while (reader.Read())
                {
                    obj = GetItemFromReader(reader);
                    collection.Add(obj);
                }
            }
            return collection;
        }
        private static SqlParameter[] CreateSqlParameter(ItemAttribute model)
        {
            return new SqlParameter[]
                {
                new SqlParameter("@MapCode", model.MapCode),
					new SqlParameter("@ItemCode", model.ItemCode),
					new SqlParameter("@AttributeCode", model.AttributeCode),
					new SqlParameter("@ItemValue", model.ItemValue),
					new SqlParameter("@CreatedUser", model.CreatedUser),
					new SqlParameter("@CreatedDate", model.CreatedDate),
					
                };
        }

        public static int DeleteItem(String itemID)
        {
            return SqlHelper.ExecuteNonQuery("tblItemAttribute_Delete", itemID);
        }
    }
}