using System.Text;
using System.Data;
using System.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using DTP.Object;
namespace ifinds.Object.GL
{
    [DataContract]
    public class Account : BaseDBEntity
    {

        [DataMember]
        public int CompanyID { get; set; }

        [DataMember]
        public int AccountID { get; set; }

        [DataMember]
        public string AccountCD { get; set; }

        [DataMember]
        public string Type { get; set; }

        [DataMember]
        public int COAOrder { get; set; }

        [DataMember]
        public string AccountClassID { get; set; }

        [DataMember]
        public int AccountGroupID { get; set; }

        [DataMember]
        public bool Active { get; set; }

        [DataMember]
        public string Description { get; set; }

        [DataMember]
        public string PostOption { get; set; }

        [DataMember]
        public bool DirectPost { get; set; }

        [DataMember]
        public string CuryID { get; set; }

        [DataMember]
        public int BranchID { get; set; }

        [DataMember]
        public byte[] GroupMask { get; set; }

        [DataMember]
        public string RevalCuryRateTypeID { get; set; }

        [DataMember]
        public string TaxCategoryID { get; set; }

        [DataMember]
        public string CreatedUser { get; set; }

        [DataMember]
        public string CreatedByScreenID { get; set; }

        [DataMember]
        public DateTime CreatedDate { get; set; }

        [DataMember]
        public string GLConsolAccountCD { get; set; }

        [DataMember]
        public bool RequireUnits { get; set; }

        [DataMember]
        public bool NoSubDetail { get; set; }

        [DataMember]
        public bool DeletedDatabaseRecord { get; set; }

        [DataMember]
        public bool isCashAccount { get; set; }

        [DataMember]
        public Guid NoteID { get; set; }


    }
    public class AccountCollection : BaseDBEntityCollection<Account> { }
    public class AccountManager
    {
        private static Account GetItemFromReader(IDataReader dataReader)
        {
            Account objItem = new Account();

            objItem.CompanyID = SqlHelper.GetInt(dataReader, "CompanyID");

            objItem.AccountID = SqlHelper.GetInt(dataReader, "AccountID");

            objItem.AccountCD = SqlHelper.GetString(dataReader, "AccountCD");

            objItem.Type = SqlHelper.GetString(dataReader, "Type");

            objItem.COAOrder = SqlHelper.GetSmallInt(dataReader, "COAOrder");

            objItem.AccountClassID = SqlHelper.GetString(dataReader, "AccountClassID");

            objItem.AccountGroupID = SqlHelper.GetInt(dataReader, "AccountGroupID");

            objItem.Active = SqlHelper.GetBoolean(dataReader, "Active");

            objItem.Description = SqlHelper.GetString(dataReader, "Description");

            objItem.PostOption = SqlHelper.GetString(dataReader, "PostOption");

            objItem.DirectPost = SqlHelper.GetBoolean(dataReader, "DirectPost");

            objItem.CuryID = SqlHelper.GetString(dataReader, "CuryID");

            objItem.BranchID = SqlHelper.GetInt(dataReader, "BranchID");

            objItem.GroupMask = SqlHelper.GetBytes(dataReader, "GroupMask");

            objItem.RevalCuryRateTypeID = SqlHelper.GetString(dataReader, "RevalCuryRateTypeID");

            objItem.TaxCategoryID = SqlHelper.GetString(dataReader, "TaxCategoryID");

            objItem.CreatedUser = SqlHelper.GetString(dataReader, "CreatedUser");

            objItem.CreatedByScreenID = SqlHelper.GetString(dataReader, "CreatedByScreenID");

            objItem.CreatedDate = SqlHelper.GetDateTime(dataReader, "CreatedDate");

            objItem.GLConsolAccountCD = SqlHelper.GetString(dataReader, "GLConsolAccountCD");

            objItem.RequireUnits = SqlHelper.GetBoolean(dataReader, "RequireUnits");

            objItem.NoSubDetail = SqlHelper.GetBoolean(dataReader, "NoSubDetail");

            objItem.DeletedDatabaseRecord = SqlHelper.GetBoolean(dataReader, "DeletedDatabaseRecord");

            objItem.isCashAccount = SqlHelper.GetBoolean(dataReader, "isCashAccount");

            objItem.NoteID = SqlHelper.GetGuid(dataReader, "NoteID");



            return objItem;
        }
        public static Account GetItemByID(Int32 AccountID,int CompanyID)
        {
            Account item = new Account();
            var sqlParams = new SqlParameter[1];
            sqlParams[0] = new SqlParameter("@AccountID", AccountID);
            sqlParams[1] = new SqlParameter("@CompanyID", CompanyID);
            using (var reader = SqlHelper.ExecuteReader("Account_GetByID", sqlParams))
            {
                while (reader.Read())
                {
                    item = GetItemFromReader(reader);
                }
            }
            return item;


        }
        public static Account AddItem(Account model)
        {
            int result = 0;
            using (var reader = SqlHelper.ExecuteReader(CommandType.StoredProcedure, "Account_Add", CreateSqlParameter(model)))
            {
                while (reader.Read())
                {
                    result = (int)reader[0];
                }
            }
            return GetItemByID(result, model.CompanyID);

        }
        public static Account UpdateItem(Account model)
        {
            String result = String.Empty;
            using (var reader = SqlHelper.ExecuteReader(CommandType.StoredProcedure, "Account_Update", CreateSqlParameter(model)))
            {
                while (reader.Read())
                {
                    result = (String)reader[0];
                }
            }
            return GetItemByID(int.Parse(result),model.CompanyID);

        }
        public static AccountCollection GetAllItem()
        {
            AccountCollection collection = new AccountCollection();
            using (var reader = SqlHelper.ExecuteReader("Account_GetAll", null))
            {
                while (reader.Read())
                {
                    Account obj = new Account();
                    obj = GetItemFromReader(reader);
                    collection.Add(obj);
                }
            }
            return collection;
        }

