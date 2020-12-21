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
        private string INSERT = "INSERT INTO Reports (CVR, AuditorId) VALUES (@CVR, @AuditorId)";
        private string COMPLETE_REPORT = "UPDATE Reports SET Completed = @Completed WHERE ReportId = @ReportId";
        private string GET_PARTICIPANTS = "SELECT EmployeeId FROM Participants WHERE ReportId = @Id";
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

            if (!reader.IsDBNull(2)) { report.Customer = new ManageCustomers().Get(reader.GetInt32(2)); }
            if (!reader.IsDBNull(3)) { report.Auditor = new ManageAuditors().Get(reader.GetInt32(3)); }

            report.LoadAnswers(new ManageQuestionAnswers().GetFromReport(report.Id));
            report.LoadEmployees(GetParticipants(report.Id));

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

        //TODO: Refactor to extract this method into ManageEmployee.GetParticipants(), since it returns employees
        public List<Employee> GetParticipants(int reportId)
        {
            List<Employee> employees = new List<Employee>();
            ManageEmployees emanager = new ManageEmployees();

            using (SqlConnection conn = new SqlConnection(ConnectionString))
            using (SqlCommand cmd = new SqlCommand(GET_PARTICIPANTS, conn))
            {
                conn.Open();

                cmd.Parameters.AddWithValue("@Id", reportId);

                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    employees.Add(emanager.Get(reader.GetInt32(0)));
                }
                reader.Close();
            }

            return employees;
        }

        public bool Post(Report report)
        {
            using (SqlConnection conn = new SqlConnection(ConnectionString))
            using (SqlCommand cmd = new SqlCommand(INSERT, conn))
            {
                conn.Open();

                cmd.Parameters.AddWithValue("@CVR", report.Customer.CVR);
                cmd.Parameters.AddWithValue("@AuditorId", report.Auditor.Id);

                //Returns true if query returns higher than 0 (affected rows)
                return cmd.ExecuteNonQuery() > 0;
            }
        }

        public bool Complete(int reportId)
        {
            using (SqlConnection conn = new SqlConnection(ConnectionString))
            using (SqlCommand cmd = new SqlCommand(COMPLETE_REPORT, conn))
            {
                conn.Open();

                cmd.Parameters.AddWithValue("@Completed", DateTime.Now.Date);
                cmd.Parameters.AddWithValue("@ReportId", reportId);

                //Returns true if query returns higher than 0 (affected rows)
                return cmd.ExecuteNonQuery() > 0;
            }
        }
    }
}
