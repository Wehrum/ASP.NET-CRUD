using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebAppProject.Data;
using WebAppProject.Models;

namespace WebAppProject.Controllers
{
    public class ClickItsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ClickItsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: ClickIts
        public async Task<IActionResult> Index()
        {
            return View(await _context.ClickIt.ToListAsync());
        }

        // GET: ClickIts/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var clickIt = await _context.ClickIt
                .FirstOrDefaultAsync(m => m.Id == id);
            if (clickIt == null)
            {
                return NotFound();
            }

            return View(clickIt);
        }

        // GET: ClickIts/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: ClickIts/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Click")] ClickIt clickIt)
        {
            if (ModelState.IsValid)
            {
                _context.Add(clickIt);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(clickIt);
        }

        // GET: ClickIts/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var clickIt = await _context.ClickIt.FindAsync(id);
            if (clickIt == null)
            {
                return NotFound();
            }
            return View(clickIt);
        }

        // POST: ClickIts/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Click")] ClickIt clickIt)
        {
            if (id != clickIt.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(clickIt);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ClickItExists(clickIt.Id))
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
            return View(clickIt);
        }

        // GET: ClickIts/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var clickIt = await _context.ClickIt
                .FirstOrDefaultAsync(m => m.Id == id);
            if (clickIt == null)
            {
                return NotFound();
            }

            return View(clickIt);
        }

        // POST: ClickIts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var clickIt = await _context.ClickIt.FindAsync(id);
            _context.ClickIt.Remove(clickIt);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ClickItExists(int id)
        {
            return _context.ClickIt.Any(e => e.Id == id);
        }
    }
}
