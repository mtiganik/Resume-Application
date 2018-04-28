using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApp.Models.ViewModel
{
    public class ResumeViewModel
    {
        public Resume Resume { get; set; }
        public Academic Academics { get; set; }
        public Job Jobs { get; set; }
        public Additional Additionals { get; set; }
    }
}
