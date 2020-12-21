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
            //ConnectionStreng = "Data Source=nikolajdbserver.database.windows.net;Initial Catalog=auditdb;User ID=nikolajlogin;Password=Secret123;Connect Timeout=30;Encrypt=True;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
            ConnectionStreng = "Data Source=nikolajdbserver.database.windows.net;Initial Catalog=auditdb;User ID=nikolajlogin;Password=Secret123;Connect Timeout=30;Encrypt=True;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
        }
    }
}
