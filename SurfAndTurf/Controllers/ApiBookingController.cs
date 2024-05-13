using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SurfAndTurf.Data;
using SurfAndTurf.Models;

[ApiController]
[Route("api/[controller]")]
public class APIBookingsController : ControllerBase
{
    private readonly SurfAndTurfContext _context;

    public APIBookingsController(SurfAndTurfContext context)
    {
        _context = context;
    }

    // GET: api/Bookings
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Bookings>>> GetBookings()
    {
        return await _context.Bookings.Include(b => b.IdentityUser).Include(b => b.SurfBoard).ToListAsync();
    }

    // GET: api/Bookings/5
    [HttpGet("{id}")]
    public async Task<ActionResult<Bookings>> GetBooking(int id)
    {
        var booking = await _context.Bookings.FindAsync(id);

        if (booking == null)
        {
            return NotFound();
        }

        return booking;
    }

    // PUT: api/Bookings/5
    [HttpPut("{id}")]
    public async Task<IActionResult> PutBooking(int id, Bookings booking)
    {
        if (id != booking.ID)
        {
            return BadRequest();
        }

        _context.Entry(booking).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!BookingExists(id))
            {
                return NotFound();
            }
            else
            {
                throw;
            }
        }

        return NoContent();
    }

    // POST: api/Bookings
    [HttpPost]
    public async Task<ActionResult<Bookings>> PostBooking(Bookings booking)
    {
        _context.Bookings.Add(booking);
        await _context.SaveChangesAsync();

        return CreatedAtAction("GetBooking", new { id = booking.ID }, booking);
    }

    // DELETE: api/Bookings/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteBooking(int id)
    {
        var booking = await _context.Bookings.FindAsync(id);
        if (booking == null)
        {
            return NotFound();
        }

        _context.Bookings.Remove(booking);
        await _context.SaveChangesAsync();

        return NoContent();
    }

    private bool BookingExists(int id)
    {
        return _context.Bookings.Any(e => e.ID == id);
    }
    /*
     * HttpGet("{id}") retrieves a specific booking by ID.
    HttpPut("{id}") updates a specific booking. Ensure the ID in the URL matches the ID in the body.
    HttpPost creates a new booking. The new booking is returned along with its newly assigned ID.
    HttpDelete("{id}") deletes a booking with the specified ID.
     */

}
