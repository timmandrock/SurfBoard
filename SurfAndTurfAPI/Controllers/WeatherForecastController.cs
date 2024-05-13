using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SurfAndTurf.Data;
using SurfAndTurf.Models;

namespace SurfAndTurfApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private readonly SurfAndTurfContext _context;

        private static readonly string[] Summaries = new[]
        {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };

        private readonly ILogger<WeatherForecastController> _logger;

        public WeatherForecastController(ILogger<WeatherForecastController> logger)
        {
            _logger = logger;
            _context = _context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<SurfBoard>>> GetSurfBoardModel()
        {
            if (_context.SurfBoard == null)
            {
                return NotFound();
            }
            return Ok(await _context.SurfBoard.ToArrayAsync());
        }

    }
}