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
    public class ManageChecklistsTest
    {
        private ManageChecklists manager;

        [TestInitialize]
        public void TestInit()
        {
            manager = new ManageChecklists();
        }

        [TestMethod]
        public void CreateChecklistManager()
        {
            ManageChecklists manager1 = new ManageChecklists();
            Assert.IsInstanceOfType(manager1, typeof(ManageChecklists));
        }

        [TestMethod]
        public void GetConnectionString()
        {
            string expectedConnectionString = new ConnectionString().ConnectionStreng;
            string connectionString = manager.ConnectionString;

            Assert.AreEqual(expectedConnectionString, connectionString);
        }

        [TestMethod]
        public void GetChecklists()
        {
            var checklists = manager.Get().ToList();

            Assert.AreNotEqual(0, checklists.Count());
            Assert.AreEqual(1, checklists[0].Id);

            Assert.IsNotNull(checklists[0].QuestionGroups);
            Assert.IsFalse(checklists[0].QuestionGroups.Count == 0);
        }

        [TestMethod]
        public void GetOneChecklist()
        {
            Checklist cl = manager.Get(1);

            Assert.AreEqual(1, cl.Id);
        }

        [TestMethod]
        public void GetChecklistThatDoesntExist()
        {
            Checklist cl = new Checklist("Test list");
            manager.Create(cl);
            List<Checklist> l = manager.Get().ToList();
            Checklist temp = l[l.Count - 1];
            manager.Delete(temp.Id);

            Assert.IsNull(manager.Get(temp.Id));
        }

        //[TestMethod]
        //public void CreateChecklist()
        //{
        //    Checklist checklist = new Checklist("Test list");

        //    Assert.IsTrue(manager.Create(checklist));

        //    var checklists = manager.Get().ToList();

        //    Assert.AreEqual("Test list", checklists.Find(cl => cl.Name == "Test list").Name);
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
        //TODO: Skriv om det her i rapporten! Det er lækkert og virker hver gang. Der findes også [Class...] og [Assembly...]
        [TestCleanup]
        public void TestCleanup()
        {
            using (SqlConnection conn = new SqlConnection(new ConnectionString().ConnectionStreng))
            using (SqlCommand cmd = new SqlCommand("DELETE FROM Checklists WHERE Name = 'Test list'", conn))
            {
                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }
    }
}
