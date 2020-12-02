using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using AuditREST;
using AuditREST.Models;

namespace AuditRESTTest
{
    [TestClass]
    public class CheckListTest
    {
        private Checklist cl;

        [TestInitialize]
        public void InitTest()
        {
            cl = new Checklist();
        }

        [TestMethod]
        public void CreateChecklist()
        {
            Checklist cl1 = new Checklist("Intern Audit");

            Assert.AreEqual("Intern Audit", cl1.Name);
        }

        [TestMethod]
        public void SetChecklistName()
        {
            cl.Name = "Intern Audit";
            Assert.AreEqual("Intern Audit", cl.Name);
        }
        [TestMethod]
        public void SetChecklistId()
        {
            cl.Id = 1;
            Assert.AreEqual(1, cl.Id);
        }

        [TestMethod]
        public void LoadQuestionGroups()
        {
            List<QuestionGroup> questionGroups = new List<QuestionGroup>();
            questionGroups.Add(new QuestionGroup());
            questionGroups.Add(new QuestionGroup());
            questionGroups.Add(new QuestionGroup());

            int count = cl.LoadQuestionGroups(questionGroups);

            Assert.AreEqual(3, count);
        }

    }
}
