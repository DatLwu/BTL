using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Cafe.Data;
using Cafe.Models;

namespace Cafe.Controllers
{
    public class DrinkController : Controller
    {
        private readonly ApplicationDbcontext _context;

        public DrinkController(ApplicationDbcontext context)
        {
            _context = context;
        }

        // GET: Drink
        public async Task<IActionResult> Index()
        {
            var applicationDbcontext = _context.Drink.Include(d => d.Bill);
            return View(await applicationDbcontext.ToListAsync());
        }

        // GET: Drink/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var drink = await _context.Drink
                .Include(d => d.Bill)
                .FirstOrDefaultAsync(m => m.DrinkId == id);
            if (drink == null)
            {
                return NotFound();
            }

            return View(drink);
        }

        // GET: Drink/Create
        public IActionResult Create()
        {
            ViewData["BillId"] = new SelectList(_context.Bill, "BillId", "BillId");
            return View();
        }

        // POST: Drink/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("DrinkId,DrinkName,SoLuong,Price,BillId")] Drink drink)
        {
            if (ModelState.IsValid)
            {
                _context.Add(drink);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["BillId"] = new SelectList(_context.Bill, "BillId", "BillId", drink.BillId);
            return View(drink);
        }

        // GET: Drink/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var drink = await _context.Drink.FindAsync(id);
            if (drink == null)
            {
                return NotFound();
            }
            ViewData["BillId"] = new SelectList(_context.Bill, "BillId", "BillId", drink.BillId);
            return View(drink);
        }

        // POST: Drink/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("DrinkId,DrinkName,SoLuong,Price,BillId")] Drink drink)
        {
            if (id != drink.DrinkId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(drink);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DrinkExists(drink.DrinkId))
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
            ViewData["BillId"] = new SelectList(_context.Bill, "BillId", "BillId", drink.BillId);
            return View(drink);
        }

        // GET: Drink/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var drink = await _context.Drink
                .Include(d => d.Bill)
                .FirstOrDefaultAsync(m => m.DrinkId == id);
            if (drink == null)
            {
                return NotFound();
            }

            return View(drink);
        }

        // POST: Drink/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var drink = await _context.Drink.FindAsync(id);
            if (drink != null)
            {
                _context.Drink.Remove(drink);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DrinkExists(string id)
        {
            return _context.Drink.Any(e => e.DrinkId == id);
        }
    }
}
