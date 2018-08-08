using System.Text;
using System.Data;
using System.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using DTP.Data;
namespace Biz.TMS.Models
{
    [DataContract]
    public class T_LMS_Trans_LeaveStory : BaseDBEntity
    {

        [DataMember]
        public int Id { get; set; }

        [DataMember]
        public int EmployeeCode { get; set; }

        [DataMember]
        public int LeaveTypeId { get; set; }

        [DataMember]
        public DateTime StartDate { get; set; }

        [DataMember]
        public DateTime EndDate { get; set; }

        [DataMember]
        public DateTime FromTime { get; set; }

        [DataMember]
        public DateTime ToTime { get; set; }

        [DataMember]
        public decimal NoOfDays { get; set; }

        [DataMember]
        public decimal NoOfHours { get; set; }

        [DataMember]
        public int HalfDayPart { get; set; }

        [DataMember]
        public string Remarks { get; set; }

        [DataMember]
        public string ApproverComments { get; set; }

        [DataMember]
        public int StatusId { get; set; }

        [DataMember]
        public int ApprovalLevel { get; set; }

        [DataMember]
        public bool IsUpdatedByAdmin { get; set; }

        [DataMember]
        public bool IsOverriddenLeave { get; set; }

        [DataMember]
        public int OverriddenLeaveId { get; set; }

        [DataMember]
        public bool IsNC { get; set; }

        [DataMember]
        public int OldStatusId { get; set; }

        [DataMember]
        public int OldApprovalLevelId { get; set; }

        [DataMember]
        public bool IsAcitve { get; set; }

        [DataMember]
        public int CreatedBy { get; set; }

        [DataMember]
        public DateTime CreatedDate { get; set; }

        [DataMember]
        public int ModifiedBy { get; set; }

        [DataMember]
        public DateTime ModifiedDate { get; set; }

        [DataMember]
        public int Ref_RequestId { get; set; }

        [DataMember]
        public int UploadStatus { get; set; }

        [DataMember]
        public DateTime LastUploadDate { get; set; }

        [DataMember]
        public DateTime LastUploadedToDate { get; set; }

        [DataMember]
        public string NextLevelId { get; set; }

        [DataMember]
        public string NextApproverRoleId { get; set; }

        [DataMember]
        public DateTime CancelRequestCreatedOn { get; set; }


    }
    public class LeaveWF : BaseDBEntity
    {
        [DataMember]
        public int ID { get; set; }
        [DataMember]
        public int EmployeeCode { get; set; }
        [DataMember]
        public string EmployeeNo { get; set; }
        [DataMember]
        public string EmployeeName_EN { get; set; }
        [DataMember]

