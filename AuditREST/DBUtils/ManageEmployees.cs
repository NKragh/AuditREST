﻿using System.Collections.Generic;
using System.Data.SqlClient;
using AuditREST.Models;

namespace AuditREST.DBUtils
{
    public class ManageEmployees : IManager<Employee>
    {
        private string GET_ALL = "SELECT * FROM Employees";
        private string GET_ONE = "SELECT * FROM Employees WHERE EmployeeId = @Id";
        public override string ConnectionString { get; set; }

        public ManageEmployees()
        {
            ConnectionString = new ConnectionString().ConnectionStreng;
        }

        public override Employee ReadNextElement(SqlDataReader reader)
        {
            Employee employee = new Employee();

            if (!reader.IsDBNull(0)) { employee.Id = reader.GetInt32(0); }
            if (!reader.IsDBNull(1)) { employee.FirstName = reader.GetString(1); }
            if (!reader.IsDBNull(2)) { employee.LastName = reader.GetString(2); }
            if (!reader.IsDBNull(3)) { employee.Email = reader.GetString(3); }
            if (!reader.IsDBNull(4)) { employee.Title = reader.GetString(4); }
            if (!reader.IsDBNull(5)) { employee.CVR = reader.GetInt32(5); }

            return employee;
        }

        public override IEnumerable<Employee> Get()
        {
            List<Employee> liste = new List<Employee>();

            using (SqlConnection conn = new SqlConnection(ConnectionString))
            using (SqlCommand cmd = new SqlCommand(GET_ALL, conn))
            {
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    Employee item = ReadNextElement(reader);
                    liste.Add(item);
                }
                reader.Close();
            }

            return liste;
        }

        public override Employee Get(int id)
        {
            Employee employee = new Employee();

            using (SqlConnection conn = new SqlConnection(ConnectionString))
            using (SqlCommand cmd = new SqlCommand(GET_ONE, conn))
            {
                conn.Open();

                cmd.Parameters.AddWithValue("@Id", id);

                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    employee = ReadNextElement(reader);
                }
                reader.Close();
            }

            return employee;
        }
        //public bool Post(Customer customer)
        //{
        //    using (SqlConnection conn = new SqlConnection(ConnectionString))
        //    using (SqlCommand cmd = new SqlCommand(INSERT, conn))
        //    {
        //        conn.Open();

        //        cmd.Parameters.AddWithValue("@CVR", customer.CVR);
        //        cmd.Parameters.AddWithValue("@Name", customer.Name);
        //        cmd.Parameters.AddWithValue("@Email", customer.Email);
        //        cmd.Parameters.AddWithValue("@Phone", customer.Phone);

        //        //Returns true if query returns higher than 0 (affected rows)
        //        return cmd.ExecuteNonQuery() > 0;
        //    }
        //}
    }
}