using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AuditREST.DBUtils;
using AuditREST.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AuditREST.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ChecklistsController : ControllerBase
    {
        private ManageChecklists manager = new ManageChecklists();

        // GET: api/Checklists
        [HttpGet]
        public IEnumerable<Checklist> Get(Trade trade)
        {
            return manager.Get(trade);
        }

        // GET: api/Checklists/5
        [HttpGet("{id}")]
        public Checklist Get(int id)
        {
            return manager.Get(id);
        }

        // POST: api/Checklists
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT: api/Checklists/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
