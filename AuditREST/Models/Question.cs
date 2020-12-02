using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AuditREST.Models
{
    public class Question
    {
        public string Text { get; set; }
        public string Type { get; set; }
        public int QuestionGroupId { get; set; }
        public int Id { get; set; }
        public List<SubQuestion> SubQuestions { get; set; }

        public Question()
        {
        }
        public Question(string text, string type, int questionGroupId)
        {
            Text = text;
            Type = type;
            QuestionGroupId = questionGroupId;
        }

        public int LoadSubQuestions(List<SubQuestion> listSubQuestions)
        {
            SubQuestions = listSubQuestions;
            return SubQuestions.Count;
        }

    }
}
