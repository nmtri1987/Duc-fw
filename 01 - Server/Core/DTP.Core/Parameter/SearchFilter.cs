using System;
using System.Collections.Generic;
using System.Data.SqlClient;


public class SearchFilter
{
    public string Keyword { get; set; }
    public string ColumnsName { get; set; }
    public string OrderBy { get; set; }
    public string OrderDirection { get; set; }
    public int Page { get; set; }
    public int PageSize { get; set; }

    public int CompanyID { get; set; }

    public string Condition { get; set; }

}

public class RBVHSearchFilter
{
    public int EntityID { get; set; }
    public string Keyword { get; set; }
    public string OrderBy { get; set; }
    public string OrderDirection { get; set; }
    public int Page { get; set; }
    public int PageSize { get; set; }
    public int CompanyID { get; set; }
    public string ColumnsName { get; set; }
    public string Condition { get; set; }
    public DateTime? CDate { get; set; }

}
public static class SearchFilterManager
{
    public static SqlParameter[] SqlSearchParam(SearchFilter value)
    {
        var pars = new SqlParameter[]
            {
                    new SqlParameter("@Keyword",value.Keyword),
                    new SqlParameter("@Columns",value.ColumnsName),
                    new SqlParameter("@OrderBy", value.OrderBy),
                    new SqlParameter("@OrderDirection", value.OrderDirection),
                    new SqlParameter("@Page", value.Page),
                    new SqlParameter("@PageSize",value.PageSize),
                    new SqlParameter("@CompanyID",value.CompanyID),
                    new SqlParameter("@Condition",value.Condition),

            };
        return pars;
    }

    public static SqlParameter[] SqlSearchParam(ScanTimeApprovalSqlParameters value)
    {
        var pars = new SqlParameter[]
            {
                    new SqlParameter("@FromDate",value.FromDate),
                    new SqlParameter("@ToDate",value.ToDate),
                    new SqlParameter("@User", value.UserLoggedIn),
                    new SqlParameter("@ShowWaiting", value.ShowWaiting),
                    new SqlParameter("@StartRow", value.StartRow),
                    new SqlParameter("@EndRow",value.EndRow),
                    new SqlParameter("@OrderBy",value.OrderBy),
                    new SqlParameter("@OrderDirection",value.OrderDirection),
                    new SqlParameter("@FilterBy",value.FilterBy),
                    new SqlParameter("@ShowUnNoReg",value.ShowUnNoReg)

            };
        return pars;
    }

    public static SqlParameter[] SqlSearchParamNoCompany(SearchFilter value)
    {
        var pars = new SqlParameter[]
            {
                    new SqlParameter("@Keyword",value.Keyword),
                    new SqlParameter("@Columns",value.ColumnsName),
                    new SqlParameter("@OrderBy", value.OrderBy),
                    new SqlParameter("@OrderDirection", value.OrderDirection),
                    new SqlParameter("@Page", value.Page),
                    new SqlParameter("@PageSize",value.PageSize),
                   // new SqlParameter("@CompanyID",value.CompanyID),
                //    new SqlParameter("@Condition",value.Condition),

            };
        return pars;
    }
    public static SqlParameter[] SqlSearchConditionNoCompany(SearchFilter value)
    {
        var pars = new SqlParameter[]
            {
                    new SqlParameter("@Keyword",value.Keyword),
                    new SqlParameter("@Columns",value.ColumnsName),
                    new SqlParameter("@OrderBy", value.OrderBy),
                    new SqlParameter("@OrderDirection", value.OrderDirection),
                    new SqlParameter("@Page", value.Page),
                    new SqlParameter("@PageSize",value.PageSize),
                   // new SqlParameter("@CompanyID",value.CompanyID),
                    new SqlParameter("@Condition",value.Condition),

            };
        return pars;
    }
    public static SqlParameter[] SqlSearchConditionNoCompany(RBVHSearchFilter value)
    {
        var pars = new SqlParameter[]
            {
                    new SqlParameter("@Keyword",value.Keyword),
                    new SqlParameter("@Columns",value.ColumnsName),
                    new SqlParameter("@OrderBy", value.OrderBy),
                    new SqlParameter("@OrderDirection", value.OrderDirection),
                    new SqlParameter("@Page", value.Page),
                    new SqlParameter("@PageSize",value.PageSize),
                   // new SqlParameter("@CompanyID",value.CompanyID),
                    new SqlParameter("@Condition",value.Condition),
                    new SqlParameter("@EntityID",value.EntityID),
                    new SqlParameter("@ReportDate",value.CDate),
            };
        return pars;
    }
    public static SqlParameter[] SqlSearchDynParam(SearchFilter value)
    {
        var pars = new SqlParameter[]
            {
                    new SqlParameter("@Keyword",value.Keyword),
                    new SqlParameter("@Columns",value.ColumnsName),
                    new SqlParameter("@OrderBy", value.OrderBy),
                    new SqlParameter("@OrderDirection", value.OrderDirection),
                    new SqlParameter("@Page", value.Page),
                    new SqlParameter("@PageSize",value.PageSize),
                    new SqlParameter("@CompanyID",value.CompanyID),
                new SqlParameter("@Condition",value.Condition)

            };
        return pars;
    }
}
public class GetParam
{
    public string ID { get; set; }
    public int CompanyID { get; set; }
    public string RefID { get; set; }
    public List<GetParamObject> ParaList { get; set; }
}
public class GetParamObject
{
    public string Field { get; set; }
    public string Values { get; set; }
}
