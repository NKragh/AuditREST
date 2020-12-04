using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;
using AuditREST.Models;

namespace AuditRESTTest.ModelTests
{
    [TestClass]
    public class QuestionTest
    {
        private Question q;
        private AnswerType answerType;

        [TestInitialize]
        public void TestInit()
        {
            q = new Question();
            answerType = new AnswerType("YesNo");
        }
        [TestMethod]
        public void CreateQuestion()
        {
            Question q1 = new Question("Text", answerType, 1);
            Assert.AreEqual("Text", q1.Text);
            Assert.AreEqual(answerType, q1.AnswerType);
            Assert.AreEqual(1, q1.QuestionGroupId);
        }

        [TestMethod]
        public void QuestionProperties()
        {
            q.QuestionId = 1;
            q.Text = "Test";
            q.AnswerType = answerType;
            q.QuestionGroupId = 1;

            Assert.AreEqual(1, q.QuestionId);
            Assert.AreEqual("Test", q.Text);
            Assert.AreEqual(answerType, q.AnswerType);
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
