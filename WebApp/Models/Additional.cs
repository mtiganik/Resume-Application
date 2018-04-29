using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebApp.Models
{
    public class Additional
    {
        public int AdditionalId { get; set; }

        [Required]
        [Display(Name = "Title")]
        [MinLength(3, ErrorMessage = "Title must be 3 - 40 characters long")]
        [MaxLength(40, ErrorMessage = "Title must be 3 - 40 characters long")]
        public string AdditionalTitle { get; set; }

        [Display(Name = "Description")]
        [MinLength(3, ErrorMessage = "Description must be 3 - 70 characters long")]
        [MaxLength(70, ErrorMessage = "Description must be 3 - 70 characters long")]
        public string AdditionalValue { get; set; }

        public int ResumeId { get; set; }

        public Resume Resume { get; set; }
    }
}
