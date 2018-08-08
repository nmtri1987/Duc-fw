namespace LV.TMS.Models
{
    using System;
    using System.Data;
    using System.Data.SqlClient;
    using DTP.Data;
    public class hrm_atd_ScanTimeApprovalCollection : BaseDBEntityCollection<hrm_atd_ScanTimeApproval> { }
    public class hrm_atd_ScanTimeApprovalManager
    {
        private static hrm_atd_ScanTimeApproval GetItemFromReader(IDataReader dataReader)
        {
            hrm_atd_ScanTimeApproval objItem = new hrm_atd_ScanTimeApproval
            {
                ID = SqlHelper.GetInt(dataReader, "ID"),
                EmployeeID = SqlHelper.GetInt(dataReader, "EmployeeID"),
                Work_Date = SqlHelper.GetDateTime(dataReader, "Work_Date"),
                User_Level = SqlHelper.GetInt(dataReader, "UserLevel"),
                Approve_Status = SqlHelper.GetInt(dataReader, "ApproveStatus"),
                Submit = SqlHelper.GetBoolean(dataReader, "Submit"),
                Approve_Late_Early = SqlHelper.GetInt(dataReader, "ApproveLateEarly"),
                User_Group_ID = SqlHelper.GetInt(dataReader, "UserGroupID"),
                Approval_Date = SqlHelper.GetDateTime(dataReader, "ApprovalDate"),
                Reject_In_Out = SqlHelper.GetBoolean(dataReader, "RejectInOut"),
                Reject_Late_Early = SqlHelper.GetBoolean(dataReader, "RejectLateEarly"),
                User_Group_ID_Level1 = SqlHelper.GetInt(dataReader, "UserGroupID_Level1"),
                Approval_OT = SqlHelper.GetBoolean(dataReader, "ApprovalOT"),
                Accept_OT = SqlHelper.GetBoolean(dataReader, "AcceptOT"),
                Note_Level_1 = SqlHelper.GetString(dataReader, "NoteLevel1"),
                Note_Level_2 = SqlHelper.GetString(dataReader, "NoteLevel2"),
                Is_Auto_Cal_Kow = SqlHelper.GetInt(dataReader, "IsAutoCalKow")
            };


            if (SqlHelper.ColumnExists(dataReader, "TotalRecord"))
            {
                objItem.TotalRecord = SqlHelper.GetInt(dataReader, "TotalRecord");
            }
            return objItem;
        }

        public static hrm_atd_ScanTimeApproval GetItemById(int id)
        {
            hrm_atd_ScanTimeApproval item = new hrm_atd_ScanTimeApproval();
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

        public static hrm_atd_ScanTimeApproval UpdateItem(hrm_atd_ScanTimeApproval model)
        {
            int result = 0;
            using (var reader = SqlHelper.ExecuteReaderService(ModuleConfig.MyConnection, CommandType.StoredProcedure, "USP_TMS_ApproveTimeSheet_Update", CreateSqlParameter(model)))
            {
                while (reader.Read())
                {
                    result = (int)reader[0];
                }
            }
            return GetItemById(result);

        }
        public static hrm_atd_ScanTimeApprovalCollection GetAllItem(ScanTimeApprovalSqlParameters value)
        {
            hrm_atd_ScanTimeApprovalCollection collection = new hrm_atd_ScanTimeApprovalCollection();
            using (var reader = SqlHelper.ExecuteReaderService(ModuleConfig.MyConnection, "USP_TMS_ApproveTimeSheet_Get", value))
            {
                while (reader.Read())
                {
                    hrm_atd_ScanTimeApproval obj = new hrm_atd_ScanTimeApproval();
                    obj = GetItemFromReader(reader);
                    collection.Add(obj);
                }
            }
            return collection;
        }

        public static hrm_atd_ScanTimeApprovalCollection Search(ScanTimeApprovalSqlParameters SearchKey)
        {
            hrm_atd_ScanTimeApprovalCollection collection = new hrm_atd_ScanTimeApprovalCollection();

            var pars = new SqlParameter[]
            {
                new SqlParameter("@FromDate", SearchKey.FromDate),
                new SqlParameter("@ToDate", SearchKey.ToDate),
                new SqlParameter("@UserLoggedIn", SearchKey.UserLoggedIn),
                new SqlParameter("@ShowWaiting", SearchKey.ShowWaiting),
                new SqlParameter("@StartRow", SearchKey.StartRow),
                new SqlParameter("@EndRow", SearchKey.EndRow),
                new SqlParameter("@OrderBy", SearchKey.OrderBy),
                new SqlParameter("@OrderDirection", SearchKey.OrderDirection),
                new SqlParameter("@FilterBy", SearchKey.FilterBy),
                new SqlParameter("@ShowUnNoReg", SearchKey.ShowUnNoReg),
                new SqlParameter("@ShowUnNoReg", SearchKey.ShowUnNoReg),
                new SqlParameter("@Keyword", SearchKey.Keyword),
                new SqlParameter("@ColumnsName", SearchKey.ColumnsName),
                new SqlParameter("@Page", SearchKey.Page),
                new SqlParameter("@PageSize", SearchKey.PageSize),
                new SqlParameter("@Condition", SearchKey.Condition)
            };



            using (var reader = SqlHelper.ExecuteReaderService(ModuleConfig.MyConnection, "USP_TMS_ApproveTimeSheet_Get", pars))
            {
                while (reader.Read())
                {
                    hrm_atd_ScanTimeApproval obj = new hrm_atd_ScanTimeApproval();
                    obj = GetItemFromReader(reader);
                    collection.Add(obj);
                }
            }
            return collection;
        }

        private static SqlParameter[] CreateSqlParameter(hrm_atd_ScanTimeApproval model)
        {
            return new SqlParameter[]
                {
                    new SqlParameter("@ID", model.ID),
                    new SqlParameter("@EmployeeID", model.EmployeeID),
                    new SqlParameter("@Work_Date", model.Work_Date),
                    new SqlParameter("@UserLevel", model.User_Level),
                    new SqlParameter("@ApproveStatus", model.Approve_Status),
                    new SqlParameter("@Submit", model.Submit),
                    new SqlParameter("@ApproveLateEarly", model.Approve_Late_Early),
                    new SqlParameter("@UserGroupID", model.User_Group_ID),
                    new SqlParameter("@ApprovalDate", model.Approval_Date),
                    new SqlParameter("@RejectInOut", model.Reject_In_Out),
                    new SqlParameter("@RejectLateEarly", model.Reject_Late_Early),
                    new SqlParameter("@UserGroupID_Level1", model.User_Group_ID_Level1),
                    new SqlParameter("@ApprovalOT", model.Approval_OT),
                    new SqlParameter("@AcceptOT", model.Accept_OT),
                    new SqlParameter("@NoteLevel1", model.Note_Level_1),
                    new SqlParameter("@NoteLevel2", model.Note_Level_2),
                    new SqlParameter("@IsAutoCalKow", model.Is_Auto_Cal_Kow),

                };
        }

        public static int DeleteItem(Guid itemID)
        {
            return SqlHelper.ExecuteNonQuery("USP_TMS_ApproveTimeSheet_Delete", itemID);
        }
    }
}