        public static AccountCollection Search(SearchFilter SearchKey)
        {
            AccountCollection collection = new AccountCollection();
            using (var reader = SqlHelper.ExecuteReader("Account_Search", SearchFilterManager.SqlSearchParam(SearchKey)))
            {
                while (reader.Read())
                {
                    Account obj = new Account();
                    obj = GetItemFromReader(reader);
                    collection.Add(obj);
                }
            }
            return collection;
        }
        public static AccountCollection GetbyUser(string CreatedUser)
        {
            AccountCollection collection = new AccountCollection();
            Account obj;
            using (var reader = SqlHelper.ExecuteReader("Account_GetAll_byUser", new SqlParameter("@CreatedUser", CreatedUser)))
            {
                while (reader.Read())
                {
                    obj = GetItemFromReader(reader);
                    collection.Add(obj);
                }
            }
            return collection;
        }

        private static SqlParameter[] CreateSqlParameter(Account model)
        {
            return new SqlParameter[]
                {
                    new SqlParameter("@CompanyID", model.CompanyID),
                    new SqlParameter("@AccountID", model.AccountID),
                    new SqlParameter("@AccountCD", model.AccountCD),
                    new SqlParameter("@Type", model.Type),
                    new SqlParameter("@COAOrder", model.COAOrder),
                    new SqlParameter("@AccountClassID", model.AccountClassID),
                    new SqlParameter("@AccountGroupID", model.AccountGroupID),
                    new SqlParameter("@Active", model.Active),
                    new SqlParameter("@Description", model.Description),
                    new SqlParameter("@PostOption", model.PostOption),
                    new SqlParameter("@DirectPost", model.DirectPost),
                    new SqlParameter("@CuryID", model.CuryID),
                    new SqlParameter("@BranchID", model.BranchID),
                    new SqlParameter("@GroupMask", model.GroupMask),
                    new SqlParameter("@RevalCuryRateTypeID", model.RevalCuryRateTypeID),
                    new SqlParameter("@TaxCategoryID", model.TaxCategoryID),
                    new SqlParameter("@CreatedUser", model.CreatedUser),
                    new SqlParameter("@CreatedByScreenID", model.CreatedByScreenID),
                    new SqlParameter("@CreatedDate", model.CreatedDate),
                    new SqlParameter("@GLConsolAccountCD", model.GLConsolAccountCD),
                    new SqlParameter("@RequireUnits", model.RequireUnits),
                    new SqlParameter("@NoSubDetail", model.NoSubDetail),
                    new SqlParameter("@DeletedDatabaseRecord", model.DeletedDatabaseRecord),
                    new SqlParameter("@isCashAccount", model.isCashAccount),
                    new SqlParameter("@NoteID", model.NoteID),

                };
        }

        public static int DeleteItem(Int32 itemID)
        {
            return SqlHelper.ExecuteNonQuery("Account_Delete", itemID);
        }
    }
}