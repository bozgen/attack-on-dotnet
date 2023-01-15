using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using AttackOnDotnetMvcCore.Data;
using AttackOnDotnetMvcCore.Models;
using Microsoft.AspNetCore.Authorization;

namespace AttackOnDotnetMvcCore.Controllers
{
    public class TechniquesController : Controller
    {
        private readonly AttackOnDotnetMvcCoreContext _context;

        public TechniquesController(AttackOnDotnetMvcCoreContext context)
        {
            _context = context;
        }

        // GET: Techniques
        public async Task<IActionResult> Index()
        {
            List<Technique> techniques = await _context.Technique.ToListAsync();
            List<Tuple<Technique, List<string>>> resultList = new ();
            foreach(var technique in techniques)
            {
                List<Platform> platforms = await _context.Platform.Where(p => p.TechniqueID == technique.ID).ToListAsync();
                List<string> platformNames = new();
                foreach (var p in platforms)
                {
                    platformNames.Add(p.Name);
                }
                resultList.Add(Tuple.Create(technique, platformNames));
            }
            return View(resultList);
        }

        // GET: Techniques/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Technique == null)
            {
                return NotFound();
            }

            var technique = await _context.Technique
                .FirstOrDefaultAsync(m => m.ID == id);
            if (technique == null)
            {
                return NotFound();
            }
            
            List<Platform> platforms = await _context.Platform.Where(p => p.TechniqueID == technique.ID).ToListAsync();
            List<string> platformNames = new();
            foreach (var p in platforms)
            {
                platformNames.Add(p.Name);
            }
            return View(Tuple.Create(technique, platformNames));
        }

        // GET: Techniques/Create
        [Authorize(Roles = "admin")]
        public IActionResult Create()
        {
            return View();
        }

        // POST: Techniques/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> Create([Bind("ID,Name,TacticName,ShortDescription,LongDescription,Url")] Technique technique)
        {
            if (ModelState.IsValid)
            {
                _context.Add(technique);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(technique);
        }

        // GET: Techniques/Edit/5
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Technique == null)
            {
                return NotFound();
            }

            var technique = await _context.Technique.FindAsync(id);
            if (technique == null)
            {
                return NotFound();
            }
            return View(technique);
        }

        // POST: Techniques/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> Edit(int id, [Bind("ID,Name,TacticName,ShortDescription,LongDescription,Url")] Technique technique)
        {
            if (id != technique.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(technique);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TechniqueExists(technique.ID))
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
            return View(technique);
        }

        // GET: Techniques/Delete/5
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Technique == null)
            {
                return NotFound();
            }

            var technique = await _context.Technique
                .FirstOrDefaultAsync(m => m.ID == id);
            if (technique == null)
            {
                return NotFound();
            }

            return View(technique);
        }

        // POST: Techniques/Delete/5
        [Authorize(Roles = "admin")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Technique == null)
            {
                return Problem("Entity set 'AttackOnDotnetMvcCoreContext.Technique'  is null.");
            }
            var technique = await _context.Technique.FindAsync(id);
            if (technique != null)
            {
                _context.Technique.Remove(technique);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TechniqueExists(int id)
        {
          return _context.Technique.Any(e => e.ID == id);
        }
    }
}
