using System.Collections.Generic;
using System.Data.SqlClient;
using AuditREST.Models;

namespace AuditREST.DBUtils
{
    public class ManageQuestionAnswers : IManager<QuestionAnswer>
    {
        private string GET_ALL = "SELECT * FROM QuestionAnswers";
        private string GET_ONE = "SELECT * FROM QuestionAnswers WHERE AnswerId = @Id";
        private string GET_IN_REPORT = "SELECT * FROM QuestionAnswers WHERE ReportId = @Id";
        private string INSERT = "INSERT INTO QuestionAnswers (Answer, Remark, Comment, CVR, QuestionId, AuditorId, ReportId) " +
                                "VALUES (@Answer, @Remark, @Comment, @CVR, @QuestionId, @AuditorId, @ReportId)";

        public override string ConnectionString { get; set; }

        public override QuestionAnswer ReadNextElement(SqlDataReader reader)
        {
            QuestionAnswer questionAnswer = new QuestionAnswer();

            if (!reader.IsDBNull(0)) { questionAnswer.Id = reader.GetInt32(0); }
            if (!reader.IsDBNull(1)) { questionAnswer.Answer = reader.GetString(1); }
            if (!reader.IsDBNull(2)) { questionAnswer.Remark = reader.GetString(2); }
            if (!reader.IsDBNull(3)) { questionAnswer.Comment = reader.GetString(3); }

            if (!reader.IsDBNull(4)) { questionAnswer.Customer.CVR = reader.GetInt32(4); }
            if (!reader.IsDBNull(5)) { questionAnswer.Question.QuestionId = reader.GetInt32(5); }
            if (!reader.IsDBNull(6)) { questionAnswer.Auditor.Id = reader.GetInt32(6); }
            if (!reader.IsDBNull(7)) { questionAnswer.Report.Id = reader.GetInt32(7); }

            return questionAnswer;
        }

        public override IEnumerable<QuestionAnswer> Get()
        {
            List<QuestionAnswer> liste = new List<QuestionAnswer>();

            using (SqlConnection conn = new SqlConnection(ConnectionString))
            using (SqlCommand cmd = new SqlCommand(GET_ALL, conn))
            {
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    QuestionAnswer item = ReadNextElement(reader);
                    liste.Add(item);
                }


                reader.Close();
            }
                foreach (QuestionAnswer questionAnswer in liste)
                {
                    questionAnswer.Customer = new ManageCustomers().Get(questionAnswer.Customer.CVR);
                    questionAnswer.Auditor = new ManageAuditors().Get(questionAnswer.Auditor.Id);
                    questionAnswer.Report = new ManageReports().Get(questionAnswer.Report.Id);
                    questionAnswer.Question = new ManageQuestions().Get(questionAnswer.Question.QuestionId);
                }

            return liste;
        }

        public override QuestionAnswer Get(int id)
        {
            QuestionAnswer questionAnswer = new QuestionAnswer();

            using (SqlConnection conn = new SqlConnection(ConnectionString))
            using (SqlCommand cmd = new SqlCommand(GET_ONE, conn))
            {
                conn.Open();

                cmd.Parameters.AddWithValue("@Id", id);

                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    questionAnswer = ReadNextElement(reader);
                }

                reader.Close();
            }

            questionAnswer.Customer = new ManageCustomers().Get(questionAnswer.Customer.CVR);
            questionAnswer.Auditor = new ManageAuditors().Get(questionAnswer.Auditor.Id);
            questionAnswer.Report = new ManageReports().Get(questionAnswer.Report.Id);
            questionAnswer.Question = new ManageQuestions().Get(questionAnswer.Question.QuestionId);

            return questionAnswer;
        }

        public List<QuestionAnswer> GetFromReport(int reportId)
        {
            List<QuestionAnswer> liste = new List<QuestionAnswer>();

            using (SqlConnection conn = new SqlConnection(ConnectionString))
            using (SqlCommand cmd = new SqlCommand(GET_IN_REPORT, conn))
            {
                conn.Open();
                cmd.Parameters.AddWithValue("@Id", reportId);
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    QuestionAnswer item = ReadNextElement(reader);
                    liste.Add(item);
                }


                reader.Close();
            }
                foreach (QuestionAnswer questionAnswer in liste)
                {
                    questionAnswer.Customer = new ManageCustomers().Get(questionAnswer.Customer.CVR);
                    questionAnswer.Auditor = new ManageAuditors().Get(questionAnswer.Auditor.Id);
                    questionAnswer.Report = new ManageReports().Get(questionAnswer.Report.Id);
                    questionAnswer.Question = new ManageQuestions().Get(questionAnswer.Question.QuestionId);
                }

            return liste;
        }

        public bool Post(QuestionAnswer questionAnswer)
        {
            using (SqlConnection conn = new SqlConnection(ConnectionString))
            using (SqlCommand cmd = new SqlCommand(INSERT, conn))
            {
                conn.Open();

                cmd.Parameters.AddWithValue("@Answer", questionAnswer.Answer);
                cmd.Parameters.AddWithValue("@Remark", questionAnswer.Remark);
                cmd.Parameters.AddWithValue("@Comment", questionAnswer.Comment);

                cmd.Parameters.AddWithValue("@CVR", questionAnswer.Customer.CVR);
                cmd.Parameters.AddWithValue("@AuditorId", questionAnswer.Auditor.Id);
                cmd.Parameters.AddWithValue("@QuestionId", questionAnswer.Question.QuestionId);
                cmd.Parameters.AddWithValue("@ReportId", questionAnswer.Report.Id);

                //Returns true if query returns higher than 0 (affected rows)
                return cmd.ExecuteNonQuery() > 0;
            }
        }
    }
}
