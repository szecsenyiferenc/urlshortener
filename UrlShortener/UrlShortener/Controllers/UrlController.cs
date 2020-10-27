using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using UrlShortener.Repository;
using UrlShortener.Services;
using UrlShortener.Validators;

namespace UrlShortener.Controllers
{
    [Route("api/url")]
    [ApiController]
    public class UrlController : ControllerBase
    {
        private readonly IUrlService _urlService;
        private readonly IUrlValidator _urlValidator;

        public UrlController(IUrlService urlService, IUrlValidator urlValidator)
        {
            _urlService = urlService;
            _urlValidator = urlValidator;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<string>> Get(string id)
        {
            if(id == null)
            {
                return NotFound();
            }

            string url = await _urlService.GetUrl(id);

            if(url == null)
            {
                return NotFound();
            }

            return Ok(url);
        }

        [HttpPost]
        public async Task<ActionResult<string>> Post([FromBody] string url)
        {
            if (!_urlValidator.Validate(url))
            {
                return BadRequest("The given url is invalid.");
            }

            string key = await _urlService.AddUrl(url);

            var createdUrl = CreateUrl(key);

            return Ok(createdUrl);
        }

        private string CreateUrl(string key)
        {
            return $"{Request.Scheme}://{Request.Host}{Request.PathBase}/{key}";
        }
    }
}
