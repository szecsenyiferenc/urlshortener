using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace UrlShortener.Controllers
{
    [Route("api/url")]
    [ApiController]
    public class UrlController : ControllerBase
    {
        [HttpGet("{id}")]
        public ActionResult<string> Get(string id)
        {
            if(id == null)
            {
                return NotFound();
            }
            return Ok("https://www.google.com/");
        }

        [HttpPost]
        public ActionResult<string> Post([FromBody] string value)
        {
            return Ok(value);
        }
    }
}
