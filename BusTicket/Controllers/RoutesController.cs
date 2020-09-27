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
    public class RoutesController : BaseController
    {
        public RoutesController(BusTicketDataContext context, IAuthorizationService authorizationService, UserManager<IdentityUser> userManager) : base(context, authorizationService, userManager)
        {
        }


        // GET: Routes
        public async Task<IActionResult> Index()
        {
            var busTicketContext = _context.Routes.Include(r => r.Owner);
            return View(await busTicketContext.ToListAsync());
        }

        // GET: Routes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var route = await _context.Routes
                .Include(r => r.Owner)
                .Include(r=>r.RouteDetails)
                .ThenInclude(rd=>rd.City)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (route == null)
            {
                return NotFound();
            }

            return View(route);
        }

        // GET: Routes/Create
        public IActionResult Create()
        {
            ViewData["OwnerID"] = new SelectList(_context.Set<IdentityUser>(), "Id", "Id");
            ViewData["City"] = _context.Cities.ToList();
            return View();
        }

        // POST: Routes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,Name")] Route route,int [] locations)
        {
            if (ModelState.IsValid)
            {
                route.OwnerID = _userManager.GetUserId(User);
                route.Status = true;
                _context.Attach(route);
                route.RouteDetails = new List<RouteDetail>();
                for (int i=0; i < locations.Length; i++)
                {
                    route.RouteDetails.Add(new RouteDetail{ RouteID=route.ID , CityID = locations[i] , Order = i+1});
                }
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["OwnerID"] = new SelectList(_context.Set<IdentityUser>(), "Id", "Id", route.OwnerID);
            return View(route);
        }

        // GET: Routes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var route = await _context.Routes.FindAsync(id);
            if (route == null)
            {
                return NotFound();
            }
            ViewData["OwnerID"] = new SelectList(_context.Set<IdentityUser>(), "Id", "Id", route.OwnerID);
            return View(route);
        }

        // POST: Routes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,Name,Status")] Route route)
        {
            if (id != route.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    route.OwnerID = _userManager.GetUserId(User);
                    _context.Update(route);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RouteExists(route.ID))
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
            ViewData["OwnerID"] = new SelectList(_context.Set<IdentityUser>(), "Id", "Id", route.OwnerID);
            return View(route);
        }

        // GET: Routes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var route = await _context.Routes
                .Include(r => r.Owner)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (route == null)
            {
                return NotFound();
            }

            return View(route);
        }

        // POST: Routes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var route = await _context.Routes.FindAsync(id);
            _context.Routes.Remove(route);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool RouteExists(int id)
        {
            return _context.Routes.Any(e => e.ID == id);
        }
    }
}
