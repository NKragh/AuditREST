using System.Collections.Generic;
using System.Data.SqlClient;
using AuditREST.Models;

namespace AuditREST.DBUtils
{
    public class ManageQuestionAnswers : IManager<QuestionAnswer>
    {
        private static string GET_WITH_REMARK = "SELECT qa.*, CASE WHEN qa.Answer = 'OK' THEN r.OK WHEN qa.Answer = 'Afvigelse' THEN r.Afvigelse " +
                                         "WHEN qa.Answer = 'Observation' THEN r.Observation WHEN qa.Answer = 'Forbedring' THEN r.Forbedring " +
                                         "WHEN qa.Answer = 'Ikke relevant' THEN r.[Ikke relevant] END AS Remark " +
                                         "FROM QuestionAnswers AS qa JOIN Remarks AS r ON r.QuestionId = qa.QuestionId";

        private static string GET_ONE = GET_WITH_REMARK + " WHERE qa.AnswerId = @Id";
        private static string GET_IN_REPORT = GET_WITH_REMARK + " WHERE qa.ReportId = @Id";
        private static string INSERT = "INSERT INTO QuestionAnswers (Answer, Comment, CVR, QuestionId, AuditorId, ReportId) " +
                                 "VALUES (@Answer, @Comment, @CVR, @QuestionId, @AuditorId, @ReportId)";


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
            if (!reader.IsDBNull(7)) { questionAnswer.Remark = reader.GetString(7); }

            return questionAnswer;
        }

        public override IEnumerable<QuestionAnswer> Get()
        {
            List<QuestionAnswer> liste = new List<QuestionAnswer>();

            using (SqlConnection conn = new SqlConnection(ConnectionString))
            using (SqlCommand cmd = new SqlCommand(GET_WITH_REMARK, conn))
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
