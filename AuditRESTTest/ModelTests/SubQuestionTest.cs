using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;
using AuditREST.Models;

namespace AuditRESTTest.ModelTests
{
    [TestClass]
    public class SubQuestionTest
    {
        [TestMethod]
        public void CreateSubQuestion()
        {
            SubQuestion sub = new SubQuestion("Text", 1, new AnswerType("YesNo"));
        }

        [TestMethod]
        public void SetProperties()
        {
            SubQuestion sub = new SubQuestion();
            sub.SubQuestionId = 2;
            sub.Text = "Text";
            sub.ParentId = 1;
            AnswerType answerType = new AnswerType("Start");
            sub.AnswerType = answerType;

            Assert.AreEqual(2, sub.SubQuestionId, "SubQuestionId does not match set value.");
            Assert.AreEqual("Text", sub.Text, "Text does not match set value.");
            Assert.AreEqual(1, sub.ParentId, "ParentId does not match set value.");
            Assert.AreEqual(answerType, sub.AnswerType, "AnswerType does not match set value.");
        }
    }
}
