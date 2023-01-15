using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using AttackOnDotnetMvcCore.Data;
using AttackOnDotnetMvcCore.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;
using static System.Net.Mime.MediaTypeNames;
using AttackOnDotnetMvcCore.Components;
using Microsoft.CodeAnalysis;
using Technique = AttackOnDotnetMvcCore.Models.Technique;
using Platform = AttackOnDotnetMvcCore.Models.Platform;
using System.Security.Claims;

namespace AttackOnDotnetMvcCore.Controllers
{
    [Authorize(Roles = "user,admin")]
    public class TestsController : Controller
    {
        private readonly AttackOnDotnetMvcCoreContext _context;

        public TestsController(AttackOnDotnetMvcCoreContext context)
        {
            _context = context;
        }

        // GET: Tests
        [Authorize(Roles = "user,admin")]
        public async Task<IActionResult> Index()
        {
            var techniques = await _context.Technique.ToListAsync();
            var tests = await _context.Test.ToListAsync();
            var platforms = await _context.Platform.GroupBy(g => g.Name).Select(p => p.First()).ToListAsync();
            return View(Tuple.Create(tests, techniques, platforms));
        }

        // GET: Tests/Details/5
        [Authorize(Roles = "user,admin")]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Test == null)
            {
                return NotFound();
            }

            var test = await _context.Test
                .FirstOrDefaultAsync(m => m.ID == id);
            if (test == null)
            {
                return NotFound();
            }
            var technique = _context.Technique.Where(t => t.ID == test.TechniqueID).FirstOrDefault();
            var platform = _context.Platform.Where(p => p.ID == test.PlatformID).FirstOrDefault();
            return View(Tuple.Create(test,technique,platform));
        }

        // GET: Tests/Create
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> Create()
        {
            List<Technique> techniques = await _context.Technique.ToListAsync();
            List<Platform> platforms = await _context.Platform.GroupBy(g => g.Name).Select(p => p.First()).ToListAsync();
            return View(Tuple.Create(new Test(), techniques, platforms));
        }

        // POST: Tests/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> Create([Bind(Prefix="Item1")] Test test)
        {
            if (ModelState.IsValid)
            {
                _context.Add(test);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            List<Technique> techniques = await _context.Technique.ToListAsync();
            List<Platform> platforms = await _context.Platform.GroupBy(g => g.Name).Select(p => p.First()).ToListAsync();
            return View(Tuple.Create(new Test(), techniques, platforms));
        }

        // GET: Tests/Edit/5
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Test == null)
            {
                return NotFound();
            }

            var test = await _context.Test.FindAsync(id);
            if (test == null)
            {
                return NotFound();
            }
            List<Technique> techniques = await _context.Technique.ToListAsync();
            List<Platform> platforms = await _context.Platform.GroupBy(g => g.Name).Select(p => p.First()).ToListAsync();
            return View(Tuple.Create(test, techniques, platforms));
        }

        // POST: Tests/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> Edit(int id, [Bind(Prefix = "Item1")] Test test)
        {
            if (id != test.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(test);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TestExists(test.ID))
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
            List<Technique> techniques = await _context.Technique.ToListAsync();
            List<Platform> platforms = await _context.Platform.GroupBy(g => g.Name).Select(p => p.First()).ToListAsync();
            return View(Tuple.Create(test, techniques, platforms));
        }

        // GET: Tests/Delete/5
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Test == null)
            {
                return NotFound();
            }

            var test = await _context.Test
                .FirstOrDefaultAsync(m => m.ID == id);
            if (test == null)
            {
                return NotFound();
            }

            return View(test);
        }

        // POST: Tests/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Test == null)
            {
                return Problem("Entity set 'AttackOnDotnetMvcCoreContext.Test'  is null.");
            }
            var test = await _context.Test.FindAsync(id);
            if (test != null)
            {
                _context.Test.Remove(test);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Permission(int id)
        {
            var test = _context.Test.Where(t => t.ID == id).First();
            return View(test);
        }

        public IActionResult Run(int id)
        {
            var test = _context.Test.Where(t => t.ID == id).First();
            Func<Test.TestResult> testFunction = Test.TestNotFound;
            string redirectAction = "";
            string redirectController = "";
            string redirectParam = "";
            TestResult testResultObject;
            
            if(test.TechniqueID == 1059)
            {
                if(test.Number == 8)
                {
                    testFunction = Test.RunTest1059_001_8;
                }
                else if(test.Number == 9)
                {
                    testFunction = Test.RunTest1059_001_9;
                }
                else
                {
                    return RedirectToAction("NotImplemented", "TestResults");
                }
            }
            else if(test.TechniqueID == 1202)
            {
                if(test.Number == 1)
                {
                    testFunction = Test.RunTest1202_000_1;
                }
                else if(test.Number == 3)
                {
                    testFunction = Test.RunTest1202_000_3;
                }
                else
                {
                    return RedirectToAction("NotImplemented", "TestResults");
                }
            }
            else
            {
                testFunction = Test.TestNotFound;
                return NotFound();
            }
            
            var result = testFunction.Invoke();
            testResultObject = new TestResult()
            {
                TestDate = DateTime.Now,
                TestID = test.ID,
                UserID = User.FindFirst(ClaimTypes.NameIdentifier).Value,
            };

            if (result == Test.TestResult.Secure)
            {
                testResultObject.Result = true;
                redirectAction = "Secure";
                redirectController = "TestResults";
                redirectParam = test.ID.ToString();
            }
            else if (result == Test.TestResult.Vulnerable)
            {
                testResultObject.Result = false;
                redirectAction = "Vulnerable";
                redirectController = "TestResults";
                redirectParam = test.ID.ToString();
            }
            else if (result == Test.TestResult.TestFailed)
            {
                redirectAction = "TestFailed";
                redirectController = "TestResults";
            }
            else if (result == Test.TestResult.NotImplemented)
            {
                redirectAction = "NotImplemented";
                redirectController = "TestResults";
            }
            else
            {
                return NotFound();
            }
            _context.TestResult.Add(testResultObject);
            _context.SaveChanges();
            return Redirect(string.Format("/{0}/{1}/{2}", redirectController, redirectAction, redirectParam));
        }

        private bool TestExists(int id)
        {
          return _context.Test.Any(e => e.ID == id);
        }
    }
}
