using System.Text;
using System.Data;
using System.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using DTP.Data;
namespace DTP.Object
{
    [DataContract]
    public class UserInfo : BaseDBEntity
    {
        public UserInfo()
        {
            this.userLogin = new UserLogin();
        }
        [DataMember]
        public string UserId { get; set; }

        [DataMember]
        public int CompanyID { get; set; }

        [DataMember]
        public int BranchID { get; set; }

        [DataMember]
        public string UserName { get; set; }

        [DataMember]
        public string Mobile { get; set; }

        [DataMember]
        public int CountryId { get; set; }

        [DataMember]
        public int LocationId { get; set; }

        [DataMember]
        public string Address { get; set; }

        [DataMember]
        public int Rating { get; set; }

        [DataMember]
        public bool Sex { get; set; }

        [DataMember]
        public string ImgUrl { get; set; }

        [DataMember]
        public string facebookId { get; set; }

        [DataMember]
        public string twitterid { get; set; }

        [DataMember]
        public bool linkFB { get; set; }

        [DataMember]
        public string SkypeId { get; set; }

        [DataMember]
        public bool linkLinken { get; set; }

        [DataMember]
        public bool linkTwitter { get; set; }

        [DataMember]
        public string Linkenid { get; set; }

        [DataMember]
        public string Position { get; set; }

        [DataMember]
        public string PhoneNumber { get; set; }

        [DataMember]
        public string WebsiteUrl { get; set; }

        [DataMember]
        public string Notes { get; set; }

        [DataMember]
        public string cmnd { get; set; }

        [DataMember]
        public bool isActive { get; set; }

        [DataMember]
        public string FirstName { get; set; }

        [DataMember]
        public string LastName { get; set; }

        [DataMember]
        public string FullName { get; set; }

        [DataMember]
        public string Curyid { get; set; }

        [DataMember]
        public UserLogin userLogin { get; set; }

        [DataMember]
        public bool isNonAutoLogout { get; set; }

        [DataMember]
        public bool isViewAllCompanyReport { get; set; }
        
    }
    public class UserLogin
    {

