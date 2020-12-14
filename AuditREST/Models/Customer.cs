using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AuditREST.Models
{
    public class Customer
    {
        public int CVR { get; set; }
        public String Name { get; set; }
        public String Email { get; set; }
        public String Phone { get; set; }

        public Customer()
        {

        }

        public Customer(int cvr, string name, string email, string phone)
        {
            CVR = cvr;
            Name = name;
            Email = email;
            Phone = phone;
        }
    }
}
