using System.Data;
using System.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using DTP.Data;
using DTP.Data.Models;
namespace RBVH.HR.Models
{
    

    public class QueuedEmailManager
    {
        private static QueuedEmail GetItemFromReader(IDataReader dataReader)
        {
            QueuedEmail objItem = new QueuedEmail();

            objItem.CompanyID = SqlHelper.GetInt(dataReader, "CompanyID");

            objItem.Id = SqlHelper.GetInt(dataReader, "Id");

            objItem.PriorityId = SqlHelper.GetInt(dataReader, "PriorityId");

            objItem.From = SqlHelper.GetString(dataReader, "From");

            objItem.FromName = SqlHelper.GetString(dataReader, "FromName");

            objItem.To = SqlHelper.GetString(dataReader, "To");

            objItem.ToName = SqlHelper.GetString(dataReader, "ToName");

            objItem.ReplyTo = SqlHelper.GetString(dataReader, "ReplyTo");

            objItem.ReplyToName = SqlHelper.GetString(dataReader, "ReplyToName");

            objItem.CC = SqlHelper.GetString(dataReader, "CC");

            objItem.Bcc = SqlHelper.GetString(dataReader, "Bcc");

            objItem.Subject = SqlHelper.GetString(dataReader, "Subject");

            objItem.Body = SqlHelper.GetString(dataReader, "Body");

            objItem.AttachmentFilePath = SqlHelper.GetString(dataReader, "AttachmentFilePath");

            objItem.AttachmentFileName = SqlHelper.GetString(dataReader, "AttachmentFileName");

            objItem.AttachedDownloadId = SqlHelper.GetInt(dataReader, "AttachedDownloadId");

            objItem.CreatedOnUtc = SqlHelper.GetDateTime(dataReader, "CreatedOnUtc");

            objItem.DontSendBeforeDateUtc = SqlHelper.GetDateTime(dataReader, "DontSendBeforeDateUtc");

            objItem.SentTries = SqlHelper.GetInt(dataReader, "SentTries");

            objItem.SentOnUtc = SqlHelper.GetDateTime(dataReader, "SentOnUtc");

            objItem.EmailAccountId = SqlHelper.GetInt(dataReader, "EmailAccountId");

            objItem.CreatedUser = SqlHelper.GetInt(dataReader, "CreatedUser");

            objItem.CreatedDate = SqlHelper.GetDateTime(dataReader, "CreatedDate");

            if (SqlHelper.ColumnExists(dataReader, "TotalRecord"))
            {
                objItem.TotalRecord = SqlHelper.GetInt(dataReader, "TotalRecord");
            }

            return objItem;
        }

       
        public static QueuedEmail GetItemByID(int Id, int CompanyID)
        {
            QueuedEmail item = new QueuedEmail();
            var sqlParams = new SqlParameter[]
                    {
                            new SqlParameter("@Id", Id),
                            new SqlParameter("@CompanyID", CompanyID),
                    };
            using (var reader = SqlHelper.ExecuteReader("QueuedEmail_GetByID", sqlParams))
            {
                while (reader.Read())
                {
                    item = GetItemFromReader(reader);
                }
            }
            return item;


        }
        public static QueuedEmail AddItem(QueuedEmail model)
        {
            int result = 0;
            try
            {
                using (var reader = SqlHelper.ExecuteReader(CommandType.StoredProcedure, "QueuedEmail_Add", CreateSqlParameter(model)))
                {
                    while (reader.Read())
                    {
                        result = (int)reader[0];
                    }
                }
            }
            catch (Exception ObjEx)
            {

            }
            return GetItemByID(result, model.CompanyID);

        }
        public static QueuedEmail UpdateItem(QueuedEmail model)
        {
            String result = String.Empty;
            using (var reader = SqlHelper.ExecuteReader(CommandType.StoredProcedure, "QueuedEmail_Update", CreateSqlParameter(model)))
            {
                while (reader.Read())
                {
                    result = (String)reader[0];
                }
            }
            return GetItemByID(model.Id, model.CompanyID);

        }
        public static QueuedEmailCollection GetAllItem(int CompanyID)
        {
            QueuedEmailCollection collection = new QueuedEmailCollection();

            var sqlParams = new SqlParameter[]
                    {
                            new SqlParameter("@CompanyID", CompanyID),
                    };
            using (var reader = SqlHelper.ExecuteReader("QueuedEmail_GetAll", sqlParams))
            {
                while (reader.Read())
                {
                    QueuedEmail obj = new QueuedEmail();
                    obj = GetItemFromReader(reader);
                    collection.Add(obj);
                }
            }
            return collection;
        }

