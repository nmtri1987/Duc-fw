using System;

public class ScanTimeApprovalSqlParameters : SearchFilter
{
    public DateTime FromDate { get; set; } = DateTime.Now.AddDays(-30);
    public DateTime ToDate { get; set; } = DateTime.Now;
    public int UserLoggedIn { get; set; }
    public bool ShowWaiting { get; set; }
    public int StartRow { get; set; }
    public int EndRow { get; set; }
    public string OrderBy { get; set; }
    public int OrderDirection { get; set; }
    public string FilterBy { get; set; }
    public bool ShowUnNoReg { get; set; }
}