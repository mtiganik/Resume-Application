using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebApp.Data;
using WebApp.Models;

namespace WebApp.Controllers
{
    public class AcademicsController : Controller
    {
        private readonly ResumeContext _context;

        public AcademicsController(ResumeContext context)
        {
            _context = context;
        }

        // GET: Academics
        public IActionResult Index()
        {
            return RedirectToAction("Index", "Home");
        }



        // GET: Academics/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var academic = await _context.Academics
                .Include(m => m.Resume)
                .SingleOrDefaultAsync(m => m.ID == id);
            if (academic == null)
            {
                return NotFound();
            }
            return View(academic);
        }

        // POST: Academics/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("StudyType,StudyName,School,ID,StartDate,EndDate,ResumeId")] Academic academic)
        {
            if (id != academic.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(academic);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AcademicExists(academic.ID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction("Index", "Resumes", new { id = academic.ResumeId });
            }
            return View(academic);
        }

        // GET: Academics/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var academic = await _context.Academics
                .Include(a => a.Resume)
                .SingleOrDefaultAsync(m => m.ID == id);
            if (academic == null)
            {
                return NotFound();
            }

            return View(academic);
        }

        // POST: Academics/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            int returnPath = await GetResumeIdAsync(id);
            var academic = await _context.Academics.SingleOrDefaultAsync(m => m.ID == id);
            _context.Academics.Remove(academic);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index", "Resumes", new { id = returnPath });
        }

        private bool AcademicExists(int id)
        {
            return _context.Academics.Any(e => e.ID == id);
        }

        private async Task<int> GetResumeIdAsync(int id)
        {
            var academic = await _context.Academics.SingleOrDefaultAsync(m => m.ID == id);
            return academic.ResumeId;

        }
    }
}
