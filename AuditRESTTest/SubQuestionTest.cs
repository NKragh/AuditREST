using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;
using AuditREST.Models;

namespace AuditRESTTest
{
    [TestClass]
    public class SubQuestionTest
    {
        [TestMethod]
        public void CreateSubQuestion()
        {
            SubQuestion sq = new SubQuestion("Text", "Type", 1, 1);
            Assert.AreEqual("Text", sq.Text);
            Assert.AreEqual("Type", sq.Type);
            Assert.AreEqual(1, sq.Id);
            Assert.AreEqual(1, sq.ParentId);
        }

        [TestMethod]
        public void IsSubQuestionSubClassOfQuestion()
        {
            bool ans = typeof(SubQuestion).IsSubclassOf(typeof(Question));

            Assert.IsTrue(ans);
        }


    }
}
