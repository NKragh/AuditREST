using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AuditREST.Models
{
    public class SubQuestion
    {
        public int SubQuestionId { get; set; }
        public int ParentId { get; set; }
        public string Text { get; set; }
        public AnswerType AnswerType { get; set; }

        public SubQuestion()
        {
        }

        public SubQuestion(string text, int parentId, AnswerType answerType)
        {
            Text = text;
            ParentId = parentId;
            AnswerType = answerType;
        }

    }
}
