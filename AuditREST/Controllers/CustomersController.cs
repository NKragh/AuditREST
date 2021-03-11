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
    public class CustomersController : ControllerBase
    {
        ManageCustomers manager = new ManageCustomers();
        // GET: api/Customers
        [HttpGet]
        public IEnumerable<Customer> Get()
        {
            return manager.Get();
        }

        // GET: api/Customers/5
        [HttpGet("{cvr}")]
        public Customer Get(int cvr)
        {
            return manager.Get(cvr);
        }
        
        // GET: api/Customers/5
        [HttpGet("auditor/{id}")]
        public IEnumerable<Customer> GetByAuditor(int id)
        {
            return manager.GetByAuditor(id);
        }

        // POST: api/Customers
        [HttpPost]
        public void Post([FromBody] Customer value)
        {
        }

        // PUT: api/Customers/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] Customer value)
        {
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
