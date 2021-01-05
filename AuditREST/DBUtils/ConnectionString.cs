using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AuditREST.DBUtils
{
    public class ConnectionString
    {
        public string ConnectionStreng { get; set; }

        public ConnectionString()
        {
            string auth = System.IO.File.ReadAllText("./Secrets/connection.txt");
            ConnectionStreng = $"Data Source=nikolajdbserver.database.windows.net;Initial Catalog=auditdb;{auth}Connect Timeout=30;Encrypt=True;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
        }
    }
}
