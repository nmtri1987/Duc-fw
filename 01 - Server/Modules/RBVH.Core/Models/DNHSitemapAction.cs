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
    public class DNHSitemapAction : BaseDBEntity
    {
       
	   [DataMember]public int CompanyID{ get; set; }
[DataMember]public int ID{ get; set; }
[DataMember]public string RoleName{ get; set; }
[DataMember]public Guid NodeID{ get; set; }
[DataMember]public string ActionName{ get; set; }
[DataMember]public bool Allow{ get; set; }
[DataMember]public bool Edit{ get; set; }
[DataMember]public string CreatedUser{ get; set; }
[DataMember]public DateTime CreatedDate{ get; set; }
[DataMember]public string ScreenID{ get; set; }


    }
    public class DNHSitemapActionCollection : BaseDBEntityCollection<DNHSitemapAction> { }
	public class DNHSitemapActionManager : IServiceManager<DNHSitemapAction>
	{
		 #region Standard of base API

        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public virtual DNHSitemapAction Get(GetParam value)
        {
            DNHSitemapAction item = new DNHSitemapAction();
            var sqlParams = new SqlParameter[]
                     {
                            new SqlParameter("@RoleName", value.ID),
                            new SqlParameter("@CompanyID", value.CompanyID),
                            new SqlParameter("@ActionName", value.RefID),
                     };
            using (var reader = SqlHelper.ExecuteReader("DNHSitemapAction_GetUnitAction_ID", sqlParams))
            {
                item = CommonHelper.DataReaderToObject<DNHSitemapAction>(reader);
                
            }
            return item;
        }

        public static IEnumerable<DNHSitemapAction> GetDataByID(int ID, int CompanyID)
        {
            var sqlParams = new SqlParameter[]
                     {
                            new SqlParameter("@ID", ID),
                            new SqlParameter("@CompanyID", CompanyID),
                     };
            using (var reader = SqlHelper.ExecuteReader("DNHSitemapAction_GetByID", sqlParams))
            {
                return CommonHelper.DataReaderToList<DNHSitemapAction>(reader);
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public virtual IEnumerable<DNHSitemapAction> GetSearch(SearchFilter value)
        {
            using (var reader = SqlHelper.ExecuteReader("DNHSitemapAction_Search", SearchFilterManager.SqlSearchDynParam(value)))
            {
                return CommonHelper.DataReaderToList<DNHSitemapAction>(reader);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public virtual DNHSitemapAction Add(DNHSitemapAction model)
        {
            int result = 0;
            DNHSitemapAction myaction = Get(new GetParam() { ID = model.RoleName, CompanyID = model.CompanyID, RefID = model.ActionName });
            if (myaction!=null)
            {
                using (var reader = SqlHelper.ExecuteReader(CommandType.StoredProcedure, "DNHSitemapAction_Add", CreateSqlParameter(model)))
                {
                    while (reader.Read())
                    {
                        result = (int)reader[0];
                    }
                }
                return Get(new GetParam() { ID = model.RoleName, CompanyID = model.CompanyID, RefID = model.ActionName });
            }
            return myaction;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public virtual DNHSitemapAction Update(DNHSitemapAction model)
        {
            Int32 result = 0;
            using (var reader = SqlHelper.ExecuteReader(CommandType.StoredProcedure, "DNHSitemapAction_Update", CreateSqlParameter(model)))
            {
                while (reader.Read())
                {
                    result = (Int32)reader[0];
                }
            }
            return model;
            //return Get(new GetParam() { ID = model.ID.ToString(), CompanyID = model.CompanyID });
            //return GetItemByID(result);

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public virtual int Del(GetParam value)
        {
            return SqlHelper.ExecuteNonQuery("DNHSitemapAction_Delete", value.ID);
        }
        #endregion
        #region extend method
        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static DNHSitemapAction Get(DNHSitemapAction model)
        {
            DNHSitemapAction item = new DNHSitemapAction();
            var sqlParams = new SqlParameter[]
                     {
                            new SqlParameter("@CompanyID", model.CompanyID),
                            new SqlParameter("@RoleName", model.RoleName),
                            new SqlParameter("@NodeID", model.NodeID),
                            new SqlParameter("@ActionName", model.ActionName),
                     };
            using (var reader = SqlHelper.ExecuteReader("DNHSitemapAction_GetBy_Action", sqlParams))
            {
                item = DTP.Core.CommonHelper.DataReaderToObject<DNHSitemapAction>(reader);

            }
            return item;
        }
        #endregion
        private static SqlParameter[] CreateSqlParameter(DNHSitemapAction model)
			{
				return new SqlParameter[]
					{
					new SqlParameter("@CompanyID", model.CompanyID),
					new SqlParameter("@ID", model.ID),
					new SqlParameter("@RoleName", model.RoleName),
					new SqlParameter("@NodeID", model.NodeID),
					new SqlParameter("@ActionName", model.ActionName),
					new SqlParameter("@Allow", model.Allow),
					new SqlParameter("@Edit", model.Edit),
					new SqlParameter("@CreatedUser", model.CreatedUser),
					new SqlParameter("@CreatedDate", model.CreatedDate),
					new SqlParameter("@ScreenID", model.ScreenID),
					
					};
			}

		}
	}