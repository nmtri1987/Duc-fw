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
    public class EventItem : BaseDBEntity
    {
        [DataMember]
        public string ItemCode { get; set; }

        [DataMember]
        public string ItemName { get; set; }

        [DataMember]
        public string EventCode { get; set; }

        [DataMember]
        public String ItemDesc { get; set; }

        [DataMember]
        public decimal Prices { get; set; }

        [DataMember]
        public decimal DonatePrices { get; set; }

        [DataMember]
        public decimal ConfirmPrices { get; set; }

        [DataMember]
        public decimal BuyerConfirm { get; set; }

        [DataMember]
        public decimal SellerConfirm { get; set; }

        [DataMember]
        public string Status { get; set; }

        [DataMember]
        public string ImgUrl { get; set; }

        [DataMember]
        public bool isSell { get; set; }

        [DataMember]
        public string CreatedUser { get; set; }

        [DataMember]
        public DateTime CreatedDate { get; set; }

        [DataMember]
        public string BuyerName { get; set; }

        [DataMember]
        public string BuyerCode { get; set; }

        [DataMember]
        public string BuyerPhone { get; set; }

        [DataMember]
        public bool IsPhoneValidate { get; set; }

        [DataMember]
        public string ItemTypeCode { get; set; }

        [DataMember]
        public string Curyid { get; set; }

        [DataMember]
        public decimal CuryAmt { get; set; }

        [DataMember]
        public string ItemClass { get; set; }
    }
    public class EventItemCollection : BaseDBEntityCollection<EventItem> { }
    public class EventItemManager
    {
        private static EventItem GetItemFromReader(IDataReader dataReader)
        {
            EventItem objItem = new EventItem();
            objItem.ItemCode = SqlHelper.GetString(dataReader, "ItemCode");

            objItem.ItemName = SqlHelper.GetString(dataReader, "ItemName");

            objItem.EventCode = SqlHelper.GetString(dataReader, "EventCode");

            objItem.ItemDesc = SqlHelper.GetString(dataReader, "ItemDesc");

            objItem.Prices = SqlHelper.GetDecimal(dataReader, "Prices");

            objItem.DonatePrices = SqlHelper.GetDecimal(dataReader, "DonatePrices");

            objItem.ConfirmPrices = SqlHelper.GetDecimal(dataReader, "ConfirmPrices");

            objItem.BuyerConfirm = SqlHelper.GetDecimal(dataReader, "BuyerConfirm");

            objItem.SellerConfirm = SqlHelper.GetDecimal(dataReader, "SellerConfirm");

            objItem.Status = SqlHelper.GetString(dataReader, "Status");

            objItem.ImgUrl = SqlHelper.GetString(dataReader, "ImgUrl");

            objItem.isSell = SqlHelper.GetBoolean(dataReader, "isSell");

            objItem.CreatedUser = SqlHelper.GetString(dataReader, "CreatedUser");

            objItem.CreatedDate = SqlHelper.GetDateTime(dataReader, "CreatedDate");

            objItem.BuyerName = SqlHelper.GetString(dataReader, "BuyerName");

            objItem.BuyerCode = SqlHelper.GetString(dataReader, "BuyerCode");

            objItem.BuyerPhone = SqlHelper.GetString(dataReader, "BuyerPhone");

            objItem.IsPhoneValidate = SqlHelper.GetBoolean(dataReader, "IsPhoneValidate");
            objItem.ItemTypeCode = SqlHelper.GetString(dataReader, "ItemTypeCode");
            objItem.Curyid = SqlHelper.GetString(dataReader, "Curyid");

            objItem.CuryAmt = SqlHelper.GetDecimal(dataReader, "CuryAmt");
            objItem.ItemClass = SqlHelper.GetString(dataReader, "ItemClass");
            return objItem;
        }
        public static EventItem GetItemByID(String itemCode)
        {
            EventItem item = new EventItem();
            var sqlParams = new SqlParameter[1];
            sqlParams[0] = new SqlParameter("@ItemCode", itemCode);
            using (var reader = SqlHelper.ExecuteReader("tblEventItem_GetByID", sqlParams))
            {
                while (reader.Read())
                {
                    item = GetItemFromReader(reader);
                }
            }
            return item;


        }
        public static EventItem AddItem(EventItem model)
        {
            String result = String.Empty;
            using (var reader = SqlHelper.ExecuteReader(CommandType.StoredProcedure, "tblEventItem_Add", CreateSqlParameter(model)))
            {
                while (reader.Read())
                {
                    result = (String)reader[0];
                }
            }
            return GetItemByID(result);

        }
        public static EventItem UpdateItem(EventItem model)
        {
            String result = String.Empty;
            using (var reader = SqlHelper.ExecuteReader(CommandType.StoredProcedure, "tblEventItem_Update", CreateSqlParameter(model)))
            {
                while (reader.Read())
                {
                    result = (String)reader[0];
                }
            }
            return GetItemByID(result);

        }
        public static EventItemCollection GetAllItem()
        {
            EventItemCollection collection = new EventItemCollection();
            using (var reader = SqlHelper.ExecuteReader("tblEventItem_GetAll", null))
            {
                while (reader.Read())
                {
                    EventItem obj = new EventItem();
                    obj = GetItemFromReader(reader);
                    collection.Add(obj);
                }
            }
            return collection;
        }

        public static EventItemCollection GetAllItemByEventCode(string EventCode)
        {
            EventItemCollection collection = new EventItemCollection();
            using (var reader = SqlHelper.ExecuteReader("tblEventItem_GetAllByEventCode", new SqlParameter("@EventCode", EventCode)))
            {
                while (reader.Read())
                {
                    EventItem obj = new EventItem();
                    obj = GetItemFromReader(reader);
                    collection.Add(obj);
                }
            }
            return collection;
        }

        public static EventItemCollection Filter(string EventCode,string ItemType)
        {
            EventItemCollection collection = new EventItemCollection();
            using (var reader = SqlHelper.ExecuteReader("tblEventItem_Filter", new SqlParameter[]
                {
                new SqlParameter("@EventCode", EventCode),
					new SqlParameter("@ItemType", ItemType)}))
            {
                while (reader.Read())
                {
                    EventItem obj = new EventItem();
                    obj = GetItemFromReader(reader);
                    collection.Add(obj);
                }
            }
            return collection;
        }

        public static EventItemCollection GetbyUser(string CreatedUser)
        {
            EventItemCollection collection = new EventItemCollection();
            EventItem obj;
            using (var reader = SqlHelper.ExecuteReader("tblEventItem_GetAll_byUser", new SqlParameter("@CreatedUser", CreatedUser)))
            {
                while (reader.Read())
                {
                    obj = GetItemFromReader(reader);
                    collection.Add(obj);
                }
            }
            return collection;
        }
        private static SqlParameter[] CreateSqlParameter(EventItem model)
        {
            return new SqlParameter[]
                {
                new SqlParameter("@ItemCode", model.ItemCode),
					new SqlParameter("@ItemName", model.ItemName),
					new SqlParameter("@EventCode", model.EventCode),
					new SqlParameter("@ItemDesc", model.ItemDesc),
					new SqlParameter("@Prices", model.Prices),
					new SqlParameter("@DonatePrices", model.DonatePrices),
					new SqlParameter("@ConfirmPrices", model.ConfirmPrices),
					new SqlParameter("@BuyerConfirm", model.BuyerConfirm),
					new SqlParameter("@SellerConfirm", model.SellerConfirm),
					new SqlParameter("@Status", model.Status),
					new SqlParameter("@ImgUrl", model.ImgUrl),
					new SqlParameter("@isSell", model.isSell),
					new SqlParameter("@CreatedUser", model.CreatedUser),
					new SqlParameter("@CreatedDate", model.CreatedDate),
					new SqlParameter("@BuyerName", model.BuyerName),
					new SqlParameter("@BuyerCode", model.BuyerCode),
					new SqlParameter("@BuyerPhone", model.BuyerPhone),
					new SqlParameter("@IsPhoneValidate", model.IsPhoneValidate),
                    new SqlParameter("@ItemTypeCode", model.ItemTypeCode),
                    new SqlParameter("@Curyid", model.Curyid),
					new SqlParameter("@CuryAmt", model.CuryAmt),
                    new SqlParameter("@ItemClass", model.ItemClass),

					
                };
        }

        public static int DeleteItem(String itemID)
        {
            return SqlHelper.ExecuteNonQuery("tblEventItem_Delete", itemID);
        }
    }
}
