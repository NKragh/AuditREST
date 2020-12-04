using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AuditREST.Models
{
    public class Question
    {
        public int QuestionId { get; set; }
        public string Text { get; set; }
        public AnswerType AnswerType { get; set; }
        public int QuestionGroupId { get; set; }
        public List<SubQuestion> SubQuestions { get; set; }

        public Question()
        {
        }

        public Question(string text, AnswerType answerType, int questionGroupId)
        {
            Text = text;
            AnswerType = answerType;
            QuestionGroupId = questionGroupId;
        }

        public int LoadSubQuestions(List<SubQuestion> listSubQuestions)
        {
            SubQuestions = listSubQuestions;
            return SubQuestions.Count;
        }

    }
}
