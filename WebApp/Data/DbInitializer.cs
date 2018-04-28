using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApp.Models;
using WebApp.Models.Enums;

namespace WebApp.Data
{
    public class DbInitializer
    {
        public static void Initialize(ResumeContext context)
        {
            context.Database.EnsureCreated();

            if (context.Resumes.Any())
            {
                return; // Db has been seeded
            }

            var resumes = new Resume[]
            {
                new Resume{ResumeId=1, Name="Mihkel Tiganik", Address = "Sitsi 15-6, 10313 Tallinn",DateOfBirth = DateTime.Parse("01.10.1989"), Email="mtiganik@gmail.com", Phone="+372 55655828", Picture="mihkel.jpg"},
                new Resume{ResumeId=2, Name="Aldiyar Ganibek", Address="Red Street 80-5, Dabur, North-Elbonia", DateOfBirth=DateTime.Parse("05.07.1976"), Email="Aldiyar123@yahoo.com", Phone="+996 673 3485", Picture="daliban.jpg"}
            };

            foreach (Resume r in resumes)
            {
                context.Add(r);
            }


            var jobs = new Job[]
            {
                new Job{ResumeId=1, CompanyName="AS Metrosert", AdditionalInformation=@"Metrologist for electrical and frequency devices. Work task was to calibrate RF analyzers, signal generators, oscilloscopes, DMM’s etc", StartDate=DateTime.Parse("08-2015"), EndDate=DateTime.Parse("04-2017")},
                new Job{ResumeId=1, CompanyName="Ericsson Eesti", AdditionalInformation=@"RF Test Development Engineer. Working with test stations (TX and RX radio units) and develop new test SW (C#)", StartDate=DateTime.Parse("06-2017"), EndDate=DateTime.Parse("11-2017")},
                new Job{ResumeId=1, CompanyName="BEST-Estonia", AdditionalInformation=@"IT-coordinator. Maintenance and development of the organization IT systems", StartDate=DateTime.Parse("31.05.2014"), EndDate=DateTime.Parse("05-2015")},
                new Job{ResumeId=2, CompanyName="Elbonin Airlines", AdditionalInformation=@"In-Flight security officer. Task was to find any suspicious items that passangers would like to bring to Elbonia", StartDate=DateTime.Parse("06-2005"), EndDate=DateTime.Parse("11-2012")},
                new Job{ResumeId=2, CompanyName="The Factory", AdditionalInformation=@"Project Manager for Gruntmaster 6000. We didn't use any babies in production. Information published that is contrary to this statement is categorically false", StartDate=DateTime.Parse("02-2013"), EndDate=DateTime.Parse("06-2015")},
                new Job{ResumeId=2, CompanyName="Parliament of Northern Elbonia", AdditionalInformation=@"Now that our mighty Republic of North-Elbonia has been separated from dictatorial regime, I work for new Goverement", StartDate=DateTime.Parse("07-2015"), EndDate=DateTime.Parse("03-2018")}

            };

            foreach (Job j in jobs)
            {
                context.Add(j);
            }


            var academics = new Academic[]
            {
                new Academic{ResumeId=1, StudyType= StudyType.Bachelor, StudyName="Engineering Physics", StartDate=DateTime.Parse("09-2010"), EndDate=DateTime.Parse("06-2013"), School="Tallinn University of Technology"},
                new Academic{ResumeId=1, StudyType=StudyType.Master, StudyName="Biomedical Engineering", StartDate=DateTime.Parse("09-2013"), EndDate=DateTime.Parse("06-2015"), School="Tallinn University of Technology"},
                new Academic{ResumeId=2, StudyType=StudyType.Bachelor, StudyName="Public Relations",  StartDate=DateTime.Parse("09-2002"), EndDate=DateTime.Parse("06-2005"), School="University of North-Elbonia"}
            };

            foreach (Academic a in academics)
            {
                context.Add(a);
            }

            var additionals = new Additional[]
            {
                new Additional{ResumeId=1, AdditionalTitle="Programming languages", AdditionalValue="C#, HTML, CSS, JS, Java, PHP, SQL"},
                new Additional{ResumeId=1, AdditionalTitle="Computer Programs", AdditionalValue="Visual Studio, EF, MVC, VSTS, Git, Eclipse, Wamp"},
                new Additional{ResumeId=1, AdditionalTitle="Hobbies", AdditionalValue="Jogging, gym, health, travel"},
                new Additional{ResumeId=1, AdditionalTitle="Driving license", AdditionalValue="B from 2009"},
                new Additional{ResumeId=2, AdditionalTitle="Gruntmaster 6000", AdditionalValue="Certificate to officially unmount it"},
                new Additional{ResumeId=2, AdditionalTitle="Weapons permit", AdditionalValue="Permit to buy AK-47, AKM and Glock-17"},

            };

            foreach (Additional a in additionals)
            {
                context.Add(a);
            }

            context.SaveChanges();
        }
    }
}
