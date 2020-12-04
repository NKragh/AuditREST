using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using AuditREST.Models;

namespace AuditREST.DBUtils
{
    public class ManageQuestions: IManager<Question>
    {
        private string GET_ALL = "SELECT * FROM Questions";
        private string GET_ALL_IN_QUESTIONGROUP = "SELECT * FROM Questions WHERE QuestionGroupId = @QuestionId";
        private string GET_ONE = "SELECT * FROM Questions WHERE QuestionId = @QuestionId";
        private string INSERT = "INSERT INTO Questions (Text, Type, QuestionGroupId) VALUES (@Text, @Type, @QuestionGroupId)";
        private string DELETE = "DELETE FROM Questions WHERE QuestionId = @QuestionId";

        public string ConnectionString { get; set; }

        public ManageQuestions()
        {
            ConnectionString = new ConnectionString().ConnectionStreng;
        }

        public IEnumerable<Question> Get()
        {
            List<Question> liste = new List<Question>();

            using (SqlConnection conn = new SqlConnection(ConnectionString))
            using (SqlCommand cmd = new SqlCommand(GET_ALL, conn))
            {
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    Question item = ReadNextElement(reader);
                    liste.Add(item);
                }
                reader.Close();
            }

            return liste;
        }

        private Question ReadNextElement(SqlDataReader reader)
        {
            Question question = new Question();

            if (!reader.IsDBNull(0)) { question.QuestionId = reader.GetInt32(0); }
            if (!reader.IsDBNull(1)) { question.Text = reader.GetString(1); }
            if (!reader.IsDBNull(2)) { question.QuestionGroupId = reader.GetInt32(2); }
            if (!reader.IsDBNull(3)) { question.AnswerType = new ManageAnswerTypes().Get(reader.GetInt32(3)); }
            
            question.SubQuestions = new ManageSubQuestions().GetWithParentQuestionId(question.QuestionId);

            return question;
        }

        public Question Get(int id)
        {
            Question question = null;

            using (SqlConnection conn = new SqlConnection(ConnectionString))
            using (SqlCommand cmd = new SqlCommand(GET_ONE, conn))
            {
                conn.Open();

                cmd.Parameters.AddWithValue("@QuestionId", id);

                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    question = ReadNextElement(reader);
                }

                reader.Close();
            }

            return question;
        }

        public bool Create(Question question)
        {
            using (SqlConnection conn = new SqlConnection(ConnectionString))
            using (SqlCommand cmd = new SqlCommand(INSERT, conn))
            {
                conn.Open();

                cmd.Parameters.AddWithValue("@Text", question.Text);
                cmd.Parameters.AddWithValue("@Type", question.AnswerType.AnswerTypeId);
                cmd.Parameters.AddWithValue("@QuestionGroupId", question.QuestionGroupId);

                //Returns true if query returns higher than 0 (affected rows)
                return cmd.ExecuteNonQuery() > 0;
            }
        }

        public bool Delete(int id)
        {
            using (SqlConnection conn = new SqlConnection(ConnectionString))
            using (SqlCommand cmd = new SqlCommand(DELETE, conn))
            {
                conn.Open();
                cmd.Parameters.AddWithValue("@QuestionId", id);
                return cmd.ExecuteNonQuery() > 0;
            }
        }

        public List<Question> GetInQuestionGroup(int id)
        {
            List<Question> liste = new List<Question>();

            using (SqlConnection conn = new SqlConnection(ConnectionString))
            using (SqlCommand cmd = new SqlCommand(GET_ALL_IN_QUESTIONGROUP, conn))
            {
                conn.Open();
                cmd.Parameters.AddWithValue("@QuestionId", id);
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    Question item = ReadNextElement(reader);
                    liste.Add(item);
                }
                reader.Close();
            }

            return liste;
        }
    }
}
