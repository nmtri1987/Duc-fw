using System.Data;
using System.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using DTP.Data;
using DTP.Core;
namespace Biz.CS
{
    [DataContract]
    public class DNHUsers : BaseDBEntity
    {

        [DataMember]
        public int CompanyID { get; set; }

        [DataMember]
        public string Id { get; set; }

        [DataMember]
        public string Email { get; set; }

        [DataMember]
        public bool EmailConfirmed { get; set; }

        [DataMember]
        public string PasswordHash { get; set; }

        [DataMember]
        public string SecurityStamp { get; set; }

        [DataMember]
        public string PhoneNumber { get; set; }

        [DataMember]
        public bool PhoneNumberConfirmed { get; set; }

        [DataMember]
        public bool TwoFactorEnabled { get; set; }

        [DataMember]
        public DateTime? LockoutEndDateUtc { get; set; }

        [DataMember]
        public bool LockoutEnabled { get; set; }

        [DataMember]
        public int AccessFailedCount { get; set; }

        [DataMember]
        public string UserName { get; set; }

        [DataMember]
        public string CreatedUser { get; set; }

        [DataMember]
        public DateTime? CreatedDate { get; set; }

        [DataMember]
        public bool IsAdmin { get; set; }

        [DataMember]
        public string Application { get; set; }


    }
    public class DNHUsersCollection : BaseDBEntityCollection<DNHUsers> { }
    public class DNHUsersManager : IServiceManager<DNHUsers>
    {
        #region Standard of base API

        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public virtual DNHUsers Get(GetParam value)
        {
            DNHUsers item = new DNHUsers();
            var sqlParams = new SqlParameter[]
                     {
                            new SqlParameter("@Id", value.ID),
                            new SqlParameter("@CompanyID", value.CompanyID),
                     };
            using (var reader = SqlHelper.ExecuteReader("DNHUsers_GetByID", sqlParams))
            {
                item = CommonHelper.DataReaderToObject<DNHUsers>(reader);

            }
            return item;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public virtual IEnumerable<DNHUsers> GetSearch(SearchFilter value)
        {
            using (var reader = SqlHelper.ExecuteReader("DNHUsers_Search", SearchFilterManager.SqlSearchDynParam(value)))
            {
                return CommonHelper.DataReaderToList<DNHUsers>(reader);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public virtual DNHUsers Add(DNHUsers model)
        {
            string result = "";
            using (var reader = SqlHelper.ExecuteReader(CommandType.StoredProcedure, "DNHUsers_Add", CreateSqlParameter(model)))
            {
                while (reader.Read())
                {
                    result = (string)reader[0];
                }
            }
            return Get(new GetParam() { ID = result, CompanyID = model.CompanyID });
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public virtual DNHUsers Update(DNHUsers model)
        {
            Int32 result = 0;
            using (var reader = SqlHelper.ExecuteReader(CommandType.StoredProcedure, "DNHUsers_Update", CreateSqlParameter(model)))
            {
                while (reader.Read())
                {
                    result = (Int32)reader[0];
                }
            }
            return Get(new GetParam() { ID = model.Id.ToString(), CompanyID = model.CompanyID });
            //return GetItemByID(result);

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public virtual int Del(GetParam value)
        {
            return SqlHelper.ExecuteNonQuery("DNHUsers_Delete", value.ID);
        }
        #endregion
        private static SqlParameter[] CreateSqlParameter(DNHUsers model)
        {
            return new SqlParameter[]
                {
                    new SqlParameter("@CompanyID", model.CompanyID),
                    new SqlParameter("@Id", model.Id),
                    new SqlParameter("@Email", model.Email),
                    new SqlParameter("@EmailConfirmed", model.EmailConfirmed),
                    new SqlParameter("@PasswordHash", model.PasswordHash),
                    new SqlParameter("@SecurityStamp", model.SecurityStamp),
                    new SqlParameter("@PhoneNumber", model.PhoneNumber),
                    new SqlParameter("@PhoneNumberConfirmed", model.PhoneNumberConfirmed),
                    new SqlParameter("@TwoFactorEnabled", model.TwoFactorEnabled),
                    new SqlParameter("@LockoutEndDateUtc", model.LockoutEndDateUtc),
                    new SqlParameter("@LockoutEnabled", model.LockoutEnabled),
                    new SqlParameter("@AccessFailedCount", model.AccessFailedCount),
                    new SqlParameter("@UserName", model.UserName),
                    new SqlParameter("@CreatedUser", model.CreatedUser),
                    new SqlParameter("@CreatedDate", model.CreatedDate),
                    new SqlParameter("@IsAdmin", model.IsAdmin),
                    new SqlParameter("@Application", model.Application),

                };
        }

    }
}