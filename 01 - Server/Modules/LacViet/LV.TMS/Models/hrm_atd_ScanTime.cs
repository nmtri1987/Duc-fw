using DTP.Data;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Runtime.Serialization;

namespace LV.TMS.Models
{
    [DataContract]
    public class hrm_atd_ScanTime : BaseDBEntity
    {
        //[DataMember]
        //public Guid RowID { get; set; }
        [DataMember]
        public int EmployeeCode { get; set; }

        [DataMember]
        public string EmployeeNo { get; set; }

        [DataMember]
        public string EmployeeName { set; get; }

        [DataMember]
        public DateTime WorkDate { set; get; }

        [DataMember]
        public string Leave_OT { set; get; }

        [DataMember]
        public string Raw_In { set; get; }

        [DataMember]
        public string Raw_Out { set; get; }

        [DataMember]
        public string Manual_In { set; get; }

        [DataMember]
        public string Manual_Out { set; get; }

        [DataMember]
        public string Hour { set; get; }

        [DataMember]
        public string Status { set; get; }

        [DataMember]
        public string Requestor_Note { get; set; }

        [DataMember]
        public string Approver_Note { get; set; }

        [DataMember]
        public Boolean Submit { get; set; }

        [DataMember]
        public bool isWeekend { get; set; }

        [DataMember]
        public bool IsHoliday { get; set; }
    }

    public class hrm_atd_ScanTimeCollection : BaseDBEntityCollection<hrm_atd_ScanTime> { }

