using System.Collections.Generic;

namespace AuditREST.Models
{
    public class QuestionGroup
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int ChecklistId { get; set; }
        public List<Question> Questions { get; set; }

        public QuestionGroup()
        {
            Questions = new List<Question>();
        }
        public QuestionGroup(string name)
        {
            Name = name;
        }

        public int LoadQuestions(List<Question> qList)
        {
            Questions = qList;
            return Questions.Count;
        }
    }
}