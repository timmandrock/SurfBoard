 using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using SurfAndTurf.Data;
using SurfAndTurf.Models;
using Microsoft.AspNetCore.Identity;
using SurfAndTurf.Migrations;
using Microsoft.AspNetCore.Mvc.ModelBinding;


namespace SurfAndTurf.Controllers
{

    public class SurfBoardsController : Controller
    {
        private readonly SurfAndTurfContext _context;
        private readonly UserManager<IdentityUser> _userManager;

        public SurfBoardsController(SurfAndTurfContext context, UserManager<IdentityUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }


        // GET: SurfBoards
        public async Task<IActionResult> Index(string sortOrder, string currentFilter, string searchString, int? pageNumber)
        {
            ViewData["CurrentSort"] = sortOrder;
            ViewData["LengthSortParm"] = String.IsNullOrEmpty(sortOrder) ? "Length_desc" : "";
            ViewData["TypeSortParm"] = sortOrder == "Type" ? "Type_desc" : "Type";
            ViewData["PriceSortParm"] = sortOrder == "Price" ? "Price_desc" : "Price";
            ViewData["CurrentFilter"] = searchString;

            if (searchString != null)
            {
                pageNumber = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            var surfboards = from s in _context.SurfBoard select s;

            var currentUser = await _userManager.GetUserAsync(User);
          
            if (!User.Identity.IsAuthenticated)
            {
                surfboards = surfboards.Take(2);
            }

            var bookings = from b in _context.Bookings
                           where b.EndDate.Date >= DateTime.Today
                           select b;

            foreach (Bookings b in bookings)
            {
                surfboards = surfboards.Where(s => s.Id != b.SurfBoardID);
            }

            if (!String.IsNullOrEmpty(searchString))
            {
                surfboards = surfboards.Where(s => s.Name.Contains(searchString) || s.Equipment.Contains(searchString));
            }

            switch (sortOrder)
            {
                case "Length_desc":
                    surfboards = surfboards.OrderByDescending(s => s.Length);
                    break;
                case "Type":
                    surfboards = surfboards.OrderBy(s => s.Type);
                    break;
                case "Type_desc":
                    surfboards = surfboards.OrderByDescending(s => s.Type);
                    break;
                case "Price":
                    surfboards = surfboards.OrderBy(s => s.Price);
                    break;
                case "Price_desc":
                    surfboards = surfboards.OrderByDescending(s => s.Price);
                    break;
                default:
                    surfboards = surfboards.OrderBy(s => s.Length);
                    break;
            }

            int pageSize = 5;
            return View(await PaginatedList<SurfBoard>.CreateAsync(surfboards.AsNoTracking(), pageNumber ?? 1, pageSize));
        }


        // GET: SurfBoards/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.SurfBoard == null)
            {
                return NotFound();
            }

            var surfBoard = await _context.SurfBoard
                .FirstOrDefaultAsync(m => m.Id == id);
            if (surfBoard == null)
            {
                return NotFound();
            }

            return View(surfBoard);
        }

        // GET: SurfBoards/Create
        [Authorize]
        public IActionResult Create()
        {
            return View();
        }

        // POST: SurfBoards/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Type,Length,Width,Thickness,Volume,Price,Equipment,ImageUrl")] SurfBoard surfBoard)
        {
            if (ModelState.IsValid)
            {
                _context.Add(surfBoard);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(surfBoard);
        }

        // GET: SurfBoards/Edit/5
        [Authorize]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.SurfBoard == null)
            {
                return NotFound();
            }

            var surfBoard = await _context.SurfBoard.FindAsync(id);
            if (surfBoard == null)
            {
                return NotFound();
            }
            return View(surfBoard);
        }

