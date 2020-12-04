using AuditREST.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AuditRESTTest.ModelTests
{
    [TestClass]
    public class AnwerTypeTest
    {
        private AnswerType type;

        [TestInitialize]
        public void TestInit()
        {
            type = new AnswerType();
        }
        [TestMethod]
        public void CreateAnswerType()
        {
            AnswerType type = new AnswerType("YesNo");
            Assert.AreEqual("YesNo", type.AnswerOption);
        }

        [TestMethod]
        public void AnswerTypeProperties()
        {
            type = new AnswerType();
            type.AnswerTypeId = 11;
            type.AnswerOption = "Satisfaction";
            Assert.AreEqual(11, type.AnswerTypeId);
            Assert.AreEqual("Satisfaction", type.AnswerOption);
        }
    }
}
