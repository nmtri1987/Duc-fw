using System.Data;
using System.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using DTP.Data;
using DTP.Core;
namespace Biz.TMS
{
    [DataContract]
    public class T_TMS_EmployeeTimesheetWeeklyDetails : BaseDBEntity
    {

        [DataMember]
        public int CompanyID { get; set; }

        [DataMember]
        public Guid ID { get; set; }

        [DataMember]
        public int EntityID { get; set; }

        [DataMember]
        public int WorkingTimeGroupID { get; set; }

        [DataMember]
        public DateTime WorkDate { get; set; }

        [DataMember]
        public string DateName { get; set; }

        [DataMember]
        public int WeekNo { get; set; }

        [DataMember]
        public int YearNo { get; set; }

        [DataMember]
        public string FinPeriod { get; set; }

        [DataMember]
        public int EmployeeCode { get; set; }

        [DataMember]
        public string EmployeeNo { get; set; }

        [DataMember]
        public int DowID { get; set; }

        [DataMember]
        public string In_Out { get; set; }

        [DataMember]
        public float StandardWorkedHour { get; set; }

        [DataMember]
        public float WorkedHour { get; set; }

        [DataMember]
        public string ND { get; set; }

        [DataMember]
        public string OT { get; set; }

        [DataMember]
        public string Leave { get; set; }

        [DataMember]
        public string LC_UnNoReg { get; set; }

        [DataMember]
        public int StatusID { get; set; }

        [DataMember]
        public bool IsRelease { get; set; }

        [DataMember]
        public DateTime CreatedDate { get; set; }

        [DataMember]
        public string CreateUser { get; set; }

        [DataMember]
        public DateTime ModifiedDate { get; set; }

        [DataMember]
        public string ModifiedUser { get; set; }


    }
    public class T_TMS_EmployeeTimesheetWeeklyDetailsCollection : BaseDBEntityCollection<T_TMS_EmployeeTimesheetWeeklyDetails> { }

    public class EMPWeelFilter
    {
       public int EmployeeCode { get; set; }
       public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }
    }
    public class T_TMS_EmployeeTimesheetWeeklyDetailsManager : IServiceManager<T_TMS_EmployeeTimesheetWeeklyDetails>
    {
        #region Standard of base API

        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public virtual T_TMS_EmployeeTimesheetWeeklyDetails Get(GetParam value)
        {
            T_TMS_EmployeeTimesheetWeeklyDetails item = new T_TMS_EmployeeTimesheetWeeklyDetails();
            var sqlParams = new SqlParameter[]
                     {
                            new SqlParameter("@ID", value.ID),
                            new SqlParameter("@CompanyID", value.CompanyID),
                     };
            using (var reader = SqlHelper.ExecuteReader("T_TMS_EmployeeTimesheetWeeklyDetails_GetByID", sqlParams))
            {
                item = CommonHelper.DataReaderToObject<T_TMS_EmployeeTimesheetWeeklyDetails>(reader);

            }
            return item;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public virtual IEnumerable<T_TMS_EmployeeTimesheetWeeklyDetails> GetSearch(SearchFilter value)
        {
            using (var reader = SqlHelper.ExecuteReader("T_TMS_EmployeeTimesheetWeeklyDetails_Search", SearchFilterManager.SqlSearchDynParam(value)))
            {
                return CommonHelper.DataReaderToList<T_TMS_EmployeeTimesheetWeeklyDetails>(reader);
            }
        }

        public static DataTable EmployeeWeeklyReport(EMPWeelFilter value)
        {
            DataTable dt = new DataTable();
            //DataTable data = new DataTable();
            var sqlParams = new SqlParameter[]
                  {
                            new SqlParameter("@EmployeeCode", value.EmployeeCode),
                            new SqlParameter("@FromDate", value.FromDate),
                            new SqlParameter("@ToDate", value.ToDate),
                  };
            //@EntityID = 10002, @FromDate = '2017-05-16', @ToDate = '2017-06-14', @OrderBy = 'EmployeeNo', @OrderDirection = 'DESC'
            DataSet ds = SqlHelper.ExecuteDataset(QTConfig.MyConnection, "USP_TMS_EmployeeTimesheetWeekly_GetByEmployee", 180, sqlParams);
            if (ds.Tables.Count > 0)
            {
                dt = ds.Tables[0];
            }
            return dt;
        }
     
        /// <summary>
        /// 
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public virtual T_TMS_EmployeeTimesheetWeeklyDetails Add(T_TMS_EmployeeTimesheetWeeklyDetails model)
        {
            string result = "";
            using (var reader = SqlHelper.ExecuteReader(CommandType.StoredProcedure, "T_TMS_EmployeeTimesheetWeeklyDetails_Add", CreateSqlParameter(model)))
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
        public virtual T_TMS_EmployeeTimesheetWeeklyDetails Update(T_TMS_EmployeeTimesheetWeeklyDetails model)
        {
            Int32 result = 0;
            using (var reader = SqlHelper.ExecuteReader(CommandType.StoredProcedure, "T_TMS_EmployeeTimesheetWeeklyDetails_Update", CreateSqlParameter(model)))
            {
                while (reader.Read())
                {
                    result = (Int32)reader[0];
                }
            }
            return Get(new GetParam() { ID = model.ID.ToString(), CompanyID = model.CompanyID });
            //return GetItemByID(result);

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public virtual int Del(GetParam value)
        {
            return SqlHelper.ExecuteNonQuery("T_TMS_EmployeeTimesheetWeeklyDetails_Delete", value.ID);
        }
        #endregion
        private static SqlParameter[] CreateSqlParameter(T_TMS_EmployeeTimesheetWeeklyDetails model)
        {
            return new SqlParameter[]
                {
                    new SqlParameter("@CompanyID", model.CompanyID),
                    new SqlParameter("@ID", model.ID),
                    new SqlParameter("@EntityID", model.EntityID),
                    new SqlParameter("@WorkingTimeGroupID", model.WorkingTimeGroupID),
                    new SqlParameter("@WorkDate", model.WorkDate),
                    new SqlParameter("@DateName", model.DateName),
                    new SqlParameter("@WeekNo", model.WeekNo),
                    new SqlParameter("@YearNo", model.YearNo),
                    new SqlParameter("@FinPeriod", model.FinPeriod),
                    new SqlParameter("@EmployeeCode", model.EmployeeCode),
                    new SqlParameter("@EmployeeNo", model.EmployeeNo),
                    new SqlParameter("@DowID", model.DowID),
                    new SqlParameter("@In_Out", model.In_Out),
                    new SqlParameter("@StandardWorkedHour", model.StandardWorkedHour),
                    new SqlParameter("@WorkedHour", model.WorkedHour),
                    new SqlParameter("@ND", model.ND),
                    new SqlParameter("@OT", model.OT),
                    new SqlParameter("@Leave", model.Leave),
                    new SqlParameter("@LC_UnNoReg", model.LC_UnNoReg),
                    new SqlParameter("@StatusID", model.StatusID),
                    new SqlParameter("@IsRelease", model.IsRelease),
                    new SqlParameter("@CreatedDate", model.CreatedDate),
                    new SqlParameter("@CreateUser", model.CreateUser),
                    new SqlParameter("@ModifiedDate", model.ModifiedDate),
                    new SqlParameter("@ModifiedUser", model.ModifiedUser),

                };
        }

    }
}