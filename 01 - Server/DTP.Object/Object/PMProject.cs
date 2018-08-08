using System.Text;
using System.Data;
using System.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using DTP.Object;
using ifinds.Data;
//using Server.DAC;
//using Server.Helpers;
namespace ifinds.Object.CS.Models
{
    [DataContract]
    public class PMProject : BaseDBEntity
    {
        [DataMember]
        public string ProjectCD { get; set; }

        [DataMember]
        public int CompanyID { get; set; }

        [DataMember]
        public string Status { get; set; }

        [DataMember]
        public string Description { get; set; }

        [DataMember]
        public DateTime StartDate { get; set; }

        [DataMember]
        public DateTime ActivationDate { get; set; }

        [DataMember]
        public DateTime ExpireDate { get; set; }

        [DataMember]
        public int CalendarID { get; set; }

        [DataMember]
        public int Duration { get; set; }

        [DataMember]
        public string DurationType { get; set; }

        [DataMember]
        public string CreatedUser { get; set; }

        [DataMember]
        public DateTime CreatedDate { get; set; }

        [DataMember]
        public string CreatedByScreenID { get; set; }

        [DataMember]
        public int OverTimeItemID { get; set; }

        [DataMember]
        public int LocationID { get; set; }

        [DataMember]
        public bool IsContinuous { get; set; }

        [DataMember]
        public bool Approved { get; set; }

        [DataMember]
        public bool Rejected { get; set; }

        [DataMember]
        public bool isActive { get; set; }

        [DataMember]
        public bool isCompleted { get; set; }

        [DataMember]
        public bool isCancelled { get; set; }

        [DataMember]
        public string OwnerID { get; set; }

        [DataMember]
        public DateTime EffectiveFrom { get; set; }

        [DataMember]
        public string CuryID { get; set; }

        [DataMember]
        public string RateTypeID { get; set; }

        [DataMember]
        public bool AutoRenew { get; set; }

        [DataMember]
        public int CustomerID { get; set; }

        [DataMember]
        public int CompletePercent { get; set; }

        [DataMember]
        public DateTime CompletedDate { get; set; }

