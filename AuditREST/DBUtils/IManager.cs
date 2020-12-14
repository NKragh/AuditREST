using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using AuditREST.Models;

namespace AuditREST.DBUtils
{
    public abstract class IManager<T>
    {
        public abstract string ConnectionString { get; set; }

        protected IManager()
        {
            ConnectionString = new ConnectionString().ConnectionStreng;
        }

        public abstract T ReadNextElement(SqlDataReader reader);

        public abstract IEnumerable<T> Get();

        public abstract T Get(int id);
    }
}
