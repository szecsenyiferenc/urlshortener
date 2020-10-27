using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using UrlShortener.Repository;
using UrlShortener.Validators;

namespace UrlShortener.Controllers
{
    [Route("api/url")]
    [ApiController]
    public class UrlController : ControllerBase
    {
        private readonly IUrlRepository _urlRepository;
        private readonly IUrlValidator _urlValidator;

        public UrlController(IUrlRepository urlRepository, IUrlValidator urlValidator)
        {
            _urlRepository = urlRepository;
            _urlValidator = urlValidator;
        }

        [HttpGet("{id}", Name = "GetUrl")]
        public ActionResult<string> Get(string guid)
        {
            if(guid == null)
            {
                return NotFound();
            }

            var url = _urlRepository.GetUrl(guid);

            if(url == null)
            {
                return NotFound();
            }

            return Ok(url);
        }

        [HttpPost]
        public ActionResult<string> Post([FromBody] string url)
        {
            if (!_urlValidator.Validate(url))
            {
                return BadRequest("The given url is invalid.");
            }

            var createdKey = _urlRepository.AddUrl(url);

            return CreatedAtRoute("GetUrl", createdKey, createdKey);
        }
    }
}
