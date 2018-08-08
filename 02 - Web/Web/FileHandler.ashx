<%@ WebHandler Language="C#" Class="FileHandler" %>

using System;
using System.Web;
using System.Data;
using Microsoft.Reporting.WebForms;
using System.Collections.Generic;
using Biz.Core;
public class FileHandler : IHttpHandler
{

    public void ProcessRequest(HttpContext context)
    {
        string Mode = CommonHelper.DecyptURLString(context.Request.QueryString["Mode"].ToString());
        if (context.Request.QueryString["RowId"].ToString() != null && context.Request.QueryString["EntityID"].ToString() != null && context.Request.QueryString["EmployeeCode"].ToString() != null && context.Request.QueryString["filepath"].ToString() != null && context.Request.QueryString["filetype"].ToString() != null)
        //if (context.Request.QueryString["RowId"].ToString() != null && context.Request.QueryString["EntityID"].ToString() != null && context.Request.QueryString["EmployeeCode"].ToString() != null && context.Request.QueryString["filepath"].ToString() != null && context.Request.QueryString["filetype"].ToString() != null)
        //if (context.Request.QueryString["EmpCode"].ToString() != null && context.Request.QueryString["EntityID"].ToString() != null && context.Request.QueryString["filepath"].ToString() != null && context.Request.QueryString["filetype"].ToString() != null)
        {

            string RowId = CommonHelper.DecyptURLString(context.Request.QueryString["RowId"].ToString());
            string EntityID = CommonHelper.DecyptURLString(context.Request.QueryString["EntityID"].ToString());
            //string EmployeeCode = BasePage.DecyptURLString(context.Request.QueryString["EmployeeCode"].ToString());
            string filepath = CommonHelper.DecyptURLString(context.Request.QueryString["filepath"].ToString());
            string EmployeeCode = CommonHelper.DecyptURLString(context.Request.QueryString["EmployeeCode"].ToString());
            string filetype = context.Request.QueryString["filetype"].ToString();
            List<ReportParameter> paramList = new List<ReportParameter>();
            ReportDataSource dataSourceReport = new ReportDataSource();
            DataTable Dtable = new DataTable();

            if (Mode == "CONTRACT")
            {

                //    HttpContext.Current.Response.Write(EntityID+"/"+EmployeeCode);
                //HttpContext.Current.Response.End();
                paramList = DataHelper.FetchParamListContract(EntityID,EmployeeCode);

                Dtable = DataHelper.LoadContractTemplateDetails(RowId);

                if (Dtable.Rows.Count > 0)
                {

                    dataSourceReport = new ReportDataSource("DataSet_Contract", Dtable);
                }


            }
            else if (Mode == "PROB")
            {
                paramList = DataHelper.FetchParamListProbation(EntityID, EmployeeCode);
                Dtable = DataHelper.LoadOfferTemplateDetails(RowId);
                if (Dtable.Rows.Count > 0)
                {

                    dataSourceReport = new ReportDataSource("DataSet_Probation", Dtable);
                }
            }
            else if (Mode == "INTERN")
            {
                paramList = DataHelper.FetchParamListIntern(EntityID, EmployeeCode);
                Dtable = DataHelper.LoadOfferInternTemplateDetails(RowId);
                if (Dtable.Rows.Count > 0)
                {

                    dataSourceReport = new ReportDataSource("DataSet_Intern", Dtable);
                }

            }

            ReportViewer RV = new ReportViewer();
            RV.ProcessingMode = ProcessingMode.Local;
            RV.LocalReport.ReportPath = filepath;
            string val="";
            if (filetype == "PDF")
            {
                val = "1";
            }
            else
            {
                val = "0";
            }

            paramList.Add(new ReportParameter("IsFooterVisible", val));
            RV.LocalReport.SetParameters(paramList);
            RV.LocalReport.DataSources.Add(dataSourceReport);
            Console.Write(RV.ToString());

            Byte[] results;

            try
            {
                if (filetype.ToUpper() != "PDF")
                {
                    results = RV.LocalReport.Render("Word");
                    context.Response.Clear();
                    //HttpContext.Current.Response.ContentType = "application/vnd.word";                
                    HttpContext.Current.Response.ContentType = "application/vnd.ms-word";
                    HttpContext.Current.Response.AddHeader("content-disposition", "attachment; filename=" + Mode + "_" + "Letter_" + DateTime.Now.Second + ".doc");
                }
                else
                {
                    results = RV.LocalReport.Render("PDF");
                    context.Response.Clear();
                    HttpContext.Current.Response.ContentType = "application/pdf";
                    //HttpContext.Current.Response.AddHeader("content-disposition", "attachment; filename=Letter_" + Empcode + ".pdf");     
                    HttpContext.Current.Response.AddHeader("content-disposition", "attachment; filename=" + Mode + "_" + "Letter_" + DateTime.Now.Second + ".pdf");
                }

                HttpContext.Current.Response.OutputStream.Write(results, 0, results.Length);
                HttpContext.Current.Response.Flush();
            }
            catch (Exception ex)
            {
              //  Common.Publish(ex);
            }
        }

    }

    public bool IsReusable
    {
        get
        {
            return false;
        }
    }

}


