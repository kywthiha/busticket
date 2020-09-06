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
    public class RouteDetailsController : Controller
    {
        private readonly BusTicketContactsContext _context;

        public RouteDetailsController(BusTicketContactsContext context)
        {
            _context = context;
        }

        // GET: RouteDetails
        public async Task<IActionResult> Index()
        {
            var busTicketContactsContext = _context.RouteDetail.Include(r => r.City).Include(r => r.Route);
            return View(await busTicketContactsContext.ToListAsync());
        }

        // GET: RouteDetails/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var routeDetail = await _context.RouteDetail
                .Include(r => r.City)
                .Include(r => r.Route)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (routeDetail == null)
            {
                return NotFound();
            }

            return View(routeDetail);
        }

        // GET: RouteDetails/Create
        public IActionResult Create()
        {
            ViewData["CityID"] = new SelectList(_context.City, "ID", "Name");
            ViewData["RouteID"] = new SelectList(_context.Route, "ID", "Title");
            return View();
        }

        // POST: RouteDetails/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,Order,RouteID,CityID")] RouteDetail routeDetail)
        {
            if (ModelState.IsValid)
            {
                _context.Add(routeDetail);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CityID"] = new SelectList(_context.City, "ID", "Name", routeDetail.CityID);
            ViewData["RouteID"] = new SelectList(_context.Route, "ID", "Title", routeDetail.RouteID);
            return View(routeDetail);
        }

        // GET: RouteDetails/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var routeDetail = await _context.RouteDetail.FindAsync(id);
            if (routeDetail == null)
            {
                return NotFound();
            }
            ViewData["CityID"] = new SelectList(_context.City, "ID", "Name", routeDetail.CityID);
            ViewData["RouteID"] = new SelectList(_context.Route, "ID", "Title", routeDetail.RouteID);
            return View(routeDetail);
        }

        // POST: RouteDetails/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,Order,RouteID,CityID")] RouteDetail routeDetail)
        {
            if (id != routeDetail.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(routeDetail);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RouteDetailExists(routeDetail.ID))
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
            ViewData["CityID"] = new SelectList(_context.City, "ID", "Name", routeDetail.CityID);
            ViewData["RouteID"] = new SelectList(_context.Route, "ID", "Title", routeDetail.RouteID);
            return View(routeDetail);
        }

        // GET: RouteDetails/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var routeDetail = await _context.RouteDetail
                .Include(r => r.City)
                .Include(r => r.Route)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (routeDetail == null)
            {
                return NotFound();
            }

            return View(routeDetail);
        }

        // POST: RouteDetails/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var routeDetail = await _context.RouteDetail.FindAsync(id);
            _context.RouteDetail.Remove(routeDetail);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool RouteDetailExists(int id)
        {
            return _context.RouteDetail.Any(e => e.ID == id);
        }
    }
}
