using System.Data;
using System.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace DTP.Object
{
    [DataContract]
    public class CompanyTree : BaseDBEntity
    {

        [DataMember]
        public int CompanyID { get; set; }

        [DataMember]
        public int WorkGroupID { get; set; }

        [DataMember]
        public string Description { get; set; }

        [DataMember]
        public int ParentWGID { get; set; }

        [DataMember]
        public int SortOrder { get; set; }

        [DataMember]
        public int AccessRights { get; set; }

        [DataMember]
        public int WaitTime { get; set; }

        [DataMember]
        public bool BypassEscalation { get; set; }

        [DataMember]
        public bool UseCalendarTime { get; set; }

        [DataMember]
        public string CreatedUser { get; set; }

        [DataMember]
        public DateTime CreatedDate { get; set; }

        [DataMember]
        public string ModifiedUser { get; set; }

        [DataMember]
        public DateTime ModifiedDate { get; set; }


    }
    public class CompanyTreeCollection : BaseDBEntityCollection<CompanyTree> { }
    public class CompanyTreeList 
    {
        public CompanyTreeList()
        {
            ListGroup = new CompanyTreeCollection();
            ListMember = new CompanyTreeMemberCollection();
        }
        public CompanyTreeCollection ListGroup { get; set; }
        public CompanyTreeMemberCollection ListMember { get; set; }
    }
    public class CompanyTreeManager
    {
        private static CompanyTree GetItemFromReader(IDataReader dataReader)
        {
            CompanyTree objItem = new CompanyTree();

            objItem.CompanyID = SqlHelper.GetInt(dataReader, "CompanyID");

            objItem.WorkGroupID = SqlHelper.GetInt(dataReader, "WorkGroupID");

            objItem.Description = SqlHelper.GetString(dataReader, "Description");

            objItem.ParentWGID = SqlHelper.GetInt(dataReader, "ParentWGID");

            objItem.SortOrder = SqlHelper.GetInt(dataReader, "SortOrder");

            objItem.AccessRights = SqlHelper.GetSmallInt(dataReader, "AccessRights");

            objItem.WaitTime = SqlHelper.GetInt(dataReader, "WaitTime");

            objItem.BypassEscalation = SqlHelper.GetBoolean(dataReader, "BypassEscalation");

            objItem.UseCalendarTime = SqlHelper.GetBoolean(dataReader, "UseCalendarTime");

            objItem.CreatedUser = SqlHelper.GetString(dataReader, "CreatedUser");

            objItem.CreatedDate = SqlHelper.GetDateTime(dataReader, "CreatedDate");

            objItem.ModifiedUser = SqlHelper.GetString(dataReader, "ModifiedUser");

            objItem.ModifiedDate = SqlHelper.GetDateTime(dataReader, "ModifiedDate");



            return objItem;
        }
        public static CompanyTree GetItemByID(Int32 WorkGroupID, int CompanyID)
        {
            CompanyTree item = new CompanyTree();
            var sqlParams = new SqlParameter[2];
            sqlParams[0] = new SqlParameter("@WorkGroupID", WorkGroupID);
            sqlParams[1] = new SqlParameter("@CompanyID", CompanyID);
            using (var reader = SqlHelper.ExecuteReader("CompanyTree_GetByID", sqlParams))
            {
                while (reader.Read())
                {
                    item = GetItemFromReader(reader);
                }
            }
            return item;


        }
        public static CompanyTree AddItem(CompanyTree model)
        {
            int result = 0;
            using (var reader = SqlHelper.ExecuteReader(CommandType.StoredProcedure, "CompanyTree_Add", CreateSqlParameter(model)))
            {
                while (reader.Read())
                {
                    result = (int)reader[0];
                }
            }
            return GetItemByID(result, model.CompanyID);

        }

        public static int CreateOrUpdate(CompanyTreeList model)
        {
            CompanyTreeCollection result = new CompanyTreeCollection();
            if (model.ListGroup.Count > 0)
            {
                foreach(var item in model.ListGroup)
                {
                    CompanyTree b = new CompanyTree();
                    if (item.WorkGroupID == 0)
                    {
                      
                        b=AddItem(item);
                    }else
                    {
                        b = UpdateItem(item);
                    }
                    result.Add(b);
                }
               
            }
            if (model.ListMember.Count > 0)
            {
                foreach (var item in model.ListMember)
                {
                    //int workGroupID = result[item.WorkGroupID].WorkGroupID;
                    //item.WorkGroupID = workGroupID;
                    CompanyTreeMember b = new CompanyTreeMember();
                    var memberGroup = CompanyTreeMemberManager.GetItemByID(item.WorkGroupID, item.CompanyID, item.EmployeeID);
                    if (string.IsNullOrEmpty(memberGroup.EmployeeID))
                    {

                        b = CompanyTreeMemberManager.AddItem(item);
                    }
                    else
                    {
                        b = CompanyTreeMemberManager.UpdateItem(item);
                    }
                }
            }
            return 1;
        }
        public static CompanyTree UpdateItem(CompanyTree model)
        {
            int result = 0;
            using (var reader = SqlHelper.ExecuteReader(CommandType.StoredProcedure, "CompanyTree_Update", CreateSqlParameter(model)))
            {
                while (reader.Read())
                {
                    result = (int)reader[0];
                }
            }
            return GetItemByID(model.WorkGroupID, model.CompanyID);

        }
        public static CompanyTreeCollection GetAllItem(int CompanyID)
        {
            CompanyTreeCollection collection = new CompanyTreeCollection();

            var sqlParams = new SqlParameter[1];
            sqlParams[0] = new SqlParameter("@CompanyID", CompanyID);
            using (var reader = SqlHelper.ExecuteReader("CompanyTree_GetAll", sqlParams))
            {
                while (reader.Read())
                {
                    CompanyTree obj = new CompanyTree();
                    obj = GetItemFromReader(reader);
                    collection.Add(obj);
                }
            }
            return collection;
        }

        public static CompanyTreeCollection Search(SearchFilter SearchKey)
        {
            CompanyTreeCollection collection = new CompanyTreeCollection();
            using (var reader = SqlHelper.ExecuteReader("CompanyTree_Search", SearchFilterManager.SqlSearchParam(SearchKey)))
            {
                while (reader.Read())
                {
                    CompanyTree obj = new CompanyTree();
                    obj = GetItemFromReader(reader);
                    collection.Add(obj);
                }
            }
            return collection;
        }
        public static CompanyTreeCollection GetbyUser(string CreatedUser)
        {
            CompanyTreeCollection collection = new CompanyTreeCollection();
            CompanyTree obj;
            using (var reader = SqlHelper.ExecuteReader("CompanyTree_GetAll_byUser", new SqlParameter("@CreatedUser", CreatedUser)))
            {
                while (reader.Read())
                {
                    obj = GetItemFromReader(reader);
                    collection.Add(obj);
                }
            }
            return collection;
        }

        private static SqlParameter[] CreateSqlParameter(CompanyTree model)
        {
            return new SqlParameter[]
                {
                    new SqlParameter("@CompanyID", model.CompanyID),
                    new SqlParameter("@WorkGroupID", model.WorkGroupID),
                    new SqlParameter("@Description", model.Description),
                    new SqlParameter("@ParentWGID", model.ParentWGID),
                    new SqlParameter("@SortOrder", model.SortOrder),
                    new SqlParameter("@AccessRights", model.AccessRights),
                    new SqlParameter("@WaitTime", model.WaitTime),
                    new SqlParameter("@BypassEscalation", model.BypassEscalation),
                    new SqlParameter("@UseCalendarTime", model.UseCalendarTime),
                    new SqlParameter("@CreatedUser", model.CreatedUser),
                    new SqlParameter("@CreatedDate", model.CreatedDate),
                    new SqlParameter("@ModifiedUser", model.ModifiedUser),
                    new SqlParameter("@ModifiedDate", model.ModifiedDate),

                };
        }

        public static int DeleteItem(Int32 itemID, int CompanyID)
        {
            return SqlHelper.ExecuteNonQuery("CompanyTree_Delete", new SqlParameter[]
            {
                new SqlParameter(@"WorkGroupID",itemID),
                    new SqlParameter("@CompanyID", CompanyID)
            });
        }
    }
}