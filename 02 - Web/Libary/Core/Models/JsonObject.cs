using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

public class JsonObject
{
    public object Data { get; set; }
    public int ErrorCode { get; set; }
    public string Message { get; set; }
    public int StatusCode { get; set; }
    public int Total { get; set; }
    public bool ShowStatus { get; set; }
}

[Serializable()]
public class HeaderItem : BaseEntity

{

    public string name { get; set; }

    public string type { get; set; }

    public string value { get; set; }

    public string alink { get; set; }
}

[Serializable()]

public class HeaderItemCollection : BaseEntityCollection<HeaderItem> { }

