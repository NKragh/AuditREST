using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using AuditREST.Models;

namespace AuditREST.DBUtils
{
    public class ManageSubQuestions
    {
        private string GET_ALL = "SELECT * FROM Questions WHERE ParentQuestionId IS NOT NULL";
        private string GET_ONE = "SELECT * FROM Questions WHERE QuestionId = @Id";

        private string GET_ALL_WITH_PARENTQUESTIONID = "SELECT * FROM Questions WHERE ParentQuestionId = @Id";

        //private string INSERT = "INSERT INTO Questions (Text, Type, QuestionGroupId, ParentQuestionId) VALUES (@Text, @Type, @QuestionGroupId, @ParentQuestionId)";
        //private string DELETE = "DELETE FROM SubQuestions WHERE SubQuestionId = @Id";
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

            if (!reader.IsDBNull(0)) { question.Id = reader.GetInt32(0); }
            if (!reader.IsDBNull(1)) { question.Text = reader.GetString(1); }
            if (!reader.IsDBNull(2)) { question.Type = reader.GetString(2); }
            if (!reader.IsDBNull(3)) { question.ParentId = reader.GetInt32(3); }
            if (!reader.IsDBNull(4)) { question.QuestionGroupId = reader.GetInt32(4); }
            
            return question;
        }

        public SubQuestion Get(int id)
        {
            SubQuestion question = null;

            using (SqlConnection conn = new SqlConnection(ConnectionString))
            using (SqlCommand cmd = new SqlCommand(GET_ONE, conn))
            {
                conn.Open();

                cmd.Parameters.AddWithValue("@Id", id);

                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    question = ReadNextElement(reader);
                }

                reader.Close();
            }

            return question;
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
        //        cmd.Parameters.AddWithValue("@Id", id);
        //        return cmd.ExecuteNonQuery() > 0;
        //    }
        //}
        public List<SubQuestion> GetWithParentQuestionId(int questionId)
        {
            List<SubQuestion> liste = new List<SubQuestion>();

            using (SqlConnection conn = new SqlConnection(ConnectionString))
            using (SqlCommand cmd = new SqlCommand(GET_ALL_WITH_PARENTQUESTIONID, conn))
            {
                conn.Open();
                cmd.Parameters.AddWithValue("@Id", questionId);
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
    }
}
