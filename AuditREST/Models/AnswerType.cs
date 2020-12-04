using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AuditREST.Models
{
    public class AnswerType
    {
        public int AnswerTypeId { get; set; }
        public string AnswerOption { get; set; }
        public AnswerType(string option)
        {
            AnswerOption = option;
        }

        public AnswerType()
        {
        }
    }
}
