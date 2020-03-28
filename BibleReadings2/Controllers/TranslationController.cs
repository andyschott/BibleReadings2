using System;
using System.Collections.Generic;
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
    public class TranslationController : ControllerBase
    {
        private readonly ITranslationsRepository _repository;
        private readonly ILogger _logger;

        public TranslationController(ITranslationsRepository repository, ILogger<ReadingController> logger)
        {
            _repository = repository;
            _logger = logger;
        }

        [HttpGet("{language}")]
        [ProducesResponseType(typeof(IEnumerable<Translation>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Get(Languages language)
        {
            try
            {
                var translations = await _repository.GetTranslations(language);
                return Ok(translations);
            }
            catch(ArgumentException)
            {
                return NotFound();
            }
        }
    }
}