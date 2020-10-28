using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
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
        private readonly ILogger<UrlController> _logger;

        public UrlController(IUrlService urlService, IUrlValidator urlValidator, ILogger<UrlController> logger)
        {
            _urlService = urlService;
            _urlValidator = urlValidator;
            _logger = logger;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<string>> Get(string id)
        {
            try
            {
                string url = await _urlService.GetUrl(id);

                if (url == null)
                {
                    return NotFound();
                }

                return Ok(url);
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message, e);
                return StatusCode(500);
            }
        }

        [HttpPost]
        public async Task<ActionResult<string>> Post([FromBody] string url)
        {
            try
            {
                if (!_urlValidator.Validate(url))
                {
                    return BadRequest("The given url is invalid.");
                }

                string key = await _urlService.AddUrl(url);

                var createdUrl = CreateUrl(key);

                return Ok(createdUrl);
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message, e);
                return StatusCode(500);
            }
        }

        private string CreateUrl(string key)
        {
            return $"{Request.Scheme}://{Request.Host}{Request.PathBase}/{key}";
        }
    }
}
