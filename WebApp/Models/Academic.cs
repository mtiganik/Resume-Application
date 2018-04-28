using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using WebApp.Models.Enums;

namespace WebApp.Models
{
    public class Academic : Experience
    {
        [Display(Name = "Study Type")]
        public StudyType StudyType { get; set; }

        [Display(Name = "Name of your study")]
        [StringLength(50, ErrorMessage = "Study name cannot be longer that 50 characters")]
        public string StudyName { get; set; }

        [StringLength(50, ErrorMessage = "School name cannot be longer that 50 characters")]
        public string School { get; set; }
    }
}
