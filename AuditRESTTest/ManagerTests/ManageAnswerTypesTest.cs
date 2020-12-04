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
    public class ManageAnswerTypesTest
    {
        private ManageAnswerTypes manager;

        [TestInitialize]
        public void TestInit()
        {
            manager = new ManageAnswerTypes();
        }

        [TestMethod]
        public void CreateAnswerTypeManager()
        {
            ManageAnswerTypes manager1 = new ManageAnswerTypes();
            Assert.IsInstanceOfType(manager1, typeof(ManageAnswerTypes));
        }

        [TestMethod]
        public void GetConnectionString()
        {
            string expectedConnectionString = new ConnectionString().ConnectionStreng;
            string connectionString = manager.ConnectionString;

            Assert.AreEqual(expectedConnectionString, connectionString);
        }

        [TestMethod]
        public void GetAnswerTypes()
        {
            var answerTypes = manager.Get().ToList();

            Assert.AreNotEqual(0, answerTypes.Count());
            Assert.IsNotNull(answerTypes.Find(at => at.AnswerOption == "Main"));
        }

        [TestMethod]
        public void GetOneAnswerType()
        {
            AnswerType answerType = manager.Get(1);

            Assert.AreEqual(1, answerType.AnswerTypeId);
            Assert.IsNull(manager.Get(0));
        }
    }
}
