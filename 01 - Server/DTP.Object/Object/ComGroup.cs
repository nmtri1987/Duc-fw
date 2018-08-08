using System.Text;
using System.Data;
using System.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using DTP.Object;
namespace DTP.Object
{
    [DataContract]
    public class ComGroup : BaseDBEntity
    {
        [DataMember]
        public string ComGroupId { get; set; }

        [DataMember]
        public string GroupName { get; set; }

        [DataMember]
        public string GroupDesc { get; set; }

        [DataMember]
        public string GroupLocation { get; set; }

        [DataMember]
        public string GroupLong { get; set; }

        [DataMember]
        public string GroupLat { get; set; }

        [DataMember]
        public int GroupEvent { get; set; }

        [DataMember]
        public decimal DonateAmount { get; set; }

        [DataMember]
        public decimal ExpenseAmount { get; set; }

        [DataMember]
        public string CurrencyCode { get; set; }

        [DataMember]
        public string CreatedUser { get; set; }

        [DataMember]
        public DateTime CreatedDate { get; set; }
        [DataMember]
        public string ImageUrl { get; set; }
    }
    public class ComGroupCollection : BaseDBEntityCollection<ComGroup> { }
    public class ComGroupManager
    {
        private static ComGroup GetItemFromReader(IDataReader dataReader)
        {
            ComGroup objItem = new ComGroup();
            objItem.ComGroupId = SqlHelper.GetString(dataReader, "ComGroupId");

            objItem.GroupName = SqlHelper.GetString(dataReader, "GroupName");

            objItem.GroupDesc = SqlHelper.GetString(dataReader, "GroupDesc");

            objItem.GroupLocation = SqlHelper.GetString(dataReader, "GroupLocation");

            objItem.GroupLong = SqlHelper.GetString(dataReader, "GroupLong");

            objItem.GroupLat = SqlHelper.GetString(dataReader, "GroupLat");

            objItem.GroupEvent = SqlHelper.GetInt(dataReader, "GroupEvent");

            objItem.DonateAmount = SqlHelper.GetDecimal(dataReader, "DonateAmount");

            objItem.ExpenseAmount = SqlHelper.GetDecimal(dataReader, "ExpenseAmount");

            objItem.CurrencyCode = SqlHelper.GetString(dataReader, "CurrencyCode");

            objItem.CreatedUser = SqlHelper.GetString(dataReader, "CreatedUser");

            objItem.CreatedDate = SqlHelper.GetDateTime(dataReader, "CreatedDate");
            objItem.ImageUrl = SqlHelper.GetString(dataReader, "ImageUrl");
            return objItem;
        }
        public static ComGroup GetItemByID(String comGroupId)
        {
            ComGroup item = new ComGroup();
            var sqlParams = new SqlParameter[1];
            sqlParams[0] = new SqlParameter("@ComGroupId", comGroupId);
            using (var reader = SqlHelper.ExecuteReader("tblComGroup_GetByID", sqlParams))
            {
                while (reader.Read())
                {
                    item = GetItemFromReader(reader);
                }
            }
            return item;


        }
        public static ComGroup AddItem(ComGroup model)
        {
            String result = String.Empty;
            using (var reader = SqlHelper.ExecuteReader(CommandType.StoredProcedure, "tblComGroup_Add", CreateSqlParameter(model)))
            {
                while (reader.Read())
                {
                    result = (String)reader[0];
                }
            }
            return GetItemByID(result);

        }
        public static ComGroup UpdateItem(ComGroup model)
        {
            String result = String.Empty;
            using (var reader = SqlHelper.ExecuteReader(CommandType.StoredProcedure, "tblComGroup_Update", CreateSqlParameter(model)))
            {
                while (reader.Read())
                {
                    result = (String)reader[0];
                }
            }
            return GetItemByID(result);

        }
        public static ComGroupCollection GetAllItem()
        {
            ComGroupCollection collection = new ComGroupCollection();
            using (var reader = SqlHelper.ExecuteReader("tblComGroup_GetAll", null))
            {
                while (reader.Read())
                {
                    ComGroup obj = new ComGroup();
                    obj = GetItemFromReader(reader);
                    collection.Add(obj);
                }
            }
            return collection;
        }

        public static ComGroupCollection GetbyUser(string CreatedUser)
        {
            ComGroupCollection collection = new ComGroupCollection();
            using (var reader = SqlHelper.ExecuteReader("tblComGroup_GetAll_byUser", new SqlParameter("@CreatedUser", CreatedUser)))
            {
                while (reader.Read())
                {
                    ComGroup obj = new ComGroup();
                    obj = GetItemFromReader(reader);
                    collection.Add(obj);
                }
            }
            return collection;
        }
        private static SqlParameter[] CreateSqlParameter(ComGroup model)
        {
            return new SqlParameter[]
                {
                new SqlParameter("@ComGroupId", model.ComGroupId),
					new SqlParameter("@GroupName", model.GroupName),
					new SqlParameter("@GroupDesc", model.GroupDesc),
					new SqlParameter("@GroupLocation", model.GroupLocation),
					new SqlParameter("@GroupLong", model.GroupLong),
					new SqlParameter("@GroupLat", model.GroupLat),
					new SqlParameter("@GroupEvent", model.GroupEvent),
					new SqlParameter("@DonateAmount", model.DonateAmount),
					new SqlParameter("@ExpenseAmount", model.ExpenseAmount),
					new SqlParameter("@CurrencyCode", model.CurrencyCode),
					new SqlParameter("@CreatedUser", model.CreatedUser),
					new SqlParameter("@CreatedDate", model.CreatedDate),
                    new SqlParameter("@ImageUrl", model.ImageUrl),
                };
        }

        public static int DeleteItem(String itemID)
        {
            return SqlHelper.ExecuteNonQuery("tblComGroup_Delete", itemID);
        }
    }
}
