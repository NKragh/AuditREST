using System;
using System.Collections.Generic;
using System.Text;
using AuditREST.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AuditRESTTest.ModelTests
{
    [TestClass]
    public class QuestionGroupTest
    {
        private QuestionGroup _qg;

        [TestInitialize]
        public void TestInit()
        {
            _qg = new QuestionGroup();
        }

        [TestMethod]
        public void CreateQuestionGroup()
        {
            QuestionGroup qg = new QuestionGroup("Kvalitetsledelsessystemet");

            Assert.AreEqual("Kvalitetsledelsessystemet", qg.Name);
        }

        [TestMethod]
        public void SetQuestionGroupId()
        {
            _qg.Id = 1;

            Assert.AreEqual(1, _qg.Id);
        }

        [TestMethod]
        public void SetQuestionGroupName()
        {
            _qg = new QuestionGroup();
            _qg.Name = "Test";
            Assert.AreEqual("Test", _qg.Name);
        }

        [TestMethod]
        public void SetChecklistId()
        {
            _qg.ChecklistId = 1;
            Assert.AreEqual(1, _qg.ChecklistId);
        }

        [TestMethod]
        public void LoadQuestions()
        {
            List<Question> qList = new List<Question>();
            qList.Add(new Question());
            qList.Add(new Question());
            qList.Add(new Question());

            int count = _qg.LoadQuestions(qList);

            Assert.AreEqual(qList.Count, count);
        }
    }
}
