using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AuditREST.DBUtils;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AuditREST.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ParticipantsController : ControllerBase
    {
        ManageParticipants manager = new ManageParticipants();
        // GET: api/Participants
        [HttpGet]
        public IEnumerable<Participant> Get()
        {
            return manager.Get();
        }

        // GET: api/Participants/5
        [HttpGet("{reportid}")]
        public Participant Get(int reportid)
        {
            return manager.Get(reportid);
        }

        // POST: api/Participants
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT: api/Participants/5
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
