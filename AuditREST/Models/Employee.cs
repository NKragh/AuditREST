using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AuditREST.Models
{
    public class Employee
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Title { get; set; }
        public int CVR { get; set; }

        public Employee()
        {
            
        }

        public Employee(int id, string firstName, string lastName, string email, string title, int cvr)
        {
            Id = id;
            FirstName = firstName;
            LastName = lastName;
            Email = email;
            Title = title;
            CVR = cvr;
        }
    }
}
