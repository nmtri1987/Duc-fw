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
    public class DNHRoles : BaseDBEntity
    {
       
	   [DataMember]public int CompanyID{ get; set; }
[DataMember]public string Rolename{ get; set; }
[DataMember]public string ApplicationName{ get; set; }
[DataMember]public string Descr{ get; set; }
[DataMember]public bool isGuest{ get; set; }
[DataMember]public string Createduser{ get; set; }
[DataMember]public DateTime CreatedDate{ get; set; }


    }
    public class DNHRolesCollection : BaseDBEntityCollection<DNHRoles> { }
	public class DNHRolesManager : IServiceManager<DNHRoles>
	{
		 #region Standard of base API

        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public virtual DNHRoles Get(GetParam value)
        {
            DNHRoles item = new DNHRoles();
            var sqlParams = new SqlParameter[]
                     {
                            new SqlParameter("@Rolename", value.ID),
                            new SqlParameter("@CompanyID", value.CompanyID),
                     };
            using (var reader = SqlHelper.ExecuteReader("DNHRoles_GetByID", sqlParams))
            {
                item = CommonHelper.DataReaderToObject<DNHRoles>(reader);
             
            }
            return item;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public virtual IEnumerable<DNHRoles> GetSearch(SearchFilter value)
        {
            using (var reader = SqlHelper.ExecuteReader("DNHRoles_Search", SearchFilterManager.SqlSearchDynParam(value)))
            {
                return CommonHelper.DataReaderToList<DNHRoles>(reader);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public virtual DNHRoles Add(DNHRoles model)
        {
            string result = "";
            using (var reader = SqlHelper.ExecuteReader(CommandType.StoredProcedure, "DNHRoles_Add", CreateSqlParameter(model)))
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
        public virtual DNHRoles Update(DNHRoles model)
        {
            Int32 result = 0;
            using (var reader = SqlHelper.ExecuteReader(CommandType.StoredProcedure, "DNHRoles_Update", CreateSqlParameter(model)))
            {
                while (reader.Read())
                {
                    result = (Int32)reader[0];
                }
            }
            return Get(new GetParam() { ID = model.Rolename, CompanyID = model.CompanyID });
            //return GetItemByID(result);

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public virtual int Del(GetParam value)
        {
            return SqlHelper.ExecuteNonQuery("DNHRoles_Delete", value.ID);
        }
		#endregion
			private static SqlParameter[] CreateSqlParameter(DNHRoles model)
			{
				return new SqlParameter[]
					{
					new SqlParameter("@CompanyID", model.CompanyID),
					new SqlParameter("@Rolename", model.Rolename),
					new SqlParameter("@ApplicationName", model.ApplicationName),
					new SqlParameter("@Descr", model.Descr),
					new SqlParameter("@isGuest", model.isGuest),
					new SqlParameter("@Createduser", model.Createduser),
					new SqlParameter("@CreatedDate", model.CreatedDate),
					
					};
			}

		}
	}