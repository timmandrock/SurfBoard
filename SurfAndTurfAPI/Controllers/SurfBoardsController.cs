using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using SurfAndTurf.Data;
using SurfAndTurf.Models;
using SurfAndTurfAPI.Models;

namespace SurfAndTurfAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class SurfboardController : ControllerBase
    {
        private readonly SurfAndTurfContext _context;
        private readonly ILogger<SurfboardController> _logger;

        public SurfboardController(ILogger<SurfboardController> logger, SurfAndTurfContext context)
        {
            _logger = logger;
            _context = context;
        }


        [Route("surfboards")]
        [HttpGet]
        public IEnumerable<SurfBoard> GetSurfboards()
        {
            var Surfboards = from s in _context.SurfBoard select s;
            return Surfboards.ToList();

        }

        [HttpGet("{id}")]
        public async Task<ActionResult<SurfBoard>> GetSurfBoard(int id)
        {
            var SurfboardItem = await _context.SurfBoard.FindAsync(id);

            if (SurfboardItem == null)
            {
                return NotFound();
            }

            return SurfboardItem;
        }

        [HttpGet]
        [Route("getBookings")]
        public async Task<IActionResult> GetBookings()
        {
            var Bookings = from b in _context.Bookings select b;

            return Ok(Bookings);
        }

        [HttpPost]
        [Route("createBooking")]
        public async Task<IActionResult> Create(BookingsDTO bookingsDTO)
        {
             string userId = _context.Users.Where(s => s.NormalizedUserName == bookingsDTO.UserName.ToUpper()).First().Id;

            Bookings bookings = new Bookings();
            bookings.IdentityUserID = userId;
            bookings.SurfBoardID = bookingsDTO.SurfBoardID;
            bookings.StartDate = bookingsDTO.StartDate;
            bookings.EndDate = bookingsDTO.EndDate;

             
            if (TryValidateModel(bookings))
            {
                _context.Add(bookings);
                await _context.SaveChangesAsync();
            }
            return StatusCode(StatusCodes.Status201Created, bookingsDTO);
        }

    }
}