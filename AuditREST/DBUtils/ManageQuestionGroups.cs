using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using AuditREST.Models;

namespace AuditREST.DBUtils
{
    public class ManageQuestionGroups: IManager<QuestionGroup>
    {
        private string GET_ALL = "SELECT * FROM QuestionGroups";
        private string GET_ONE = "SELECT * FROM QuestionGroups WHERE QuestionGroupId = @QuestionId";
        private string GET_ALL_IN_CHECKLIST = "SELECT * FROM QuestionGroups WHERE ChecklistId = @ChecklistId";
        public override string ConnectionString { get; set; }

        public ManageQuestionGroups()
        {
            ConnectionString = new ConnectionString().ConnectionStreng;
        }

        public override IEnumerable<QuestionGroup> Get()
        {
            List<QuestionGroup> liste = new List<QuestionGroup>();

            using (SqlConnection conn = new SqlConnection(ConnectionString))
            using (SqlCommand cmd = new SqlCommand(GET_ALL, conn))
            {
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    QuestionGroup item = ReadNextElement(reader);
                    liste.Add(item);
                }
                reader.Close();
            }

            return liste;
        }

        public override QuestionGroup ReadNextElement(SqlDataReader reader)
        {
            QuestionGroup questionGroup = new QuestionGroup();

            if (!reader.IsDBNull(0)) { questionGroup.Id = reader.GetInt32(0); }
            if (!reader.IsDBNull(1)) { questionGroup.Name = reader.GetString(1); }
            if (!reader.IsDBNull(2)) { questionGroup.ChecklistId = reader.GetInt32(2); }

            questionGroup.Questions = new ManageQuestions().GetInQuestionGroup(questionGroup.Id);

            //SubQuestions
            //if (!reader.IsDBNull(3)) { QuestionGroup. = reader.GetString(3); }

            return questionGroup;
        }

        public override QuestionGroup Get(int id)
        {
            QuestionGroup questionGroup = null;

            using (SqlConnection conn = new SqlConnection(ConnectionString))
            using (SqlCommand cmd = new SqlCommand(GET_ONE, conn))
            {
                conn.Open();

                cmd.Parameters.AddWithValue("@QuestionId", id);

                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    questionGroup = ReadNextElement(reader);
                }

                reader.Close();
            }

            return questionGroup;
        }

        public List<QuestionGroup> GetInChecklist(int id)
        {
            List<QuestionGroup> liste = new List<QuestionGroup>();

            using (SqlConnection conn = new SqlConnection(ConnectionString))
            using (SqlCommand cmd = new SqlCommand(GET_ALL_IN_CHECKLIST, conn))
            {
                conn.Open();
                cmd.Parameters.AddWithValue("@ChecklistId", id);
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                { 
                    QuestionGroup item = ReadNextElement(reader);
                    liste.Add(item);
                }
                reader.Close();
            }

            return liste;

        }
    }
}
