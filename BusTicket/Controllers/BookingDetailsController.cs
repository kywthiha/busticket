using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BusTicket.Data;
using BusTicket.Models;

namespace BusTicket.Controllers
{
    public class BookingDetailsController : Controller
    {
        private readonly BusTicketModalContext _context;

        public BookingDetailsController(BusTicketModalContext context)
        {
            _context = context;
        }

        // GET: BookingDetails
        public async Task<IActionResult> Index()
        {
            var busTicketModalContext = _context.BookingDetail.Include(b => b.Booking).Include(b => b.BusSeat);
            return View(await busTicketModalContext.ToListAsync());
        }

        // GET: BookingDetails/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bookingDetail = await _context.BookingDetail
                .Include(b => b.Booking)
                .Include(b => b.BusSeat)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (bookingDetail == null)
            {
                return NotFound();
            }

            return View(bookingDetail);
        }

        // GET: BookingDetails/Create
        public IActionResult Create()
        {
            ViewData["BookingID"] = new SelectList(_context.Booking, "ID", "ID");
            ViewData["BusSeatID"] = new SelectList(_context.BusSeat, "ID", "ID");
            return View();
        }

        // POST: BookingDetails/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,BookingID,BusSeatID")] BookingDetail bookingDetail)
        {
            if (ModelState.IsValid)
            {
                _context.Add(bookingDetail);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["BookingID"] = new SelectList(_context.Booking, "ID", "ID", bookingDetail.BookingID);
            ViewData["BusSeatID"] = new SelectList(_context.BusSeat, "ID", "ID", bookingDetail.BusSeatID);
            return View(bookingDetail);
        }

        // GET: BookingDetails/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bookingDetail = await _context.BookingDetail.FindAsync(id);
            if (bookingDetail == null)
            {
                return NotFound();
            }
            ViewData["BookingID"] = new SelectList(_context.Booking, "ID", "ID", bookingDetail.BookingID);
            ViewData["BusSeatID"] = new SelectList(_context.BusSeat, "ID", "ID", bookingDetail.BusSeatID);
            return View(bookingDetail);
        }

        // POST: BookingDetails/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,BookingID,BusSeatID")] BookingDetail bookingDetail)
        {
            if (id != bookingDetail.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(bookingDetail);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BookingDetailExists(bookingDetail.ID))
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
            ViewData["BookingID"] = new SelectList(_context.Booking, "ID", "ID", bookingDetail.BookingID);
            ViewData["BusSeatID"] = new SelectList(_context.BusSeat, "ID", "ID", bookingDetail.BusSeatID);
            return View(bookingDetail);
        }

        // GET: BookingDetails/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bookingDetail = await _context.BookingDetail
                .Include(b => b.Booking)
                .Include(b => b.BusSeat)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (bookingDetail == null)
            {
                return NotFound();
            }

            return View(bookingDetail);
        }

        // POST: BookingDetails/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var bookingDetail = await _context.BookingDetail.FindAsync(id);
            _context.BookingDetail.Remove(bookingDetail);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BookingDetailExists(int id)
        {
            return _context.BookingDetail.Any(e => e.ID == id);
        }
    }
}
