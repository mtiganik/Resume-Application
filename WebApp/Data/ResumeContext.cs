using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApp.Models;

namespace WebApp.Data
{
    public class ResumeContext :DbContext
    {
        public ResumeContext(DbContextOptions<ResumeContext> options) : base(options)
        {

        }

        public DbSet<Resume> Resumes { get; set; }
        public DbSet<Job> Jobs { get; set; }
        public DbSet<Academic> Academics { get; set; }
        public DbSet<Additional> Additionals { get; set; }
        public DbSet<Experience> Experiences { get; set; }

    }
}
