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
        public int? ParentId { get; set; }
        public AnswerType AnswerType { get; set; }
        public int? QuestionGroupId { get; set; }
        public List<Question> SubQuestions { get; set; }
        public List<Trade> Trades { get; set; }

        public Question()
        {
            AnswerType = new AnswerType();
        }

        public Question(string text, AnswerType answerType, int? questionGroupId, int? parentId)
        {
            Text = text;
            AnswerType = answerType;
            QuestionGroupId = questionGroupId;
            ParentId = parentId;
        }

        public int LoadSubQuestions(List<Question> listSubQuestions)
        {
            SubQuestions = listSubQuestions;
            return SubQuestions.Count;
        }

    }
}
