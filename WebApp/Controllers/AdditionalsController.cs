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
    public class AdditionalsController : Controller
    {
        private readonly ResumeContext _context;

        public AdditionalsController(ResumeContext context)
        {
            _context = context;
        }

        // GET: Additionals
        public IActionResult Index()
        {
            return RedirectToAction("Index", "Home");
        }

        // GET: Additionals/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var additional = await _context.Additionals
                .Include(a => a.Resume)
                .SingleOrDefaultAsync(m => m.AdditionalId == id);
            if (additional == null)
            {
                return NotFound();
            }
            return View(additional);
        }

        // POST: Additionals/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("AdditionalId,AdditionalTitle,AdditionalValue,ResumeId")] Additional additional)
        {
            if (id != additional.AdditionalId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(additional);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AdditionalExists(additional.AdditionalId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction("Index", "Resumes", new { id = additional.ResumeId });
            }
            return View(additional);
        }

        // GET: Additionals/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var additional = await _context.Additionals
                .Include(a => a.Resume)
                .SingleOrDefaultAsync(m => m.AdditionalId == id);
            if (additional == null)
            {
                return NotFound();
            }
            ViewData["Resume"] = additional.Resume.Name;
            return View(additional);
        }

        // POST: Additionals/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {

            int returnPath = await GetResumeIdAsync(id);
            var additional = await _context.Additionals.SingleOrDefaultAsync(m => m.AdditionalId == id);
            _context.Additionals.Remove(additional);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index", "Resumes", new { id = returnPath });
        }

        private bool AdditionalExists(int id)
        {
            return _context.Additionals.Any(e => e.AdditionalId == id);
        }

        
        private async Task<int> GetResumeIdAsync(int id)
        {
            var additional = await _context.Additionals.SingleOrDefaultAsync(m => m.AdditionalId == id);
            return additional.ResumeId;
        }
                
    }
}
