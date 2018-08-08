using System.Text;
using System.Data;
using System.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using DTP.Data;
namespace RBVH.HR.Models
{
    public class MissingScanTime : BaseDBEntity
    {
        [DataMember]
        public int EntityID { get; set; }
        [DataMember]
        public string EmployeeNo { get; set; }
        [DataMember]
        public int EmployeeCode { get; set; }
        [DataMember]
        public string FirstName_EN { get; set; }
        [DataMember]
        public string MiddleName_EN { get; set; }
        [DataMember]
        public string LastName_EN { get; set; }
        [DataMember]
        public DateTime Work_Date { get; set; }
        [DataMember]
        public string InTime { get; set; }
        [DataMember]
        public string OutTime { get; set; }

        [DataMember]
        public string ST_InTime { get; set; }


        [DataMember]
        public string ST_OutTime { get; set; }
        [DataMember]
        public int DirectManager { get; set; }
        [DataMember]
        public string DomainId { get; set; }
      
    }
    public class MissCanTimePara
    {
        public int EmployeeCode { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        public int EntityID { get; set; }
    }
    public class EMPTMSPara
    {
        public int EntityID { get; set; }
        public int DeptID { get; set; }
        public DateTime? FromDate { get; set; }
        public DateTime? ToDate { get; set; }
        public string OrderBy { get; set; }
        public string OrderDirection { get; set; }
        public int Page { get; set; }

        public int PageSize { get; set; }
            
    }
    public class MissingScanTimeCollection : BaseDBEntityCollection<MissingScanTime> { }

    public class MissingScanTimeManager
    {
        private static MissingScanTime BindScanTimeData(IDataReader dataReader)
        {
            MissingScanTime objItem = new MissingScanTime();
            objItem.EntityID = SqlHelper.GetInt(dataReader, "EntityID");
            objItem.EmployeeNo = SqlHelper.GetString(dataReader, "EmployeeNo");
            objItem.EmployeeCode = SqlHelper.GetInt(dataReader, "EmployeeCode");
            objItem.FirstName_EN = SqlHelper.GetString(dataReader, "FirstName_EN");
            objItem.MiddleName_EN = SqlHelper.GetString(dataReader, "MiddleName_EN");
            objItem.LastName_EN = SqlHelper.GetString(dataReader, "LastName_EN");
            objItem.Work_Date = SqlHelper.GetDateTime(dataReader, "Work_Date");
            objItem.ST_InTime = SqlHelper.GetString(dataReader, "ST_InTime");
            objItem.ST_OutTime = SqlHelper.GetString(dataReader, "ST_OutTime");
            objItem.InTime = SqlHelper.GetString(dataReader, "InTime");
            objItem.OutTime = SqlHelper.GetString(dataReader, "OutTime");
            objItem.DirectManager = SqlHelper.GetInt(dataReader, "DirectManager");
            objItem.DomainId = SqlHelper.GetString(dataReader, "DomainId");
            return objItem;
        }
        public static MissingScanTimeCollection GetMissingScanTime(MissCanTimePara objPara)
        {
            var pars = new SqlParameter[]
          {
                    new SqlParameter("@EmployeeCode",objPara.EmployeeCode),
                    new SqlParameter("@StartDate",objPara.StartDate),
                    new SqlParameter("@EndDate",objPara.EndDate),
                    new SqlParameter("@EntityID",objPara.EntityID),
          };

            MissingScanTimeCollection collection = new MissingScanTimeCollection();
            using (var reader = SqlHelper.ExecuteReaderService(ModuleConfig.MyConnection, "USP_TMS_NotFillScanTime_Report", pars))
            {
                MissingScanTime obj = new MissingScanTime();
                while (reader.Read())
                {
                    obj = BindScanTimeData(reader);
                    collection.Add(obj);
                }
            }
            return collection;
        }
        public static DataTable EmployeeTMSReport(EMPTMSPara value)
        {
            DataTable dt = new DataTable();
            //DataTable data = new DataTable();
            var pars = new SqlParameter[]
            {
                    new SqlParameter("@EntityID",value.EntityID),
                    new SqlParameter("@DeptID",value.DeptID),
                    new SqlParameter("@FromDate",value.FromDate),
                    new SqlParameter("@ToDate",value.ToDate),
                    new SqlParameter("@OrderBy", value.OrderBy),
                    new SqlParameter("@OrderDirection", value.OrderDirection),
                    new SqlParameter("@Page", value.Page),
                    new SqlParameter("@PageSize",value.PageSize),
                    //new SqlParameter("@CompanyID",value.CompanyID),
            };
            //@EntityID = 10002, @FromDate = '2017-05-16', @ToDate = '2017-06-14', @OrderBy = 'EmployeeNo', @OrderDirection = 'DESC'
            DataSet ds = SqlHelper.ExecuteDataset(QTConfig.MyConnection,  "USP_TMS_TimeSheet_Report_v1", 180, pars);
            if (ds.Tables.Count > 0)
            {
                dt = ds.Tables[0];
            }
            return dt;
        }

    }

}