        public DateTime StartDate { get; set; }
        [DataMember]
        public DateTime EndDate { get; set; }
        [DataMember]
        public string FullName_EN { get; set; }
        [DataMember]
        public string StatusName { get; set; }
        [DataMember]
        public int ApprovalLevel { get; set; }
        [DataMember]
        public int DirectManagerCode { get; set; }
        [DataMember]
        public string DirectManagerNo { get; set; }
        [DataMember]
        public string DirectManagerName { get; set; }
        [DataMember]
        public int InDirectManagerCode { get; set; }
        [DataMember]
        public string InDirectManagerNo { get; set; }
        [DataMember]
        public string InDirectManagerName { get; set; }
        [DataMember]
        public string RoleFullName { get; set; }
        [DataMember]
        public string Description { get; set; }
        [DataMember]
        public DateTime CreatedDate { get; set; }
    }
    public class LeaveWFPara{
        public int EntityID { get; set; }
        public int EmployeeCode { get; set; }
        public DateTime WorkDate { get; set; }
    }
    public class LeaveWFCollection : BaseDBEntityCollection<LeaveWF> { }
    public class T_LMS_Trans_LeaveStoryCollection : BaseDBEntityCollection<T_LMS_Trans_LeaveStory> { }
    public class T_LMS_Trans_LeaveStoryManager
    {
        private static T_LMS_Trans_LeaveStory GetItemFromReader(IDataReader dataReader)
        {
            T_LMS_Trans_LeaveStory objItem = new T_LMS_Trans_LeaveStory();

            objItem.Id = SqlHelper.GetInt(dataReader, "Id");

            objItem.EmployeeCode = SqlHelper.GetInt(dataReader, "EmployeeCode");

            objItem.LeaveTypeId = SqlHelper.GetInt(dataReader, "LeaveTypeId");

            objItem.StartDate = SqlHelper.GetDateTime(dataReader, "StartDate");

            objItem.EndDate = SqlHelper.GetDateTime(dataReader, "EndDate");

            objItem.FromTime = SqlHelper.GetDateTime(dataReader, "FromTime");

            objItem.ToTime = SqlHelper.GetDateTime(dataReader, "ToTime");

            objItem.NoOfDays = SqlHelper.GetDecimal(dataReader, "NoOfDays");

            objItem.NoOfHours = SqlHelper.GetDecimal(dataReader, "NoOfHours");

            objItem.HalfDayPart = SqlHelper.GetInt(dataReader, "HalfDayPart");

            objItem.Remarks = SqlHelper.GetString(dataReader, "Remarks");

            objItem.ApproverComments = SqlHelper.GetString(dataReader, "ApproverComments");

            objItem.StatusId = SqlHelper.GetInt(dataReader, "StatusId");

            objItem.ApprovalLevel = SqlHelper.GetInt(dataReader, "ApprovalLevel");

            objItem.IsUpdatedByAdmin = SqlHelper.GetBoolean(dataReader, "IsUpdatedByAdmin");

            objItem.IsOverriddenLeave = SqlHelper.GetBoolean(dataReader, "IsOverriddenLeave");

            objItem.OverriddenLeaveId = SqlHelper.GetInt(dataReader, "OverriddenLeaveId");

            objItem.IsNC = SqlHelper.GetBoolean(dataReader, "IsNC");

            objItem.OldStatusId = SqlHelper.GetInt(dataReader, "OldStatusId");

            objItem.OldApprovalLevelId = SqlHelper.GetInt(dataReader, "OldApprovalLevelId");

            objItem.IsAcitve = SqlHelper.GetBoolean(dataReader, "IsAcitve");

            objItem.CreatedBy = SqlHelper.GetInt(dataReader, "CreatedBy");

            objItem.CreatedDate = SqlHelper.GetDateTime(dataReader, "CreatedDate");

            objItem.ModifiedBy = SqlHelper.GetInt(dataReader, "ModifiedBy");

            objItem.ModifiedDate = SqlHelper.GetDateTime(dataReader, "ModifiedDate");

            objItem.Ref_RequestId = SqlHelper.GetInt(dataReader, "Ref_RequestId");

            objItem.UploadStatus = SqlHelper.GetInt(dataReader, "UploadStatus");

            objItem.LastUploadDate = SqlHelper.GetDateTime(dataReader, "LastUploadDate");

            objItem.LastUploadedToDate = SqlHelper.GetDateTime(dataReader, "LastUploadedToDate");

            objItem.NextLevelId = SqlHelper.GetString(dataReader, "NextLevelId");

            objItem.NextApproverRoleId = SqlHelper.GetString(dataReader, "NextApproverRoleId");

            objItem.CancelRequestCreatedOn = SqlHelper.GetDateTime(dataReader, "CancelRequestCreatedOn");



            if (SqlHelper.ColumnExists(dataReader, "TotalRecord"))
            {
                objItem.TotalRecord = SqlHelper.GetInt(dataReader, "TotalRecord");
            }
            return objItem;
        }
        private static LeaveWF GetItem(IDataReader dataReader)
        {
            LeaveWF objItem = new LeaveWF();
            objItem.ID = SqlHelper.GetInt(dataReader, "ID");
            objItem.EmployeeCode = SqlHelper.GetInt(dataReader, "EmployeeCode");
            objItem.EmployeeNo= SqlHelper.GetString(dataReader, "EmployeeNo");
            objItem.EmployeeName_EN = SqlHelper.GetString(dataReader, "EmployeeName_EN");
            objItem.CreatedDate = SqlHelper.GetDateTime(dataReader, "CreatedDate");
            objItem.StartDate = SqlHelper.GetDateTime(dataReader, "StartDate");
            objItem.EndDate = SqlHelper.GetDateTime(dataReader, "EndDate");
            objItem.FullName_EN = SqlHelper.GetString(dataReader, "FullName_EN");
            objItem.StatusName = SqlHelper.GetString(dataReader, "StatusName");
            objItem.ApprovalLevel = SqlHelper.GetInt(dataReader, "ApprovalLevel");
            objItem.DirectManagerCode = SqlHelper.GetInt(dataReader, "DirectManagerCode");
            objItem.DirectManagerNo = SqlHelper.GetString(dataReader, "DirectManagerNo");
            objItem.DirectManagerName = SqlHelper.GetString(dataReader, "DirectManagerName");
            objItem.InDirectManagerCode = SqlHelper.GetInt(dataReader, "InDirectManagerCode");
            objItem.InDirectManagerNo = SqlHelper.GetString(dataReader, "InDirectManagerNo");
            objItem.InDirectManagerName = SqlHelper.GetString(dataReader, "InDirectManagerName");
            objItem.RoleFullName = SqlHelper.GetString(dataReader, "RoleFullName");
            objItem.Description = SqlHelper.GetString(dataReader, "Description");

            return objItem;
        }
        public static T_LMS_Trans_LeaveStory GetItemByID(int Id)
        {
            T_LMS_Trans_LeaveStory item = new T_LMS_Trans_LeaveStory();
            var sqlParams = new SqlParameter[1];
            sqlParams[0] = new SqlParameter("@Id", Id);
            using (var reader = SqlHelper.ExecuteReaderService(ModuleConfig.MyConnection, "T_LMS_Trans_LeaveStory_GetByID", sqlParams))
            {
                while (reader.Read())
                {
                    item = GetItemFromReader(reader);
                }
            }
            return item;


        }
        public static LeaveWFCollection GetLeaveReason(LeaveWFPara Filter)
        {
            LeaveWFCollection collection = new LeaveWFCollection();
            var sqlParams = new SqlParameter[]
                {
                    new SqlParameter("@EntityID", Filter.EntityID),
                    new SqlParameter("@WorkDate", Filter.WorkDate),
                    new SqlParameter("@EmployeeCode", Filter.EmployeeCode),
                    
                };
            using (var reader = SqlHelper.ExecuteReaderService(ModuleConfig.MyConnection, "MIS_HRApps.dbo.USP_LMS_LeaveInfoDetail_Get", sqlParams))
            {
                LeaveWF obj = new LeaveWF();
                while (reader.Read())
                {
                    obj = GetItem(reader);
                    collection.Add(obj);
                }
            }
            return collection;
        }
        public static T_LMS_Trans_LeaveStory AddItem(T_LMS_Trans_LeaveStory model)
        {
            int result = 0;
            using (var reader = SqlHelper.ExecuteReaderService(ModuleConfig.MyConnection, CommandType.StoredProcedure, "T_LMS_Trans_LeaveStory_Add", CreateSqlParameter(model)))
            {
                while (reader.Read())
                {
                    result = (int)reader[0];
                }
            }
            return GetItemByID(result);

        }

      
        public static T_LMS_Trans_LeaveStory UpdateItem(T_LMS_Trans_LeaveStory model)
        {
            int result = 0;
            using (var reader = SqlHelper.ExecuteReaderService(ModuleConfig.MyConnection, CommandType.StoredProcedure, "T_LMS_Trans_LeaveStory_Update", CreateSqlParameter(model)))
            {
                while (reader.Read())
                {
                    result = (int)reader[0];
                }
            }
            return GetItemByID(result);

        }

