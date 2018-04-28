using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebApp.Data;
using WebApp.Models;
using WebApp.Models.ViewModel;

namespace WebApp.Controllers
{
    public class ResumesController : Controller
    {
        private readonly ResumeContext _context;

        public ResumesController(ResumeContext context)
        {
            _context = context;
        }


        // GET: Resumes/Details/5
        public async Task<IActionResult> Index(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("Index", "Home");
            }
            var viewModel = new ResumeViewModel();

            viewModel.Resume = await _context.Resumes
                .Include(i => i.Jobs)
                .Include(i => i.Academics)
                .Include(i => i.Additionals)
                .SingleOrDefaultAsync(m => m.ResumeId == id);
            if (viewModel == null)
            {
                return NotFound();
            }

            return View(viewModel);
        }

        // GET: Resumes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Resumes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Name,DateOfBirth,Address,Email,Phone")] Resume resume)
        {
            if (ModelState.IsValid)
            {
                resume.ResumeId = GetNewId();
                resume.Picture = "noPic.jpg";
                _context.Add(resume);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(resume);
        }


        // GET: Resumes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var resume = await _context.Resumes.SingleOrDefaultAsync(m => m.ResumeId == id);
            if (resume == null)
            {
                return NotFound();
            }
            return View(resume);
        }

        // POST: Resumes/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ResumeId,Name,DateOfBirth,Address,Picture,Email,Phone")] Resume resume)
        {
            if (id != resume.ResumeId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(resume);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ResumeExists(resume.ResumeId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction("Index", "Resumes", new { id = resume.ResumeId });
            }
            return View(resume);
        }

        // GET: Resumes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var resume = await _context.Resumes
                .SingleOrDefaultAsync(m => m.ResumeId == id);
            if (resume == null)
            {
                return NotFound();
            }

            return View(resume);
        }

        // POST: Resumes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var resume = await _context.Resumes.SingleOrDefaultAsync(m => m.ResumeId == id);
            _context.Resumes.Remove(resume);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateAcademic(int id, [Bind("StudyType,StudyName,School,StartDate,EndDate")] Academic academic)
        {
            if (ModelState.IsValid)
            {
                academic.ResumeId = id;
                _context.Add(academic);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index", "Resumes", new { id = academic.ResumeId });
            }
            return View(academic);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateAdditional(int id, [Bind("AdditionalTitle, AdditionalValue")]Additional additional)
        {
            if (ModelState.IsValid)
            {
                additional.ResumeId = id;
                _context.Add(additional);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index", "Resumes", new { id = additional.ResumeId });
            }
            return View(additional);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateJob(int id, [Bind("CompanyName, AdditionalInformation, StartDate, EndDate")]Job job)
        {
            if (ModelState.IsValid)
            {
                job.ResumeId = id;
                _context.Add(job);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index", "Resumes", new { id = job.ResumeId });
            }
            return View(job);
        }

        [HttpPost]
        public async Task<IActionResult> UploadFile(IFormFile file, int ResumeId)
        {
            if(file.Length /1024 / 1024 > 5)
            {
                throw new Exception("Pictures can't be larger than 5 MB");
            }
            if (file == null || file.Length == 0)
                return Content("file not selected");

            var path = Path.Combine(
                        Directory.GetCurrentDirectory(), "wwwroot/images",
                        file.FileName);

            using (var stream = new FileStream(path, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            var resume = _context.Resumes.SingleOrDefault(x => x.ResumeId == ResumeId);
            resume.Picture = file.FileName;
            try
            {
                _context.Update(resume);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ResumeExists(resume.ResumeId))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToAction("Index", "Resumes", new { id = ResumeId });
        }


        private int GetNewId()
        {
            return _context.Resumes.OrderByDescending(r => r.ResumeId).FirstOrDefault().ResumeId + 1;
        }

        private bool ResumeExists(int id)
        {
            return _context.Resumes.Any(e => e.ResumeId == id);
        }
    }
}
