using System.Text;
using System.Data;
using System.Data.SqlClient;
using System;

using System.Collections.Generic;
using System.Runtime.Serialization;

namespace DTP.Object
{
    [DataContract]
    public class ComUserGroup : BaseDBEntity
    {
        [DataMember]
        public Int64 ComUserGroupId { get; set; }

        [DataMember]
        public string GroupCode { get; set; }

        [DataMember]
        public string UserName { get; set; }

        [DataMember]
        public decimal DonateAmount { get; set; }

        [DataMember]
        public decimal ExpAmount { get; set; }

        [DataMember]
        public string UserDesc { get; set; }

        [DataMember]
        public DateTime CreatedDate { get; set; }

        [DataMember]
        public string Status { get; set; }

        [DataMember]
        public int Rate { get; set; }

        [DataMember]
        public bool Active { get; set; }
    }
    public class ComUserGroupCollection : BaseDBEntityCollection<ComUserGroup> { }
    public class ComUserGroupManager
    {
        private static ComUserGroup GetItemFromReader(IDataReader dataReader)
        {
            ComUserGroup objItem = new ComUserGroup();
            objItem.ComUserGroupId = SqlHelper.GetInt(dataReader, "ComUserGroup");

            objItem.GroupCode = SqlHelper.GetString(dataReader, "GroupCode");

            objItem.UserName = SqlHelper.GetString(dataReader, "UserName");

            objItem.DonateAmount = SqlHelper.GetDecimal(dataReader, "DonateAmount");

            objItem.ExpAmount = SqlHelper.GetDecimal(dataReader, "ExpAmount");

            objItem.UserDesc = SqlHelper.GetString(dataReader, "UserDesc");

            objItem.CreatedDate = SqlHelper.GetDateTime(dataReader, "CreatedDate");

            objItem.Status = SqlHelper.GetString(dataReader, "Status");

            objItem.Rate = SqlHelper.GetInt(dataReader, "Rate");

            objItem.Active = SqlHelper.GetBoolean(dataReader, "Active");
            return objItem;
        }
        public static ComUserGroupCollection GetItemByGroup(string GroupCode)
        {           

            ComUserGroupCollection collection = new ComUserGroupCollection();
            var sqlParams = new SqlParameter[1];
            sqlParams[0] = new SqlParameter("@GroupCode", GroupCode);
            using (var reader = SqlHelper.ExecuteReader("tblComUserGroup_GetByGroupCode", sqlParams))
            {
                while (reader.Read())
                {
                    ComUserGroup obj = new ComUserGroup();
                    obj = GetItemFromReader(reader);
                    collection.Add(obj);
                }
            }
            return collection;

        }
        public static ComUserGroup GetItemByID(Int64 comUserGroup)
        {
            ComUserGroup item = new ComUserGroup();
            var sqlParams = new SqlParameter[1];
            sqlParams[0] = new SqlParameter("@ComUserGroup", comUserGroup);
            using (var reader = SqlHelper.ExecuteReader("tblComUserGroup_GetByID", sqlParams))
            {
                while (reader.Read())
                {
                    item = GetItemFromReader(reader);
                }
            }
            return item;


        }
        public static ComUserGroup AddItem(ComUserGroup model)
        {
            Int64 result = 0;
            using (var reader = SqlHelper.ExecuteReader(CommandType.StoredProcedure, "tblComUserGroup_Add", CreateSqlParameter(model)))
            {
                while (reader.Read())
                {
                    result = (Int64)reader[0];
                }
            }
            return GetItemByID(result);

        }
        public static ComUserGroup UpdateItem(ComUserGroup model)
        {
            Int64 result = 0;
            using (var reader = SqlHelper.ExecuteReader(CommandType.StoredProcedure, "tblComUserGroup_Update", CreateSqlParameter(model)))
            {
                while (reader.Read())
                {
                    result = (Int64)reader[0];
                }
            }
            return GetItemByID(result);

        }
        public static ComUserGroupCollection GetAllItem()
        {
            ComUserGroupCollection collection = new ComUserGroupCollection();
            using (var reader = SqlHelper.ExecuteReader("tblComUserGroup_GetAll", null))
            {
                while (reader.Read())
                {
                    ComUserGroup obj = new ComUserGroup();
                    obj = GetItemFromReader(reader);
                    collection.Add(obj);
                }
            }
            return collection;
        }

        private static SqlParameter[] CreateSqlParameter(ComUserGroup model)
        {
            return new SqlParameter[]
                {
                new SqlParameter("@ComUserGroup", model.ComUserGroupId),
					new SqlParameter("@GroupCode", model.GroupCode),
					new SqlParameter("@UserName", model.UserName),
					new SqlParameter("@DonateAmount", model.DonateAmount),
					new SqlParameter("@ExpAmount", model.ExpAmount),
					new SqlParameter("@UserDesc", model.UserDesc),
					new SqlParameter("@CreatedDate", model.CreatedDate),
					new SqlParameter("@Status", model.Status),
					new SqlParameter("@Rate", model.Rate),
					new SqlParameter("@Active", model.Active),
					
                };
        }

        public static int DeleteItem(Int64 itemID)
        {
            return SqlHelper.ExecuteNonQuery("tblComUserGroup_Delete", itemID);
        }
    }
}
