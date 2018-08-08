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
    public class ItemImage : BaseDBEntity
    {
        [DataMember]
        public string ImageCode { get; set; }

        [DataMember]
        public string ItemCode { get; set; }

        [DataMember]
        public string ImgUrl { get; set; }

        [DataMember]
        public string ImgDesc { get; set; }

        [DataMember]
        public DateTime CreatedDate { get; set; }

        [DataMember]
        public string CreatedUser { get; set; }
    }
    public class ItemImageCollection : BaseDBEntityCollection<ItemImage> { }
    public class ItemImageManager
    {
        private static ItemImage GetItemFromReader(IDataReader dataReader)
        {
            ItemImage objItem = new ItemImage();
            objItem.ImageCode = SqlHelper.GetString(dataReader, "ImageCode");

            objItem.ItemCode = SqlHelper.GetString(dataReader, "ItemCode");

            objItem.ImgUrl = SqlHelper.GetString(dataReader, "ImgUrl");

            objItem.ImgDesc = SqlHelper.GetString(dataReader, "ImgDesc");

            objItem.CreatedDate = SqlHelper.GetDateTime(dataReader, "CreatedDate");

            objItem.CreatedUser = SqlHelper.GetString(dataReader, "CreatedUser");
            return objItem;
        }
        public static ItemImage GetItemByID(String imageCode)
        {
            ItemImage item = new ItemImage();
            var sqlParams = new SqlParameter[1];
            sqlParams[0] = new SqlParameter("@ImageCode", imageCode);
            using (var reader = SqlHelper.ExecuteReader("tblItemImage_GetByID", sqlParams))
            {
                while (reader.Read())
                {
                    item = GetItemFromReader(reader);
                }
            }
            return item;


        }
        public static ItemImage AddItem(ItemImage model)
        {
            String result = String.Empty;
            using (var reader = SqlHelper.ExecuteReader(CommandType.StoredProcedure, "tblItemImage_Add", CreateSqlParameter(model)))
            {
                while (reader.Read())
                {
                    result = (String)reader[0];
                }
            }
            return GetItemByID(result);

        }
        public static ItemImage UpdateItem(ItemImage model)
        {
            String result = String.Empty;
            using (var reader = SqlHelper.ExecuteReader(CommandType.StoredProcedure, "tblItemImage_Update", CreateSqlParameter(model)))
            {
                while (reader.Read())
                {
                    result = (String)reader[0];
                }
            }
            return GetItemByID(result);

        }
        public static ItemImageCollection GetAllItem()
        {
            ItemImageCollection collection = new ItemImageCollection();
            using (var reader = SqlHelper.ExecuteReader("tblItemImage_GetAll", null))
            {
                while (reader.Read())
                {
                    ItemImage obj = new ItemImage();
                    obj = GetItemFromReader(reader);
                    collection.Add(obj);
                }
            }
            return collection;
        }
        public static ItemImageCollection GetbyUser(string CreatedUser)
        {
            ItemImageCollection collection = new ItemImageCollection();
            ItemImage obj;
            using (var reader = SqlHelper.ExecuteReader("tblItemImage_GetAll_byUser", new SqlParameter("@CreatedUser", CreatedUser)))
            {
                while (reader.Read())
                {
                    obj = GetItemFromReader(reader);
                    collection.Add(obj);
                }
            }
            return collection;
        }

        public static ItemImageCollection GetbyItemCode(string ItemCode)
        {
            ItemImageCollection collection = new ItemImageCollection();
            ItemImage obj;
            using (var reader = SqlHelper.ExecuteReader("tblItemImage_GetByItemCode", new SqlParameter("@ItemCode", ItemCode)))
            {
                while (reader.Read())
                {
                    obj = GetItemFromReader(reader);
                    collection.Add(obj);
                }
            }
            return collection;
        }

        private static SqlParameter[] CreateSqlParameter(ItemImage model)
        {
            return new SqlParameter[]
                {
                new SqlParameter("@ImageCode", model.ImageCode),
					new SqlParameter("@ItemCode", model.ItemCode),
					new SqlParameter("@ImgUrl", model.ImgUrl),
					new SqlParameter("@ImgDesc", model.ImgDesc),
					new SqlParameter("@CreatedDate", model.CreatedDate),
					new SqlParameter("@CreatedUser", model.CreatedUser),
					
                };
        }

        public static int DeleteItem(String itemID)
        {
            return SqlHelper.ExecuteNonQuery("tblItemImage_Delete", itemID);
        }
    }
}