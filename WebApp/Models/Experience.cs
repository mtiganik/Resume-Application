using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebApp.Models
{
    public class Experience
    {
        [Key]
        public int ID { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "Start Date")]
        public DateTime StartDate { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "End Date")]
        public DateTime EndDate { get; set; }

        // If date was long time ago, then only show year. Otherwise show month and year
        [Display(Name = "Time")]
        public string StartToEndDate
        {
            get
            {
                if (DateTime.Now.Year - EndDate.Year > 2)
                {
                    return StartDate.ToString("yyyy") + " - " + EndDate.ToString("yyyy");
                }

                if (DateTime.Now.Year - StartDate.Year > 2)
                {
                    return StartDate.ToString("yyyy") + " - " + EndDate.ToString("MM.yyyy");
                }
                else
                {
                    return StartDate.ToString("MM.yyyy") + " - " + EndDate.ToString("MM.yyyy");
                }
            }
        }

        public int ResumeId { get; set; }

        public Resume Resume { get; set; }


    }
}
