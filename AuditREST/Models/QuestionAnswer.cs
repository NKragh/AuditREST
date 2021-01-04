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
        public int CVR { get; set; }
        public int QuestionId { get; set; }
        public int AuditorId { get; set; }
        public int ReportId { get; set; }
    }
}
