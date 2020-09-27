using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BusTicket.Data;
using BusTicket.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;

namespace BusTicket.Controllers
{
    public class BookingsController : BaseController
    {
        public BookingsController(BusTicketDataContext context, IAuthorizationService authorizationService, UserManager<IdentityUser> userManager) : base(context, authorizationService, userManager)
        {
        }


        // GET: Bookings
        public async Task<IActionResult> Index()
        {
            var busTicketContext = _context.Bookings.Include(b => b.Owner).Include(b => b.Traveller);
            return View(await busTicketContext.ToListAsync());
        }

        // GET: Bookings/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var booking = await _context.Bookings
                .Include(b => b.Owner)
                .Include(b => b.Traveller)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (booking == null)
            {
                return NotFound();
            }

            return View(booking);
        }

        // GET: Bookings/Create
        public IActionResult Create()
        {
            ViewData["OwnerID"] = new SelectList(_context.Set<IdentityUser>(), "Id", "Id");
            ViewData["TravellerID"] = new SelectList(_context.Travellers, "ID", "ID");
            return View();
        }

        // POST: Bookings/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,DepartureDate,SeatedDate,ConfirmedDate,CanceledDate,TimeoutDate,BookingStatus,TravellerID,OwnerID")] Booking booking)
        {
            if (ModelState.IsValid)
            {
                _context.Add(booking);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["OwnerID"] = new SelectList(_context.Set<IdentityUser>(), "Id", "Id", booking.OwnerID);
            ViewData["TravellerID"] = new SelectList(_context.Travellers, "ID", "ID", booking.TravellerID);
            return View(booking);
        }

        // GET: Bookings/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var booking = await _context.Bookings.FindAsync(id);
            if (booking == null)
            {
                return NotFound();
            }
            ViewData["OwnerID"] = new SelectList(_context.Set<IdentityUser>(), "Id", "Id", booking.OwnerID);
            ViewData["TravellerID"] = new SelectList(_context.Travellers, "ID", "ID", booking.TravellerID);
            return View(booking);
        }

        // POST: Bookings/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,DepartureDate,SeatedDate,ConfirmedDate,CanceledDate,TimeoutDate,BookingStatus,TravellerID,OwnerID")] Booking booking)
        {
            if (id != booking.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(booking);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BookingExists(booking.ID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["OwnerID"] = new SelectList(_context.Set<IdentityUser>(), "Id", "Id", booking.OwnerID);
            ViewData["TravellerID"] = new SelectList(_context.Travellers, "ID", "ID", booking.TravellerID);
            return View(booking);
        }

        // GET: Bookings/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var booking = await _context.Bookings
                .Include(b => b.Owner)
                .Include(b => b.Traveller)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (booking == null)
            {
                return NotFound();
            }

            return View(booking);
        }

        // POST: Bookings/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var booking = await _context.Bookings.FindAsync(id);
            _context.Bookings.Remove(booking);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BookingExists(int id)
        {
            return _context.Bookings.Any(e => e.ID == id);
        }
    }
}
