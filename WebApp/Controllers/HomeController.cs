using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;
using WebApp.Data;
using WebApp.Models;

namespace WebApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ResumeContext _context;

        public HomeController(ResumeContext context)
        {
            _context = context;
        }

        public async Task<ActionResult> Index()
        {
            return View(await _context.Resumes.ToListAsync());
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Resume Application.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Contact.";

            return View();
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}

