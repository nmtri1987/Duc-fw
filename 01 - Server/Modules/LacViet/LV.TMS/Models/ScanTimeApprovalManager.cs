namespace LV.TMS.Models
{
    using System;
    using System.Data;
    using System.Data.SqlClient;
    using DTP.Data;
    using System.Linq;
    using DTP.Core;

    public class ScanTimeApprovalCollection : BaseDBEntityCollection<ScanTimeApprovalReceiver> { }
    public class ScanTimeApprovalManager
    {
        private static ScanTimeApprovalReceiver GetItemFromReader(IDataReader dataReader)
        {
            ScanTimeApprovalReceiver objItem = new ScanTimeApprovalReceiver
            {
                EmployeeCode = SqlHelper.GetInt(dataReader, "EmployeeCode"),
                EmployeeNo = SqlHelper.GetString(dataReader, "EmployeeNo"),
                EmployeeName = SqlHelper.GetString(dataReader, "EmployeeName"),
                EmployeeNameVN = SqlHelper.GetString(dataReader, "EmployeeNameVN"),
                WorkDate = SqlHelper.GetDateTime(dataReader, "WorkDate"),
                RootIn = SqlHelper.GetString(dataReader, "RootIn"),
                RootOut = SqlHelper.GetString(dataReader, "RootOut"),
                ManualIn = SqlHelper.GetString(dataReader, "ManualIn"),
                ManualOut = SqlHelper.GetString(dataReader, "ManualOut"),
                ScanIn = SqlHelper.GetBoolean(dataReader, "ScanIn"),
                ScanOut = SqlHelper.GetBoolean(dataReader, "ScanOut"),
                ShiftCode = SqlHelper.GetString(dataReader, "ShiftCode"),
                Leave = SqlHelper.GetString(dataReader, "Leave"),
                OverTime = SqlHelper.GetString(dataReader, "OverTime"),
                IsApproved = SqlHelper.GetInt(dataReader, "IsApproved"),
                IsSubmited = SqlHelper.GetInt(dataReader, "IsSubmited"),
                IsRejected = SqlHelper.GetBoolean(dataReader, "IsRejected"),
                StatusName = SqlHelper.GetString(dataReader, "StatusName"),
                RequestorNote = SqlHelper.GetString(dataReader, "RequestorNote"),
                ApproverNote = SqlHelper.GetString(dataReader, "ApproverNote"),
                IsHoliday = SqlHelper.GetInt(dataReader, "IsHoliday"),
                IsWeekend = SqlHelper.GetBoolean(dataReader, "IsWeekend"),
                OverDue = SqlHelper.GetInt(dataReader, "OverDue"),
                RowNum = SqlHelper.GetInt(dataReader, "RowNum"),
                TotalRecord = SqlHelper.GetInt(dataReader, "TotalRecord")
            };


            if (SqlHelper.ColumnExists(dataReader, "TotalRecord"))
            {
                objItem.TotalRecord = SqlHelper.GetInt(dataReader, "TotalRecord");
            }
            return objItem;
        }

        public static ScanTimeApprovalReceiver GetItemById(int id)
        {
            ScanTimeApprovalReceiver item = new ScanTimeApprovalReceiver();
            var sqlParams = new SqlParameter[1];
            sqlParams[0] = new SqlParameter("@ID", id);
            using (var reader = SqlHelper.ExecuteReaderService(ModuleConfig.MyConnection, "USP_TMS_ApproveTimeSheet_GetByID", sqlParams))
            {
                while (reader.Read())
                {
                    item = GetItemFromReader(reader);
                }
            }
            return item;
        }

        /// <summary>
        /// Approve or Reject timesheet
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static int UpdateItems(TimesheetApproveReject model)
        {
            var result = 0;
            DataTable dt = new DataTable();
            dt= CommonHelper.ToDataTable(model.TimesheetEntryCollection, "TimeSheetEntry");
            dt.Columns.Remove("TotalRecord");
            var sqlParams = new SqlParameter[3];
            sqlParams[0] = new SqlParameter("@TimeSheetEntries", SqlDbType.Structured);
            sqlParams[0].Value = dt;
            //sqlParams[0]= new SqlParameter("@TimeSheetEntries", model);
            sqlParams[1] = new SqlParameter("@User", model.UserLoggedIn);
            sqlParams[2] = new SqlParameter("@IsApproved", model.IsApproved);

            using (var reader = SqlHelper.ExecuteReaderService(ModuleConfig.MyConnection, CommandType.StoredProcedure, "DNH_TimesheetApproval_Approve", sqlParams))
            {
                while (reader.Read())
                {
                    result = (int)reader[0];
                }
            }

            return result;
        }
        public static ScanTimeApprovalCollection GetAllItemByManager(ScanTimeApprovalSqlParameters SearchKey)
        {
            ScanTimeApprovalCollection collection = new ScanTimeApprovalCollection();

            var pars = new SqlParameter[]
            {
                new SqlParameter("@FromDate", SearchKey.FromDate),
                new SqlParameter("@ToDate", SearchKey.ToDate),
                new SqlParameter("@User", SearchKey.UserLoggedIn),
                new SqlParameter("@ShowWaiting", SearchKey.ShowWaiting),
                new SqlParameter("@StartRow", SearchKey.StartRow),
                new SqlParameter("@EndRow", SearchKey.EndRow),
                new SqlParameter("@OrderBy", SearchKey.OrderBy),
                new SqlParameter("@OrderDirection", SearchKey.OrderDirection),
                new SqlParameter("@FilterBy", SearchKey.FilterBy),
                new SqlParameter("@ShowUnNoReg", SearchKey.ShowUnNoReg)
            };

            using (var reader = SqlHelper.ExecuteReaderService(ModuleConfig.MyConnection, "DNH_TimesheetApproval_Get", pars))
            {
                while (reader.Read())
                {
                    ScanTimeApprovalReceiver obj = new ScanTimeApprovalReceiver();
                    obj = GetItemFromReader(reader);
                    obj.TimeIn = MergeTime(obj.RootIn, obj.ManualIn,"in");
                    obj.TimeOut = MergeTime(obj.RootOut, obj.ManualOut,"out");
                    collection.Add(obj);
                }
            }
            return collection;
        }
        public static string MergeTime(string sysDate, string manualdate,string type)
        {
            
            return string.Format("<b title='Manual {2}' style='color:#14f514'>m</b>: {0} </br> <b title='System {2}' style='color:#f8af00'>s</b>: {1}", manualdate, sysDate , type) ;
        }
        public static ScanTimeApprovalCollection Search(ScanTimeApprovalSqlParameters SearchKey)
        {
            ScanTimeApprovalCollection collection = new ScanTimeApprovalCollection();

            var pars = new SqlParameter[]
            {
                new SqlParameter("@FromDate", SearchKey.FromDate),
                new SqlParameter("@ToDate", SearchKey.ToDate),
                new SqlParameter("@User", SearchKey.UserLoggedIn),
                new SqlParameter("@ShowWaiting", SearchKey.ShowWaiting),
                new SqlParameter("@StartRow", SearchKey.StartRow),
                new SqlParameter("@EndRow", SearchKey.EndRow),
                new SqlParameter("@OrderBy", SearchKey.OrderBy),
                new SqlParameter("@OrderDirection", SearchKey.OrderDirection),
                new SqlParameter("@FilterBy", SearchKey.FilterBy),
                new SqlParameter("@ShowUnNoReg", SearchKey.ShowUnNoReg)
            };



            using (var reader = SqlHelper.ExecuteReaderService(ModuleConfig.MyConnection, "USP_TMS_ApproveTimeSheet_Get", pars))
            {
                while (reader.Read())
                {
                    ScanTimeApprovalReceiver obj = new ScanTimeApprovalReceiver();
                    obj = GetItemFromReader(reader);
                    collection.Add(obj);
                }
            }
            return collection;
        }

        public static int DeleteItem(Guid itemID)
        {
            return SqlHelper.ExecuteNonQuery("USP_TMS_ApproveTimeSheet_Delete", itemID);
        }
    }
}
