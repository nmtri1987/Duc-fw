using System;
using System.Web.Mvc;
using DataTables.Mvc;

//   delegate IEnumerable<T> SearchData<T>(SearchFilter Search);
public class ScanTimeApprovalSqlParameters : SearchFilter
{
    public DateTime FromDate { get; set; }
    public DateTime ToDate { get; set; }
    public int UserLoggedIn { get; set; }
    public bool ShowWaiting { get; set; }
    public int StartRow { get; set; }
    public int EndRow { get; set; }
    public string OrderBy { get; set; }
    public string OrderDirection { get; set; }
    public string FilterBy { get; set; }
    public bool ShowUnNoReg { get; set; }
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

    public static RBVHSearchFilter SearchData(int CompanyID, [ModelBinder(typeof(DataTablesBinder))] IDataTablesRequest requestModel,
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

        return new RBVHSearchFilter()
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

    public static RBVHSearchFilter SearchData(RBVHSearchFilter SearchKey, [ModelBinder(typeof(DataTablesBinder))] IDataTablesRequest requestModel)
    {
        ColumnCollection col = requestModel.Columns;
        string OrderBy = SearchKey.OrderBy;

        foreach (Column item in col)
        {
            //if (item.IsOrdered && item.Data!="EmpPoint")
            if (item.IsOrdered)
            {
                OrderBy = item.Data;
                SearchKey.OrderDirection = item.SortDirection == Column.OrderDirection.Ascendant ? "ASC" : "DESC";
                break;
            }
        }
        if (string.IsNullOrEmpty(SearchKey.Keyword))
        {
            SearchKey.Keyword = requestModel.Search.Value;
        }
        SearchKey.Page = (requestModel.Start / requestModel.Length) + 1;
        SearchKey.PageSize = requestModel.Length;
        return SearchKey;
    }

    public static RBVHSearchFilter SearchPG(int CompanyID,
           int pg, int pgs, string Col, string OrderByDefault, string OrderDirection, string strCondition = "")
    {
        return new RBVHSearchFilter()
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
}

public class SearchFilter
{
    public string Keyword { get; set; }
    public string OrderBy { get; set; }
    public string OrderDirection { get; set; }
    public int Page { get; set; }
    public int PageSize { get; set; }

    public int CompanyID { get; set; }
    public string ColumnsName { get; set; }
    public string Condition { get; set; }

    /// <summary>
    /// Indicating ordering operation
    /// </summary>
    public bool IsOrdering { get; set; } = false;
    /// <summary>
    ///
    /// </summary>
    /// <param name="CompanyID">Company ID</param>
    /// <param name="requestModel">Request Binding</param>
    /// <param name="Col">Default Colum Field </param>
    /// <param name="OrderByDefault">Default Order Field</param>
    /// <returns></returns>
    public static SearchFilter SearchData(int CompanyID, [ModelBinder(typeof(DataTablesBinder))] IDataTablesRequest requestModel,
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

        return new SearchFilter()
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

    public static SearchFilter SearchData(SearchFilter SearchKey, [ModelBinder(typeof(DataTablesBinder))] IDataTablesRequest requestModel)
    {
        ColumnCollection col = requestModel.Columns;
        string OrderBy = SearchKey.OrderBy;

        foreach (Column item in col)
        {
            //if (item.IsOrdered && item.Data!="EmpPoint")
            if (item.IsOrdered)
            {
                OrderBy = item.Data;
                SearchKey.OrderDirection = item.SortDirection == Column.OrderDirection.Ascendant ? "ASC" : "DESC";
                break;
            }
        }
        if (string.IsNullOrEmpty(SearchKey.Keyword))
        {
            SearchKey.Keyword = requestModel.Search.Value;
        }
        SearchKey.Page = (requestModel.Start / requestModel.Length) + 1;
        SearchKey.PageSize = requestModel.Length;
        return SearchKey;
    }

    public static SearchFilter SearchPG(int CompanyID,
        int pg, int pgs, string Col, string OrderByDefault, string OrderDirection, string strCondition = "")
    {
        return new SearchFilter()
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

    public static SearchFilter SearchData([ModelBinder(typeof(DataTablesBinder))] IDataTablesRequest requestModel)
    {
        return new SearchFilter()
        {
            Keyword = requestModel.Search.Value,
            Page = (requestModel.Start / requestModel.Length) + 1,
            PageSize = requestModel.Length
        };
    }
}