        public string UserName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
    }
    public class UserInfoCollection : BaseDBEntityCollection<UserInfo> { }
    public class UserInfoManager
    {
        private static UserInfo GetItemFromReader(IDataReader dataReader)
        {
            UserInfo objItem = new UserInfo();

            objItem.UserId = SqlHelper.GetString(dataReader, "UserId");

            objItem.CompanyID = SqlHelper.GetInt(dataReader, "CompanyID");

            objItem.BranchID = SqlHelper.GetInt(dataReader, "BranchID");

            objItem.UserName = SqlHelper.GetString(dataReader, "UserName");

            objItem.Mobile = SqlHelper.GetString(dataReader, "Mobile");

            objItem.CountryId = SqlHelper.GetInt(dataReader, "CountryId");

            objItem.LocationId = SqlHelper.GetInt(dataReader, "LocationId");

            objItem.Address = SqlHelper.GetString(dataReader, "Address");

            objItem.Rating = SqlHelper.GetInt(dataReader, "Rating");

            objItem.Sex = SqlHelper.GetBoolean(dataReader, "Sex");

            objItem.ImgUrl = SqlHelper.GetString(dataReader, "ImgUrl");

            objItem.facebookId = SqlHelper.GetString(dataReader, "facebookId");

            objItem.twitterid = SqlHelper.GetString(dataReader, "twitterid");

            objItem.linkFB = SqlHelper.GetBoolean(dataReader, "linkFB");

            objItem.SkypeId = SqlHelper.GetString(dataReader, "SkypeId");

            objItem.linkLinken = SqlHelper.GetBoolean(dataReader, "linkLinken");

            objItem.linkTwitter = SqlHelper.GetBoolean(dataReader, "linkTwitter");

            objItem.Linkenid = SqlHelper.GetString(dataReader, "Linkenid");

            objItem.Position = SqlHelper.GetString(dataReader, "Position");

            objItem.PhoneNumber = SqlHelper.GetString(dataReader, "PhoneNumber");

            objItem.WebsiteUrl = SqlHelper.GetString(dataReader, "WebsiteUrl");

            objItem.Notes = SqlHelper.GetString(dataReader, "Notes");

            objItem.cmnd = SqlHelper.GetString(dataReader, "cmnd");

            objItem.isActive = SqlHelper.GetBoolean(dataReader, "isActive");

            objItem.FirstName = SqlHelper.GetString(dataReader, "FirstName");

            objItem.LastName = SqlHelper.GetString(dataReader, "LastName");

            objItem.FullName = SqlHelper.GetString(dataReader, "FullName");

            objItem.Curyid = SqlHelper.GetString(dataReader, "Curyid");
            try { objItem.userLogin.Email = SqlHelper.GetString(dataReader, "Email"); } catch { }
            try { objItem.userLogin.Password = SqlHelper.GetString(dataReader, "Password"); } catch { }
            try { objItem.userLogin.UserName = SqlHelper.GetString(dataReader, "UserName"); } catch { }
            objItem.isNonAutoLogout = SqlHelper.GetBoolean(dataReader, "isNonAutoLogout");
            objItem.isViewAllCompanyReport = SqlHelper.GetBoolean(dataReader, "isViewAllCompanyReport");

            return objItem;
        }
        public static UserInfo GetItemByUserName(String UserName, int CompanyID)
        {
            UserInfo item = new UserInfo();
            var sqlParams = new SqlParameter[2];
            sqlParams[0] = new SqlParameter("@UserName", UserName);
            sqlParams[1] = new SqlParameter("@CompanyID", CompanyID);
            using (var reader = SqlHelper.ExecuteReader("tblUserInfo_GetByUserName", sqlParams))
            {
                while (reader.Read())
                {
                    item = GetItemFromReader(reader);
                }
            }
            return item;


        }
        public static UserInfo GetItemByID(string UserId, int CompanyID)
        {
            UserInfo item = new UserInfo();
            var sqlParams = new SqlParameter[]
                    {
                            new SqlParameter("@UserId", UserId),
                            new SqlParameter("@CompanyID", CompanyID),
                    };
            using (var reader = SqlHelper.ExecuteReader("tblUserInfo_GetByID", sqlParams))
            {
                while (reader.Read())
                {
                    item = GetItemFromReader(reader);
                }
            }
            return item;


        }
        public static UserInfo AddItem(UserInfo model)
        {
            String result = String.Empty;
            try
            {
                using (var reader = SqlHelper.ExecuteReader(CommandType.StoredProcedure, "tblUserInfo_Add", CreateSqlParameter(model)))
                {
                    while (reader.Read())
                    {
                        result = (String)reader[0];
                    }
                }
            }
            catch (Exception ObjEx)
            {

            }
            return GetItemByID(result, model.CompanyID);

        }
        public static UserInfo UpdateItem(UserInfo model)
        {
            String result = String.Empty;
            using (var reader = SqlHelper.ExecuteReader(CommandType.StoredProcedure, "tblUserInfo_Update", CreateSqlParameter(model)))
            {
                while (reader.Read())
                {
                    result = (String)reader[0];
                }
            }
            return GetItemByID(model.UserId, model.CompanyID);

        }
        public static UserInfoCollection GetAllItem(int CompanyID)
        {
            UserInfoCollection collection = new UserInfoCollection();

            var sqlParams = new SqlParameter[]
                    {
                            new SqlParameter("@CompanyID", CompanyID),
                    };
            using (var reader = SqlHelper.ExecuteReader("tblUserInfo_GetAll", sqlParams))
            {
                while (reader.Read())
                {
                    UserInfo obj = new UserInfo();
                    obj = GetItemFromReader(reader);
                    collection.Add(obj);
                }
            }
            return collection;
        }

