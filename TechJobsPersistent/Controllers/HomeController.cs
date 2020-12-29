using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using TechJobsPersistent.Models;
using TechJobsPersistent.ViewModels;
using TechJobsPersistent.Data;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace TechJobsPersistent.Controllers
{
    public class HomeController : Controller
    {
        private JobDbContext context;

        public HomeController(JobDbContext dbContext)
        {
            context = dbContext;
        }

        public IActionResult Index()
        {
            List<Job> jobs = context.Jobs.Include(j => j.Employer).ToList();

            return View(jobs);
        }

        public IActionResult AddJob()
        {
            List<Employer> newEmployers = context.Employers.ToList();
            List<Skill> newSkills = context.Skills.ToList();
            AddJobViewModel addJobViewModel = new AddJobViewModel(newEmployers, newSkills);
            return View(addJobViewModel);
        }

        [HttpPost]
        public IActionResult ProcessAddJobForm(AddJobViewModel addJobViewModel, string[] selectedSkills)
        {
            if (ModelState.IsValid)
            {
                Employer newEmployers = context.Employers.Find(addJobViewModel.EmployerId);
                Job job = new Job
                    {
                        Name = addJobViewModel.Name,
                        EmployerId = addJobViewModel.EmployerId,
                        Employer = newEmployers
                    };

                    foreach (string skill in selectedSkills)
                    {
                        JobSkill jobSkill = new JobSkill
                        {
                            SkillId = int.Parse(skill),
                            Job = job
                        };
                        context.JobSkills.Add(jobSkill);
                    }

                context.Jobs.Add(job);
                context.SaveChanges();
                return Redirect("/Home/Index");
            }
            return View(addJobViewModel);
        }

        public IActionResult Detail(int id)
        {
            Job theJob = context.Jobs
                .Include(j => j.Employer)
                .Single(j => j.Id == id);

            List<JobSkill> jobSkills = context.JobSkills
                .Where(js => js.JobId == id)
                .Include(js => js.Skill)
                .ToList();

            JobDetailViewModel viewModel = new JobDetailViewModel(theJob, jobSkills);
            return View(viewModel);
        }
    }
}
