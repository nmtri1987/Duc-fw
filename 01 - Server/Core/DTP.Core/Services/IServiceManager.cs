using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


public partial interface IServiceManager<T>
{
//    T Get(string ID);
//    IEnumerable<T> GetSearch(SearchFilter value);
//    T Add(T value);

//    T Update(T value);

//    int Del(string ID);
    T Get(GetParam value);

    IEnumerable<T> GetSearch(SearchFilter value);
    T Add(T value);

    T Update(T value);

    int Del(GetParam value);
}

