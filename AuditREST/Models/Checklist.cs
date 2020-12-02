using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AuditREST.Models
{
    public class Checklist
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<QuestionGroup> QuestionGroups { get; set; }

        public Checklist()
        {
        }

        public Checklist(string name)
        {
            Name = name;
        }

        public int LoadQuestionGroups(List<QuestionGroup> questionGroups)
        {
            QuestionGroups = questionGroups;
            return QuestionGroups.Count;
        }
    }
}
