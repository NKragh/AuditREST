using System.Collections.Generic;
using System.Data.SqlClient;
using AuditREST.Models;

namespace AuditREST.DBUtils
{
    public class ManageAuditors : IManager<Auditor>
    {
        private string GET_ALL = "SELECT * FROM Auditors";
        private string GET_ONE = "SELECT * FROM Auditors WHERE AuditorId = @Id";
        private string GET_BY_REPORT = "SELECT a.* FROM Reports as r " +
                                       "JOIN Auditors as a ON a.AuditorId = r.AuditorId " +
                                       "WHERE r.ReportId = @ReportId";

        private string GET_BY_EMAIL = "SELECT * FROM Auditors WHERE AuditorEmail = @Email";

        public override string ConnectionString { get; set; }

        public override Auditor ReadNextElement(SqlDataReader reader)
        {
            Auditor auditor = new Auditor();

            if (!reader.IsDBNull(0)) { auditor.Id = reader.GetInt32(0); }
            if (!reader.IsDBNull(0)) { auditor.Name = reader.GetString(1); }
            if (!reader.IsDBNull(0)) { auditor.Email = reader.GetString(2); }

            return auditor;
        }

        public override IEnumerable<Auditor> Get()
        {
            List<Auditor> liste = new List<Auditor>();

            using (SqlConnection conn = new SqlConnection(ConnectionString))
            using (SqlCommand cmd = new SqlCommand(GET_ALL, conn))
            {
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    Auditor item = ReadNextElement(reader);
                    liste.Add(item);
                }
                reader.Close();
            }

            return liste;
        }

        public override Auditor Get(int id)
        {
            Auditor auditor = new Auditor();

            using (SqlConnection conn = new SqlConnection(ConnectionString))
            using (SqlCommand cmd = new SqlCommand(GET_ONE, conn))
            {
                conn.Open();

                cmd.Parameters.AddWithValue("@Id", id);

                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    auditor = ReadNextElement(reader);
                }
                reader.Close();
            }

            return auditor;
        }


        public Auditor GetByEmail(string email)
        {
            Auditor auditor = new Auditor();

            using (SqlConnection conn = new SqlConnection(ConnectionString))
            using (SqlCommand cmd = new SqlCommand(GET_BY_EMAIL, conn))
            {
                conn.Open();

                cmd.Parameters.AddWithValue("@Email", email);

                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    auditor = ReadNextElement(reader);
                }

                reader.Close();
            }

            return auditor;
        }

        public Auditor GetByReport(int reportId)
        {
            Auditor auditor = new Auditor();

            using (SqlConnection conn = new SqlConnection(ConnectionString))
            using (SqlCommand cmd = new SqlCommand(GET_BY_REPORT, conn))
            {
                conn.Open();

                cmd.Parameters.AddWithValue("@ReportId", reportId);

                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    auditor = ReadNextElement(reader);
                }
                reader.Close();
            }

            return auditor;
        }
    }
}
