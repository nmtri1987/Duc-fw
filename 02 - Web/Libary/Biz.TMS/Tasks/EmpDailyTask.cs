using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Biz.Core.Services.Tasks;
using Biz.TMS.Services;
using Biz.TMS;
namespace Biz.TMS.Tasks
{
    public partial class EmpDailyTask : ITask
    {
        public virtual void Execute()
        {
            try
            {
                SearchFilter SearchKey = new SearchFilter();
                DateTime CurrentDate = SystemConfig.CurrentDate.AddDays(-1);
                SearchKey.ColumnsName = "EmployeeCode,EntityID,DateID";
                SearchKey.CompanyID = 1;
                SearchKey.Page = 1;
                SearchKey.PageSize = 10;
                SearchKey.OrderBy = "EmployeeCode";
                SearchKey.OrderDirection = "Desc";
            

                SearchKey.Condition = "  DateID='"+CurrentDate.ToString("yyyy-MM-dd")+"' ";
               // SearchKey.Condition = "  DateID='2017-09-13' ";
                T_TMS_EmployeeDailyTimesheetTransactionCollection obj;
                Models.EmpTaskFilter filter = new Models.EmpTaskFilter();
                filter.DateID = CurrentDate;
                
                for (int i = 10001; i < 10005; i++)
                {
                    SearchKey.Keyword = i.ToString();
                    obj= T_TMS_EmployeeDailyTimesheetTransactionManager.Search(SearchKey);
                    if (obj.Count == 0)
                    {
                        //insert day
                        filter.EntityID = i;
                        filter.WorkingTimeGroupID = null;
                        if (i == 10003)
                        {
                            filter.WorkingTimeGroupID = 1;
                        }
                        
                        EmpTaskManager.EmployeeGroup_Daily_Add(filter);
                    }
                }
                T_TMS_EmployeeTimesheetWeeklyDetailsCollection mycol;
                SearchKey.Condition = "  WorkDate='" + CurrentDate.ToString("yyyy-MM-dd") + "' ";
                for (int i = 10001; i < 10005; i++)
                {
                    SearchKey.Keyword = i.ToString();
                    mycol = T_TMS_EmployeeTimesheetWeeklyDetailsManager.Search(SearchKey);
                    if (mycol.Count == 0)
                    {
                        //insert day
                        filter.EntityID = i;
                        filter.WorkingTimeGroupID = null;
                        if (i == 10003)
                        {
                            filter.WorkingTimeGroupID = 1;
                        }

                        EmpTaskManager.Employee_Weekly_Add(filter);
                    }
                }
                //T_TMS_EmployeeDailyTimesheetTransactionCollection obj = T_TMS_EmployeeDailyTimesheetTransactionManager.Search(SearchKey);


            }
            catch (Exception exc)
            {
                // _logger.Error(string.Format("Error sending e-mail. {0}", exc.Message), exc);
            }
            finally
            {
                //queuedEmail.SentTries = queuedEmail.SentTries + 1;
                //    _queuedEmailService.UpdateQueuedEmail(queuedEmail);
            }
        }
    }
}
