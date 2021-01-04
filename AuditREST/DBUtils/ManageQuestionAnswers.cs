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
        private string INSERT = "INSERT INTO QuestionAnswers (Answer, Comment, CVR, QuestionId, AuditorId, ReportId) " +
                                "VALUES (@Answer, @Comment, @CVR, @QuestionId, @AuditorId, @ReportId)";

        private string GET_WITH_REMARK = "SELECT qa.*, r.OK, r.Afvigelse, r.Observation, r.Forbedring, r.[Ikke relevant] FROM QuestionAnswers AS qa JOIN Remarks AS r ON r.QuestionId = qa.QuestionId";

        public override string ConnectionString { get; set; }

        public override QuestionAnswer ReadNextElement(SqlDataReader reader)
        {
            QuestionAnswer questionAnswer = new QuestionAnswer();

            if (!reader.IsDBNull(0)) { questionAnswer.Id = reader.GetInt32(0); }
            if (!reader.IsDBNull(1)) { questionAnswer.Answer = reader.GetString(1); }
            if (!reader.IsDBNull(2)) { questionAnswer.Comment = reader.GetString(2); }
            if (!reader.IsDBNull(3)) { questionAnswer.CVR = reader.GetInt32(3); }
            if (!reader.IsDBNull(4)) { questionAnswer.QuestionId = reader.GetInt32(4); }
            if (!reader.IsDBNull(5)) { questionAnswer.AuditorId = reader.GetInt32(5); }
            if (!reader.IsDBNull(6)) { questionAnswer.ReportId = reader.GetInt32(6); }

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
                questionAnswer.Remark =
                    new ManageRemarks().GetRemarkText(questionAnswer.QuestionId, questionAnswer.Answer);
            }
            return liste;
        }
        
        public IEnumerable<QuestionAnswer> TestGet()
        {
            List<QuestionAnswer> liste = new List<QuestionAnswer>();

            using (SqlConnection conn = new SqlConnection(ConnectionString))
            using (SqlCommand cmd = new SqlCommand(GET_ALL, conn))
            {
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    QuestionAnswer questionAnswer = new QuestionAnswer();

                    if (!reader.IsDBNull(0)) { questionAnswer.Id = reader.GetInt32(0); }
                    if (!reader.IsDBNull(1)) { questionAnswer.Answer = reader.GetString(1); }
                    if (!reader.IsDBNull(2)) { questionAnswer.Comment = reader.GetString(2); }
                    if (!reader.IsDBNull(3)) { questionAnswer.CVR = reader.GetInt32(3); }
                    if (!reader.IsDBNull(4)) { questionAnswer.QuestionId = reader.GetInt32(4); }
                    if (!reader.IsDBNull(5)) { questionAnswer.AuditorId = reader.GetInt32(5); }
                    if (!reader.IsDBNull(6)) { questionAnswer.ReportId = reader.GetInt32(6); }


                    liste.Add(questionAnswer);
                }


                reader.Close();


            }
            foreach (QuestionAnswer questionAnswer in liste)
            {
                questionAnswer.Remark =
                    new ManageRemarks().GetRemarkText(questionAnswer.QuestionId, questionAnswer.Answer);
            }

            //questionAnswer.CVR = new ManageCustomers().Get(questionAnswer.CVR.CVR);
            //questionAnswer.AuditorId = new ManageAuditors().Get(questionAnswer.AuditorId.Id);
            //questionAnswer.ReportId = new ManageReports().Get(questionAnswer.ReportId.Id);
            //questionAnswer.QuestionId = new ManageQuestions().Get(questionAnswer.QuestionId.QuestionId);
            //}

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
            
            questionAnswer.Remark = new ManageRemarks().GetRemarkText(questionAnswer.QuestionId, questionAnswer.Answer);

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
                questionAnswer.Remark =
                    new ManageRemarks().GetRemarkText(questionAnswer.QuestionId, questionAnswer.Answer);
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
                cmd.Parameters.AddWithValue("@Comment", questionAnswer.Comment);
                cmd.Parameters.AddWithValue("@CVR", questionAnswer.CVR);
                cmd.Parameters.AddWithValue("@AuditorId", questionAnswer.AuditorId);
                cmd.Parameters.AddWithValue("@QuestionId", questionAnswer.QuestionId);
                cmd.Parameters.AddWithValue("@ReportId", questionAnswer.ReportId);

                //Returns true if query returns higher than 0 (affected rows)
                return cmd.ExecuteNonQuery() > 0;
            }
        }
    }
}
