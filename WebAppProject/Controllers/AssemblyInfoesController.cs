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
    public class AssemblyInfoesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AssemblyInfoesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: AssemblyInfoes
        public async Task<IActionResult> Index()
        {
            return View(await _context.AssemblyInfo.ToListAsync());
        }

        // GET: AssemblyInfoes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var assemblyInfo = await _context.AssemblyInfo
                .FirstOrDefaultAsync(m => m.Id == id);
            if (assemblyInfo == null)
            {
                return NotFound();
            }

            return View(assemblyInfo);
        }

        // GET: AssemblyInfoes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: AssemblyInfoes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,JokeQuestion,JokeAnswer")] AssemblyInfo assemblyInfo)
        {
            if (ModelState.IsValid)
            {
                _context.Add(assemblyInfo);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(assemblyInfo);
        }

        // GET: AssemblyInfoes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var assemblyInfo = await _context.AssemblyInfo.FindAsync(id);
            if (assemblyInfo == null)
            {
                return NotFound();
            }
            return View(assemblyInfo);
        }

        // POST: AssemblyInfoes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,JokeQuestion,JokeAnswer")] AssemblyInfo assemblyInfo)
        {
            if (id != assemblyInfo.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(assemblyInfo);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AssemblyInfoExists(assemblyInfo.Id))
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
            return View(assemblyInfo);
        }

        // GET: AssemblyInfoes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var assemblyInfo = await _context.AssemblyInfo
                .FirstOrDefaultAsync(m => m.Id == id);
            if (assemblyInfo == null)
            {
                return NotFound();
            }

            return View(assemblyInfo);
        }

        // POST: AssemblyInfoes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var assemblyInfo = await _context.AssemblyInfo.FindAsync(id);
            _context.AssemblyInfo.Remove(assemblyInfo);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AssemblyInfoExists(int id)
        {
            return _context.AssemblyInfo.Any(e => e.Id == id);
        }
    }
}