        public static UserInfoCollection GetItembyEmpCD(int CompanyID,string EmpCD)
        {
            UserInfoCollection collection = new UserInfoCollection();

            var sqlParams = new SqlParameter[]
                    {
                            new SqlParameter("@CompanyID", CompanyID),
                            new SqlParameter("@EmpCD", EmpCD),
                    };
            using (var reader = SqlHelper.ExecuteReader("tblUserInfo_GetbyEmpCD", sqlParams))
            {
                while (reader.Read())
                {
                    UserInfo obj = new UserInfo();
                    obj = GetItemFromReader(reader);
                    collection.Add(obj);
                }
            }
            return collection;
        }

        public static UserInfoCollection Search(SearchFilter SearchKey)
        {
            UserInfoCollection collection = new UserInfoCollection();
            using (var reader = SqlHelper.ExecuteReader("UserInfo_Search", SearchFilterManager.SqlSearchParam(SearchKey)))
            {
                while (reader.Read())
                {
                    UserInfo obj = new UserInfo();
                    obj = GetItemFromReader(reader);
                    obj.userLogin.Password = "******";
                    collection.Add(obj);
                }
            }
            return collection;
        }
        public static UserInfoCollection GetbyUser(string CreatedUser, int CompanyID)
        {
            UserInfoCollection collection = new UserInfoCollection();
            UserInfo obj;
            var sqlParams = new SqlParameter[]
              {
                            new SqlParameter("@CreatedUser", CreatedUser),
                            new SqlParameter("@CompanyID", CompanyID),
              };
            using (var reader = SqlHelper.ExecuteReader("tblUserInfo_GetAll_byUser", sqlParams))
            {
                while (reader.Read())
                {
                    obj = GetItemFromReader(reader);
                    collection.Add(obj);
                }
            }
            return collection;
        }

        private static SqlParameter[] CreateSqlParameter(UserInfo model)
        {
            return new SqlParameter[]
                {
                    new SqlParameter("@UserId", model.UserId),
                    new SqlParameter("@CompanyID", model.CompanyID),
                    new SqlParameter("@BranchID", model.BranchID),
                    new SqlParameter("@UserName", model.UserName),
                    new SqlParameter("@Mobile", model.Mobile),
                    new SqlParameter("@CountryId", model.CountryId),
                    new SqlParameter("@LocationId", model.LocationId),
                    new SqlParameter("@Address", model.Address),
                    new SqlParameter("@Rating", model.Rating),
                    new SqlParameter("@Sex", model.Sex),
                    new SqlParameter("@ImgUrl", model.ImgUrl),
                    new SqlParameter("@facebookId", model.facebookId),
                    new SqlParameter("@twitterid", model.twitterid),
                    new SqlParameter("@linkFB", model.linkFB),
                    new SqlParameter("@SkypeId", model.SkypeId),
                    new SqlParameter("@linkLinken", model.linkLinken),
                    new SqlParameter("@linkTwitter", model.linkTwitter),
                    new SqlParameter("@Linkenid", model.Linkenid),
                    new SqlParameter("@Position", model.Position),
                    new SqlParameter("@PhoneNumber", model.PhoneNumber),
                    new SqlParameter("@WebsiteUrl", model.WebsiteUrl),
                    new SqlParameter("@Notes", model.Notes),
                    new SqlParameter("@cmnd", model.cmnd),
                    new SqlParameter("@isActive", model.isActive),
                    new SqlParameter("@FirstName", model.FirstName),
                    new SqlParameter("@LastName", model.LastName),
                    new SqlParameter("@FullName", model.FullName),
                    new SqlParameter("@Curyid", model.Curyid),
                      new SqlParameter("@isNonAutoLogout", model.isNonAutoLogout),
                      new SqlParameter("@isViewAllCompanyReport", model.isViewAllCompanyReport),
                };
        }

        public static int DeleteItem(string itemID, int CompanyID)
        {
            return SqlHelper.ExecuteNonQuery("tblUserInfo_Delete", new SqlParameter[]
            {
                new SqlParameter(@"UserId",itemID),
                    new SqlParameter("@CompanyID", CompanyID) });
        }
    }
}