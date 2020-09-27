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
    public class TravellersController : BaseController
    {
        public TravellersController(BusTicketDataContext context, IAuthorizationService authorizationService, UserManager<IdentityUser> userManager) : base(context, authorizationService, userManager)
        {
        }


        // GET: Travellers
        public async Task<IActionResult> Index()
        {
            var busTicketContext = _context.Travellers.Include(t => t.Owner);
            return View(await busTicketContext.ToListAsync());
        }

        // GET: Travellers/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var traveller = await _context.Travellers
                .Include(t => t.Owner)
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
            ViewData["OwnerID"] = new SelectList(_context.Set<IdentityUser>(), "Id", "Id");
            return View();
        }

        // POST: Travellers/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,Name,Gender,Email,PhoneNumber,Address,OwnerID")] Traveller traveller)
        {
            if (ModelState.IsValid)
            {
                _context.Add(traveller);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["OwnerID"] = new SelectList(_context.Set<IdentityUser>(), "Id", "Id", traveller.OwnerID);
            return View(traveller);
        }

        // GET: Travellers/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var traveller = await _context.Travellers.FindAsync(id);
            if (traveller == null)
            {
                return NotFound();
            }
            ViewData["OwnerID"] = new SelectList(_context.Set<IdentityUser>(), "Id", "Id", traveller.OwnerID);
            return View(traveller);
        }

        // POST: Travellers/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("ID,Name,Gender,Email,PhoneNumber,Address,OwnerID")] Traveller traveller)
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
            ViewData["OwnerID"] = new SelectList(_context.Set<IdentityUser>(), "Id", "Id", traveller.OwnerID);
            return View(traveller);
        }

        // GET: Travellers/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var traveller = await _context.Travellers
                .Include(t => t.Owner)
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
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var traveller = await _context.Travellers.FindAsync(id);
            _context.Travellers.Remove(traveller);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TravellerExists(string id)
        {
            return _context.Travellers.Any(e => e.ID == id);
        }
    }
}
