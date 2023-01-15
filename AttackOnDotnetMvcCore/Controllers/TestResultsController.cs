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
using Microsoft.Extensions.Configuration.UserSecrets;
using System.Security.Claims;

namespace AttackOnDotnetMvcCore.Controllers
{
    [Authorize(Roles = "user,admin")]
    public class TestResultsController : Controller
    {
        private readonly AttackOnDotnetMvcCoreContext _context;

        public TestResultsController(AttackOnDotnetMvcCoreContext context)
        {
            _context = context;
        }

        // GET: TestResults
        public async Task<IActionResult> Index()
        {
            string UserID = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            var results = await _context.TestResult.Where(r => r.UserID == UserID).OrderByDescending(r=>r.TestDate).ToListAsync();
            var tests = await _context.Test.ToListAsync();
            return View(Tuple.Create(results, tests));
        }

        // GET: TestResults/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.TestResult == null)
            {
                return NotFound();
            }
            string UserID = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            var testResult = await _context.TestResult
                .FirstOrDefaultAsync(m => m.ID == id && m.UserID == UserID);
            if (testResult == null)
            {
                return NotFound();
            }
            var test = _context.Test.Where(t => t.ID == testResult.TestID).First();
            return View(Tuple.Create(testResult, test));
        }

        // GET: TestResults/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.TestResult == null)
            {
                return NotFound();
            }

            var testResult = await _context.TestResult
                .FirstOrDefaultAsync(m => m.ID == id);
            if (testResult == null)
            {
                return NotFound();
            }

            return View(testResult);
        }

        // POST: TestResults/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.TestResult == null)
            {
                return Problem("Entity set 'AttackOnDotnetMvcCoreContext.TestResult'  is null.");
            }
            var testResult = await _context.TestResult.FindAsync(id);
            if (testResult != null)
            {
                _context.TestResult.Remove(testResult);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Secure(int id)
        {
            var test = _context.Test.Where(t => t.ID == id).First();
            return View(test);
        }
        public IActionResult Vulnerable(int id)
        {
            var test = _context.Test.Where(t => t.ID == id).First();
            return View(test);
        }
        public IActionResult TestFailed()
        {
            return View();
        }

        public IActionResult NotImplemented()
        {
            return View();
        }

        private bool TestResultExists(int id)
        {
          return _context.TestResult.Any(e => e.ID == id);
        }
    }
}