        public static T_LMS_Trans_LeaveStoryCollection GetAllItem()
        {
            T_LMS_Trans_LeaveStoryCollection collection = new T_LMS_Trans_LeaveStoryCollection();
            using (var reader = SqlHelper.ExecuteReaderService(ModuleConfig.MyConnection, "T_LMS_Trans_LeaveStory_GetAll", null))
            {
                while (reader.Read())
                {
                    T_LMS_Trans_LeaveStory obj = new T_LMS_Trans_LeaveStory();
                    obj = GetItemFromReader(reader);
                    collection.Add(obj);
                }
            }
            return collection;
        }

        public static T_LMS_Trans_LeaveStoryCollection Search(SearchFilter SearchKey)
        {
            T_LMS_Trans_LeaveStoryCollection collection = new T_LMS_Trans_LeaveStoryCollection();
            using (var reader = SqlHelper.ExecuteReaderService(ModuleConfig.MyConnection, "T_LMS_Trans_LeaveStory_Search", SearchFilterManager.SqlSearchConditionNoCompany(SearchKey)))
            {
                while (reader.Read())
                {
                    T_LMS_Trans_LeaveStory obj = new T_LMS_Trans_LeaveStory();
                    obj = GetItemFromReader(reader);
                    collection.Add(obj);
                }
            }
            return collection;
        }
        public static T_LMS_Trans_LeaveStoryCollection GetbyUser(string CreatedUser)
        {
            T_LMS_Trans_LeaveStoryCollection collection = new T_LMS_Trans_LeaveStoryCollection();
            T_LMS_Trans_LeaveStory obj;
            using (var reader = SqlHelper.ExecuteReaderService(ModuleConfig.MyConnection, "T_LMS_Trans_LeaveStory_GetAll_byUser", new SqlParameter("@CreatedUser", CreatedUser)))
            {
                while (reader.Read())
                {
                    obj = GetItemFromReader(reader);
                    collection.Add(obj);
                }
            }
            return collection;
        }

