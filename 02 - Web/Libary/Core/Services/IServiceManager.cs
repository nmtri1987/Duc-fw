using System.Collections.Generic;
using System.Data;
using Biz.Core.Models;
using System;
public partial interface IServiceManager<T>
{
    DataTable ImportData(DataTable objList);
    IEnumerable<T> SearchData(SearchFilter value);
    string Get(string ID);
    T Default();
    T Save(T model);
    void Del(T model);

}

