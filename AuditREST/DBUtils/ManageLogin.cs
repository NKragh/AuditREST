using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using AuditREST.DBUtils;
using AuditREST.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
namespace AuditREST.Controllers
{
    public class ManageLogin: IManager<string>
    {
        private readonly string LOGIN = "SELECT password FROM Login WHERE AuditorId = @Id";

        public Auditor Login(Login login)
        {
            int auditorId = 0;

            ManageAuditors amanager = new ManageAuditors();
            Auditor auditor = amanager.GetByEmail(login.email);

            using (SqlConnection conn = new SqlConnection(ConnectionString))
            using (SqlCommand cmd = new SqlCommand(LOGIN, conn))
            {
                conn.Open();

                cmd.Parameters.AddWithValue("@Id", auditor.Id.ToString());

                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    if (ReadNextElement(reader) == login.password)
                    {
                        auditorId = auditor.Id;
                    }
                }

                reader.Close();
            }

            if (auditorId == 0) return new Auditor();
            return amanager.Get(auditorId);
        }

        public override string ConnectionString { get; set; }

        public override string ReadNextElement(SqlDataReader reader)
        {
            if (!reader.IsDBNull(0)) { return reader.GetString(0); }

            return "";
        }

        public override IEnumerable<string> Get()
        {
            throw new NotImplementedException();
        }

        public override string Get(int id)
        {
            throw new NotImplementedException();
        }
    }
}
