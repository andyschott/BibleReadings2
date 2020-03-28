using System;
using System.Threading.Tasks;
using BibleReadings2.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace BibleReadings2.Controllers
{
    [Route("api/[controller]")]
    [Produces("application/json")]
    [ApiController]
    public class ReadingController : ControllerBase
    {
        private readonly IReadingsRepository _repository;
        private readonly ILogger _logger;

        public ReadingController(IReadingsRepository repository, ILogger<ReadingController> logger)
        {
            _repository = repository;
            _logger = logger;
        }

        [HttpGet("{month}/{day}")]
        [ProducesResponseType(typeof(Day), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Get(int month, int day)
        {
            try
            {
                var readings = await _repository.GetReadings(month, day);
                return Ok(readings);
            }
            catch(ArgumentOutOfRangeException)
            {
                return NotFound();
            }
        }
    }
}