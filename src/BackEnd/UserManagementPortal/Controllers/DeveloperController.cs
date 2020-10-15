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
    public class DeveloperController : ControllerBase
    {
        // GET: api/<DeveloperController>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<DeveloperController>/5
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            return Ok("Developer can read");
        }

        // POST api/<DeveloperController>
        [HttpPost]
        public IActionResult Post([FromBody] string value)
        {
            return Ok("Developer can create");
        }

        // PUT api/<DeveloperController>/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] string value)
        {
            return Ok("Developer can modify");
        }

        // DELETE api/<DeveloperController>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            return Ok("Developer can delete");
        }
    }
}
