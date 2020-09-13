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
    public class BusOperatorsController : Controller
    {
        private readonly BusTicketModalContext _context;

        public BusOperatorsController(BusTicketModalContext context)
        {
            _context = context;
        }

        // GET: BusOperators
        public async Task<IActionResult> Index()
        {
            return View(await _context.BusOperator.ToListAsync());
        }

        // GET: BusOperators/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var busOperator = await _context.BusOperator
                .FirstOrDefaultAsync(m => m.ID == id);
            if (busOperator == null)
            {
                return NotFound();
            }

            return View(busOperator);
        }

        // GET: BusOperators/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: BusOperators/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,OwnerID,Name,Phoneno,Email,Address")] BusOperator busOperator)
        {
            if (ModelState.IsValid)
            {
                _context.Add(busOperator);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(busOperator);
        }

        // GET: BusOperators/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var busOperator = await _context.BusOperator.FindAsync(id);
            if (busOperator == null)
            {
                return NotFound();
            }
            return View(busOperator);
        }

        // POST: BusOperators/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,OwnerID,Name,Phoneno,Email,Address")] BusOperator busOperator)
        {
            if (id != busOperator.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(busOperator);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BusOperatorExists(busOperator.ID))
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
            return View(busOperator);
        }

        // GET: BusOperators/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var busOperator = await _context.BusOperator
                .FirstOrDefaultAsync(m => m.ID == id);
            if (busOperator == null)
            {
                return NotFound();
            }

            return View(busOperator);
        }

        // POST: BusOperators/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var busOperator = await _context.BusOperator.FindAsync(id);
            _context.BusOperator.Remove(busOperator);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BusOperatorExists(int id)
        {
            return _context.BusOperator.Any(e => e.ID == id);
        }
    }
}
