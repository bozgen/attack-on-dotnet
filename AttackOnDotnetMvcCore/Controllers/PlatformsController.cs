using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using AttackOnDotnetMvcCore.Data;
using AttackOnDotnetMvcCore.Models;

namespace AttackOnDotnetMvcCore.Controllers
{
    public class PlatformsController : Controller
    {
        private readonly AttackOnDotnetMvcCoreContext _context;

        public PlatformsController(AttackOnDotnetMvcCoreContext context)
        {
            _context = context;
        }

        // GET: Platforms
        public async Task<IActionResult> Index()
        {
              return View(await _context.Platform.ToListAsync());
        }

        // GET: Platforms/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Platform == null)
            {
                return NotFound();
            }

            var platform = await _context.Platform
                .FirstOrDefaultAsync(m => m.ID == id);
            if (platform == null)
            {
                return NotFound();
            }

            return View(platform);
        }

        // GET: Platforms/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Platforms/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,TechniqueID,Name")] Platform platform)
        {
            if (ModelState.IsValid)
            {
                _context.Add(platform);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(platform);
        }

        // GET: Platforms/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Platform == null)
            {
                return NotFound();
            }

            var platform = await _context.Platform.FindAsync(id);
            if (platform == null)
            {
                return NotFound();
            }
            return View(platform);
        }

        // POST: Platforms/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,TechniqueID,Name")] Platform platform)
        {
            if (id != platform.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(platform);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PlatformExists(platform.ID))
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
            return View(platform);
        }

        // GET: Platforms/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Platform == null)
            {
                return NotFound();
            }

            var platform = await _context.Platform
                .FirstOrDefaultAsync(m => m.ID == id);
            if (platform == null)
            {
                return NotFound();
            }

            return View(platform);
        }

        // POST: Platforms/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Platform == null)
            {
                return Problem("Entity set 'AttackOnDotnetMvcCoreContext.Platform'  is null.");
            }
            var platform = await _context.Platform.FindAsync(id);
            if (platform != null)
            {
                _context.Platform.Remove(platform);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PlatformExists(int id)
        {
          return _context.Platform.Any(e => e.ID == id);
        }
    }
}
