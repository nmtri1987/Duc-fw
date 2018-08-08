using System.Text;
using System.Data;
using System.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using DTP.Data;

namespace ifinds.Data
{
    [DataContract]
    public class Attachments : BaseDBEntity
    {

        [DataMember]
        public Guid AttachID { get; set; }

        [DataMember]
        public int CompanyID { get; set; }

        [DataMember]
        public string Filetype { get; set; }

        [DataMember]
        public string Title { get; set; }

        [DataMember]
        public string Description { get; set; }

        [DataMember]
        public string Version { get; set; }

        [DataMember]
        public DateTime CreatedDate { get; set; }

        [DataMember]
        public string CreatedUser { get; set; }

        [DataMember]
        public string ModifiedUser { get; set; }

        [DataMember]
        public DateTime ModifiedDate { get; set; }


    }
    public class AttachmentsCollection : BaseDBEntityCollection<Attachments> { }
    public class AttachmentsManager
    {
        private static Attachments GetItemFromReader(IDataReader dataReader)
        {
            Attachments objItem = new Attachments();

            objItem.AttachID = SqlHelper.GetGuid(dataReader, "AttachID");

            objItem.CompanyID = SqlHelper.GetInt(dataReader, "CompanyID");

            objItem.Filetype = SqlHelper.GetString(dataReader, "Filetype");

            objItem.Title = SqlHelper.GetString(dataReader, "Title");

            objItem.Description = SqlHelper.GetString(dataReader, "Description");

            objItem.Version = SqlHelper.GetString(dataReader, "Version");

            objItem.CreatedDate = SqlHelper.GetDateTime(dataReader, "CreatedDate");

            objItem.CreatedUser = SqlHelper.GetString(dataReader, "CreatedUser");

            objItem.ModifiedUser = SqlHelper.GetString(dataReader, "ModifiedUser");

            objItem.ModifiedDate = SqlHelper.GetDateTime(dataReader, "ModifiedDate");



            return objItem;
        }
        public static Attachments GetItemByID(Guid AttachID, int CompanyID)
        {
            Attachments item = new Attachments();
            var sqlParams = new SqlParameter[]
                    {
                            new SqlParameter("@AttachID", AttachID),
                            new SqlParameter("@CompanyID", CompanyID),
                    };
            using (var reader = SqlHelper.ExecuteReader("tbAttachment_GetByID", sqlParams))
            {
                while (reader.Read())
                {
                    item = GetItemFromReader(reader);
                }
            }
            return item;


        }
        public static Attachments AddItem(Attachments model)
        {
            Guid result = Guid.Empty;
            using (var reader = SqlHelper.ExecuteReader(CommandType.StoredProcedure, "tbAttachment_Add", CreateSqlParameter(model)))
            {
                while (reader.Read())
                {
                    result = Guid.Parse(reader[0].ToString());
                }
            }
            return GetItemByID(result, model.CompanyID);

        }
        public static Attachments UpdateItem(Attachments model)
        {
            String result = String.Empty;
            using (var reader = SqlHelper.ExecuteReader(CommandType.StoredProcedure, "tbAttachment_Update", CreateSqlParameter(model)))
            {
                while (reader.Read())
                {
                    result = (String)reader[0];
                }
            }
            return GetItemByID(model.AttachID, model.CompanyID);

        }
        public static AttachmentsCollection GetAllItem(int CompanyID)
        {
            AttachmentsCollection collection = new AttachmentsCollection();

            var sqlParams = new SqlParameter[]
                    {
                            new SqlParameter("@CompanyID", CompanyID),
                    };
            using (var reader = SqlHelper.ExecuteReader("tbAttachment_GetAll", sqlParams))
            {
                while (reader.Read())
                {
                    Attachments obj = new Attachments();
                    obj = GetItemFromReader(reader);
                    collection.Add(obj);
                }
            }
            return collection;
        }

        
        public static AttachmentsCollection GetbyUser(string CreatedUser, int CompanyID)
        {
            AttachmentsCollection collection = new AttachmentsCollection();
            Attachments obj;
            var sqlParams = new SqlParameter[]
              {
                            new SqlParameter("@CreatedUser", CreatedUser),
                            new SqlParameter("@CompanyID", CompanyID),
              };
            using (var reader = SqlHelper.ExecuteReader("tbAttachment_GetAll_byUser", sqlParams))
            {
                while (reader.Read())
                {
                    obj = GetItemFromReader(reader);
                    collection.Add(obj);
                }
            }
            return collection;
        }

        private static SqlParameter[] CreateSqlParameter(Attachments model)
        {
            return new SqlParameter[]
                {
                    new SqlParameter("@AttachID", model.AttachID),
                    new SqlParameter("@CompanyID", model.CompanyID),
                    new SqlParameter("@Filetype", model.Filetype),
                    new SqlParameter("@Title", model.Title),
                    new SqlParameter("@Description", model.Description),
                    new SqlParameter("@Version", model.Version),
                    new SqlParameter("@CreatedDate", model.CreatedDate),
                    new SqlParameter("@CreatedUser", model.CreatedUser),
                    new SqlParameter("@ModifiedUser", model.ModifiedUser),
                    new SqlParameter("@ModifiedDate", model.ModifiedDate),

                };
        }

        public static int DeleteItem(Guid itemID, int CompanyID)
        {
            return SqlHelper.ExecuteNonQuery("tbAttachment_Delete", new SqlParameter[]
            {
                new SqlParameter(@"AttachID",itemID),
                    new SqlParameter("@CompanyID", CompanyID) });
        }
    }
}