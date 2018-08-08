using System.Text;
using System.Data;
using System.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using DTP.Data;
namespace BRVH.HR.OG
{
    [DataContract]
    public class TimeLog : BaseDBEntity
    {

        [DataMember]
        public int TimeLogId { get; set; }

        [DataMember]
        public string LAC { get; set; }

        [DataMember]
        public string ReaderType { get; set; }

        [DataMember]
        public string Door { get; set; }

        [DataMember]
        public string Name { get; set; }

        [DataMember]
        public string AssignID { get; set; }

        [DataMember]
        public string Department { get; set; }

        [DataMember]
        public string AccessType { get; set; }

        [DataMember]
        public string DateLog { get; set; }

        [DataMember]
        public string TimeLogs { get; set; }



    }
    public class TimeLogCollection : BaseDBEntityCollection<TimeLog> { }
    public class TimeLogManager
    {
        private static TimeLog GetItemFromReader(IDataReader dataReader)
        {
            TimeLog objItem = new TimeLog();

            objItem.TimeLogId = SqlHelper.GetInt(dataReader, "TimeLogId");

            objItem.LAC = SqlHelper.GetString(dataReader, "LAC");

            objItem.ReaderType = SqlHelper.GetString(dataReader, "ReaderType");

            objItem.Door = SqlHelper.GetString(dataReader, "Door");

            objItem.Name = SqlHelper.GetString(dataReader, "Name");

            objItem.AssignID = SqlHelper.GetString(dataReader, "AssignID");

            objItem.Department = SqlHelper.GetString(dataReader, "Department");

            objItem.AccessType = SqlHelper.GetString(dataReader, "AccessType");

            objItem.DateLog = SqlHelper.GetString(dataReader, "DateLog");

            objItem.TimeLogs = SqlHelper.GetString(dataReader, "TimeLogs");



            if (SqlHelper.ColumnExists(dataReader, "TotalRecord"))
            {
                objItem.TotalRecord = SqlHelper.GetInt(dataReader, "TotalRecord");
            }
            return objItem;
        }
        public static TimeLog GetItemByID(int TimeLogId)
        {
            TimeLog item = new TimeLog();
            var sqlParams = new SqlParameter[1];
            sqlParams[0] = new SqlParameter("@TimeLogId", TimeLogId);
            using (var reader = SqlHelper.ExecuteReader("TimeLog_GetByID", sqlParams))
            {
                while (reader.Read())
                {
                    item = GetItemFromReader(reader);
                }
            }
            return item;


        }
        public static TimeLog AddItem(TimeLog model)
        {
            int result = 0;
            using (var reader = SqlHelper.ExecuteReader(CommandType.StoredProcedure, "TimeLog_Add", CreateSqlParameter(model)))
            {
                while (reader.Read())
                {
                    result = (int)reader[0];
                }
            }
            return GetItemByID(result);

        }
        public static TimeLog UpdateItem(TimeLog model)
        {
            int result = 0;
            using (var reader = SqlHelper.ExecuteReader(CommandType.StoredProcedure, "TimeLog_Update", CreateSqlParameter(model)))
            {
                while (reader.Read())
                {
                    result = (int)reader[0];
                }
            }
            return GetItemByID(result);

        }
        //public static TimeLogCollection GetAllItem()
        //{
        //    TimeLogCollection collection = new TimeLogCollection();
        //    using (var reader = SqlHelper.ExecuteReader("TimeLog_GetAll", null))
        //    {
        //        while (reader.Read())
        //        {
        //            TimeLog obj = new TimeLog();
        //            obj = GetItemFromReader(reader);
        //            collection.Add(obj);
        //        }
        //    }
        //    return collection;
        //}

        public static TimeLogCollection Search(SearchFilter SearchKey)
        {
            TimeLogCollection collection = new TimeLogCollection();
            using (var reader = SqlHelper.ExecuteReader("TimeLog_Search", SearchFilterManager.SqlSearchParamNoCompany(SearchKey)))
            {
                while (reader.Read())
                {
                    TimeLog obj = new TimeLog();
                    obj = GetItemFromReader(reader);
                    collection.Add(obj);
                }
            }
            return collection;
        }
        public static TimeLogCollection GetbyUser(string CreatedUser)
        {
            TimeLogCollection collection = new TimeLogCollection();
            TimeLog obj;
            using (var reader = SqlHelper.ExecuteReader("TimeLog_GetAll_byUser", new SqlParameter("@CreatedUser", CreatedUser)))
            {
                while (reader.Read())
                {
                    obj = GetItemFromReader(reader);
                    collection.Add(obj);
                }
            }
            return collection;
        }

        private static SqlParameter[] CreateSqlParameter(TimeLog model)
        {
            return new SqlParameter[]
                {
                    new SqlParameter("@TimeLogId", model.TimeLogId),
                    new SqlParameter("@LAC", model.LAC),
                    new SqlParameter("@ReaderType", model.ReaderType),
                    new SqlParameter("@Door", model.Door),
                    new SqlParameter("@Name", model.Name),
                    new SqlParameter("@AssignID", model.AssignID),
                    new SqlParameter("@Department", model.Department),
                    new SqlParameter("@AccessType", model.AccessType),
                    new SqlParameter("@DateLog", model.DateLog),
                    new SqlParameter("@TimeLogs", model.TimeLogs),

                };
        }

        public static int DeleteItem(int itemID)
        {
            return SqlHelper.ExecuteNonQuery("TimeLog_Delete", itemID);
        }
    }
}