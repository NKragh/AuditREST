using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using AuditREST.Models;

namespace AuditREST.DBUtils
{
    public class ManageSubQuestions: IManager<SubQuestion>
    {
        private string GET_ALL = "SELECT * FROM SubQuestions";
        private string GET_ONE = "SELECT * FROM SubQuestions WHERE SubQuestionId = @QuestionId";

        private string GET_ALL_WITH_PARENTQUESTIONID = "SELECT * FROM SubQuestions WHERE ParentQuestionId = @QuestionId";

        //private string INSERT = "INSERT INTO Questions (Text, Type, QuestionGroupId, ParentQuestionId) VALUES (@Text, @Type, @QuestionGroupId, @ParentQuestionId)";
        //private string DELETE = "DELETE FROM SubQuestions WHERE SubQuestionId = @QuestionId";

        public string ConnectionString { get; set; }

        public ManageSubQuestions()
        {
            ConnectionString = new ConnectionString().ConnectionStreng;
        }

        public IEnumerable<SubQuestion> Get()
        {
            List<SubQuestion> liste = new List<SubQuestion>();

            using (SqlConnection conn = new SqlConnection(ConnectionString))
            using (SqlCommand cmd = new SqlCommand(GET_ALL, conn))
            {
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    SubQuestion item = ReadNextElement(reader);
                    liste.Add(item);
                }
                reader.Close();
            }

            return liste;
        }

        private SubQuestion ReadNextElement(SqlDataReader reader)
        {
            SubQuestion question = new SubQuestion();

            if (!reader.IsDBNull(0)) { question.SubQuestionId = reader.GetInt32(0); }
            if (!reader.IsDBNull(1)) { question.Text = reader.GetString(1); }
            if (!reader.IsDBNull(2)) { question.ParentId = reader.GetInt32(2); }

            question.AnswerType = new ManageAnswerTypes().Get(reader.GetInt32(3));

            return question;
        }

        public SubQuestion Get(int id)
        {
            SubQuestion question = null;

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

        public bool Create(SubQuestion input)
        {
            throw new NotImplementedException();
        }

        public List<SubQuestion> GetWithParentQuestionId(int questionId)
        {
            List<SubQuestion> liste = new List<SubQuestion>();

            using (SqlConnection conn = new SqlConnection(ConnectionString))
            using (SqlCommand cmd = new SqlCommand(GET_ALL_WITH_PARENTQUESTIONID, conn))
            {
                conn.Open();
                cmd.Parameters.AddWithValue("@QuestionId", questionId);
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    SubQuestion item = ReadNextElement(reader);
                    liste.Add(item);
                }
                reader.Close();
            }

            return liste;
        }

        //public bool Create(SubQuestion question)
        //{
        //    using (SqlConnection conn = new SqlConnection(ConnectionString))
        //    using (SqlCommand cmd = new SqlCommand(INSERT, conn))
        //    {
        //        conn.Open();

        //        cmd.Parameters.AddWithValue("@Text", question.Text);
        //        cmd.Parameters.AddWithValue("@Type", question.Type);
        //        cmd.Parameters.AddWithValue("@QuestionGroupId", question.QuestionGroupId);

        //        //Returns true if query returns higher than 0 (affected rows)
        //        return cmd.ExecuteNonQuery() > 0;
        //    }
        //}

        //public bool Delete(int id)
        //{
        //    using (SqlConnection conn = new SqlConnection(ConnectionString))
        //    using (SqlCommand cmd = new SqlCommand(DELETE, conn))
        //    {
        //        conn.Open();
        //        cmd.Parameters.AddWithValue("@QuestionId", id);
        //        return cmd.ExecuteNonQuery() > 0;
        //    }
        //}
    }
}
