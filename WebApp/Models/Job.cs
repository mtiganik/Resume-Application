using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebApp.Models
{
    public class Job : Experience
    {

        public string CompanyName { get; set; }

        public string AdditionalInformation { get; set; }


    }
}
