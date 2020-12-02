using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AuditREST.Models
{
    public class SubQuestion: Question
    {
        public int ParentId { get; set; }

        public SubQuestion()
        {
        }
        
        public SubQuestion(string text, string type, int id, int parentId)
        {
            Text = text;
            Type = type;
            Id = id;
            ParentId = parentId;
        }
    }
}
