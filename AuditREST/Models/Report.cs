using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AuditREST.Models
{
    public class Report
    {
        public int Id { get; set; }
        public DateTime Completed { get; set; }
        public Customer Customer { get; set; }
        public Auditor Auditor { get; set; }
        public List<Employee> Employees { get; set; }
        public List<QuestionAnswer> QuestionAnswers { get; set; }
        public DateTime Archived { get; set; }

        public Report()
        {
            Auditor = new Auditor();
            Customer = new Customer();
            Employees = new List<Employee>();
        }

        public int LoadAnswers(List<QuestionAnswer> questionAnswers)
        {
            QuestionAnswers = questionAnswers;

            return QuestionAnswers.Count;
        }

        public int LoadEmployees(List<Employee> employees)
        {
            Employees = employees;

            return Employees.Count;
        }
    }
}
