using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.Serialization;

    [DataContract]
    public abstract class BaseDBEntity
    {
    }
    [CollectionDataContract]
    public abstract class BaseDBEntityCollection<T> : List<T> where T : BaseDBEntity, new()
    {

    }

