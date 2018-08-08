using System.Text;
using System.Data;
using System.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using DTP.Data;

namespace RBVH.HR.Models
{
    public class REmployee : BaseDBEntity
    {
        [DataMember]
        public int EntityID { get; set; }

        [DataMember]
        public int EmployeeCode { get; set; }

        [DataMember]
        public string EmployeeNo { get; set; }

        [DataMember]
        public string EmployeeName_EN { get; set; }

        [DataMember]
        public string Email { get; set; }

        [DataMember]
        public int DirectManager { get; set; }

        [DataMember]
        public string DirectManagerNo { get; set; }

        [DataMember]
        public string DirectManagerName { get; set; }

        [DataMember]
        public int IndirectManager { get; set; }

        [DataMember]
        public string InDirectManagerNo { get; set; }

        [DataMember]
        public string InDirectManagerName { get; set; }

        [DataMember]
        public int GroupCode { get; set; }

        [DataMember]
        public string GroupName { get; set; }

        [DataMember]
        public int DeptCode { get; set; }

        [DataMember]
        public string DeptName { get; set; }

        [DataMember]
        public int PositionID { get; set; }

        [DataMember]
        public string PositionName { get; set; }

        [DataMember]
        public bool IsScan { get; set; }

        [DataMember]
        public string RangeDate { get; set; }
    }
    public class REmployeeCollections : BaseDBEntityCollection<REmployee> { }

    public class REmployeePara
    {
        public int EntityID { get; set; }
        public DateTime FromDate { get; set; }
        public int NumOfDay { get; set; }
    }
    public class REmployeeManager
    {
        private static REmployee GetItemFromReader(IDataReader dataReader)
        {
            REmployee objItem = new REmployee();
            objItem.EntityID = SqlHelper.GetInt(dataReader, "EntityID");

            objItem.EmployeeCode = SqlHelper.GetInt(dataReader, "EmployeeCode");
            
            objItem.EmployeeNo = SqlHelper.GetString(dataReader, "EmployeeNo");
            
            objItem.EmployeeName_EN = SqlHelper.GetString(dataReader, "EmployeeName_EN");
            
            objItem.Email = SqlHelper.GetString(dataReader, "Email");
            
            objItem.DirectManager = SqlHelper.GetInt(dataReader, "DirectManager");
            
            objItem.DirectManagerNo = SqlHelper.GetString(dataReader, "DirectManagerNo");
            
            objItem.DirectManagerName = SqlHelper.GetString(dataReader, "DirectManagerName");
            
            objItem.IndirectManager = SqlHelper.GetInt(dataReader, "IndirectManager");
            
            objItem.InDirectManagerNo = SqlHelper.GetString(dataReader, "InDirectManagerNo");
            
            objItem.InDirectManagerName = SqlHelper.GetString(dataReader, "InDirectManagerName");
            
            objItem.GroupCode = SqlHelper.GetInt(dataReader, "GroupCode");
            
            objItem.GroupName = SqlHelper.GetString(dataReader, "GroupName");
            
            objItem.DeptCode = SqlHelper.GetInt(dataReader, "DeptCode");
            
            objItem.DeptName = SqlHelper.GetString(dataReader, "DeptName");
            
            objItem.PositionID = SqlHelper.GetInt(dataReader, "PositionID");
            
            objItem.PositionName = SqlHelper.GetString(dataReader, "PositionName");
            
            objItem.IsScan = SqlHelper.GetBoolean(dataReader, "IsScan");
            
            objItem.RangeDate = SqlHelper.GetString(dataReader, "RangeDate");
            
            return objItem;
        }
        public static REmployeeCollections GetReminderList(REmployeePara objPara)
        {
            REmployeeCollections collection = new REmployeeCollections();
            
            using (var reader = SqlHelper.ExecuteReaderService(ModuleConfig.MyConnection, "USP_TMS_NotCheckINOUT_Get", new SqlParameter[]
                {
                    new SqlParameter("@EntityID", objPara.EntityID),
                    new SqlParameter("@FromDate", objPara.FromDate),
                    new SqlParameter("@NumOfDay", objPara.NumOfDay),
                }))
            {
                while (reader.Read())
                {
                    REmployee obj = new REmployee();
                    obj = GetItemFromReader(reader);
                    collection.Add(obj);
                }
            }
            return collection;
        }
    }
}
