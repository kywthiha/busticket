﻿using System;
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
    public class BusSeatsController : BaseController
    {
        public BusSeatsController(BusTicketDataContext context, IAuthorizationService authorizationService, UserManager<IdentityUser> userManager) : base(context, authorizationService, userManager)
        {
        }

        // GET: BusSeats
        public async Task<IActionResult> Index()
        {
            var busTicketContext = _context.BusSeats.Include(b => b.BusType);
            return View(await busTicketContext.ToListAsync());
        }

        // GET: BusSeats/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var busSeat = await _context.BusSeats
                .Include(b => b.BusType)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (busSeat == null)
            {
                return NotFound();
            }

            return View(busSeat);
        }

        // GET: BusSeats/Create
        public IActionResult Create()
        {
            ViewData["BusTypeID"] = new SelectList(_context.Set<BusType>(), "ID", "Name");
            return View();
        }

        // POST: BusSeats/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,SeatNo,PositionX,PositionY,BusTypeID")] BusSeat busSeat)
        {
            if (ModelState.IsValid)
            {
                _context.Add(busSeat);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["BusTypeID"] = new SelectList(_context.Set<BusType>(), "ID", "Name", busSeat.BusTypeID);
            return View(busSeat);
        }

        // GET: BusSeats/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var busSeat = await _context.BusSeats.FindAsync(id);
            if (busSeat == null)
            {
                return NotFound();
            }
            ViewData["BusTypeID"] = new SelectList(_context.Set<BusType>(), "ID", "Name", busSeat.BusTypeID);
            return View(busSeat);
        }

        // POST: BusSeats/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,SeatNo,PositionX,PositionY,BusTypeID")] BusSeat busSeat)
        {
            if (id != busSeat.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(busSeat);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BusSeatExists(busSeat.ID))
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
            ViewData["BusTypeID"] = new SelectList(_context.Set<BusType>(), "ID", "Name", busSeat.BusTypeID);
            return View(busSeat);
        }

        // GET: BusSeats/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var busSeat = await _context.BusSeats
                .Include(b => b.BusType)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (busSeat == null)
            {
                return NotFound();
            }

            return View(busSeat);
        }

        // POST: BusSeats/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var busSeat = await _context.BusSeats.FindAsync(id);
            _context.BusSeats.Remove(busSeat);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BusSeatExists(int id)
        {
            return _context.BusSeats.Any(e => e.ID == id);
        }
    }
}
