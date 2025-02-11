using Domain.Abstractions;
using Domain.Entities;
using Domain.Exceptions;
using Microsoft.AspNetCore.Mvc;

namespace UrlShortener.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ShortenUrlController : ControllerBase  
    {   
        private readonly IShortedUrlRepository _shortedUrlRepository;
        public ShortenUrlController(IShortedUrlRepository shortedUrlRepository)
        {
            _shortedUrlRepository = shortedUrlRepository;
        }

        [HttpPost]
        public async Task<IActionResult> ShortenUrl(ShortedUrlDTO shortedUrlDTO)
        {
            try
            {
                string code = await _shortedUrlRepository.AddShortedUrl(shortedUrlDTO);
                return Ok(code);
            }
            catch (UrlExistedException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{code}")]
        public async Task<IActionResult> GetShortedUrl(string code)
        {
            var shortedUrl = await _shortedUrlRepository.GetShortedUrlByCode(code);
            if (shortedUrl == null)
            {
                return NotFound();
            }
            return Redirect(shortedUrl.OriginalUrl);
        }
    }
}
