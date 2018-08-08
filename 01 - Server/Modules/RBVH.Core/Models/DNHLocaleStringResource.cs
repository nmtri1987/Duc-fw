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
    public class DNHLocaleStringResource : BaseDBEntity
    {
        [DataMember]
        public int LocaleStringResourceID { get; set; }

        [DataMember]
        public int CompanyID { get; set; }

        [DataMember]
        public int LanguageID { get; set; }

        [DataMember]
        public string ResourceName { get; set; }

        [DataMember]
        public string ResourceValue { get; set; }

        [DataMember]
        public string CreatedUser { get; set; }

        [DataMember]
        public DateTime CreatedDate { get; set; }

    }
    public class DNHLocaleStringResourceCollection : BaseDBEntityCollection<DNHLocaleStringResource> { }
    public class DNHLocaleStringResourceManager : IServiceManager<DNHLocaleStringResource>
    {

        public static DNHLocaleStringResource GetByID(int ID,int CompanyID)
        {
            DNHLocaleStringResource item = new DNHLocaleStringResource();
            var sqlParams = new SqlParameter[]
                     {
                            new SqlParameter("@LocaleStringResourceID", ID),
                            new SqlParameter("@CompanyID", CompanyID),
                            

                     };
            using (var reader = SqlHelper.ExecuteReader("DNHLocaleStringResource_GetByLocaleStringResourceID", sqlParams))
            {
                item = CommonHelper.DataReaderToObject<DNHLocaleStringResource>(reader);

            }
            return item;
        }

        #region Standard of base API

        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public virtual DNHLocaleStringResource Get(GetParam value)
        {
            DNHLocaleStringResource item = new DNHLocaleStringResource();
            var sqlParams = new SqlParameter[]
                     {
                            new SqlParameter("@ResourceName", value.ID),
                            new SqlParameter("@CompanyID", value.CompanyID),
                            new SqlParameter("@LanguageID", value.RefID),
                            
                     };
            using (var reader = SqlHelper.ExecuteReader("DNHLocaleStringResource_GetByID", sqlParams))
            {
                item = CommonHelper.DataReaderToObject<DNHLocaleStringResource>(reader);

            }
            return item;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public virtual IEnumerable<DNHLocaleStringResource> GetSearch(SearchFilter value)
        {
            using (var reader = SqlHelper.ExecuteReader("DNHLocaleStringResource_Search", SearchFilterManager.SqlSearchDynParam(value)))
            {
                return CommonHelper.DataReaderToList<DNHLocaleStringResource>(reader);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public virtual DNHLocaleStringResource Add(DNHLocaleStringResource model)
        {
            string result = "";
            using (var reader = SqlHelper.ExecuteReader(CommandType.StoredProcedure, "DNHLocaleStringResource_Add", CreateSqlParameter(model)))
            {
                while (reader.Read())
                {
                    result = (string)reader[0];
                }
            }
            return Get(new GetParam() { ID = result, CompanyID = model.CompanyID,RefID= model.LanguageID.ToString() });
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public virtual DNHLocaleStringResource Update(DNHLocaleStringResource model)
        {
            Int32 result = 0;
            using (var reader = SqlHelper.ExecuteReader(CommandType.StoredProcedure, "DNHLocaleStringResource_Update", CreateSqlParameter(model)))
            {
                while (reader.Read())
                {
                    result = (Int32)reader[0];
                }
            }
            return Get(new GetParam() { ID = model.ResourceName.ToString(), CompanyID = model.CompanyID,RefID=model.LanguageID.ToString() });
            //return GetItemByID(result);

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public virtual int Del(GetParam value)
        {
            return SqlHelper.ExecuteNonQuery("DNHLocaleStringResource_Delete", value.ID);
        }
        #endregion
        private static SqlParameter[] CreateSqlParameter(DNHLocaleStringResource model)
        {
            return new SqlParameter[]
                {
                    new SqlParameter("@CompanyID", model.CompanyID),
                    new SqlParameter("@LanguageID", model.LanguageID),
                    new SqlParameter("@ResourceName", model.ResourceName),
                    new SqlParameter("@ResourceValue", model.ResourceValue),
                    new SqlParameter("@CreatedUser", model.CreatedUser),
                    new SqlParameter("@CreatedDate", model.CreatedDate),

                };
        }

    }
}