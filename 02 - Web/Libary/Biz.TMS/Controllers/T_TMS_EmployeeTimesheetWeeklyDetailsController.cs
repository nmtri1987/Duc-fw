using System;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using Newtonsoft.Json;
using Biz.TMS;
using DataTables.Mvc;
using System.Data;
using Biz.TMS.Services;
using System.Collections.Generic;
using System.Linq;
using Biz.TMS.Models;
namespace Biz.TMS.Controllers
{
    public class TMSEmployeeWeeklyController : BaseController<T_TMS_EmployeeTimesheetWeeklyDetails>
    {
        string ViewFolder = "~/Views/TMS/T_TMS_EmployeeTimesheetWeeklyDetails/";
        public ActionResult Info(string searchprm)
        {
            EmpFilter ObjPara = new EmpFilter();
            EMPWeelFilter filEmp = new EMPWeelFilter();
            if (!string.IsNullOrEmpty(searchprm))
            {
                 ObjPara = JsonConvert.DeserializeObject<EmpFilter>(searchprm,
                    new JsonSerializerSettings
                    {
                        NullValueHandling = NullValueHandling.Ignore,
                        DateFormatString = "dd/MM/yyyy"
                    });

            }
            if (ObjPara != null)
            {
                ls_PayrollDOWS_RBVH objDown = ls_PayrollDOWS_RBVHManager.GetById(ObjPara.Dow_ID);
                if (objDown != null)
                {
                    //if()
                    if (filEmp.Number == 0)
                    {
                        filEmp.Number = filEmp.Number + 1;
                    }
                    int dow = (int)objDown.Beg_Day.DayOfWeek;
                    filEmp.StartWeek = objDown.Beg_Day;
                    filEmp.EndDateWeek = objDown.End_Day;
                    if (dow != 0)
                    {
                        filEmp.StartWeek = objDown.Beg_Day.AddDays(-dow+1);
                    }
                    dow = (int)objDown.End_Day.DayOfWeek;
                    if (objDown.End_Day.DayOfWeek != DayOfWeek.Sunday)
                    {
                        filEmp.EndDateWeek = objDown.End_Day.AddDays(7-dow-1);
                    }
                    filEmp.EndWeek = filEmp.StartWeek.AddDays(6);
                    filEmp.FromDate = objDown.Beg_Day;
                    filEmp.ToDate = objDown.End_Day;
                    filEmp.EmployeeCode = ObjPara.EmployeeCode;
                }

            }

            return View(ViewFolder + "Info.cshtml", filEmp);
        }
        public ActionResult NextWeek(string searchprm)
        {
            EMPWeelFilter filEmp = new EMPWeelFilter();
            if (!string.IsNullOrEmpty(searchprm))
            {
                filEmp = JsonConvert.DeserializeObject<EMPWeelFilter>(searchprm,
                   new JsonSerializerSettings
                   {
                       NullValueHandling = NullValueHandling.Ignore,
                       DateFormatString = "dd/MM/yyyy"
                   });
                if (filEmp != null)
                {
                    filEmp.StartWeek = filEmp.EndWeek.AddDays(1);
                    filEmp.EndWeek = filEmp.StartWeek.AddDays(6);
                    filEmp.Number = filEmp.Number + 1;
                }
            }
            
            return View(ViewFolder + "Info.cshtml", filEmp);
        }
        public ContentResult GetEmpWeeklyData([ModelBinder(typeof(DataTablesBinder))] IDataTablesRequest requestModel, string searchprm)//, int EntityID = 10002,  DateTime? fromdate = null, DateTime? todate = null)
        {
            EMPWeelFilter ObjPara = new EMPWeelFilter()
            {
                EmployeeCode = CurrentUser.EmployeeCode,
                FromDate = new DateTime(2017, 08, 15),
                ToDate = new DateTime(2017, 09, 15)
            };
            if (!string.IsNullOrEmpty(searchprm))
            {
                ObjPara = JsonConvert.DeserializeObject<EMPWeelFilter>(searchprm,
                    new JsonSerializerSettings
                    {
                        NullValueHandling = NullValueHandling.Ignore,
                        DateFormatString = "dd/MM/yyyy"
                    });
            }
         //   ObjPara.EmployeeCode = 1863;
            //EMPTMSPara SearchKey = new EMPTMSPara()
            //{
            //    EntityID = 10002,
            //    DeptID = 0,
            //    @FromDate = fromdate ,
            //    @ToDate = todate,
            //    OrderBy = "EmployeeNo",
            //    OrderDirection = "DESC",
            //    Page = (requestModel.Start / requestModel.Length) + 1,
            //    PageSize = requestModel.Length,
            //};
            //fromdate = fromdate == null ? SystemConfig.CurrentDate : fromdate;
            //todate = todate == null ? SystemConfig.CurrentDate : todate;
            //ObjPara.OrderBy = "DeptName";
            //ObjPara.OrderDirection = "ASC";
            //ObjPara.Page = (requestModel.Start / requestModel.Length) + 1;
            //ObjPara.PageSize = requestModel.Length;
            DataTable collection = new DataTable();
            collection = T_TMS_EmployeeTimesheetWeeklyDetailsManager.EmployeeTMSWeeeklyFilter(ObjPara);

            System.Data.DataColumn newColumn = new System.Data.DataColumn("Accumulate", typeof(System.String));
            //newColumn.DefaultValue = "Summary";
            collection.Columns.Add(newColumn);
            int TotalRecord = 0;
            EmpWeekSummary myWeek = new EmpWeekSummary();
            if (collection.Rows.Count > 0)
            {
                for(int i=0; i< collection.Rows.Count; i++)
                {
                    
                    //collection.Rows[i]["Accumulate"] = data(collection.Rows[i],i,out myWeek);
                    data(collection.Rows[i], i, ref myWeek);
                }
                myWeek.TotalWorkHour = 42;
                collection.Rows[1]["Accumulate"] = myWeek.IntoWeek;
                collection.Rows[2]["Accumulate"] = myWeek.WorkHourString;
                collection.Rows[3]["Accumulate"] = myWeek.OTSummary==0? "":myWeek.OTString.ToString();
                collection.Rows[4]["Accumulate"] = myWeek.LeaveSummary == 0 ? "" : myWeek.LeaveString.ToString(); 
                collection.Rows[5]["Accumulate"] = myWeek.UnNoregSummary == 0 ? "" : myWeek.UnoRegString.ToString();  
                collection.Rows[6]["Accumulate"] = myWeek.LackingHour == 0 ? "" : myWeek.LackingHourString; 
                TotalRecord = Convert.ToInt32(collection.Rows.Count);

            }

            IEnumerable<DataRow> rows = collection.AsEnumerable().ToList();
            object temp = new object();
            foreach (var item in rows)
            {
                temp = item.Table;
            }
            //DataTablesResponseExtend results = new DataTablesResponseExtend(requestModel.Draw, temp, TotalRecord, TotalRecord);
            return Content(JsonConvert.SerializeObject(new DataTablesResponseExtend(requestModel.Draw, temp, TotalRecord, TotalRecord)), "application/json");
        }
        private void data(System.Data.DataRow dtrow, int idx,ref EmpWeekSummary Empsum)
        {
            string rvalue = "";
            //switch (idx)
            //{
            //    case 3:

            //        break;
            //    default: break;


            //}
            object item;
            if (idx == 0)
            {
                rvalue = "Total";
            }
            else if (idx == 1)
            {
                rvalue = "";
            }
            else if (idx == 2)
            {
                decimal value = 0;
                decimal d = 0;
                string mystrValue = "";
                for (int i = 1; i <= dtrow.ItemArray.Count() - 1; i++)
                {
                    mystrValue = dtrow.ItemArray[i].ToString();
                    if (mystrValue != "")
                    {
                        d = System.Convert.ToDecimal(mystrValue.ToString(), new System.Globalization.CultureInfo("en-US"));
                        //item = (double)dtrow.ItemArray[i];
                        value += d;
                        Empsum.WorkHour += d;

                    }
                }
                rvalue = value.ToString();
            }
            else if (idx == 3)
            {
                decimal d = 0;
                string[] mySplit;
                string value = "";
                bool hasField = false;
                string LName = "";
                for (int i = 1; i <= dtrow.ItemArray.Count() - 1; i++)
                {
                    value = dtrow.ItemArray[i].ToString();
                    hasField = false;
                    if (!string.IsNullOrEmpty(value))
                    {
                        mySplit = value.Split(',');
                        if (mySplit.Length > 0)
                        {
                            LName = mySplit[1].ToLower();
                            d = System.Convert.ToDecimal(mySplit[0], new System.Globalization.CultureInfo("en-US"));

                            if (Empsum.OT == null)
                            {
                                Empsum.OT = new TMSDataCollection();
                            }
                            foreach (TMSData objData in Empsum.OT)
                            {
                                if (objData.Name.ToLower() == LName)
                                {
                                    objData.summary += d;
                                    hasField = true;
                                    break;
                                }
                            }
                            if (!hasField)
                            {
                                TMSData b = new TMSData();
                                b.Name = mySplit[1];
                                b.summary = d;
                                Empsum.OT.Add(b);
                            }

                            Empsum.OTSummary += d;
                        }

                    }
                }
            }
            else if (idx == 4)
            {
                decimal d = 0;
                string[] mySplit;
                string value = "";
                bool hasField = false;
                string LName = "";
                for (int i = 1; i <= dtrow.ItemArray.Count() - 1; i++)
                {
                    value = dtrow.ItemArray[i].ToString();
                    hasField = false;
                    if (!string.IsNullOrEmpty(value))
                    {
                        mySplit = value.Split(',');
                        if (mySplit.Length > 0)
                        {
                            LName = mySplit[1].ToLower();
                            d = System.Convert.ToDecimal(mySplit[0], new System.Globalization.CultureInfo("en-US"));
                       
                            if (Empsum.Leave == null)
                            {
                                Empsum.Leave = new TMSDataCollection();
                            }
                            foreach (TMSData objData in Empsum.Leave)
                            {
                                if (objData.Name.ToLower() == LName)
                                {
                                    objData.summary += d;
                                    hasField = true;
                                    break;
                                }
                            }
                            if (!hasField)
                            {
                                TMSData b = new TMSData();
                                b.Name = mySplit[1];
                                b.summary = d;
                                Empsum.Leave.Add(b);
                            }

                            Empsum.LeaveSummary += d;
                        }

                    }

                }
            }
            else if (idx == 5)
            {
             
                decimal d = 0;
                string[] mySplit;
                string value = "";
                bool hasField = false;
                string LName = "";
                for (int i = 1; i <= dtrow.ItemArray.Count() - 1; i++)
                {
                    value = dtrow.ItemArray[i].ToString();
                    hasField = false;
                    if (!string.IsNullOrEmpty(value))
                    {
                        mySplit = value.Split(',');
                        if (mySplit.Length > 0)
                        {
                            LName = mySplit[1].ToLower();
                            d = System.Convert.ToDecimal(mySplit[0], new System.Globalization.CultureInfo("en-US"));
                            //if (mySplit[1].ToLower() == "unnoreg")
                            //{

                            //    UnNoReg += d;
                            //}
                            //else
                            //{
                            //    dLC += d;
                            //}
                            if (Empsum.UnoReg == null)
                            {
                                Empsum.UnoReg = new TMSDataCollection();
                            }
                            foreach (TMSData objData in Empsum.UnoReg)
                            {
                                if (objData.Name.ToLower() == LName)
                                {
                                    objData.summary += d;
                                    hasField = true;
                                    break;
                                }
                            }
                            if (!hasField)
                            {
                                TMSData b = new TMSData();
                                b.Name = mySplit[1];
                                b.summary = d;
                                Empsum.UnoReg.Add(b);
                            }

                            Empsum.UnNoregSummary += d;
                        }

                    }

                }
             
            }
            else
            {
                decimal d = 0;
                for (int i = 1; i <= dtrow.ItemArray.Count() - 1; i++)
                {
                    item = dtrow.ItemArray[i];
                    rvalue += item;
                    if (idx == 6)
                    {
                        if (!string.IsNullOrEmpty(item.ToString()))
                        {
                            d = System.Convert.ToDecimal(item, new System.Globalization.CultureInfo("en-US"));
                            Empsum.LackingHour += d;
                        }

                    }
                }
            }
            //return rvalue;
        }
       
    }
}