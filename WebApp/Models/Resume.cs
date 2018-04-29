using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WebApp.Models
{
    public class Resume
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int ResumeId { get; set; }

        [StringLength(50, ErrorMessage = "Name cannot be longer that 50 characters")]
        public string Name { get; set; }

        [Display(Name = "Birth date")]
        [DataType(DataType.Date)]
        public DateTime DateOfBirth { get; set; }

        public string Picture { get; set; }

        [StringLength(150, ErrorMessage = "Address cannot be longer that 150 characters")]
        public string Address { get; set; }

        [Display(Name = "E-mail")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Display(Name = "Phone number")]
        [DataType(DataType.PhoneNumber)]
        public string Phone { get; set; }

        [Display(Name = "Entry added")]
        [DataType(DataType.Date)]
        public static DateTime EntryAdded { get; set; }

        public int Age { get
            {
                int age = DateTime.Now.Year - DateOfBirth.Year;
                int m = DateTime.Now.Month - DateOfBirth.Month;
                if(m < 0 || (m == 0 && DateTime.Now.Day < DateOfBirth.Day))
                {
                    age--;
                }
                return age;
            }
        }


        public List<Job> Jobs { get; set; } = new List<Job>();

        public List<Academic> Academics { get; set; } = new List<Academic>();

        public List<Additional> Additionals { get; set; } = new List<Additional>();
    }
}
