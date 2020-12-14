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
        public int CVR { get; set; }
        public Auditor Auditor { get; set; }
        public Employee Employee1 { get; set; }
        public Employee Employee2 { get; set; }
        public List<QuestionAnswer> QuestionAnswers { get; set; }

        public Report()
        {
            Auditor = new Auditor();
            Employee1 = new Employee();
            Employee2 = new Employee();
        }

        public Report(int id, DateTime completed, int cvr, Auditor auditor, Employee employee1, Employee employee2)
        {
            Id = id;
            Completed = completed;
            CVR = cvr;
            Auditor = auditor;
            Employee1 = employee1;
            Employee2 = employee2;
        }

        public int LoadAnswers(List<QuestionAnswer> questionAnswers)
        {
            QuestionAnswers = questionAnswers;

            return QuestionAnswers.Count;
        }
    }
}
