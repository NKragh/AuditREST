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
    public class AnswersController : ControllerBase
    {
        ManageQuestionAnswers manager = new ManageQuestionAnswers();

        // GET: api/Answers
        [HttpGet]
        public IEnumerable<QuestionAnswer> Get()
        {
            return manager.Get();
        }

        // GET: api/Answers/5
        [HttpGet("{id}", Name = "Get")]
        public QuestionAnswer Get(int id)
        {
            return manager.Get(id);
        }

        // POST: api/Answers
        [HttpPost]
        public void Post([FromBody] List<QuestionAnswer> questionAnswers)
        {
            foreach (var questionAnswer in questionAnswers)
            {
                manager.Post(questionAnswer);
            }
        }

        // PUT: api/Answers/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] QuestionAnswer value)
        {
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