        // POST: SurfBoards/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Type,Length,Width,Thickness,Volume,Price,Equipment,ImageUrl,Rowversion")] SurfBoard surfBoard)
        {
            if (id != surfBoard.Id)
            {
                return NotFound();
            }

            if (!ModelState.IsValid)
            {
                return View(surfBoard);
            }

            _context.Entry(surfBoard).Property("Rowversion").OriginalValue = surfBoard.Rowversion;

            try
            {
                _context.Update(surfBoard);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                var exceptionEntry = ex.Entries.Single();
                var databaseEntry = exceptionEntry.GetDatabaseValues();

                if (databaseEntry == null)
                {
                    ModelState.AddModelError(string.Empty, "Unable to save changes. The surfboard was deleted by another user.");
                }
                else
                {
                    var dbValues = (SurfBoard)databaseEntry.ToObject();
                    

                    ModelState.AddModelError(string.Empty, "The record you attempted to edit was modified by another user after you got the original value. " +
                        "The edit operation was canceled. If you still want to edit this record, click Save again.");

                    // Update the RowVersion to the value when the database was last updated
                    surfBoard.Rowversion = (byte[])databaseEntry["Rowversion"];
                    ModelState.Remove("Rowversion");
                }
                return View(surfBoard); // Return to the same view with the error message
            }

            return RedirectToAction(nameof(Index));
        }




        // GET: SurfBoards/Delete/5
        [Authorize]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.SurfBoard == null)
            {
                return NotFound();
            }

            var surfBoard = await _context.SurfBoard
                .FirstOrDefaultAsync(m => m.Id == id);
            if (surfBoard == null)
            {
                return NotFound();
            }

            return View(surfBoard);
        }

        // POST: SurfBoards/Delete/5
        [Authorize]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.SurfBoard == null)
            {
                return Problem("Entity set 'SurfAndTurfContext.SurfBoard'  is null.");
            }
            var surfBoard = await _context.SurfBoard.FindAsync(id);
            if (surfBoard != null)
            {
                _context.SurfBoard.Remove(surfBoard);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SurfBoardExists(int id)
        {
          return (_context.SurfBoard?.Any(e => e.Id == id)).GetValueOrDefault();
        }

       
        [HttpGet]
        public async Task<IActionResult> Rent(int? id)
        {
            IdentityUser user = new IdentityUser();

            if (!User.Identity.IsAuthenticated)
            {
                var guestUser = await _userManager.FindByEmailAsync("GUEST@GUEST.COM");
                if (guestUser != null)
                {
                    user = guestUser;
                }
                else
                {
                    user = await _userManager.GetUserAsync(User);
                    ModelState.AddModelError(String.Empty, "An error occurred. Please try again.");
                }
            }
            
            string name = user.UserName;

            var boards = from b in _context.SurfBoard select b;
            var board = await boards.FirstAsync(b => b.Id == id);

            ViewData["IdentityUserName"] = name;
            ViewData["SurfBoardName"] = board.Name;

            var model = new Bookings
            {
                IdentityUserID = user.Id,
                SurfBoardID= board.Id,
                StartDate= DateTime.Now,
                EndDate= DateTime.Now
                
            };
            return View(model);
        }

        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Rent([Bind("IdentityUserID,SurfBoardID,StartDate,EndDate")] Bookings bookings)
        {

            if (!User.Identity.IsAuthenticated)
            {
                var guestUser = await _userManager.FindByEmailAsync("GUEST@GUEST.COM");
                if (guestUser != null)
                {
                    bookings.IdentityUserID = guestUser.Id;
                }
                else
                {
                    ModelState.AddModelError(String.Empty, "An error occurred. Please try again.");
                    return View();
                }
            }

            //Validate rent period
            DateTime endDate = bookings.EndDate;
            DateTime maxDate = DateTime.Today.AddDays(14);
            if (endDate > maxDate)
            {
                ModelState.AddModelError("EndDate", "Can not rent for more than 14 days.");
            }
            else if (endDate < DateTime.Today)
            {
                ModelState.AddModelError("EndDate", "Can not rent in the past.");
            }

            //Check for bookings of current board
            var otherBookings = from b in _context.Bookings
                           where b.EndDate.Date >= DateTime.Today
                           && b.SurfBoardID == bookings.SurfBoardID
                           select b;
            
            if(otherBookings.Count() >= 1)
            {
                ModelState.AddModelError(String.Empty, "Sadly this board has already been booked");
                return View();
            }

            if (ModelState.IsValid)
            {
                _context.Add(bookings);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View();
        }
    }
}
