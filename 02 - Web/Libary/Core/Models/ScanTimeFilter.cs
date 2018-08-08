using DataTables.Mvc;
using System;
using System.Web.Mvc;

public class ScanTimeFilter
{
    public string Keyword { get; set; }
    public string ColumnsName { get; set; }
    public string OrderBy { get; set; }
    public string OrderDirection { get; set; }
    public int Page { get; set; }
    public int PageSize { get; set; }
    public int CompanyID { get; set; }
    public int EmployeeCode { get; set; }
    public DateTime FromDate { get; set; }
    public DateTime ToDate { get; set; }
    public string Condition { get; set; }

    public static ScanTimeFilter SearchData(int CompanyID, [ModelBinder(typeof(DataTablesBinder))] IDataTablesRequest requestModel,
        string Col, string OrderByDefault, string strCondition = "", string OrderDirection = "ASC")
    {
        ColumnCollection col = requestModel.Columns;
        string OrderBy = OrderByDefault;

        foreach (Column item in col)
        {
            //if (item.IsOrdered && item.Data!="EmpPoint")
            if (item.IsOrdered)
            {
                OrderBy = item.Data;
                OrderDirection = item.SortDirection == Column.OrderDirection.Ascendant ? "ASC" : "DESC";
                break;
            }
        }

        return new ScanTimeFilter()
        {
            CompanyID = CompanyID,
            Keyword = requestModel.Search.Value,
            Page = (requestModel.Start / requestModel.Length) + 1,
            PageSize = requestModel.Length,
            ColumnsName = Col,
            OrderBy = OrderBy,
            OrderDirection = OrderDirection,
            Condition = strCondition
        };
    }

    public static ScanTimeFilter SearchPG(int CompanyID,
        int pg, int pgs, string Col, string OrderByDefault, string OrderDirection, string strCondition = "")
    {
        return new ScanTimeFilter()
        {
            CompanyID = CompanyID,
            Keyword = "",
            Page = pg,
            PageSize = pgs,
            ColumnsName = Col,
            OrderBy = OrderByDefault,
            OrderDirection = OrderDirection,
            Condition = strCondition
        };
    }

    public static ScanTimeFilter SearchData([ModelBinder(typeof(DataTablesBinder))] IDataTablesRequest requestModel)
    {
        return new ScanTimeFilter()
        {
            Keyword = requestModel.Search.Value,
            Page = (requestModel.Start / requestModel.Length) + 1,
            PageSize = requestModel.Length
        };
    }
}