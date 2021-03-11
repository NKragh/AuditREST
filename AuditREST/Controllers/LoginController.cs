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
    public class LoginController : ControllerBase
    {
        // POST: api/Login
        [HttpPost]
        public Auditor Post([FromBody] Login login)
        {
            ManageLogin manager = new ManageLogin();

            return manager.Login(login);
        }
    }

    public class Login 
    {
        public string email { get; set; }
        public string password { get; set; }
    }
}