        [DataMember]
        public string ImgUrl { get; set; }
        
    }
    public class PMProjectCollection : BaseDBEntityCollection<PMProject> { }
    public class PMProjectManager
    {
        private static PMProject GetItemFromReader(IDataReader dataReader)
        {
            PMProject objItem = new PMProject();
            objItem.ProjectCD = SqlHelper.GetString(dataReader, "ProjectCD");

            objItem.CompanyID = SqlHelper.GetInt(dataReader, "CompanyID");

            objItem.Status = SqlHelper.GetString(dataReader, "Status");

            objItem.Description = SqlHelper.GetString(dataReader, "Description");

            objItem.StartDate = SqlHelper.GetDateTime(dataReader, "StartDate");

            objItem.ActivationDate = SqlHelper.GetDateTime(dataReader, "ActivationDate");

            objItem.ExpireDate = SqlHelper.GetDateTime(dataReader, "ExpireDate");

            objItem.CalendarID = SqlHelper.GetInt(dataReader, "CalendarID");

            objItem.Duration = SqlHelper.GetInt(dataReader, "Duration");

            objItem.DurationType = SqlHelper.GetString(dataReader, "DurationType");

            objItem.CreatedUser = SqlHelper.GetString(dataReader, "CreatedUser");

            objItem.CreatedDate = SqlHelper.GetDateTime(dataReader, "CreatedDate");

            objItem.CreatedByScreenID = SqlHelper.GetString(dataReader, "CreatedByScreenID");

            objItem.OverTimeItemID = SqlHelper.GetInt(dataReader, "OverTimeItemID");

            objItem.LocationID = SqlHelper.GetInt(dataReader, "LocationID");

            objItem.IsContinuous = SqlHelper.GetBoolean(dataReader, "IsContinuous");

            objItem.Approved = SqlHelper.GetBoolean(dataReader, "Approved");

            objItem.Rejected = SqlHelper.GetBoolean(dataReader, "Rejected");

            objItem.isActive = SqlHelper.GetBoolean(dataReader, "isActive");

            objItem.isCompleted = SqlHelper.GetBoolean(dataReader, "isCompleted");

            objItem.isCancelled = SqlHelper.GetBoolean(dataReader, "isCancelled");

            objItem.OwnerID = SqlHelper.GetString(dataReader, "OwnerID");

            objItem.EffectiveFrom = SqlHelper.GetDateTime(dataReader, "EffectiveFrom");

            objItem.CuryID = SqlHelper.GetString(dataReader, "CuryID");

            objItem.RateTypeID = SqlHelper.GetString(dataReader, "RateTypeID");

            objItem.AutoRenew = SqlHelper.GetBoolean(dataReader, "AutoRenew");

            objItem.CustomerID = SqlHelper.GetInt(dataReader, "CustomerID");

            objItem.CompletePercent = SqlHelper.GetInt(dataReader, "CompletePercent");

            objItem.CompletedDate = SqlHelper.GetDateTime(dataReader, "CompletedDate");
            objItem.ImgUrl = SqlHelper.GetString(dataReader, "ImgUrl");
            
            return objItem;
        }
        public static PMProject GetItemByID(String projectCD,int CompanyID)
        {
            PMProject item = new PMProject();
            var sqlParams = new SqlParameter[2];
            sqlParams[0] = new SqlParameter("@ProjectCD", projectCD);
            sqlParams[1] = new SqlParameter("@CompanyID", CompanyID);
            using (var reader = SqlHelper.ExecuteReader("PMProject_GetByID", sqlParams))
            {
                while (reader.Read())
                {
                    item = GetItemFromReader(reader);
                }
            }
            return item;


        }
        public static PMProject AddItem(PMProject model)
        {
            String result = String.Empty;
            try
            {
                using (var reader = SqlHelper.ExecuteReader(CommandType.StoredProcedure, "PMProject_Add", CreateSqlParameter(model)))
                {
                    while (reader.Read())
                    {
                        result = (String)reader[0];
                    }
                }
            }catch(Exception Objex)
            {

            }
            return GetItemByID(result,model.CompanyID);

        }
        public static PMProject UpdateItem(PMProject model)
        {
            String result = String.Empty;
            try
            {
                using (var reader = SqlHelper.ExecuteReader(CommandType.StoredProcedure, "PMProject_Update", CreateSqlParameter(model)))
                {
                    while (reader.Read())
                    {
                        result = (String)reader[0];
                    }
                }
            }catch(Exception Objecx)
            {

            }
            return GetItemByID(model.ProjectCD, model.CompanyID);

        }
        public static PMProjectCollection GetAllItem(int CompanyID)
        {
            PMProjectCollection collection = new PMProjectCollection();
            var sqlParams = new SqlParameter[]
                   {
                            new SqlParameter("@CompanyID", CompanyID),
                   };
            using (var reader = SqlHelper.ExecuteReader("PMProject_GetAll", sqlParams))
            {
                while (reader.Read())
                {
                    PMProject obj = new PMProject();
                    obj = GetItemFromReader(reader);
                    collection.Add(obj);
                }
            }
            return collection;
        }
        public static PMProjectCollection GetbyUser(string CreatedUser)
        {
            PMProjectCollection collection = new PMProjectCollection();
            PMProject obj;
            using (var reader = SqlHelper.ExecuteReader("PMProject_GetAll_byUser", new SqlParameter("@CreatedUser", CreatedUser)))
            {
                while (reader.Read())
                {
                    obj = GetItemFromReader(reader);
                    collection.Add(obj);
                }
            }
            return collection;
        }

