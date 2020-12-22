using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AuditREST.Models
{
    public class QuestionAnswer
    {
        public int Id { get; set; }
        public string Answer { get; set; }
        public string Remark { get; set; }
        public string Comment { get; set; }
        public Customer Customer { get; set; }
        public Question Question { get; set; }
        public Auditor Auditor { get; set; }
        public Report Report { get; set; }

        public QuestionAnswer()
        {
            Customer = new Customer();
            Question = new Question();
            Auditor = new Auditor();
            Report = new Report();
        }
    }
}