        private static SqlParameter[] CreateSqlParameter(T_LMS_Trans_LeaveStory model)
        {
            return new SqlParameter[]
                {
                    new SqlParameter("@Id", model.Id),
                    new SqlParameter("@EmployeeCode", model.EmployeeCode),
                    new SqlParameter("@LeaveTypeId", model.LeaveTypeId),
                    new SqlParameter("@StartDate", model.StartDate),
                    new SqlParameter("@EndDate", model.EndDate),
                    new SqlParameter("@FromTime", model.FromTime),
                    new SqlParameter("@ToTime", model.ToTime),
                    new SqlParameter("@NoOfDays", model.NoOfDays),
                    new SqlParameter("@NoOfHours", model.NoOfHours),
                    new SqlParameter("@HalfDayPart", model.HalfDayPart),
                    new SqlParameter("@Remarks", model.Remarks),
                    new SqlParameter("@ApproverComments", model.ApproverComments),
                    new SqlParameter("@StatusId", model.StatusId),
                    new SqlParameter("@ApprovalLevel", model.ApprovalLevel),
                    new SqlParameter("@IsUpdatedByAdmin", model.IsUpdatedByAdmin),
                    new SqlParameter("@IsOverriddenLeave", model.IsOverriddenLeave),
                    new SqlParameter("@OverriddenLeaveId", model.OverriddenLeaveId),
                    new SqlParameter("@IsNC", model.IsNC),
                    new SqlParameter("@OldStatusId", model.OldStatusId),
                    new SqlParameter("@OldApprovalLevelId", model.OldApprovalLevelId),
                    new SqlParameter("@IsAcitve", model.IsAcitve),
                    new SqlParameter("@CreatedBy", model.CreatedBy),
                    new SqlParameter("@CreatedDate", model.CreatedDate),
                    new SqlParameter("@ModifiedBy", model.ModifiedBy),
                    new SqlParameter("@ModifiedDate", model.ModifiedDate),
                    new SqlParameter("@Ref_RequestId", model.Ref_RequestId),
                    new SqlParameter("@UploadStatus", model.UploadStatus),
                    new SqlParameter("@LastUploadDate", model.LastUploadDate),
                    new SqlParameter("@LastUploadedToDate", model.LastUploadedToDate),
                    new SqlParameter("@NextLevelId", model.NextLevelId),
                    new SqlParameter("@NextApproverRoleId", model.NextApproverRoleId),
                    new SqlParameter("@CancelRequestCreatedOn", model.CancelRequestCreatedOn),

                };
        }

        public static int DeleteItem(int itemID)
        {
            return SqlHelper.ExecuteNonQuery("T_LMS_Trans_LeaveStory_Delete", itemID);
        }
    }
}