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
    public class EventType : BaseDBEntity
    {
        [DataMember]
        public int EventTypeId { get; set; }

        [DataMember]
        public string TypeName { get; set; }

        [DataMember]
        public bool isActive { get; set; }

        [DataMember]
        public string CreatedUser { get; set; }

        [DataMember]
        public DateTime CreatedDate { get; set; }


    }
    public class EventTypeCollection : BaseDBEntityCollection<EventType> { }
    public class EventTypeManager
    {
        private static EventType GetItemFromReader(IDataReader dataReader)
        {
            EventType objItem = new EventType();
            objItem.EventTypeId = SqlHelper.GetInt(dataReader, "EventTypeId");

            objItem.TypeName = SqlHelper.GetString(dataReader, "TypeName");

            objItem.isActive = SqlHelper.GetBoolean(dataReader, "isActive");

            objItem.CreatedUser = SqlHelper.GetString(dataReader, "CreatedUser");

            objItem.CreatedDate = SqlHelper.GetDateTime(dataReader, "CreatedDate");


            return objItem;
        }
        public static EventType GetItemByID(Int32 eventTypeId)
        {
            EventType item = new EventType();
            var sqlParams = new SqlParameter[1];
            sqlParams[0] = new SqlParameter("@EventTypeId", eventTypeId);
            using (var reader = SqlHelper.ExecuteReader("tblEventType_GetByID", sqlParams))
            {
                while (reader.Read())
                {
                    item = GetItemFromReader(reader);
                }
            }
            return item;


        }
        public static EventType AddItem(EventType model)
        {
            Int32 result = 0;
            using (var reader = SqlHelper.ExecuteReader(CommandType.StoredProcedure, "tblEventType_Add", CreateSqlParameter(model)))
            {
                while (reader.Read())
                {
                    result = (Int32)reader[0];
                }
            }
            return GetItemByID(result);

        }
        public static EventType UpdateItem(EventType model)
        {
            Int32 result = 0;
            using (var reader = SqlHelper.ExecuteReader(CommandType.StoredProcedure, "tblEventType_Update", CreateSqlParameter(model)))
            {
                while (reader.Read())
                {
                    result = (Int32)reader[0];
                }
            }
            return GetItemByID(result);

        }
        public static EventTypeCollection GetAllItem()
        {
            EventTypeCollection collection = new EventTypeCollection();
            using (var reader = SqlHelper.ExecuteReader("tblEventType_GetAll", null))
            {
                while (reader.Read())
                {
                    EventType obj = new EventType();
                    obj = GetItemFromReader(reader);
                    collection.Add(obj);
                }
            }
            return collection;
        }

        public static EventTypeCollection GetbyEventCode(string EventCode)
        {
            EventTypeCollection collection = new EventTypeCollection();
            EventType obj;
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
        public static EventTypeCollection GetbyUser(string CreatedUser)
        {
            EventTypeCollection collection = new EventTypeCollection();
            EventType obj;
            using (var reader = SqlHelper.ExecuteReader("tblEventType_GetAll_byUser", new SqlParameter("@CreatedUser", CreatedUser)))
            {
                while (reader.Read())
                {
                    obj = GetItemFromReader(reader);
                    collection.Add(obj);
                }
            }
            return collection;
        }

        private static SqlParameter[] CreateSqlParameter(EventType model)
        {
            return new SqlParameter[]
                {
                new SqlParameter("@EventTypeId", model.EventTypeId),
					new SqlParameter("@TypeName", model.TypeName),
					new SqlParameter("@isActive", model.isActive),
					new SqlParameter("@CreatedUser", model.CreatedUser),
					new SqlParameter("@CreatedDate", model.CreatedDate),
					
                };
        }

        public static int DeleteItem(Int32 itemID)
        {
            return SqlHelper.ExecuteNonQuery("tblEventType_Delete", itemID);
        }
    }
}