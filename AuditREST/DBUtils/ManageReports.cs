using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using AuditREST.Models;

namespace AuditREST.DBUtils
{
    public class ManageReports : IManager<Report>
    {
        private string GET_ALL = "SELECT * FROM Reports";
        private string GET_ONE = "SELECT * FROM Reports WHERE ReportId = @Id";
        private string INSERT = "INSERT INTO Reports (CVR, AuditorId, Employee1Id, Employee2Id) VALUES (@CVR, @AuditorId, @Employee1Id, @Employee2Id)";
        private string COMPLETE_REPORT = "UPDATE Reports WHERE ReportId = @ReportId";
        public override string ConnectionString { get; set; }

        public ManageReports()
        {
            ConnectionString = new ConnectionString().ConnectionStreng;
        }
        public override Report ReadNextElement(SqlDataReader reader)
        {
            Report report = new Report();

            if (!reader.IsDBNull(0)) { report.Id = reader.GetInt32(0); }
            if (!reader.IsDBNull(1)) { report.Completed = reader.GetDateTime(1); }
            if (!reader.IsDBNull(2)) { report.CVR = reader.GetInt32(2); }
            if (!reader.IsDBNull(3)) { report.Auditor.Id = reader.GetInt32(3); }
            if (!reader.IsDBNull(4)) { report.Employee1.Id = reader.GetInt32(4); }
            if (!reader.IsDBNull(5)) { report.Employee2.Id = reader.GetInt32(5); }

            report.LoadAnswers(new ManageQuestionAnswers().GetFromReport(report.Id));

            report.Auditor = new ManageAuditors().Get(report.Employee1.Id);

            report.Employee1 = new ManageEmployees().Get(report.Employee1.Id);
            report.Employee2 = new ManageEmployees().Get(report.Employee2.Id);


            return report;
        }

        public override IEnumerable<Report> Get()
        {
            List<Report> liste = new List<Report>();

            using (SqlConnection conn = new SqlConnection(ConnectionString))
            using (SqlCommand cmd = new SqlCommand(GET_ALL, conn))
            {
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    Report item = ReadNextElement(reader);
                    liste.Add(item);
                }
                reader.Close();
            }

            return liste;
        }

        public override Report Get(int id)
        {
            Report report = new Report();

            using (SqlConnection conn = new SqlConnection(ConnectionString))
            using (SqlCommand cmd = new SqlCommand(GET_ONE, conn))
            {
                conn.Open();

                cmd.Parameters.AddWithValue("@Id", id);

                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    report = ReadNextElement(reader);
                }
                reader.Close();
            }

            return report;
        }

        public bool Post(Report report)
        {
            using (SqlConnection conn = new SqlConnection(ConnectionString))
            using (SqlCommand cmd = new SqlCommand(INSERT, conn))
            {
                conn.Open();

                cmd.Parameters.AddWithValue("@CVR", report.CVR);
                cmd.Parameters.AddWithValue("@AuditorId", report.Auditor.Id);
                cmd.Parameters.AddWithValue("@Employee1Id", report.Employee1.Id);
                cmd.Parameters.AddWithValue("@Employee2Id", report.Employee2.Id);

                //Returns true if query returns higher than 0 (affected rows)
                return cmd.ExecuteNonQuery() > 0;
            }
        }

        public bool Put(int reportId, Report report)
        {
            using (SqlConnection conn = new SqlConnection(ConnectionString))
            using (SqlCommand cmd = new SqlCommand(COMPLETE_REPORT, conn))
            {
                conn.Open();

                cmd.Parameters.AddWithValue("@Completed", report.Completed);

                //Returns true if query returns higher than 0 (affected rows)
                return cmd.ExecuteNonQuery() > 0;
            }
        }
    }
}
