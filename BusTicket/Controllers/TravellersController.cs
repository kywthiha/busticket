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
    public class TravellersController : Controller
    {
        private readonly BusTicketModalContext _context;

        public TravellersController(BusTicketModalContext context)
        {
            _context = context;
        }

        // GET: Travellers
        public async Task<IActionResult> Index()
        {
            return View(await _context.Traveller.ToListAsync());
        }

        // GET: Travellers/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var traveller = await _context.Traveller
                .FirstOrDefaultAsync(m => m.ID == id);
            if (traveller == null)
            {
                return NotFound();
            }

            return View(traveller);
        }

        // GET: Travellers/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Travellers/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,OwnerID,Name,Email,PhoneNumber,Address")] Traveller traveller)
        {
            if (ModelState.IsValid)
            {
                _context.Add(traveller);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(traveller);
        }

        // GET: Travellers/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var traveller = await _context.Traveller.FindAsync(id);
            if (traveller == null)
            {
                return NotFound();
            }
            return View(traveller);
        }

        // POST: Travellers/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,OwnerID,Name,Email,PhoneNumber,Address")] Traveller traveller)
        {
            if (id != traveller.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(traveller);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TravellerExists(traveller.ID))
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
            return View(traveller);
        }

        // GET: Travellers/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var traveller = await _context.Traveller
                .FirstOrDefaultAsync(m => m.ID == id);
            if (traveller == null)
            {
                return NotFound();
            }

            return View(traveller);
        }

        // POST: Travellers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var traveller = await _context.Traveller.FindAsync(id);
            _context.Traveller.Remove(traveller);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TravellerExists(int id)
        {
            return _context.Traveller.Any(e => e.ID == id);
        }
    }
}
