﻿using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using AuditREST.Models;

namespace AuditREST.DBUtils
{
    public class ManageChecklists : IManager<Checklist>
    {
        public string ConnectionString { get; set; }

        private string GET_ALL = "SELECT * FROM Checklists";
        //private string GET_ALL = "SELECT q.*, qg.QuestionGroupTitle, qg.ChecklistId, c.Name\r\nFROM Questions as q\r\nJOIN QuestionGroups as qg ON qg.QuestionGroupId = q.QuestionGroupId\r\nJOIN Checklists as c ON c.ChecklistId = qg.ChecklistId";
        private string INSERT = "INSERT INTO Checklists (Name) VALUES (@Name)";
        private string GET_ONE = "SELECT * FROM Checklists WHERE ChecklistId = @Id";
        private string DELETE = "DELETE FROM Checklists WHERE ChecklistId = @Id";
        private string UPDATE = "";

        public ManageChecklists()
        {
            ConnectionString = new ConnectionString().ConnectionStreng;
        }

        public IEnumerable<Checklist> Get()
        {
            List<Checklist> liste = new List<Checklist>();
            using (SqlConnection conn = new SqlConnection(ConnectionString))
            using (SqlCommand cmd = new SqlCommand(GET_ALL, conn))
            {
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    Checklist item = ReadNextElement(reader);
                    liste.Add(item);
                }
                reader.Close();
            }
            return liste;
        }

        private Checklist ReadNextElement(SqlDataReader reader)
        {
            Checklist cl = new Checklist();
            QuestionGroup qg = new QuestionGroup();

            if (!reader.IsDBNull(0)) { cl.Id = reader.GetInt32(0); }
            if (!reader.IsDBNull(1)) { cl.Name = reader.GetString(1); }
            
            cl.QuestionGroups = new ManageQuestionGroups().GetInQuestionGroup(cl.Id);
            

            return cl;
        }

        public Checklist Get(int id)
        {
            Checklist checklist = null;
            using (SqlConnection conn = new SqlConnection(ConnectionString))
            using (SqlCommand cmd = new SqlCommand(GET_ONE, conn))
            {
                conn.Open();

                cmd.Parameters.AddWithValue("@Id", id);

                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    checklist = ReadNextElement(reader);
                }

                reader.Close();
            }
            return checklist;
        }

        public bool Create(Checklist checklist)
        {
            bool succes;
            using (SqlConnection conn = new SqlConnection(ConnectionString))
            using (SqlCommand cmd = new SqlCommand(INSERT, conn))
            {
                conn.Open();

                cmd.Parameters.AddWithValue("@Name", checklist.Name);

                int res = cmd.ExecuteNonQuery();
                succes = (res > 0);
            }

            return succes;
        }


        public bool Delete(int id)
        {
            using (SqlConnection conn = new SqlConnection(ConnectionString))
            using (SqlCommand cmd = new SqlCommand(DELETE, conn))
            {
                conn.Open();
                cmd.Parameters.AddWithValue("@Id", id);
                int res = cmd.ExecuteNonQuery();
                return res > 0;
            }
        }

        public bool Update(Checklist prevChecklist, Checklist updatedChecklist)
        {
            using (SqlConnection conn = new SqlConnection(ConnectionString))
            using (SqlCommand cmd = new SqlCommand(UPDATE, conn))
            {
                conn.Open();

                cmd.Parameters.AddWithValue("@Id", updatedChecklist.Id);
                

                return cmd.ExecuteNonQuery() > 0;
            }
        }
    }
}
