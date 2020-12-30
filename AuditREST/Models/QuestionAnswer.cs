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
        public int Customer { get; set; }
        public int Question { get; set; }
        public int Auditor { get; set; }
        public int Report { get; set; }
    }
}
