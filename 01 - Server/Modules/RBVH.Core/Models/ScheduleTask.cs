using System.Text;
using System.Data;
using System.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using DTP.Data;
namespace RBVH.Core
{
    [DataContract]
    public class ScheduleTask : BaseDBEntity
    {

        [DataMember]
        public int CompanyID { get; set; }

        [DataMember]
        public int Id { get; set; }

        [DataMember]
        public string Name { get; set; }

        [DataMember]
        public int Seconds { get; set; }

        [DataMember]
        public string Type { get; set; }

        [DataMember]
        public bool Enabled { get; set; }

        [DataMember]
        public bool StopOnError { get; set; }

        [DataMember]
        public string LeasedByMachineName { get; set; }

        [DataMember]
        public DateTime LeasedUntilUtc { get; set; }

        [DataMember]
        public DateTime LastStartUtc { get; set; }

        [DataMember]
        public DateTime LastEndUtc { get; set; }

        [DataMember]
        public DateTime LastSuccessUtc { get; set; }

        [DataMember]
        public DateTime CreatedDate { get; set; }

        [DataMember]
        public int CreatedUser { get; set; }

        [DataMember]
        public string ScreenID { get; set; }

    }
    public class ScheduleTaskCollection : BaseDBEntityCollection<ScheduleTask> { }
    public class ScheduleTaskManager
    {
        private static ScheduleTask GetItemFromReader(IDataReader dataReader)
        {
            ScheduleTask objItem = new ScheduleTask();

            objItem.CompanyID = SqlHelper.GetInt(dataReader, "CompanyID");

            objItem.Id = SqlHelper.GetInt(dataReader, "Id");

            objItem.Name = SqlHelper.GetString(dataReader, "Name");

            objItem.Seconds = SqlHelper.GetInt(dataReader, "Seconds");

            objItem.Type = SqlHelper.GetString(dataReader, "Type");

            objItem.Enabled = SqlHelper.GetBoolean(dataReader, "Enabled");

            objItem.StopOnError = SqlHelper.GetBoolean(dataReader, "StopOnError");

            objItem.LeasedByMachineName = SqlHelper.GetString(dataReader, "LeasedByMachineName");

            objItem.LeasedUntilUtc = SqlHelper.GetDateTime(dataReader, "LeasedUntilUtc");

            objItem.LastStartUtc = SqlHelper.GetDateTime(dataReader, "LastStartUtc");

            objItem.LastEndUtc = SqlHelper.GetDateTime(dataReader, "LastEndUtc");

            objItem.LastSuccessUtc = SqlHelper.GetDateTime(dataReader, "LastSuccessUtc");

            objItem.CreatedDate = SqlHelper.GetDateTime(dataReader, "CreatedDate");

            objItem.CreatedUser = SqlHelper.GetInt(dataReader, "CreatedUser");

            objItem.ScreenID = SqlHelper.GetString(dataReader, "ScreenID");

            if (SqlHelper.ColumnExists(dataReader, "TotalRecord"))
            {
                objItem.TotalRecord = SqlHelper.GetInt(dataReader, "TotalRecord");
            }

            return objItem;
        }
        public static ScheduleTask GetItemByID(int Id, int CompanyID)
        {
            ScheduleTask item = new ScheduleTask();
            var sqlParams = new SqlParameter[]
                    {
                            new SqlParameter("@Id", Id),
                            new SqlParameter("@CompanyID", CompanyID),
                    };
            using (var reader = SqlHelper.ExecuteReader("ScheduleTask_GetByID", sqlParams))
            {
                while (reader.Read())
                {
                    item = GetItemFromReader(reader);
                }
            }
            return item;


        }
        public static ScheduleTask AddItem(ScheduleTask model)
        {
            int result = 0;
            try
            {
                using (var reader = SqlHelper.ExecuteReader(CommandType.StoredProcedure, "ScheduleTask_Add", CreateSqlParameter(model)))
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
        public static ScheduleTask UpdateItem(ScheduleTask model)
        {
            String result = String.Empty;
            using (var reader = SqlHelper.ExecuteReader(CommandType.StoredProcedure, "ScheduleTask_Update", CreateSqlParameter(model)))
            {
                while (reader.Read())
                {
                    result = (String)reader[0];
                }
            }
            return GetItemByID(model.Id, model.CompanyID);

        }
        public static ScheduleTaskCollection GetAllItem(int CompanyID)
        {
            ScheduleTaskCollection collection = new ScheduleTaskCollection();

            var sqlParams = new SqlParameter[]
                    {
                            new SqlParameter("@CompanyID", CompanyID),
                    };
            using (var reader = SqlHelper.ExecuteReader("ScheduleTask_GetAll", sqlParams))
            {
                while (reader.Read())
                {
                    ScheduleTask obj = new ScheduleTask();
                    obj = GetItemFromReader(reader);
                    collection.Add(obj);
                }
            }
            return collection;
        }

        public static ScheduleTaskCollection Search(SearchFilter SearchKey)
        {
            ScheduleTaskCollection collection = new ScheduleTaskCollection();
            using (var reader = SqlHelper.ExecuteReader("ScheduleTask_Search", SearchFilterManager.SqlSearchParam(SearchKey)))
            {
                while (reader.Read())
                {
                    ScheduleTask obj = new ScheduleTask();
                    obj = GetItemFromReader(reader);
                    collection.Add(obj);
                }
            }
            return collection;
        }
        public static ScheduleTaskCollection GetbyUser(string CreatedUser, int CompanyID)
        {
            ScheduleTaskCollection collection = new ScheduleTaskCollection();
            ScheduleTask obj;
            var sqlParams = new SqlParameter[]
              {
                            new SqlParameter("@CreatedUser", CreatedUser),
                            new SqlParameter("@CompanyID", CompanyID),
              };
            using (var reader = SqlHelper.ExecuteReader("ScheduleTask_GetAll_byUser", sqlParams))
            {
                while (reader.Read())
                {
                    obj = GetItemFromReader(reader);
                    collection.Add(obj);
                }
            }
            return collection;
        }

        //public static ScheduleTask GetbyType(string Type, int CompanyID)
        //{
        //    ScheduleTaskCollection collection = new ScheduleTaskCollection();
        //    ScheduleTask obj;
        //    var sqlParams = new SqlParameter[]
        //      {
        //                    new SqlParameter("@Type", Type),
        //                    new SqlParameter("@CompanyID", CompanyID),
        //      };
        //    using (var reader = SqlHelper.ExecuteReader("ScheduleTask_GetAll_byType", sqlParams))
        //    {
        //        while (reader.Read())
        //        {
        //            obj = GetItemFromReader(reader);
        //            collection.Add(obj);
        //        }
        //    }
        //    return collection;
        //}
        public static ScheduleTask GetbyType(string Type, int CompanyID)
        {
            ScheduleTask item = new ScheduleTask();
            var sqlParams = new SqlParameter[]
                    {
                            new SqlParameter("@Type", Type),
                            new SqlParameter("@CompanyID", CompanyID),
                    };
            using (var reader = SqlHelper.ExecuteReader("ScheduleTask_GetAll_byType", sqlParams))
            {
                while (reader.Read())
                {
                    item = GetItemFromReader(reader);
                }
            }
            return item;


        }
        private static SqlParameter[] CreateSqlParameter(ScheduleTask model)
        {
            return new SqlParameter[]
                {
                    new SqlParameter("@CompanyID", model.CompanyID),
                    new SqlParameter("@Id", model.Id),
                    new SqlParameter("@Name", model.Name),
                    new SqlParameter("@Seconds", model.Seconds),
                    new SqlParameter("@Type", model.Type),
                    new SqlParameter("@Enabled", model.Enabled),
                    new SqlParameter("@StopOnError", model.StopOnError),
                    new SqlParameter("@LeasedByMachineName", model.LeasedByMachineName),
                    new SqlParameter("@LeasedUntilUtc", model.LeasedUntilUtc),
                    new SqlParameter("@LastStartUtc", model.LastStartUtc),
                    new SqlParameter("@LastEndUtc", model.LastEndUtc),
                    new SqlParameter("@LastSuccessUtc", model.LastSuccessUtc),
                    new SqlParameter("@CreatedDate", model.CreatedDate),
                    new SqlParameter("@CreatedUser", model.CreatedUser),
                    new SqlParameter("@ScreenID", model.ScreenID),

                };
        }

        public static int DeleteItem(int itemID, int CompanyID)
        {
            return SqlHelper.ExecuteNonQuery("ScheduleTask_Delete", new SqlParameter[]
            {
                new SqlParameter(@"Id",itemID),
                    new SqlParameter("@CompanyID", CompanyID) });
        }
    }
}