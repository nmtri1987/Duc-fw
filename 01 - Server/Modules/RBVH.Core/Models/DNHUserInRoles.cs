using System.Text;
using System.Data;
using System.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using DTP.Data;
using DTP.Core;
namespace RBVH.Core.Models
{

    [DataContract]
    public class DNHUserInRoles : BaseDBEntity
    {

        [DataMember]
        public int CompanyID { get; set; }

        [DataMember]
        public string UserID { get; set; }

        [DataMember]
        public string RoleName { get; set; }

        [DataMember]
        public string ApplicationName { get; set; }

        [DataMember]
        public string UserName { get; set; }

        [DataMember]
        public bool IsActive { get; set; }

        [DataMember]
        public DateTime CreatedDate { get; set; }

        [DataMember]
        public string CreatedUser { get; set; }

        [DataMember]
        public DateTime ModifiedDate { get; set; }

        [DataMember]
        public string ModifiedUser { get; set; }

    }
    public class DNHUserInRolesCollection : BaseDBEntityCollection<DNHUserInRoles> { }
    public class DNHUserInRolesManager :IServiceManager<DNHUserInRoles>
    {
        #region Standard of base API

        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public virtual DNHUserInRoles Get(GetParam value)
        {
            DNHUserInRoles item = new DNHUserInRoles();
            string RoleName = "";
            //if (value.ParaList.Count > 0)
            //{
            //    RoleName = value.ParaList[0].Values;
            //}
            var sqlParams = new SqlParameter[]
                     {
                            new SqlParameter("@UserID", new Guid(value.ID)),
                            new SqlParameter("@CompanyID", value.CompanyID),
                            new SqlParameter("@RoleName", value.RefID),
                     };
            using (var reader = SqlHelper.ExecuteReaderService(ModuleConfig.MyConnection, "DNHUserInRoles_GetByID", sqlParams))
            {
                item = CommonHelper.DataReaderToObject<DNHUserInRoles>(reader);

            }
            return item;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public virtual IEnumerable<DNHUserInRoles> GetSearch(SearchFilter value)
        {
            using (var reader = SqlHelper.ExecuteReaderService(ModuleConfig.MyConnection, "DNHUserInRoles_Search", SearchFilterManager.SqlSearchDynParam(value)))
            {
                return CommonHelper.DataReaderToList<DNHUserInRoles>(reader);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public virtual DNHUserInRoles Add(DNHUserInRoles model)
        {
            string result = "";
            using (var reader = SqlHelper.ExecuteReaderService(ModuleConfig.MyConnection, CommandType.StoredProcedure, "DNHUserInRoles_Add", CreateSqlParameter(model)))
            {
                while (reader.Read())
                {
                    result = (string)reader[0];
                }
            }
            return Get(new GetParam() { ID = model.UserID, CompanyID = model.CompanyID, RefID=model.RoleName, ParaList = new List<GetParamObject>() { new GetParamObject() { Field = "RoleName", Values = model.RoleName } } });
           // return Get(new GetParam() { ID = result, CompanyID = model.CompanyID });
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public virtual DNHUserInRoles Update(DNHUserInRoles model)
        {
            Int32 result = 0;
            using (var reader = SqlHelper.ExecuteReaderService(ModuleConfig.MyConnection, CommandType.StoredProcedure, "DNHUserInRoles_Update", CreateSqlParameter(model)))
            {
                while (reader.Read())
                {
                    result = (Int32)reader[0];
                }
            }
            return model;
        //    return Get(new GetParam() { ID = model.UserID, CompanyID = model.CompanyID, ParaList = new List<GetParamObject>() { new GetParamObject() { Field = "RoleName", Values = model.RoleName } } } );
            //return GetItemByID(result);

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public virtual int Del(GetParam value)
        {
            string RoleName = "";
            if (value.ParaList.Count > 0)
            {
                RoleName = value.ParaList[0].Values;
            }
            return SqlHelper.ExecuteNonQuery(ModuleConfig.MyConnection, "DNHUserInRoles_Delete", new SqlParameter[]
                     {
                            new SqlParameter("@UserID", value.ID),
                            new SqlParameter("@CompanyID", value.CompanyID),
                            new SqlParameter("@RoleName", RoleName),
                     });
        }

        #endregion
        private static SqlParameter[] CreateSqlParameter(DNHUserInRoles model)
        {
            return new SqlParameter[]
                {
                    new SqlParameter("@CompanyID", model.CompanyID),
                    new SqlParameter("@UserID", model.UserID),
                    new SqlParameter("@RoleName", model.RoleName),
                    new SqlParameter("@ApplicationName", model.ApplicationName),
                    new SqlParameter("@UserName", model.UserName),
                    new SqlParameter("@IsActive", model.IsActive),
                    new SqlParameter("@CreatedDate", model.CreatedDate),
                    new SqlParameter("@CreatedUser", model.CreatedUser),
                    new SqlParameter("@ModifiedDate", model.ModifiedDate),
                    new SqlParameter("@ModifiedUser", model.ModifiedUser),

                };
        }


    }

}
