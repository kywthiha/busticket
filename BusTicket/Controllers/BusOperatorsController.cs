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
    public class BusOperatorsController : BaseController
    {
        public BusOperatorsController(BusTicketDataContext context, IAuthorizationService authorizationService, UserManager<IdentityUser> userManager) : base(context, authorizationService, userManager)
        {
        }


        // GET: BusOperators
        public async Task<IActionResult> Index()
        {
            var busTicketContext = _context.BusOperators.Include(b => b.Owner);
            return View(await busTicketContext.ToListAsync());
        }

        // GET: BusOperators/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var busOperator = await _context.BusOperators
                .Include(b => b.Owner)
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
            ViewData["OwnerID"] = new SelectList(_context.Set<IdentityUser>(), "Id", "Id");
            return View();
        }

        // POST: BusOperators/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,Name,Phoneno,Email,Address")] BusOperator busOperator)
        {
            if (ModelState.IsValid)
            {
                busOperator.OwnerID = _userManager.GetUserId(User);
                _context.Add(busOperator);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["OwnerID"] = new SelectList(_context.Set<IdentityUser>(), "Id", "Id", busOperator.OwnerID);
            return View(busOperator);
        }

        // GET: BusOperators/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var busOperator = await _context.BusOperators.FindAsync(id);
            if (busOperator == null)
            {
                return NotFound();
            }
            ViewData["OwnerID"] = new SelectList(_context.Set<IdentityUser>(), "Id", "Id", busOperator.OwnerID);
            return View(busOperator);
        }

        // POST: BusOperators/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,Name,Phoneno,Email,Address")] BusOperator busOperator)
        {
            if (id != busOperator.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    busOperator.OwnerID = _userManager.GetUserId(User);
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
            ViewData["OwnerID"] = new SelectList(_context.Set<IdentityUser>(), "Id", "Id", busOperator.OwnerID);
            return View(busOperator);
        }

        // GET: BusOperators/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var busOperator = await _context.BusOperators
                .Include(b => b.Owner)
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
            var busOperator = await _context.BusOperators.FindAsync(id);
            _context.BusOperators.Remove(busOperator);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BusOperatorExists(int id)
        {
            return _context.BusOperators.Any(e => e.ID == id);
        }
    }
}