    public class ScanTimeFilter
    {
        public string Keyword { get; set; }
        public string ColumnsName { get; set; }
        public string OrderBy { get; set; }
        public string OrderDirection { get; set; }
        public int Page { get; set; }
        public int PageSize { get; set; }
        public int CompanyID { get; set; }
        public int EmployeeCode { get; set; }
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }
        public string Condition { get; set; }
    }

    public class hrm_atd_ScanTimeManager
    {
        private static hrm_atd_ScanTime GetItemFromReader(IDataReader dataReader)
        {
            hrm_atd_ScanTime objItem = new hrm_atd_ScanTime();

            objItem.EmployeeCode = SqlHelper.GetInt(dataReader, "EmployeeCode");
            objItem.EmployeeNo = SqlHelper.GetString(dataReader, "EmployeeNo");

            //objItem.RowID = SqlHelper.GetGuid(dataReader, "RowID");

            objItem.EmployeeName = SqlHelper.GetString(dataReader, "EmployeeName");

            objItem.WorkDate = SqlHelper.GetDateTime(dataReader, "WorkDate");

            objItem.Leave_OT = SqlHelper.GetString(dataReader, "Leave_OT");

            objItem.Raw_In = SqlHelper.GetString(dataReader, "Raw_In");

            objItem.Raw_Out = SqlHelper.GetString(dataReader, "Raw_Out");

            objItem.Manual_In = SqlHelper.GetString(dataReader, "Manual_In");

            objItem.Manual_Out = SqlHelper.GetString(dataReader, "Manual_Out");

            objItem.Hour = SqlHelper.GetString(dataReader, "Hour");

            objItem.Status = SqlHelper.GetString(dataReader, "Status");

            objItem.Requestor_Note = SqlHelper.GetString(dataReader, "Requestor_Note");

            objItem.Approver_Note = SqlHelper.GetString(dataReader, "Approver_Note");

            objItem.Submit = SqlHelper.GetBoolean(dataReader, "Submit");
            objItem.isWeekend = SqlHelper.GetBoolean(dataReader, "IsWeekend");
            
            objItem.IsHoliday = SqlHelper.GetBoolean(dataReader, "IsHoliday");

            if (SqlHelper.ColumnExists(dataReader, "TotalRecord"))
            {
                objItem.TotalRecord = SqlHelper.GetInt(dataReader, "TotalRecord");
            }
            return objItem;
        }

        public static hrm_atd_ScanTime GetItemByID(Guid ID)
        {
            hrm_atd_ScanTime item = new hrm_atd_ScanTime();
            var sqlParams = new SqlParameter[1];
            sqlParams[0] = new SqlParameter("@ID", ID);
            using (var reader = SqlHelper.ExecuteReaderService(ModuleConfig.MyConnection, "hrm_atd_ScanTime_GetByID", sqlParams))
            {
                while (reader.Read())
                {
                    item = GetItemFromReader(reader);
                }
            }
            return item;
        }

        public static hrm_atd_ScanTime AddItem(hrm_atd_ScanTime model)
        {
            Guid result = Guid.Empty;
            using (var reader = SqlHelper.ExecuteReaderService(ModuleConfig.MyConnection, CommandType.StoredProcedure, "hrm_atd_ScanTime_Add", CreateSqlParameter(model)))
            {
                while (reader.Read())
                {
                    result = (Guid)reader[0];
                }
            }
            return GetItemByID(result);
        }

        public static hrm_atd_ScanTime Submit(hrm_atd_ScanTime model)
        {
            Guid result = Guid.Empty;
            using (var reader = SqlHelper.ExecuteReaderService(ModuleConfig.MyConnection, CommandType.StoredProcedure, "hrm_atd_ScanTime_PreSubmit", CreateSqlSubmitParameter(model)))
            {
                while (reader.Read())
                {
                   // result = (Guid)reader[0];
                }
            }
           return new hrm_atd_ScanTime();
        }

        public static hrm_atd_ScanTime UpdateItem(hrm_atd_ScanTime model)
        {
            Guid result = Guid.Empty;
            using (var reader = SqlHelper.ExecuteReaderService(ModuleConfig.MyConnection, CommandType.StoredProcedure, "hrm_atd_ScanTime_Update", CreateSqlParameter(model)))
            {
                while (reader.Read())
                {
                    result = (Guid)reader[0];
                }
            }
            return GetItemByID(result);
        }

        public static hrm_atd_ScanTimeCollection GetAllItem()
        {
            hrm_atd_ScanTimeCollection collection = new hrm_atd_ScanTimeCollection();
            using (var reader = SqlHelper.ExecuteReaderService(ModuleConfig.MyConnection, "hrm_atd_ScanTime_GetAll", null))
            {
                while (reader.Read())
                {
                    hrm_atd_ScanTime obj = new hrm_atd_ScanTime();
                    obj = GetItemFromReader(reader);
                    collection.Add(obj);
                }
            }
            return collection;
        }

        public static hrm_atd_ScanTimeCollection Search(ScanTimeFilter value)
        {
            hrm_atd_ScanTimeCollection collection = new hrm_atd_ScanTimeCollection();
            var pars = new SqlParameter[]
           {
                    new SqlParameter("@Keyword",value.Keyword),
                    new SqlParameter("@Columns",value.ColumnsName),
                    new SqlParameter("@OrderBy", value.OrderBy),
                    new SqlParameter("@OrderDirection", value.OrderDirection),
                    new SqlParameter("@Page", value.Page),
                    new SqlParameter("@PageSize",value.PageSize),
                    new SqlParameter("@CompanyID",value.CompanyID),
                    new SqlParameter("@EmployeeCode",value.EmployeeCode),
                    new SqlParameter("@FromDate",value.FromDate),
                    new SqlParameter("@ToDate",value.ToDate),
                    new SqlParameter("@Condition",value.Condition)
           };

            using (var reader = SqlHelper.ExecuteReaderService(ModuleConfig.MyConnection, "DNHTMS_Search", pars))
            {
                while (reader.Read())
                {
                    hrm_atd_ScanTime obj = new hrm_atd_ScanTime();
                    obj = GetItemFromReader(reader);
                    collection.Add(obj);
                }
            }
            return collection;
        }

        public static hrm_atd_ScanTimeCollection GetbyUser(string CreatedUser)
        {
            hrm_atd_ScanTimeCollection collection = new hrm_atd_ScanTimeCollection();
            hrm_atd_ScanTime obj;
            using (var reader = SqlHelper.ExecuteReaderService(ModuleConfig.MyConnection, "hrm_atd_ScanTime_GetAll_byUser", new SqlParameter("@CreatedUser", CreatedUser)))
            {
                while (reader.Read())
                {
                    obj = GetItemFromReader(reader);
                    collection.Add(obj);
                }
            }
            return collection;
        }
        private static SqlParameter[] CreateSqlSubmitParameter(hrm_atd_ScanTime model)
        {
            return new SqlParameter[]
                {
                    //new SqlParameter("@RowID", model.RowID),
                    new SqlParameter("@EmployeeCode", model.EmployeeCode),
                    new SqlParameter("@WorkDate", model.WorkDate),
                    new SqlParameter("@ManualIn", model.Manual_In),
                    new SqlParameter("@ManualOut", model.Manual_Out),
                    new SqlParameter("@RequestorNote", model.Requestor_Note),
                    new SqlParameter("@ApproverNote", model.Approver_Note),
                    new SqlParameter("@User", model.EmployeeCode),
                    new SqlParameter("@Submit", 1),
        };
        }
        private static SqlParameter[] CreateSqlParameter(hrm_atd_ScanTime model)
        {
            return new SqlParameter[]
                {
                    //new SqlParameter("@RowID", model.RowID),
                    new SqlParameter("@EmployeeNo", model.EmployeeNo),
                    new SqlParameter("@EmployeeName", model.EmployeeName),
                    new SqlParameter("@WorkDate", model.WorkDate),
                    new SqlParameter("@Leave_OT", model.Leave_OT),
                    new SqlParameter("@Raw_In", model.Raw_In),
                    new SqlParameter("@Raw_Out", model.Raw_Out),
                    new SqlParameter("@Manual_In", model.Manual_In),
                    new SqlParameter("@Manual_Out", model.Manual_Out),
                    new SqlParameter("@Hour", model.Hour),
                    new SqlParameter("@Status", model.Status),
                    new SqlParameter("@Requestor_Note", model.Requestor_Note),
                    new SqlParameter("@Approver_Note", model.Approver_Note),
                    new SqlParameter("@Submit", model.Submit),
        };
        }

        public static int DeleteItem(Guid itemID)
        {
            return SqlHelper.ExecuteNonQuery("hrm_atd_ScanTime_Delete", itemID);
        }
    }
}