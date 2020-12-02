using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;
using AuditREST.Models;

namespace AuditRESTTest
{
    [TestClass]
    public class QuestionTest
    {
        private Question q;

        [TestInitialize]
        public void TestInit()
        {
            q = new Question();
        }
        [TestMethod]
        public void CreateQuestion()
        {
            Question q1 = new Question("Text", "Type", 1);
            Assert.AreEqual("Text", q1.Text);
            Assert.AreEqual("Type", q1.Type);
            Assert.AreEqual(1, q1.QuestionGroupId);
        }

        [TestMethod]
        public void SetQuestionId()
        {
            q.Id = 1;

            Assert.AreEqual(1, q.Id);
        }

        [TestMethod]
        public void SetQuestionText()
        {
            q.Text = "Test";
            Assert.AreEqual("Test", q.Text);
        }

        [TestMethod]
        public void SetQuestionType()
        {
            q.Type = "Test";
            Assert.AreEqual("Test", q.Type);
        }

        [TestMethod]
        public void SetQuestionQuestionGroupId()
        {
            q.QuestionGroupId = 1;
            Assert.AreEqual(1, q.QuestionGroupId);
        }

        [TestMethod]
        public void LoadSubQuestions()
        {
            List<SubQuestion> listSubQuestions = new List<SubQuestion>();
            listSubQuestions.Add(new SubQuestion());
            listSubQuestions.Add(new SubQuestion());
            listSubQuestions.Add(new SubQuestion());

            int count = q.LoadSubQuestions(listSubQuestions);
            Assert.AreEqual(3, count);
        }
    }
}
