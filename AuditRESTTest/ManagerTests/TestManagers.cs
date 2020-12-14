using System;
using System.Collections.Generic;
using System.Text;
using AuditREST.DBUtils;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AuditRESTTest.ManagerTests
{
    [TestClass]
    public class TestManagers
    {
        List<object> managers = new List<object>();

        [TestInitialize]
        public void TestInit()
        {
            managers.Add(new ManageChecklists());
            managers.Add(new ManageAnswerTypes());
            managers.Add(new ManageAuditors());
        }

        [TestMethod]
        public void TestGet()
        {
            foreach (dynamic manager in managers)
            {
                var result = manager.Get();
                Assert.AreNotEqual(0, result.Count());
            }
        }
    }
}
