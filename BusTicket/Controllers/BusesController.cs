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
    public class BusesController : BaseController
    {
        public BusesController(BusTicketDataContext context, IAuthorizationService authorizationService, UserManager<IdentityUser> userManager) : base(context, authorizationService, userManager)
        {

        }


        // GET: Buses
        public async Task<IActionResult> Index()
        {
            var busTicketContext = _context.Buses.Include(b => b.BusOperator).Include(b => b.BusType).Include(b => b.Owner).Include(b => b.Route);
            return View(await busTicketContext.ToListAsync());
        }

        // GET: Buses/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bus = await _context.Buses
                .Include(b => b.BusOperator)
                .Include(b => b.BusType)
                .Include(b => b.Owner)
                .Include(b => b.Route)
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
            ViewData["BusOperatorID"] = new SelectList(_context.Set<BusOperator>(), "ID", "Address");
            ViewData["BusTypeID"] = new SelectList(_context.Set<BusType>(), "ID", "Name");
            ViewData["OwnerID"] = new SelectList(_context.Set<IdentityUser>(), "Id", "Id");
            ViewData["RouteID"] = new SelectList(_context.Set<Route>(), "ID", "Name");
            return View();
        }

        // POST: Buses/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,FromDate,ToDate,RouteID,BusOperatorID,BusTypeID,OwnerID")] Bus bus)
        {
            if (ModelState.IsValid)
            {
                _context.Add(bus);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["BusOperatorID"] = new SelectList(_context.Set<BusOperator>(), "ID", "Address", bus.BusOperatorID);
            ViewData["BusTypeID"] = new SelectList(_context.Set<BusType>(), "ID", "Name", bus.BusTypeID);
            ViewData["OwnerID"] = new SelectList(_context.Set<IdentityUser>(), "Id", "Id", bus.OwnerID);
            ViewData["RouteID"] = new SelectList(_context.Set<Route>(), "ID", "Name", bus.RouteID);
            return View(bus);
        }

        // GET: Buses/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bus = await _context.Buses.FindAsync(id);
            if (bus == null)
            {
                return NotFound();
            }
            ViewData["BusOperatorID"] = new SelectList(_context.Set<BusOperator>(), "ID", "Address", bus.BusOperatorID);
            ViewData["BusTypeID"] = new SelectList(_context.Set<BusType>(), "ID", "Name", bus.BusTypeID);
            ViewData["OwnerID"] = new SelectList(_context.Set<IdentityUser>(), "Id", "Id", bus.OwnerID);
            ViewData["RouteID"] = new SelectList(_context.Set<Route>(), "ID", "Name", bus.RouteID);
            return View(bus);
        }

        // POST: Buses/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,FromDate,ToDate,RouteID,BusOperatorID,BusTypeID,OwnerID")] Bus bus)
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
            ViewData["BusOperatorID"] = new SelectList(_context.Set<BusOperator>(), "ID", "Address", bus.BusOperatorID);
            ViewData["BusTypeID"] = new SelectList(_context.Set<BusType>(), "ID", "Name", bus.BusTypeID);
            ViewData["OwnerID"] = new SelectList(_context.Set<IdentityUser>(), "Id", "Id", bus.OwnerID);
            ViewData["RouteID"] = new SelectList(_context.Set<Route>(), "ID", "Name", bus.RouteID);
            return View(bus);
        }

        // GET: Buses/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bus = await _context.Buses
                .Include(b => b.BusOperator)
                .Include(b => b.BusType)
                .Include(b => b.Owner)
                .Include(b => b.Route)
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
            var bus = await _context.Buses.FindAsync(id);
            _context.Buses.Remove(bus);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BusExists(int id)
        {
            return _context.Buses.Any(e => e.ID == id);
        }
    }
}
