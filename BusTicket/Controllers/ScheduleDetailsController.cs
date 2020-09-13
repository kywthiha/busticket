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
    public class ScheduleDetailsController : Controller
    {
        private readonly BusTicketModalContext _context;

        public ScheduleDetailsController(BusTicketModalContext context)
        {
            _context = context;
        }

        // GET: ScheduleDetails
        public async Task<IActionResult> Index()
        {
            var busTicketModalContext = _context.ScheduleDetail.Include(s => s.Schedule);
            return View(await busTicketModalContext.ToListAsync());
        }

        // GET: ScheduleDetails/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var scheduleDetail = await _context.ScheduleDetail
                .Include(s => s.Schedule)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (scheduleDetail == null)
            {
                return NotFound();
            }

            return View(scheduleDetail);
        }

        // GET: ScheduleDetails/Create
        public IActionResult Create()
        {
            ViewData["ScheduleID"] = new SelectList(_context.Schedule, "ID", "ID");
            return View();
        }

        // POST: ScheduleDetails/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,ScheduleID,WeekDay")] ScheduleDetail scheduleDetail)
        {
            if (ModelState.IsValid)
            {
                _context.Add(scheduleDetail);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ScheduleID"] = new SelectList(_context.Schedule, "ID", "ID", scheduleDetail.ScheduleID);
            return View(scheduleDetail);
        }

        // GET: ScheduleDetails/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var scheduleDetail = await _context.ScheduleDetail.FindAsync(id);
            if (scheduleDetail == null)
            {
                return NotFound();
            }
            ViewData["ScheduleID"] = new SelectList(_context.Schedule, "ID", "ID", scheduleDetail.ScheduleID);
            return View(scheduleDetail);
        }

        // POST: ScheduleDetails/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,ScheduleID,WeekDay")] ScheduleDetail scheduleDetail)
        {
            if (id != scheduleDetail.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(scheduleDetail);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ScheduleDetailExists(scheduleDetail.ID))
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
            ViewData["ScheduleID"] = new SelectList(_context.Schedule, "ID", "ID", scheduleDetail.ScheduleID);
            return View(scheduleDetail);
        }

        // GET: ScheduleDetails/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var scheduleDetail = await _context.ScheduleDetail
                .Include(s => s.Schedule)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (scheduleDetail == null)
            {
                return NotFound();
            }

            return View(scheduleDetail);
        }

        // POST: ScheduleDetails/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var scheduleDetail = await _context.ScheduleDetail.FindAsync(id);
            _context.ScheduleDetail.Remove(scheduleDetail);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ScheduleDetailExists(int id)
        {
            return _context.ScheduleDetail.Any(e => e.ID == id);
        }
    }
}
