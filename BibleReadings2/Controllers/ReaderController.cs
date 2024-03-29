using System;
using System.Threading.Tasks;
using BibleReadings2.Extensions;
using BibleReadings2.Helpers;
using BibleReadings2.Models;
using BibleReadings2.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace BibleReadings2.Controllers
{
    [Route("api/[controller]")]
    [Produces("application/json")]
    [ApiController]
    public class ReaderController : ControllerBase
    {
        private readonly IReaderRepository _repository;
        private readonly ILogger<ReaderController> _logger;

        public ReaderController(IReaderRepository repository, ILogger<ReaderController> logger)
        {
            _repository = repository;
            _logger = logger;
        }

        [HttpGet]
        [ProducesResponseType(typeof(Reader), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Get()
        {
            var reader = await _repository.GetReader();

            if(reader == null)
            {
                return NotFound();
            }

            if (HttpContext.Request.Cookies.TryGetValue("timezone", out var timeZoneId))
            {
                reader.Date = reader.Date.ToTimeZone(timeZoneId);
            }

            return Ok(reader);
        }

        [HttpPut]
        [ProducesResponseType(typeof(ReaderDescription), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Save([FromBody] Reader reader)
        {
            if (reader.Date == default)
            {
                reader.Date = new DateTimeOffset(DateTime.UtcNow, TimeSpan.Zero);
            }

            try
            {
                await _repository.SaveReader(reader);

                if (HttpContext.Request.Cookies.TryGetValue("timezone", out var timeZoneId))
                {
                    reader.Date = reader.Date.ToTimeZone(timeZoneId);
                }

                var description = new ReaderDescription
                {
                    Description = Utilities.BuildReaderDescription(reader)
                };
                return Ok(description);
            }
            catch(Exception ex)
            {
                _logger.LogError("Error saving {reader}: {exception}", reader, ex);
                return BadRequest();
            }
        }
    }
}