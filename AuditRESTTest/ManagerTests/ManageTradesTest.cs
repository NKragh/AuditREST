using System;
using System.Collections.Generic;
using System.Text;
using AuditREST.DBUtils;
using AuditREST.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AuditRESTTest.ManagerTests
{
    [TestClass]
    public class ManageTradesTest
    {
        private ManageTrades manager;
        [TestInitialize]
        public void TestInit()
        {
            manager = new ManageTrades();
        }

        [TestMethod]
        public void CreateTradesManager()
        {
            ManageTrades manager1 = new ManageTrades();
            Assert.IsInstanceOfType(manager1, typeof(ManageTrades));
        }

        [TestMethod]
        public void GetTrades()
        {
            List<Trade> l = manager.Get();

            Assert.AreNotEqual(0, l.Count);
        }
    }
}
