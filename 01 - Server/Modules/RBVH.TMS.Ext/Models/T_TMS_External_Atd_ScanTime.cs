using System.Text;
using System.Data;
using System.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using DTP.Data;
namespace RBVH.TMS.Ext.Models
{
    [DataContract]
    public class T_TMS_External_Atd_ScanTime : BaseDBEntity
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
        public bool RecordStatus { get; set; }

        [DataMember]
        public bool Is_OverF { get; set; }

        [DataMember]
        public Guid ID { get; set; }

        [DataMember]
        public int Shift_ID { get; set; }

        [DataMember]
        public bool IsValid { get; set; }

        [DataMember]
        public string RootScan { get; set; }

        [DataMember]
        public string ScanTimeIn_Edit { get; set; }

        [DataMember]
        public DateTime CreateDate { get; set; }

        [DataMember]
        public bool IsCopy { get; set; }

    }
    public class T_TMS_External_Atd_ScanTimeCollection : BaseDBEntityCollection<T_TMS_External_Atd_ScanTime> { }
    public class T_TMS_External_Atd_ScanTimeManager
    {
        private static T_TMS_External_Atd_ScanTime GetItemFromReader(IDataReader dataReader)
        {
            T_TMS_External_Atd_ScanTime objItem = new T_TMS_External_Atd_ScanTime();

            objItem.Scan_Time = SqlHelper.GetString(dataReader, "Scan_Time");

            objItem.Bar_Code = SqlHelper.GetString(dataReader, "Bar_Code");

            objItem.Reader_ID = SqlHelper.GetString(dataReader, "Reader_ID");

            objItem.Manual_Input = SqlHelper.GetBoolean(dataReader, "Manual_Input");

            objItem.In1Out0 = SqlHelper.GetBoolean(dataReader, "In1Out0");

            objItem.Work_Date = SqlHelper.GetDateTime(dataReader, "Work_Date");

            objItem.Is_Over = SqlHelper.GetBoolean(dataReader, "Is_Over");

            objItem.RecordStatus = SqlHelper.GetBoolean(dataReader, "RecordStatus");

            objItem.Is_OverF = SqlHelper.GetBoolean(dataReader, "Is_OverF");

            objItem.ID = SqlHelper.GetGuid(dataReader, "ID");

            objItem.Shift_ID = SqlHelper.GetInt(dataReader, "Shift_ID");

            objItem.IsValid = SqlHelper.GetBoolean(dataReader, "IsValid");

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
        private static ExternalDaily GetItem(IDataReader dataReader)
        {
            ExternalDaily objItem = new ExternalDaily();
            objItem.AssociateNo = SqlHelper.GetString(dataReader, "AssociateNo");
            objItem.AssociateName = SqlHelper.GetString(dataReader, "AssociateName");
            objItem.Work_Date = SqlHelper.GetDateTime(dataReader, "Work_Date");
            objItem.JoinDate = SqlHelper.GetDateTime(dataReader, "JoinDate");
            objItem.EndDate = SqlHelper.GetDateTime(dataReader, "EndDate");
            objItem.RootIn = SqlHelper.GetString(dataReader, "InTime");
            objItem.RootOut = SqlHelper.GetString(dataReader, "OutTime");
            objItem.WorkHour = SqlHelper.GetDecimal(dataReader, "WorkHour");
            return objItem;
        }

        private static External_Times GetMonthly(IDataReader dataReader)
        {
            External_Times objItem = new External_Times();
            objItem.AssociateNo = SqlHelper.GetString(dataReader, "AssociateNo");
            objItem.AssociateName = SqlHelper.GetString(dataReader, "AssociateName");
            objItem.JoinDate = SqlHelper.GetDateTime(dataReader, "JoinDate");
            objItem.EndDate = SqlHelper.GetDateTime(dataReader, "EndDate");

            objItem.Standard = SqlHelper.GetDecimal(dataReader, "Standard");
            objItem.Actual = SqlHelper.GetDecimal(dataReader, "Actual");

            objItem.IsActive = SqlHelper.GetBoolean(dataReader, "IsActive");
            objItem.NT_ID = SqlHelper.GetString(dataReader, "NT_ID");
            objItem.AccessCardNo = SqlHelper.GetInt(dataReader, "AccessCardNo");
            objItem.VendorName = SqlHelper.GetString(dataReader, "VendorName");
            objItem.Role = SqlHelper.GetString(dataReader, "Role");
            objItem.DeptName = SqlHelper.GetString(dataReader, "DeptName");

            objItem.DeptName = SqlHelper.GetString(dataReader, "DeptName");
            objItem.GroupName = SqlHelper.GetString(dataReader, "GroupName");
            objItem.ProjectCode = SqlHelper.GetString(dataReader, "ProjectCode");
            objItem.PONumber = SqlHelper.GetString(dataReader, "PONumber");

            objItem.DirectManager = SqlHelper.GetString(dataReader, "DirectManager");
            
            return objItem;
        }
        public static T_TMS_External_Atd_ScanTime GetItemByID(Guid ID)
        {
            T_TMS_External_Atd_ScanTime item = new T_TMS_External_Atd_ScanTime();
            var sqlParams = new SqlParameter[1];
            sqlParams[0] = new SqlParameter("@ID", ID);
            using (var reader = SqlHelper.ExecuteReaderService(ModuleConfig.MyConnection, "T_TMS_External_Atd_ScanTime_GetByID", sqlParams))
            {
                while (reader.Read())
                {
                    item = GetItemFromReader(reader);
                }
            }
            return item;


        }
        public static T_TMS_External_Atd_ScanTime AddItem(T_TMS_External_Atd_ScanTime model)
        {
            Guid result = Guid.Empty;
            using (var reader = SqlHelper.ExecuteReaderService(ModuleConfig.MyConnection, CommandType.StoredProcedure, "T_TMS_External_Atd_ScanTime_Add", CreateSqlParameter(model)))
            {
                while (reader.Read())
                {
                    result = (Guid)reader[0];
                }
            }
            return GetItemByID(result);

        }
        public static T_TMS_External_Atd_ScanTime UpdateItem(T_TMS_External_Atd_ScanTime model)
        {
            Guid result = Guid.Empty;
            using (var reader = SqlHelper.ExecuteReaderService(ModuleConfig.MyConnection, CommandType.StoredProcedure, "T_TMS_External_Atd_ScanTime_Update", CreateSqlParameter(model)))
            {
                while (reader.Read())
                {
                    result = (Guid)reader[0];
                }
            }
            return GetItemByID(result);

        }
        public static T_TMS_External_Atd_ScanTimeCollection GetAllItem()
        {
            T_TMS_External_Atd_ScanTimeCollection collection = new T_TMS_External_Atd_ScanTimeCollection();
            using (var reader = SqlHelper.ExecuteReaderService(ModuleConfig.MyConnection, "T_TMS_External_Atd_ScanTime_GetAll", null))
            {
                while (reader.Read())
                {
                    T_TMS_External_Atd_ScanTime obj = new T_TMS_External_Atd_ScanTime();
                    obj = GetItemFromReader(reader);
                    collection.Add(obj);
                }
            }
            return collection;
        }

        public static T_TMS_External_Atd_ScanTimeCollection Search(SearchFilter SearchKey)
        {
            T_TMS_External_Atd_ScanTimeCollection collection = new T_TMS_External_Atd_ScanTimeCollection();
            using (var reader = SqlHelper.ExecuteReaderService(ModuleConfig.MyConnection, "T_TMS_External_Atd_ScanTime_Search", SearchFilterManager.SqlSearchConditionNoCompany(SearchKey)))
            {
                while (reader.Read())
                {
                    T_TMS_External_Atd_ScanTime obj = new T_TMS_External_Atd_ScanTime();
                    obj = GetItemFromReader(reader);
                    collection.Add(obj);
                }
            }
            return collection;
        }



        public static External_TimesCollection GetAllItemByMonth(DateTime FromDate, DateTime ToDate, int UserID,string FilterBy, string FilterValue)
        {
            var pars = new SqlParameter[]
          {
                    new SqlParameter("@FromDate",FromDate),
                    new SqlParameter("@ToDate",ToDate),
                    new SqlParameter("@FilterBy",FilterBy),
                    new SqlParameter("@FilterValue",FilterValue),
                    new SqlParameter("@ManagerID",UserID),
                    new SqlParameter("@OrderBy","AssociateNo"),
                    new SqlParameter("@OrderDirection",1),
          };
            External_TimesCollection collection = new External_TimesCollection();
            using (var reader = SqlHelper.ExecuteReaderService(ModuleConfig.MyConnection, "USP_TMS_External_AssociatesTimeSheet_Get_MonthlyScantime_V1", pars))
            {
                //return CommonHelper.DataReaderToList<External_TimesCollection>(reader);
                External_Times obj = new External_Times();
                while (reader.Read())
                {
                    obj = GetMonthly(reader);
                    collection.Add(obj);
                }
            }
            return collection;
        }
        public static ExternalDailyCollection GetExDailyTMS(int ManagerID, string FD, string TD, int AID)
        {
            var pars = new SqlParameter[]
          {
                    new SqlParameter("@FromDate",FD),
                    new SqlParameter("@ToDate",TD),
                    new SqlParameter("@ManagerID",ManagerID),
                    new SqlParameter("@AssosiateID",AID),
          };
            ExternalDailyCollection collection = new ExternalDailyCollection();
            using (var reader = SqlHelper.ExecuteReaderService(ModuleConfig.MyConnection, "USP_TMS_Associate_GetByManager", pars))
            {
                ExternalDaily obj = new ExternalDaily();
                while (reader.Read())
                {
                    obj = GetItem(reader);
                    collection.Add(obj);
                }
            }
            return collection;
        }

        public static T_TMS_External_Atd_ScanTimeCollection GetbyUser(string CreatedUser)
        {
            T_TMS_External_Atd_ScanTimeCollection collection = new T_TMS_External_Atd_ScanTimeCollection();
            T_TMS_External_Atd_ScanTime obj;
            using (var reader = SqlHelper.ExecuteReaderService(ModuleConfig.MyConnection, "T_TMS_External_Atd_ScanTime_GetAll_byUser", new SqlParameter("@CreatedUser", CreatedUser)))
            {
                while (reader.Read())
                {
                    obj = GetItemFromReader(reader);
                    collection.Add(obj);
                }
            }
            return collection;
        }

        private static SqlParameter[] CreateSqlParameter(T_TMS_External_Atd_ScanTime model)
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
                    new SqlParameter("@RecordStatus", model.RecordStatus),
                    new SqlParameter("@Is_OverF", model.Is_OverF),
                    new SqlParameter("@ID", model.ID),
                    new SqlParameter("@Shift_ID", model.Shift_ID),
                    new SqlParameter("@IsValid", model.IsValid),
                    new SqlParameter("@RootScan", model.RootScan),
                    new SqlParameter("@ScanTimeIn_Edit", model.ScanTimeIn_Edit),
                    new SqlParameter("@CreateDate", model.CreateDate),
                    new SqlParameter("@IsCopy", model.IsCopy),

                };
        }

        public static int DeleteItem(Guid itemID)
        {
            return SqlHelper.ExecuteNonQuery("T_TMS_External_Atd_ScanTime_Delete", itemID);
        }
    }

}
