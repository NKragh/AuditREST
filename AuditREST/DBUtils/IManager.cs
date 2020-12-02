using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AuditREST.DBUtils
{
    interface IManager<T>
    {
        string ConnectionString { get; set; }
        IEnumerable<T> Get();
        T Get(int id);
        bool Create(T input);
    }
}
