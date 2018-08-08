using System.Data;
using System.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using DTP.Data;
using DTP.Core;

namespace RBVH.Core.Models
{
    public class DNHLanguage : BaseDBEntity
    {
        [DataMember]
        public int CompanyID { get; set; }
        [DataMember]
        public int LanguageID { get; set; }
        [DataMember]
        public string Name { get; set; }
        [DataMember]
        public string LanguageCulture { get; set; }
        [DataMember]
        public bool Published { get; set; }
        [DataMember]
        public int DisplayOrder { get; set; }
        [DataMember]
        public string CreatedUser { get; set; }
        [DataMember]
        public DateTime? CreatedDate { get; set; }
    }
    public class DNHLanguageCollection : BaseDBEntityCollection<DNHLanguage> { }

    public class DNHLanguageManager : IServiceManager<DNHLanguage>
    {
        #region Standard of base API

        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public virtual DNHLanguage Get(GetParam value)
        {
            DNHLanguage item = new DNHLanguage();
            var sqlParams = new SqlParameter[]
                     {
                            new SqlParameter("@LanguageID", value.ID),
                            new SqlParameter("@CompanyID", value.CompanyID),
                     };
            using (var reader = SqlHelper.ExecuteReader("DNHLanguage_GetByID", sqlParams))
            {
                item = CommonHelper.DataReaderToObject<DNHLanguage>(reader);

            }
            return item;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public virtual IEnumerable<DNHLanguage> GetSearch(SearchFilter value)
        {
            using (var reader = SqlHelper.ExecuteReader("DNHLanguage_Search", SearchFilterManager.SqlSearchDynParam(value)))
            {
                return CommonHelper.DataReaderToList<DNHLanguage>(reader);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public virtual DNHLanguage Add(DNHLanguage model)
        {
            string result = "";
            using (var reader = SqlHelper.ExecuteReader(CommandType.StoredProcedure, "DNHLanguage_Add", CreateSqlParameter(model)))
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
        public virtual DNHLanguage Update(DNHLanguage model)
        {
            Int32 result = 0;
            using (var reader = SqlHelper.ExecuteReader(CommandType.StoredProcedure, "DNHLanguage_Update", CreateSqlParameter(model)))
            {
                while (reader.Read())
                {
                    result = (Int32)reader[0];
                }
            }
            return Get(new GetParam() { ID = model.LanguageID.ToString(), CompanyID = model.CompanyID });
            //return GetItemByID(result);

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public virtual int Del(GetParam value)
        {
            return SqlHelper.ExecuteNonQuery("DNHLanguage_Delete", value.ID);
        }

        #endregion

        private static SqlParameter[] CreateSqlParameter(DNHLanguage model)
        {
            return new SqlParameter[]
                {
                    new SqlParameter("@CompanyID", model.CompanyID),
                    new SqlParameter("@LanguageID", model.LanguageID),
                    new SqlParameter("@Name", model.Name),
                    new SqlParameter("@LanguageCulture", model.LanguageCulture),
                    new SqlParameter("@Published", model.Published),
                    new SqlParameter("@DisplayOrder", model.DisplayOrder),
                    new SqlParameter("@CreatedUser", model.CreatedUser),
                    new SqlParameter("@CreatedDate", model.CreatedDate),


                };
        }
    }
}
