using System;
using System.Collections.Generic;
using System.Text;
using AuditREST.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AuditRESTTest.ModelTests
{
    [TestClass]
    public class TradeTest
    {
        [TestMethod]
        public void CreateTrade()
        {
            Trade t = new Trade("El");

            Assert.AreEqual("El", t.Name);
        }

        [TestMethod]
        public void TradeProperties()
        {
            Trade t = new Trade();
            t.TradeId = 1;
            t.Name = "VVS";

            Assert.AreEqual(1, t.TradeId);
            Assert.AreEqual("VVS", t.Name);
        }
    }
}
