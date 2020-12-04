using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using AuditREST.Models;

namespace AuditREST.DBUtils
{
    public class ManageAnswerTypes: IManager<AnswerType>
    {
        private string GET_ALL = "SELECT * FROM AnswerTypes";
        private string GET_ONE = "SELECT * FROM AnswerTypes WHERE AnswerTypeId = @Id";

        public string ConnectionString { get; set; }

        public ManageAnswerTypes()
        {
            ConnectionString = new ConnectionString().ConnectionStreng;
        }

        public IEnumerable<AnswerType> Get()
        {
            List<AnswerType> liste = new List<AnswerType>();

            using (SqlConnection conn = new SqlConnection(ConnectionString))
            using (SqlCommand cmd = new SqlCommand(GET_ALL, conn))
            {
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    AnswerType item = ReadNextElement(reader);
                    liste.Add(item);
                }
                reader.Close();
            }

            return liste;
        }

        private AnswerType ReadNextElement(SqlDataReader reader)
        {
            AnswerType question = new AnswerType();

            if (!reader.IsDBNull(0)) { question.AnswerTypeId = reader.GetInt32(0); }
            if (!reader.IsDBNull(1)) { question.AnswerOption = reader.GetString(1); }

            return question;
        }

        public AnswerType Get(int id)
        {
            AnswerType question = null;

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

        public bool Create(AnswerType input)
        {
            throw new NotImplementedException();
        }
    }
}