        private static SqlParameter[] CreateSqlParameter(PMProject model)
        {
            return new SqlParameter[]
                {
                new SqlParameter("@ProjectCD", model.ProjectCD),
                    new SqlParameter("@CompanyID", model.CompanyID),
                    new SqlParameter("@Status", model.Status),
                    new SqlParameter("@Description", model.Description),
                    new SqlParameter("@StartDate", model.StartDate),
                    new SqlParameter("@ActivationDate", model.ActivationDate),
                    new SqlParameter("@ExpireDate", model.ExpireDate),
                    new SqlParameter("@CalendarID", model.CalendarID),
                    new SqlParameter("@Duration", model.Duration),
                    new SqlParameter("@DurationType", model.DurationType),
                    new SqlParameter("@CreatedUser", model.CreatedUser),
                    new SqlParameter("@CreatedDate", model.CreatedDate),
                    new SqlParameter("@CreatedByScreenID", model.CreatedByScreenID),
                    new SqlParameter("@OverTimeItemID", model.OverTimeItemID),
                    new SqlParameter("@LocationID", model.LocationID),
                    new SqlParameter("@IsContinuous", model.IsContinuous),
                    new SqlParameter("@Approved", model.Approved),
                    new SqlParameter("@Rejected", model.Rejected),
                    new SqlParameter("@isActive", model.isActive),
                    new SqlParameter("@isCompleted", model.isCompleted),
                    new SqlParameter("@isCancelled", model.isCancelled),
                    new SqlParameter("@OwnerID", model.OwnerID),
                    new SqlParameter("@EffectiveFrom", model.EffectiveFrom),
                    new SqlParameter("@CuryID", model.CuryID),
                    new SqlParameter("@RateTypeID", model.RateTypeID),
                    new SqlParameter("@AutoRenew", model.AutoRenew),
                    new SqlParameter("@CustomerID", model.CustomerID),
                    new SqlParameter("@CompletePercent", model.CompletePercent),
                    new SqlParameter("@CompletedDate", model.CompletedDate),
                    new SqlParameter("@ImgUrl", model.ImgUrl),
                };
        }

        public static int DeleteItem(String itemID,int CompanyID)
        {
            return SqlHelper.ExecuteNonQuery("PMProject_Delete", new SqlParameter[]
                {
                new SqlParameter("@ProjectCD",itemID),
                    new SqlParameter("@CompanyID", CompanyID) });
        }

        #region attachment
        public static void AddPMProjectAttach(int CompanyID, string ProjectCD, Guid AttachID)
        {

            var reader = SqlHelper.ExecuteReader(CommandType.StoredProcedure, "PMProjectAttach_Add", new SqlParameter[]
            {
                new SqlParameter("@ProjectCD",ProjectCD),
                    new SqlParameter("@CompanyID", CompanyID),
                    new SqlParameter("@AttachID", AttachID)
            });

        }
        public static void DeletePMProjectAttach(int CompanyID, string ProjectCD, Guid AttachID)
        {

            var reader = SqlHelper.ExecuteReader(CommandType.StoredProcedure, "PMProjectAttach_Delete", new SqlParameter[]
            {
                new SqlParameter("@ProjectCD",ProjectCD),
                    new SqlParameter("@CompanyID", CompanyID),
                    new SqlParameter("@AttachID", AttachID)
            });

        }

        public static AttachmentsCollection GetAllPMProjectAttach(int CompanyID, string ProjectCD)
        {
            AttachmentsCollection collection = new AttachmentsCollection();
            using (var reader = SqlHelper.ExecuteReader(CommandType.StoredProcedure, "PMProjectAttach_GetAll", new SqlParameter[]
            {
                new SqlParameter("@ProjectCD",ProjectCD),
                    new SqlParameter("@CompanyID", CompanyID),
            }))
            {
                while (reader.Read())
                {
                    Attachments objItem = new Attachments();
                    objItem.AttachID = SqlHelper.GetGuid(reader, "AttachID");

                    objItem.CompanyID = SqlHelper.GetInt(reader, "CompanyID");

                    objItem.Filetype = SqlHelper.GetString(reader, "Filetype");

                    objItem.Title = SqlHelper.GetString(reader, "Title");

                    objItem.Description = SqlHelper.GetString(reader, "Description");

                    objItem.Version = SqlHelper.GetString(reader, "Version");
                    collection.Add(objItem);
                }
            }
            return collection;

        }
        #endregion
    }
}