using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net.Mime;
using System.Threading.Tasks;
using AuditREST.Models;
using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Wordprocessing;
using HtmlToOpenXml;
using HtmlToOpenXml.IO;
using PuppeteerSharp;

namespace AuditREST.DBUtils
{
    public class ManageReports : IManager<Report>
    {
        private readonly string GET_ALL = "SELECT * FROM Reports ORDER BY Completed DESC";
        private readonly string GET_ONE = "SELECT * FROM Reports WHERE ReportId = @Id";
        private readonly string INSERT = "INSERT INTO Reports (CVR, AuditorId) VALUES (@CVR, @AuditorId) SELECT SCOPE_IDENTITY() AS [Id]";
        private readonly string COMPLETE_REPORT = "UPDATE Reports SET Completed = @Completed WHERE ReportId = @ReportId";
        private readonly string GET_PARTICIPANTS = "SELECT EmployeeId FROM Participants WHERE ReportId = @Id";
        private readonly string GET_BY_CUSTOMER = "SELECT * FROM Reports WHERE CVR = @CVR ORDER BY Completed DESC";
        private readonly string ARCHIVE_REPORT = "UPDATE Reports SET Archived = @Archived WHERE ReportId = @ReportId";
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
            if (!reader.IsDBNull(2)) { report.Customer.CVR = reader.GetInt32(2); }
            if (!reader.IsDBNull(3)) { report.Auditor.Id = reader.GetInt32(3); }
            if (!reader.IsDBNull(4)) { report.Archived = reader.GetDateTime(4); }

            return report;
        }

        public override IEnumerable<Report> Get()
        {
            List<Report> liste = new List<Report>();

            using (SqlConnection conn = new SqlConnection(ConnectionString)) //Send conn med videre som parameter
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

            foreach (Report report in liste)
            {
                report.Customer = new ManageCustomers().Get(report.Customer.CVR);
                report.Auditor = new ManageAuditors().Get(report.Auditor.Id);
                report.LoadAnswers(new ManageQuestionAnswers().GetFromReport(report.Id));
                report.LoadEmployees(GetParticipants(report.Id));
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
            report.Customer = new ManageCustomers().Get(report.Customer.CVR);
            report.Auditor = new ManageAuditors().Get(report.Auditor.Id);
            report.LoadAnswers(new ManageQuestionAnswers().GetFromReport(report.Id));
            report.LoadEmployees(GetParticipants(report.Id));

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

        public IEnumerable<Report> GetByCustomer(int cvr)
        {
            List<Report> liste = new List<Report>();

            using (SqlConnection conn = new SqlConnection(ConnectionString)) //Send conn med videre som parameter
            using (SqlCommand cmd = new SqlCommand(GET_BY_CUSTOMER, conn))
            {
                cmd.Parameters.AddWithValue("@CVR", cvr);
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    Report item = ReadNextElement(reader);
                    liste.Add(item);
                }

                reader.Close();
            }

            foreach (Report report in liste)
            {
                report.Customer = new ManageCustomers().Get(report.Customer.CVR);
                report.Auditor = new ManageAuditors().Get(report.Auditor.Id);
                report.LoadAnswers(new ManageQuestionAnswers().GetFromReport(report.Id));
                report.LoadEmployees(GetParticipants(report.Id));
            }

            return liste;
        }

        public int Post(Report report)
        {
            int id = 0;

            using (SqlConnection conn = new SqlConnection(ConnectionString))
            using (SqlCommand cmd = new SqlCommand(INSERT, conn))
            {
                conn.Open();

                cmd.Parameters.AddWithValue("@CVR", report.Customer.CVR);
                cmd.Parameters.AddWithValue("@AuditorId", report.Auditor.Id);

                //Returns true if query returns higher than 0 (affected rows)
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    id = Decimal.ToInt32(reader.GetDecimal(0));
                };
            }

            return id;
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

        public void Archive(int id)
        {
            using (SqlConnection conn = new SqlConnection(ConnectionString))
            using (SqlCommand cmd = new SqlCommand(ARCHIVE_REPORT, conn))
            {
                conn.Open();

                cmd.Parameters.AddWithValue("@Archived", DateTime.Now.Date);
                cmd.Parameters.AddWithValue("@ReportId", id);

                cmd.ExecuteNonQuery();
            }
        }

        public async void GenerateReport(int id)
        {
            Report report = Get(id);

            String reportTitle = report.Customer.Name + " - KLS tjekliste og rapport fra intern efterprøvning - " +
                                 report.Completed.ToShortDateString();

            //Go to website (get HTML)
            string url = "http://localhost:3000/audit/report/" + id;
            await new BrowserFetcher().DownloadAsync(BrowserFetcher.DefaultRevision);
            var browser = await Puppeteer.LaunchAsync(new LaunchOptions
            {
                Headless = true
            });
            var page = await browser.NewPageAsync();
            await page.GoToAsync(url);
            string html = await page.GetContentAsync();
            //string html = Properties.Resources.rapport;

            //Generate PDF file from HTML
            await page.PdfAsync("C:/temp/" + reportTitle + ".pdf");
            await page.CloseAsync();

            //Generate Docx file from HTML
            string docxname = "C:/temp/" + reportTitle + ".docx";
            if (File.Exists(docxname)) File.Delete(docxname);
            using (MemoryStream generatedDocument = new MemoryStream())
            {
                using (WordprocessingDocument package = WordprocessingDocument.Create(generatedDocument, WordprocessingDocumentType.Document))
                {
                    MainDocumentPart mainPart = package.MainDocumentPart;
                    if (mainPart == null)
                    {
                        mainPart = package.AddMainDocumentPart();
                        new Document(new Body()).Save(mainPart);
                    }

                    HtmlConverter converter = new HtmlConverter(mainPart);
                    converter.BaseImageUrl = new Uri(url);
                    converter.ParseHtml(html);

                    mainPart.Document.Save();
                }

                File.WriteAllBytes(docxname, generatedDocument.ToArray());
            }
        }

        public void Dropbox()
        {

        }
    }
}
