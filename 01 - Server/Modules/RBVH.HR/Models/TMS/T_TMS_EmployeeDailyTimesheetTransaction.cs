using System.Data;
using System.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using DTP.Data;
using DTP.Core;
namespace RBVH.HR
{
    [DataContract]
    public class T_TMS_EmployeeDailyTimesheetTransaction : BaseDBEntity
    {

        [DataMember]
        public int CompanyID { get; set; }

        [DataMember]
        public Guid ID { get; set; }

        [DataMember]
        public Guid RefNo { get; set; }

        [DataMember]
        public DateTime? DateID { get; set; }

        [DataMember]
        public bool IsWeekend { get; set; }

        [DataMember]
        public bool IsHoliday { get; set; }

        [DataMember]
        public int EmployeeCode { get; set; }

        [DataMember]
        public bool IsScan { get; set; }

        [DataMember]
        public DateTime? RawIn { get; set; }

        [DataMember]
        public DateTime? RawOut { get; set; }

        [DataMember]
        public DateTime? ManualIn { get; set; }

        [DataMember]
        public DateTime? ManualOut { get; set; }

        [DataMember]
        public double TotalWorkedHour { get; set; }

        [DataMember]
        public int KowID { get; set; }

        [DataMember]
        public double DayNum { get; set; }

        [DataMember]
        public int DowID { get; set; }

        [DataMember]
        public string ScreenID { get; set; }

        [DataMember]
        public DateTime CreatedDate { get; set; }

        [DataMember]
        public string CreatedUser { get; set; }

        [DataMember]
        public DateTime ModifiedDate { get; set; }

        [DataMember]
        public string ModifiedUser { get; set; }

    }
    public class T_TMS_EmployeeDailyTimesheetTransactionCollection : BaseDBEntityCollection<T_TMS_EmployeeDailyTimesheetTransaction> { }
    public class T_TMS_EmployeeDailyTimesheetTransactionManager : IServiceManager<T_TMS_EmployeeDailyTimesheetTransaction>
    {
        #region Standard of base API

        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public virtual T_TMS_EmployeeDailyTimesheetTransaction Get(GetParam value)
        {
            T_TMS_EmployeeDailyTimesheetTransaction item = new T_TMS_EmployeeDailyTimesheetTransaction();
            var sqlParams = new SqlParameter[]
                     {
                            new SqlParameter("@ID", value.ID),
                            new SqlParameter("@CompanyID", value.CompanyID),
                     };
            using (var reader = SqlHelper.ExecuteReader("T_TMS_EmployeeDailyTimesheetTransaction_GetByID", sqlParams))
            {
                item = CommonHelper.DataReaderToObject<T_TMS_EmployeeDailyTimesheetTransaction>(reader);

            }
            return item;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public virtual IEnumerable<T_TMS_EmployeeDailyTimesheetTransaction> GetSearch(SearchFilter value)
        {
            using (var reader = SqlHelper.ExecuteReader("T_TMS_EmployeeDailyTimesheetTransaction_Search", SearchFilterManager.SqlSearchDynParam(value)))
            {
                return CommonHelper.DataReaderToList<T_TMS_EmployeeDailyTimesheetTransaction>(reader);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public virtual T_TMS_EmployeeDailyTimesheetTransaction Add(T_TMS_EmployeeDailyTimesheetTransaction model)
        {
            string result = "";
            using (var reader = SqlHelper.ExecuteReader(CommandType.StoredProcedure, "T_TMS_EmployeeDailyTimesheetTransaction_Add", CreateSqlParameter(model)))
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
        public virtual T_TMS_EmployeeDailyTimesheetTransaction Update(T_TMS_EmployeeDailyTimesheetTransaction model)
        {
            Int32 result = 0;
            using (var reader = SqlHelper.ExecuteReader(CommandType.StoredProcedure, "T_TMS_EmployeeDailyTimesheetTransaction_Update", CreateSqlParameter(model)))
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
            return SqlHelper.ExecuteNonQuery("T_TMS_EmployeeDailyTimesheetTransaction_Delete", value.ID);
        }
        #endregion
        private static SqlParameter[] CreateSqlParameter(T_TMS_EmployeeDailyTimesheetTransaction model)
        {
            return new SqlParameter[]
                {
                    new SqlParameter("@CompanyID", model.CompanyID),
                    new SqlParameter("@ID", model.ID),
                    new SqlParameter("@RefNo", model.RefNo),
                    new SqlParameter("@DateID", model.DateID),
                    new SqlParameter("@IsWeekend", model.IsWeekend),
                    new SqlParameter("@IsHoliday", model.IsHoliday),
                    new SqlParameter("@EmployeeCode", model.EmployeeCode),
                    new SqlParameter("@IsScan", model.IsScan),
                    new SqlParameter("@RawIn", model.RawIn),
                    new SqlParameter("@RawOut", model.RawOut),
                    new SqlParameter("@ManualIn", model.ManualIn),
                    new SqlParameter("@ManualOut", model.ManualOut),
                    new SqlParameter("@TotalWorkedHour", model.TotalWorkedHour),
                    new SqlParameter("@KowID", model.KowID),
                    new SqlParameter("@DayNum", model.DayNum),
                    new SqlParameter("@DowID", model.DowID),
                    new SqlParameter("@ScreenID", model.ScreenID),
                    new SqlParameter("@CreatedDate", model.CreatedDate),
                    new SqlParameter("@CreatedUser", model.CreatedUser),
                    new SqlParameter("@ModifiedDate", model.ModifiedDate),
                    new SqlParameter("@ModifiedUser", model.ModifiedUser),

                };
        }

    }
}