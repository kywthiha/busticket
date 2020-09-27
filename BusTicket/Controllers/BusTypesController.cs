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
    public class BusTypesController : BaseController
    {
        public BusTypesController(BusTicketDataContext context, IAuthorizationService authorizationService, UserManager<IdentityUser> userManager) : base(context, authorizationService, userManager)
        {
        }


        // GET: BusTypes
        public async Task<IActionResult> Index()
        {
            var busTicketContext = _context.BusTypes.Include(b => b.Owner);
            return View(await busTicketContext.ToListAsync());
        }

        // GET: BusTypes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var busType = await _context.BusTypes
                .Include(b => b.Owner)
                .Include(b=>b.BusSeats)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (busType == null)
            {
                return NotFound();
            }

            return View(busType);
        }

        // GET: BusTypes/Create
        public IActionResult Create()
        {
            ViewData["OwnerID"] = new SelectList(_context.Set<IdentityUser>(), "Id", "Id");
            return View();
        }

        // POST: BusTypes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,Name,Seats")] BusType busType)
        {
            if (ModelState.IsValid)
            {
                busType.OwnerID = _userManager.GetUserId(User);
                busType.Status = true;
                _context.Attach(busType);
                busType.BusSeats = new List<BusSeat>();
                int xx = 0;
                for(int i = 0; i < busType.Seats; i+=4)
                {
                    for(int ii = 0; ii < 4 && (i + ii) < busType.Seats; ii++)
                    {
                        busType.BusSeats.Add(new BusSeat { BusTypeID = busType.ID, SeatNo = i+ii + 1,PositionX = xx,PositionY = ii });
                    }
                    xx++;
                    
                }
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["OwnerID"] = new SelectList(_context.Set<IdentityUser>(), "Id", "Id", busType.OwnerID);
            return View(busType);
        }

        // GET: BusTypes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var busType = await _context.BusTypes.FindAsync(id);
            if (busType == null)
            {
                return NotFound();
            }
            ViewData["OwnerID"] = new SelectList(_context.Set<IdentityUser>(), "Id", "Id", busType.OwnerID);
            return View(busType);
        }

        // POST: BusTypes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,Name,Seats,Status")] BusType busType)
        {
            if (id != busType.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    busType.OwnerID = _userManager.GetUserId(User);

                    var busSeats = await _context.BusSeats
                            .Where(m=>m.BusTypeID == busType.ID)
                            .ToListAsync();
                   
                    _context.BusSeats.RemoveRange(busSeats);

                    busType.BusSeats = new List<BusSeat>();
                    int xx = 0;
                    for (int i = 0; i < busType.Seats; i += 4)
                    {
                        for (int ii = 0; ii < 4 && (i + ii) < busType.Seats; ii++)
                        {
                            busType.BusSeats.Add(new BusSeat { BusTypeID = busType.ID, SeatNo = i + ii + 1, PositionX = xx, PositionY = ii });
                        }
                        xx++;

                    }
                    _context.Update(busType);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BusTypeExists(busType.ID))
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
            ViewData["OwnerID"] = new SelectList(_context.Set<IdentityUser>(), "Id", "Id", busType.OwnerID);
            return View(busType);
        }

        // GET: BusTypes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var busType = await _context.BusTypes
                .Include(b => b.Owner)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (busType == null)
            {
                return NotFound();
            }

            return View(busType);
        }

        // POST: BusTypes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var busType = await _context.BusTypes.FindAsync(id);
            _context.BusTypes.Remove(busType);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BusTypeExists(int id)
        {
            return _context.BusTypes.Any(e => e.ID == id);
        }
    }
}
