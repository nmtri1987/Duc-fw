using System.Data;
using System.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using DTP.Data;
using DTP.Core;
namespace RBVH.Core
{
    [DataContract]
    public class DNHRoleSitemap : BaseDBEntity
    {

        [DataMember]
        public int CompanyID { get; set; }

        [DataMember]
        public string RoleName { get; set; }

        [DataMember]
        public Guid NodeID { get; set; }

        [DataMember]
        public int Access { get; set; }

        [DataMember]
        public string CreatedUser { get; set; }

        [DataMember]
        public DateTime CreateDate { get; set; }

    }
    public class DNHRoleSitemapCollection : BaseDBEntityCollection<DNHRoleSitemap> { }
    public class DNHRoleSitemapManager : IServiceManager<DNHRoleSitemap>
    {
        #region Standard of base API

        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public virtual DNHRoleSitemap Get(GetParam value)
        {
            DNHRoleSitemap item = new DNHRoleSitemap();
         
            var sqlParams = new SqlParameter[]
                     {
                            new SqlParameter("@NodeID", new Guid(value.ID)),
                            new SqlParameter("@RoleName",value.RefID),
                            new SqlParameter("@CompanyID", value.CompanyID),
                            
                     };
            using (var reader = SqlHelper.ExecuteReader("DNHRoleSitemap_GetByID", sqlParams))
            {
                item = CommonHelper.DataReaderToObject<DNHRoleSitemap>(reader);

            }
            return item;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public virtual IEnumerable<DNHRoleSitemap> GetSearch(SearchFilter value)
        {
            using (var reader = SqlHelper.ExecuteReader("DNHRoleSitemap_Search", SearchFilterManager.SqlSearchDynParam(value)))
            {
                return CommonHelper.DataReaderToList<DNHRoleSitemap>(reader);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public virtual DNHRoleSitemap Add(DNHRoleSitemap model)
        {
            string result = "";
            using (var reader = SqlHelper.ExecuteReader(CommandType.StoredProcedure, "DNHRoleSitemap_Add", CreateSqlParameter(model)))
            {
                while (reader.Read())
                {
                    result = (string)reader[0];
                }
            }
            return Get(new GetParam() { ID = result, CompanyID = model.CompanyID, RefID=model.RoleName });
         //   return Get(new GetParam() { ID = result, CompanyID = model.CompanyID });
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public virtual DNHRoleSitemap Update(DNHRoleSitemap model)
        {
            Int32 result = 0;
            using (var reader = SqlHelper.ExecuteReader(CommandType.StoredProcedure, "DNHRoleSitemap_Update", CreateSqlParameter(model)))
            {
                while (reader.Read())
                {
                    result = (Int32)reader[0];
                }
            }
            return model;
            //return Get(new GetParam() { ID = model.RoleName.ToString(), CompanyID = model.CompanyID });
            //return GetItemByID(result);

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public virtual int Del(GetParam value)
        {
            return SqlHelper.ExecuteNonQuery("DNHRoleSitemap_Delete", value.ID);
        }
        #endregion
        private static SqlParameter[] CreateSqlParameter(DNHRoleSitemap model)
        {
            return new SqlParameter[]
                {
                    new SqlParameter("@CompanyID", model.CompanyID),
                    new SqlParameter("@RoleName", model.RoleName),
                    new SqlParameter("@NodeID", model.NodeID),
                    new SqlParameter("@Access", model.Access),
                    new SqlParameter("@CreatedUser", model.CreatedUser),
                    new SqlParameter("@CreateDate", model.CreateDate),

                };
        }

    }
}