        public static QueuedEmailCollection Search(SearchFilter SearchKey)
        {
            QueuedEmailCollection collection = new QueuedEmailCollection();
            using (var reader = SqlHelper.ExecuteReader("QueuedEmail_Search", SearchFilterManager.SqlSearchParam(SearchKey)))
            {
                while (reader.Read())
                {
                    QueuedEmail obj = new QueuedEmail();
                    obj = GetItemFromReader(reader);
                    collection.Add(obj);
                }
            }
            return collection;
        }
        public static QueuedEmailCollection GetbyUser(string CreatedUser, int CompanyID)
        {
            QueuedEmailCollection collection = new QueuedEmailCollection();
            QueuedEmail obj;
            var sqlParams = new SqlParameter[]
              {
                            new SqlParameter("@CreatedUser", CreatedUser),
                            new SqlParameter("@CompanyID", CompanyID),
              };
            using (var reader = SqlHelper.ExecuteReader("QueuedEmail_GetAll_byUser", sqlParams))
            {
                while (reader.Read())
                {
                    obj = GetItemFromReader(reader);
                    collection.Add(obj);
                }
            }
            return collection;
        }

        private static SqlParameter[] CreateSqlParameter(QueuedEmail model)
        {
            return new SqlParameter[]
                {
                    new SqlParameter("@CompanyID", model.CompanyID),
                    new SqlParameter("@Id", model.Id),
                    new SqlParameter("@PriorityId", model.PriorityId),
                    new SqlParameter("@From", model.From),
                    new SqlParameter("@FromName", model.FromName),
                    new SqlParameter("@To", model.To),
                    new SqlParameter("@ToName", model.ToName),
                    new SqlParameter("@ReplyTo", model.ReplyTo),
                    new SqlParameter("@ReplyToName", model.ReplyToName),
                    new SqlParameter("@CC", model.CC),
                    new SqlParameter("@Bcc", model.Bcc),
                    new SqlParameter("@Subject", model.Subject),
                    new SqlParameter("@Body", model.Body),
                    new SqlParameter("@AttachmentFilePath", model.AttachmentFilePath),
                    new SqlParameter("@AttachmentFileName", model.AttachmentFileName),
                    new SqlParameter("@AttachedDownloadId", model.AttachedDownloadId),
                    new SqlParameter("@CreatedOnUtc", model.CreatedOnUtc),
                    new SqlParameter("@DontSendBeforeDateUtc", model.DontSendBeforeDateUtc),
                    new SqlParameter("@SentTries", model.SentTries),
                    new SqlParameter("@SentOnUtc", model.SentOnUtc),
                    new SqlParameter("@EmailAccountId", model.EmailAccountId),
                    new SqlParameter("@CreatedUser", model.CreatedUser),
                    new SqlParameter("@CreatedDate", model.CreatedDate),

                };
        }

        public static int DeleteItem(int itemID, int CompanyID)
        {
            return SqlHelper.ExecuteNonQuery("QueuedEmail_Delete", new SqlParameter[]
            {
                new SqlParameter(@"Id",itemID),
                    new SqlParameter("@CompanyID", CompanyID) });
        }
    }
}
