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
        public string CompanyName { get; set; }
        public int CVR { get; set; }
        public Auditor Auditor { get; set; }
        public List<Employee> Employees { get; set; }
        public List<QuestionAnswer> QuestionAnswers { get; set; }

        public Report()
        {
            Auditor = new Auditor();
            Employees = new List<Employee>();
        }

        public Report(int id, DateTime completed, int cvr, Auditor auditor, List<Employee> employees, string companyName)
        {
            Id = id;
            Completed = completed;
            CVR = cvr;
            Auditor = auditor;
            Employees = employees;
            CompanyName = companyName;
        }

        public int LoadAnswers(List<QuestionAnswer> questionAnswers)
        {
            QuestionAnswers = questionAnswers;

            return QuestionAnswers.Count;
        }

    }
}
