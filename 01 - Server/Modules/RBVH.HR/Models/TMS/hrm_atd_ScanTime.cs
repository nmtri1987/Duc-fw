using System.Text;
using System.Data;
using System.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using DTP.Data;
namespace LV.TMS.Models
{
    [DataContract]
    public class hrm_atd_ScanTime : BaseDBEntity
    {

        [DataMember]
        public string Scan_Time { get; set; }

        [DataMember]
        public string Bar_Code { get; set; }

        [DataMember]
        public string Reader_ID { get; set; }

        [DataMember]
        public bool Manual_Input { get; set; }

        [DataMember]
        public bool In1Out0 { get; set; }

        [DataMember]
        public DateTime Work_Date { get; set; }

        [DataMember]
        public bool Is_Over { get; set; }

        [DataMember]
        public int EmployeeID { get; set; }

        [DataMember]
        public bool RecordStatus { get; set; }

        [DataMember]
        public bool Is_OverF { get; set; }

        [DataMember]
        public Guid ID { get; set; }

        [DataMember]
        public int Shift_ID { get; set; }

        [DataMember]
        public bool valid { get; set; }

        [DataMember]
        public string RootScan { get; set; }

        [DataMember]
        public string ScanTimeIn_Edit { get; set; }

        [DataMember]
        public DateTime CreateDate { get; set; }

        [DataMember]
        public bool IsCopy { get; set; }

    }
    public class hrm_atd_ScanTimeCollection : BaseDBEntityCollection<hrm_atd_ScanTime> { }
    public class hrm_atd_ScanTimeManager
    {
        private static hrm_atd_ScanTime GetItemFromReader(IDataReader dataReader)
        {
            hrm_atd_ScanTime objItem = new hrm_atd_ScanTime();

            objItem.Scan_Time = SqlHelper.GetString(dataReader, "Scan_Time");

            objItem.Bar_Code = SqlHelper.GetString(dataReader, "Bar_Code");

            objItem.Reader_ID = SqlHelper.GetString(dataReader, "Reader_ID");

            objItem.Manual_Input = SqlHelper.GetBoolean(dataReader, "Manual_Input");

            objItem.In1Out0 = SqlHelper.GetBoolean(dataReader, "In1Out0");

            objItem.Work_Date = SqlHelper.GetDateTime(dataReader, "Work_Date");

            objItem.Is_Over = SqlHelper.GetBoolean(dataReader, "Is_Over");

            objItem.EmployeeID = SqlHelper.GetInt(dataReader, "EmployeeID");

            objItem.RecordStatus = SqlHelper.GetBoolean(dataReader, "RecordStatus");

            objItem.Is_OverF = SqlHelper.GetBoolean(dataReader, "Is_OverF");

            objItem.ID = SqlHelper.GetGuid(dataReader, "ID");

            objItem.Shift_ID = SqlHelper.GetInt(dataReader, "Shift_ID");

            objItem.valid = SqlHelper.GetBoolean(dataReader, "valid");

            objItem.RootScan = SqlHelper.GetString(dataReader, "RootScan");

            objItem.ScanTimeIn_Edit = SqlHelper.GetString(dataReader, "ScanTimeIn_Edit");

            objItem.CreateDate = SqlHelper.GetDateTime(dataReader, "CreateDate");

            objItem.IsCopy = SqlHelper.GetBoolean(dataReader, "IsCopy");

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
            using (var reader = SqlHelper.ExecuteReaderService(QTConfig.MyConnection, "hrm_atd_ScanTime_GetByID", sqlParams))
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
            using (var reader = SqlHelper.ExecuteReaderService(QTConfig.MyConnection, CommandType.StoredProcedure, "hrm_atd_ScanTime_Add", CreateSqlParameter(model)))
            {
                while (reader.Read())
                {
                    result = (Guid)reader[0];
                }
            }
            return GetItemByID(result);

        }
        public static hrm_atd_ScanTime UpdateItem(hrm_atd_ScanTime model)
        {
            Guid result = Guid.Empty;
            using (var reader = SqlHelper.ExecuteReaderService(QTConfig.MyConnection, CommandType.StoredProcedure, "hrm_atd_ScanTime_Update", CreateSqlParameter(model)))
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
            using (var reader = SqlHelper.ExecuteReaderService(QTConfig.MyConnection, "hrm_atd_ScanTime_GetAll", null))
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

        public static hrm_atd_ScanTimeCollection Search(SearchFilter SearchKey)
        {
            hrm_atd_ScanTimeCollection collection = new hrm_atd_ScanTimeCollection();
            using (var reader = SqlHelper.ExecuteReaderService(QTConfig.MyConnection, "hrm_atd_ScanTime_Search", SearchFilterManager.SqlSearchConditionNoCompany(SearchKey)))
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
            using (var reader = SqlHelper.ExecuteReaderService(QTConfig.MyConnection, "hrm_atd_ScanTime_GetAll_byUser", new SqlParameter("@CreatedUser", CreatedUser)))
            {
                while (reader.Read())
                {
                    obj = GetItemFromReader(reader);
                    collection.Add(obj);
                }
            }
            return collection;
        }

        private static SqlParameter[] CreateSqlParameter(hrm_atd_ScanTime model)
        {
            return new SqlParameter[]
                {
                    new SqlParameter("@Scan_Time", model.Scan_Time),
                    new SqlParameter("@Bar_Code", model.Bar_Code),
                    new SqlParameter("@Reader_ID", model.Reader_ID),
                    new SqlParameter("@Manual_Input", model.Manual_Input),
                    new SqlParameter("@In1Out0", model.In1Out0),
                    new SqlParameter("@Work_Date", model.Work_Date),
                    new SqlParameter("@Is_Over", model.Is_Over),
                    new SqlParameter("@EmployeeID", model.EmployeeID),
                    new SqlParameter("@RecordStatus", model.RecordStatus),
                    new SqlParameter("@Is_OverF", model.Is_OverF),
                    new SqlParameter("@ID", model.ID),
                    new SqlParameter("@Shift_ID", model.Shift_ID),
                    new SqlParameter("@valid", model.valid),
                    new SqlParameter("@RootScan", model.RootScan),
                    new SqlParameter("@ScanTimeIn_Edit", model.ScanTimeIn_Edit),
                    new SqlParameter("@CreateDate", model.CreateDate),
                    new SqlParameter("@IsCopy", model.IsCopy),

                };
        }

        public static int DeleteItem(Guid itemID)
        {
            return SqlHelper.ExecuteNonQuery("hrm_atd_ScanTime_Delete", itemID);
        }
    }
}