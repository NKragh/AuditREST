using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using AuditREST.Models;

namespace AuditREST.DBUtils
{
    public class ManageCustomers : IManager<Customer>
    {
        private string GET_ALL = "SELECT * FROM Customers";
        private string GET_ONE = "SELECT * FROM Customers WHERE CVR = @CVR";
        private string INSERT = "INSERT INTO Customers(CVR, Name, Email, Phone) VALUES (@CVR, @Name, @Email, @Phone)";
        public override string ConnectionString { get; set; }
        public override Customer ReadNextElement(SqlDataReader reader)
        {
            Customer customer = new Customer();

            if (!reader.IsDBNull(0)) { customer.CVR = reader.GetInt32(0); }
            if (!reader.IsDBNull(1)) { customer.Email = reader.GetString(1); }
            if (!reader.IsDBNull(2)) { customer.Name = reader.GetString(2); }
            if (!reader.IsDBNull(3)) { customer.Phone = reader.GetString(3); }

            return customer;
        }

        public override IEnumerable<Customer> Get()
        {
            List<Customer> liste = new List<Customer>();

            using (SqlConnection conn = new SqlConnection(ConnectionString))
            using (SqlCommand cmd = new SqlCommand(GET_ALL, conn))
            {
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    Customer item = ReadNextElement(reader);
                    liste.Add(item);
                }
                reader.Close();
            }

            return liste;
        }

        public override Customer Get(int id)
        {
            Customer customer = new Customer();

            using (SqlConnection conn = new SqlConnection(ConnectionString))
            using (SqlCommand cmd = new SqlCommand(GET_ONE, conn))
            {
                conn.Open();

                cmd.Parameters.AddWithValue("@CVR", id);

                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    customer = ReadNextElement(reader);
                }
                reader.Close();
            }

            return customer;
        }

        public bool Post(Customer customer)
        {
            using (SqlConnection conn = new SqlConnection(ConnectionString))
            using (SqlCommand cmd = new SqlCommand(INSERT, conn))
            {
                conn.Open();

                cmd.Parameters.AddWithValue("@CVR", customer.CVR);
                cmd.Parameters.AddWithValue("@Name", customer.Name);
                cmd.Parameters.AddWithValue("@Email", customer.Email);
                cmd.Parameters.AddWithValue("@Phone", customer.Phone);

                //Returns true if query returns higher than 0 (affected rows)
                return cmd.ExecuteNonQuery() > 0;
            }
        }
    }
}
