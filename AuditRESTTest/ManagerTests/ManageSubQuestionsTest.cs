using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AuditREST.DBUtils;
using AuditREST.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Configuration;
using System.Data.SqlClient;

namespace AuditRESTTest.ManagerTests
{
    [TestClass]
    public class ManageSubQuestionsTest
    {
        private ManageSubQuestions manager;

        [TestInitialize]
        public void TestInit()
        {
            manager = new ManageSubQuestions();
        }

        //[TestCleanup]
        //public void TestCleanup()
        //{
        //    using (SqlConnection conn = new SqlConnection(new ConnectionString().ConnectionStreng))
        //    using (SqlCommand cmd = new SqlCommand("DELETE FROM Checklists WHERE Name = 'Test list'", conn))
        //    {
        //        conn.Open();
        //        cmd.ExecuteNonQuery();
        //    }
        //}

        [TestMethod]
        public void CreateQuestionManager()
        {
            ManageSubQuestions manager1 = new ManageSubQuestions();
            Assert.IsInstanceOfType(manager1, typeof(ManageSubQuestions));
        }

        [TestMethod]
        public void GetConnectionString()
        {
            string expectedConnectionString = new ConnectionString().ConnectionStreng;
            string connectionString = manager.ConnectionString;

            Assert.AreEqual(expectedConnectionString, connectionString);
        }

        [TestMethod]
        public void GetSubQuestions()
        {
            var questions = manager.Get().ToList();

            Assert.AreNotEqual(0, questions.Count());
        }

        [TestMethod]
        public void GetOneSubQuestion()
        {
            SubQuestion question = manager.Get(1);

            Assert.AreEqual(1, question.SubQuestionId);
        }

        [TestMethod]
        public void GetSubQuestionThatDoesntExist()
        {
            Assert.IsNull(manager.Get(0));
        }

        //[TestMethod]
        //public void CreateQuestion()
        //{
        //    Question question = new Question("Test question", "Main", 1);

        //    Assert.IsTrue(manager.Create(question));

        //    var questions = manager.Get().ToList();

        //    Assert.AreEqual("Test question", questions.Find(q => q.Text == "Test question").Text);
        //}

        //[TestMethod]
        //public void DeleteChecklist()
        //{
        //    Checklist tempChecklist = new Checklist("Test list");
        //    manager.Create(tempChecklist);

        //    var checklists = manager.Get().ToList();

        //    Checklist checklist = checklists[checklists.Count - 1];

        //    Assert.AreEqual(tempChecklist.Name, checklist.Name);

        //    bool response = manager.Delete(checklist.QuestionId);

        //    Assert.IsTrue(response);

        //    Assert.IsNull(manager.Get(checklist.QuestionId));
        //}

        //[TestMethod]
        //public void UpdateChecklist()
        //{
        //    Checklist cl = new Checklist("Test list");
        //    manager.Create(cl);
        //    Checklist updatedChecklist = new Checklist("Test liste");
        //    manager.Update(cl, updatedChecklist);
        //    var checklists = manager.Get().ToList();
        //    Checklist checklist = checklists.Find(c => c.Name == cl.Name);
        //    Assert.AreNotEqual(checklist.Name, cl.Name);
        //}
    }
}
