using System.Web;
using BibleReadings2.Helpers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BibleReadings2.Controllers
{
    [Route("api/[controller]")]
    [Produces("application/json")]
    [ApiController]
    public class LookupController : ControllerBase
    {
        [HttpGet("{passage}")]
        [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
        public IActionResult LookupPassage(string passage)
        {
            HttpContext.Request.Cookies.TryGetValue("english", out var english);
            HttpContext.Request.Cookies.TryGetValue("german", out var german);

            passage = HttpUtility.UrlDecode(passage);

            var url = BibleGateway.BuildReadingUrl(passage, english, german);

            return Ok(url);
        }
    }
}