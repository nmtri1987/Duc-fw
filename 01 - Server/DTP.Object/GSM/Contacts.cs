using System.Text;
using System.Data;
using System.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
//using Server.DAC;
//using Server.Helpers;
namespace DTP.Object.GSM
{
    [DataContract]
    public class Contacts : BaseDBEntity
    {
        [DataMember]
        public int Id { get; set; }

        [DataMember]
        public string PhoneNumber { get; set; }

        [DataMember]
        public string ComPort { get; set; }

        [DataMember]
        public DateTime CreatedDate { get; set; }
    }
    public class ContactsCollection : BaseDBEntityCollection<Contacts> { }
    public class ContactsManager
    {
        private static Contacts GetItemFromReader(IDataReader dataReader)
        {
            Contacts objItem = new Contacts();
            objItem.Id = SqlHelper.GetInt(dataReader, "Id");

            objItem.PhoneNumber = SqlHelper.GetString(dataReader, "PhoneNumber");

            objItem.ComPort = SqlHelper.GetString(dataReader, "ComPort");

            objItem.CreatedDate = SqlHelper.GetDateTime(dataReader, "CreatedDate");
            return objItem;
        }
        public static Contacts GetItemByID(Int32 id)
        {
            Contacts item = new Contacts();
            var sqlParams = new SqlParameter[1];
            sqlParams[0] = new SqlParameter("@Id", id);
            using (var reader = SqlHelper.ExecuteReader("Contacts_GetByID", sqlParams))
            {
                while (reader.Read())
                {
                    item = GetItemFromReader(reader);
                }
            }
            return item;


        }
        public static Contacts AddItem(Contacts model)
        {
            Int32 result = 0;
            using (var reader = SqlHelper.ExecuteReader(CommandType.StoredProcedure, "Contacts_Add", CreateSqlParameter(model)))
            {
                while (reader.Read())
                {
                    result = (Int32)reader[0];
                }
            }
            return GetItemByID(result);

        }
        public static Contacts UpdateItem(Contacts model)
        {
            Int32 result = 0;
            using (var reader = SqlHelper.ExecuteReader(CommandType.StoredProcedure, "Contacts_Update", CreateSqlParameter(model)))
            {
                while (reader.Read())
                {
                    result = (Int32)reader[0];
                }
            }
            return GetItemByID(result);

        }
        public static ContactsCollection GetAllItem()
        {
            ContactsCollection collection = new ContactsCollection();
            using (var reader = SqlHelper.ExecuteReader("Contacts_GetAll", null))
            {
                while (reader.Read())
                {
                    Contacts obj = new Contacts();
                    obj = GetItemFromReader(reader);
                    collection.Add(obj);
                }
            }
            return collection;
        }
        public static ContactsCollection GetbyUser(string CreatedUser)
        {
            ContactsCollection collection = new ContactsCollection();
            Contacts obj;
            using (var reader = SqlHelper.ExecuteReader("Contacts_GetAll_byUser", new SqlParameter("@CreatedUser", CreatedUser)))
            {
                while (reader.Read())
                {
                    obj = GetItemFromReader(reader);
                    collection.Add(obj);
                }
            }
            return collection;
        }

        private static SqlParameter[] CreateSqlParameter(Contacts model)
        {
            return new SqlParameter[]
                {
                new SqlParameter("@Id", model.Id),
                    new SqlParameter("@PhoneNumber", model.PhoneNumber),
                    new SqlParameter("@ComPort", model.ComPort),
                    new SqlParameter("@CreatedDate", model.CreatedDate),

                };
        }

        public static int DeleteItem(Int32 itemID)
        {
            return SqlHelper.ExecuteNonQuery("Contacts_Delete", itemID);
        }
    }
}
