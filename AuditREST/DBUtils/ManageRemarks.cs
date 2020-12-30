using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using AuditREST.Models;

namespace AuditREST.DBUtils
{
    public class ManageRemarks : IManager<Remark>
    {
        private string GET_ALL = "SELECT * FROM Remarks WHERE QuestionId = @Id";
        private string GET_Ok = "SELECT OK FROM Remarks WHERE QuestionId = @Id";
        private string GET_Afvigelse = "SELECT Afvigelse FROM Remarks WHERE QuestionId = @Id";
        private string GET_Observation = "SELECT Observation FROM Remarks WHERE QuestionId = @Id";
        private string GET_Forbedring = "SELECT Forbedring FROM Remarks WHERE QuestionId = @Id";
        private string GET_IkkeRelevant = "SELECT [Ikke Relevant] FROM Remarks WHERE QuestionId = @Id";

        public String GetRemarkText(int questionid, string answer)
        {
            String sql = "";
            switch (answer)
            {
                case "OK":
                    sql = GET_Ok;
                    break;
                case "Afvigelse":
                    sql = GET_Afvigelse;
                    break;
                case "Observation":
                    sql = GET_Observation;
                    break;
                case "Forbedring":
                    sql = GET_Forbedring;
                    break;
                case "IkkeRelevant":
                    sql = GET_IkkeRelevant;
                    break;
                default:
                    return "";
            }

            using (SqlConnection conn = new SqlConnection(ConnectionString))
            using (SqlCommand cmd = new SqlCommand(sql, conn))
            {
                conn.Open();
                cmd.Parameters.AddWithValue("@Id", questionid);

                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    if (!reader.IsDBNull(0)) { return reader.GetString(0); }
                }
                reader.Close();
            }

            return "";
        }

        public override string ConnectionString { get; set; }
        public override Remark ReadNextElement(SqlDataReader reader)
        {
            throw new NotImplementedException();
        }

        public override IEnumerable<Remark> Get()
        {
            throw new NotImplementedException();
        }

        public override Remark Get(int id)
        {
            throw new NotImplementedException();
        }
    }
}

/*
if (!reader.IsDBNull(8)) { questionAnswer.Remark.Ok = reader.GetString(7); }
if (!reader.IsDBNull(8)) { questionAnswer.Remark.Afvigelse = reader.GetString(8); }
if (!reader.IsDBNull(8)) { questionAnswer.Remark.Observation = reader.GetString(9); }
if (!reader.IsDBNull(8)) { questionAnswer.Remark.Forbedring = reader.GetString(10); }
if (!reader.IsDBNull(8)) { questionAnswer.Remark.IkkeRelevant = reader.GetString(11); }
*/