using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using AuditREST.Models;

namespace AuditREST.DBUtils
{
    public class ManageParticipants : IManager<Participant>
    {
        private ManageEmployees emanager;
        private ManageAuditors amanager;

        public ManageParticipants()
        {
            emanager = new ManageEmployees();
            amanager = new ManageAuditors();
        }

        public override string ConnectionString { get; set; }

        public override Participant ReadNextElement(SqlDataReader reader)
        {
            throw new NotImplementedException();
        }

        public override IEnumerable<Participant> Get()
        {
            List<Participant> l = new List<Participant>();
            
            return l;

        }
        public override Participant Get(int reportId)
        {
            Participant p = new Participant();
            p.Auditor = amanager.GetByReport(reportId);
            p.Employees = emanager.GetByReport(reportId);
            return p;
        }
    }

    public class Participant
    {
        public Auditor Auditor { get; set; }
        public List<Employee> Employees { get; set; }
    }
}
