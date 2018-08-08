using System.Text;
using System.Data;
using System.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using DTP.Data;
namespace RBVH.HR.Models
{
    [DataContract]
    public class TMSMissingScanTime : BaseDBEntity
    {
        public int EntityID { get; set; }
        public string EmployeeNo { get; set; }
        public int EmployeeCode { get; set; }
        public string FirstName_EN { get; set; }
        public string MiddleName_EN { get; set; }
        public string LastName_EN { get; set; }
        public DateTime Work_Date { get; set; }
        public DateTime InTime { get; set; }
        public DateTime OutTime { get; set; }
        public DateTime ST_InTime { get; set; }
        public DateTime ST_OutTime { get; set; }
        public int DirectManager { get; set; }
        public string DomainId { get; set; }

    }
    public class TMSMissingScanTimeCollection : BaseDBEntityCollection<TMSMissingScanTime> { }
    public class TMSMissingScanTimeManager
    {
        private static TMSMissingScanTime GetItemFromReader(IDataReader dataReader)
        {
            TMSMissingScanTime objItem = new TMSMissingScanTime();

            objItem.EntityID = SqlHelper.GetInt(dataReader, "EntityID");

            objItem.EmployeeNo = SqlHelper.GetString(dataReader, "EmployeeNo");

            objItem.EmployeeCode = SqlHelper.GetInt(dataReader, "EmployeeCode");

            objItem.FirstName_EN = SqlHelper.GetString(dataReader, "FirstName_EN");

            objItem.MiddleName_EN = SqlHelper.GetString(dataReader, "MiddleName_EN");

            objItem.LastName_EN = SqlHelper.GetString(dataReader, "LastName_EN");

            objItem.Work_Date = SqlHelper.GetDateTime(dataReader, "Work_Date");

            objItem.InTime = SqlHelper.GetDateTime(dataReader, "InTime");

            objItem.OutTime = SqlHelper.GetDateTime(dataReader, "OutTime");

            objItem.ST_InTime = SqlHelper.GetDateTime(dataReader, "ST_InTime");

            objItem.ST_OutTime = SqlHelper.GetDateTime(dataReader, "ST_OutTime");

            objItem.DirectManager = SqlHelper.GetInt(dataReader, "DirectManager");

            objItem.DomainId = SqlHelper.GetString(dataReader, "DomainId");

            if (SqlHelper.ColumnExists(dataReader, "TotalRecord"))
            {
                objItem.TotalRecord = SqlHelper.GetInt(dataReader, "TotalRecord");
            }
            return objItem;
        }

        public static TMSMissingScanTimeCollection GetMissingData(int EmployeeCode, DateTime StartDate, DateTime EndDate,int EntityID)
        {
            TMSMissingScanTimeCollection collection = new TMSMissingScanTimeCollection();
            var pars = new SqlParameter[]
            {
                        new SqlParameter("@EmployeeCode",EmployeeCode),
                        new SqlParameter("@StartDate",StartDate),
                        new SqlParameter("@EndDate",EndDate),
                        new SqlParameter("@EntityID",EntityID),
                        
            };
            using (var reader = SqlHelper.ExecuteReaderService(ModuleConfig.MyConnection, "USP_TMS_NotFillScanTime_Report", pars))
            {
                TMSMissingScanTime obj;
                while (reader.Read())
                {
                    obj = GetItemFromReader(reader);
                    collection.Add(obj);
                }
            }
            return collection;
        }
       
        //public static TMSMissingScanTimeCollection Search(SearchFilter SearchKey)
        //{
        //    TMSMissingScanTimeCollection collection = new TMSMissingScanTimeCollection();
        //    using (var reader = SqlHelper.ExecuteReaderService(ModuleConfig.MyConnection, "TMSMissingScanTime_Search", SearchFilterManager.SqlSearchParamNoCompany(SearchKey)))
        //    {
        //        while (reader.Read())
        //        {
        //            TMSMissingScanTime obj = new TMSMissingScanTime();
        //            obj = GetItemFromReader(reader);
        //            collection.Add(obj);
        //        }
        //    }
        //    return collection;
        //}
        public static TMSMissingScanTimeCollection GetbyUser(string CreatedUser)
        {
            TMSMissingScanTimeCollection collection = new TMSMissingScanTimeCollection();
            TMSMissingScanTime obj;
            using (var reader = SqlHelper.ExecuteReaderService(ModuleConfig.MyConnection, "TMSMissingScanTime_GetAll_byUser", new SqlParameter("@CreatedUser", CreatedUser)))
            {
                while (reader.Read())
                {
                    obj = GetItemFromReader(reader);
                    collection.Add(obj);
                }
            }
            return collection;
        }

        private static SqlParameter[] CreateSqlParameter(TMSMissingScanTime model)
        {
            return new SqlParameter[]
                {
                    new SqlParameter("@EntityID", model.EntityID),
                    new SqlParameter("@EmployeeNo", model.EmployeeNo),
                    new SqlParameter("@EmployeeCode", model.EmployeeCode),
                    new SqlParameter("@FirstName_EN", model.FirstName_EN),
                    new SqlParameter("@MiddleName_EN", model.MiddleName_EN),
                    new SqlParameter("@LastName_EN", model.LastName_EN),
                    new SqlParameter("@Work_Date", model.Work_Date),
                    new SqlParameter("@InTime", model.InTime),
                    new SqlParameter("@OutTime", model.OutTime),
                    new SqlParameter("@ST_InTime", model.ST_InTime),
                    new SqlParameter("@ST_OutTime", model.ST_OutTime),
                    new SqlParameter("@DirectManager", model.DirectManager),
                    new SqlParameter("@DomainId", model.DomainId),
                };
        }

        public static int DeleteItem(int itemID)
        {
            return SqlHelper.ExecuteNonQuery("TMSMissingScanTime_Delete", itemID);
        }
    }
}