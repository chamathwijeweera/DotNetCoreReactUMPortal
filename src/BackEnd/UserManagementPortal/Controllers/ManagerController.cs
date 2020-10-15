using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace UserManagementPortal.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ManagerController : ControllerBase
    {
        // GET: api/<ManagerController>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<ManagerController>/5
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            return Ok("Manager can read");
        }

        // POST api/<ManagerController>
        [HttpPost]
        public IActionResult Post([FromBody] string value)
        {
            return Ok("Manager can read");
        }

        // PUT api/<ManagerController>/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] string value)
        {
            return Ok("Manager can read");
        }

        // DELETE api/<ManagerController>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            return Ok("Manager can read");
        }
    }
}
