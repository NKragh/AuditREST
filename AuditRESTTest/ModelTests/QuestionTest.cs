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
            Question q1 = new Question("Text", answerType, 1, null);
            Assert.AreEqual("Text", q1.Text);
            Assert.AreEqual(answerType, q1.AnswerType);
            Assert.AreEqual(1, q1.QuestionGroupId, "QuestionGroup");

            Question q2 = new Question("Text", answerType, null, 2);
            Assert.AreEqual("Text", q2.Text);
            Assert.AreEqual(answerType, q2.AnswerType);
            Assert.AreEqual(2, q2.ParentId, "Parent");
        }

        [TestMethod]
        public void QuestionProperties()
        {
            q.QuestionId = 1;
            q.Text = "Test";
            q.AnswerType = answerType;
            q.QuestionGroupId = 1;
            q.ParentId = 2;

            Assert.AreEqual(1, q.QuestionId);
            Assert.AreEqual("Test", q.Text);
            Assert.AreEqual(answerType, q.AnswerType);
            Assert.AreEqual(1, q.QuestionGroupId);
            Assert.AreEqual(2, q.ParentId);

        }

        [TestMethod]
        public void LoadSubQuestions()
        {
            List<Question> listSubQuestions = new List<Question>();
            listSubQuestions.Add(new Question());
            listSubQuestions.Add(new Question());
            listSubQuestions.Add(new Question());

            int count = q.LoadSubQuestions(listSubQuestions);
            Assert.AreEqual(3, count);
        }
    }
}
