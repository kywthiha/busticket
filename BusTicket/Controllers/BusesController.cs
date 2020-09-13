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
    public class BusesController : Controller
    {
        private readonly BusTicketModalContext _context;

        public BusesController(BusTicketModalContext context)
        {
            _context = context;
        }

        // GET: Buses
        public async Task<IActionResult> Index()
        {
            var busTicketModalContext = _context.Bus.Include(b => b.BusOperator);
            return View(await busTicketModalContext.ToListAsync());
        }

        // GET: Buses/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bus = await _context.Bus
                .Include(b => b.BusOperator)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (bus == null)
            {
                return NotFound();
            }

            return View(bus);
        }

        // GET: Buses/Create
        public IActionResult Create()
        {
            ViewData["BusOperatorID"] = new SelectList(_context.BusOperator, "ID", "Address");
            return View();
        }

        // POST: Buses/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,OwnerID,NoOfSeat,Type,BusOperatorID")] Bus bus)
        {
            if (ModelState.IsValid)
            {
                _context.Add(bus);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["BusOperatorID"] = new SelectList(_context.BusOperator, "ID", "Address", bus.BusOperatorID);
            return View(bus);
        }

        // GET: Buses/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bus = await _context.Bus.FindAsync(id);
            if (bus == null)
            {
                return NotFound();
            }
            ViewData["BusOperatorID"] = new SelectList(_context.BusOperator, "ID", "Address", bus.BusOperatorID);
            return View(bus);
        }

        // POST: Buses/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,OwnerID,NoOfSeat,Type,BusOperatorID")] Bus bus)
        {
            if (id != bus.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(bus);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BusExists(bus.ID))
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
            ViewData["BusOperatorID"] = new SelectList(_context.BusOperator, "ID", "Address", bus.BusOperatorID);
            return View(bus);
        }

        // GET: Buses/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bus = await _context.Bus
                .Include(b => b.BusOperator)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (bus == null)
            {
                return NotFound();
            }

            return View(bus);
        }

        // POST: Buses/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var bus = await _context.Bus.FindAsync(id);
            _context.Bus.Remove(bus);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BusExists(int id)
        {
            return _context.Bus.Any(e => e.ID == id);
        }
    }
}
