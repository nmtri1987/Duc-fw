using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.Serialization;

[DataContract]
public abstract class BaseDBEntity
{
    [DataMember]
    public int TotalRecord { get; set; }
}
[CollectionDataContract]
public abstract class BaseDBEntityCollection<T> : List<T> where T : BaseDBEntity, new()
{
    [DataMember]
    public int TotalRecord { get; set; }
}

public abstract class SearchResponse<T> where T : BaseDBEntity
{
    public int draw { get; set; }

    public int recordsTotal { get; set; }

    public int recordsFiltered { get; set; }

    public IList<T> data { get; set; }
}
