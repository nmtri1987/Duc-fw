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
    public class ItemType : BaseDBEntity
    {
        [DataMember]
        public int Id { get; set; }

        [DataMember]
        public string GroupCode { get; set; }

        [DataMember]
        public string TypeName { get; set; }

        [DataMember]
        public bool Gobal { get; set; }

        [DataMember]
        public string createdUser { get; set; }

        [DataMember]
        public DateTime CreatedDate { get; set; }

        [DataMember]
        public string ImgUrl { get; set; }


    }
    public class ItemTypeCollection : BaseDBEntityCollection<ItemType> { }
    public class ItemTypeManager
    {
        private static ItemType GetItemFromReader(IDataReader dataReader)
        {
            ItemType objItem = new ItemType();
            objItem.Id = SqlHelper.GetInt(dataReader, "Id");

            objItem.GroupCode = SqlHelper.GetString(dataReader, "GroupCode");

            objItem.TypeName = SqlHelper.GetString(dataReader, "TypeName");

            objItem.Gobal = SqlHelper.GetBoolean(dataReader, "Gobal");

            objItem.createdUser = SqlHelper.GetString(dataReader, "createdUser");

            objItem.CreatedDate = SqlHelper.GetDateTime(dataReader, "CreatedDate");

            objItem.ImgUrl = SqlHelper.GetString(dataReader, "ImgUrl");


            return objItem;
        }
        public static ItemType GetItemByID(Int32 id)
        {
            ItemType item = new ItemType();
            var sqlParams = new SqlParameter[1];
            sqlParams[0] = new SqlParameter("@Id", id);
            using (var reader = SqlHelper.ExecuteReader("tblItemType_GetByID", sqlParams))
            {
                while (reader.Read())
                {
                    item = GetItemFromReader(reader);
                }
            }
            return item;


        }
        public static ItemType AddItem(ItemType model)
        {
            Int32 result = 0;
            using (var reader = SqlHelper.ExecuteReader(CommandType.StoredProcedure, "tblItemType_Add", CreateSqlParameter(model)))
            {
                while (reader.Read())
                {
                    result = (Int32)reader[0];
                }
            }
            return GetItemByID(result);

        }
        public static ItemType UpdateItem(ItemType model)
        {
            Int32 result = 0;
            using (var reader = SqlHelper.ExecuteReader(CommandType.StoredProcedure, "tblItemType_Update", CreateSqlParameter(model)))
            {
                while (reader.Read())
                {
                    result = (Int32)reader[0];
                }
            }
            return GetItemByID(result);

        }
        public static ItemTypeCollection GetAllItem()
        {
            ItemTypeCollection collection = new ItemTypeCollection();
            using (var reader = SqlHelper.ExecuteReader("tblItemType_GetAll", null))
            {
                while (reader.Read())
                {
                    ItemType obj = new ItemType();
                    obj = GetItemFromReader(reader);
                    collection.Add(obj);
                }
            }
            return collection;
        }

        public static ItemTypeCollection GetbyUser(string CreatedUser)
        {
            ItemTypeCollection collection = new ItemTypeCollection();
            ItemType obj;
            using (var reader = SqlHelper.ExecuteReader("tblItemType_GetAll_byUser", new SqlParameter("@CreatedUser", CreatedUser)))
            {
                while (reader.Read())
                {
                    obj = GetItemFromReader(reader);
                    collection.Add(obj);
                }
            }
            return collection;
        }

        public static ItemTypeCollection GetbyEventCode(string EventCode)
        {
            ItemTypeCollection collection = new ItemTypeCollection();
            ItemType obj;
            using (var reader = SqlHelper.ExecuteReader("tblItemType_GetByEventCode", new SqlParameter("@EventCode", EventCode)))
            {
                while (reader.Read())
                {
                    obj = GetItemFromReader(reader);
                    collection.Add(obj);
                }
            }
            return collection;
        }

        private static SqlParameter[] CreateSqlParameter(ItemType model)
        {
            return new SqlParameter[]
                {
                new SqlParameter("@Id", model.Id),
					new SqlParameter("@GroupCode", model.GroupCode),
					new SqlParameter("@TypeName", model.TypeName),
					new SqlParameter("@Gobal", model.Gobal),
					new SqlParameter("@createdUser", model.createdUser),
					new SqlParameter("@CreatedDate", model.CreatedDate),
					new SqlParameter("@ImgUrl", model.ImgUrl),
					
                };
        }

        public static int DeleteItem(Int32 itemID)
        {
            return SqlHelper.ExecuteNonQuery("tblItemType_Delete", itemID);
        }
    }
}