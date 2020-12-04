using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AuditREST.DBUtils;
using AuditREST.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AuditRESTTest.ManagerTests
{
    [TestClass]
    public class ManageQuestionGroupsTest
    {
        private ManageQuestionGroups manager;

        [TestInitialize]
        public void TestInit()
        {
            manager = new ManageQuestionGroups();
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
            ManageQuestionGroups manager1 = new ManageQuestionGroups();
            Assert.IsInstanceOfType(manager1, typeof(ManageQuestionGroups));
        }

        [TestMethod]
        public void GetConnectionString()
        {
            string expectedConnectionString = new ConnectionString().ConnectionStreng;
            string connectionString = manager.ConnectionString;

            Assert.AreEqual(expectedConnectionString, connectionString);
        }

        [TestMethod]
        public void GetQuestionGroups()
        {
            var questions = manager.Get().ToList();

            Assert.AreNotEqual(0, questions.Count());
        }

        [TestMethod]
        public void GetOneQuestionGroup()
        {
            QuestionGroup questionGroup = manager.Get(1);

            Assert.AreEqual(1, questionGroup.Id);
        }

        [TestMethod]
        public void GetQuestionGroupThatDoesntExist()
        {
            Assert.IsNull(manager.Get(0));
        }
    }
}
