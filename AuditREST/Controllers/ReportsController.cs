﻿using System;
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
    public class ReportsController : ControllerBase
    {
        ManageReports manager = new ManageReports();
        // GET: api/Reports
        [HttpGet]
        public IEnumerable<Report> Get()
        {
            return manager.Get();
        }

        // GET: api/Reports/5
        [HttpGet("{id}")]
        public Report Get(int id)
        {
            return manager.Get(id);
        }

        // POST: api/Reports
        [HttpPost]
        public void Post([FromBody] Report value)
        {
            manager.Post(value);
        }

        // PUT: api/Reports/Complete/5
        [HttpGet("complete/{id}")]
        public void Complete(int id)
        {
            manager.Complete(id);
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}