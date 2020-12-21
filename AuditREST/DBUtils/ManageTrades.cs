using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using AuditREST.Models;

namespace AuditREST.DBUtils
{
    public class ManageTrades : IManager<Trade>
    {
        public List<Trade> Trades { get; set; }
        public override string ConnectionString { get; set; }

        private string GET_ALL = "SELECT * FROM Trades";
        private string GET_ON_QUESTION = "SELECT q.*, t.* FROM QuestionTrades as qt JOIN Questions as q ON q.QuestionId = qt.QuestionId " +
                                            "JOIN Trades as t ON t.TradeId = qt.TradeId WHERE qt.QuestionId = @QuestionId";
        private string GET_ONE = "SELECT * FROM Trades WHERE TradeId = @Id";

        public ManageTrades()
        {
            ConnectionString = new ConnectionString().ConnectionStreng;
            Trades = new List<Trade>();
        }

        public override Trade ReadNextElement(SqlDataReader reader)
        {
            Trade t = new Trade();

            if (!reader.IsDBNull(0)) { t.TradeId = reader.GetInt32(0); }
            if (!reader.IsDBNull(1)) { t.Name = reader.GetString(1); }

            return t;
        }

        public override IEnumerable<Trade> Get()
        {
            List<Trade> liste = new List<Trade>();
            using (SqlConnection conn = new SqlConnection(ConnectionString))
            using (SqlCommand cmd = new SqlCommand(GET_ALL, conn))
            {
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    Trade item = ReadNextElement(reader);
                    liste.Add(item);
                }
                reader.Close();
            }
            return liste;
        }

        public List<Trade> GetOnQuestion(Question q)
        {
            List<Trade> liste = new List<Trade>();
            using (SqlConnection conn = new SqlConnection(ConnectionString))
            using (SqlCommand cmd = new SqlCommand(GET_ON_QUESTION, conn))
            {
                conn.Open();

                cmd.Parameters.AddWithValue("@QuestionId", q.QuestionId);

                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    Trade item = ReadNextElement(reader);
                    liste.Add(item);
                }
                reader.Close();
            }
            return liste;
        }

        public override Trade Get(int id)
        {
            Trade trade = new Trade();

            using (SqlConnection conn = new SqlConnection(ConnectionString))
            using (SqlCommand cmd = new SqlCommand(GET_ONE, conn))
            {
                conn.Open();

                cmd.Parameters.AddWithValue("@Id", id);

                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    trade = ReadNextElement(reader);
                }
                reader.Close();
            }

            return trade;
        }
    }
}
