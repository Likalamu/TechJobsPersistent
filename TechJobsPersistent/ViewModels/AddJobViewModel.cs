using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TechJobsPersistent.Models;

namespace TechJobsPersistent.ViewModels
{
    public class AddJobViewModel
    {
        private List<JobSkill> jobSkills;

        public string Name { get; set; }
        public int EmployerId { get; set; }
        public string Description { get; set; }

        public List<SelectListItem> Jobs { get; set; }

        public Skill Skill { get; set; }

        public AddJobViewModel(List<Skill> possibleJobs, Skill theSkill)
        {
            Jobs = new List<SelectListItem>();

            foreach (var job in possibleJobs)
            {
                Jobs.Add(
                    new SelectListItem
                    {
                        Value = job.Id.ToString(),
                        Text = job.Name
                    }
                ); ;
            }

            Skill = theSkill;
        }

        public AddJobViewModel()
        {
        }

        public AddJobViewModel(List<JobSkill> jobSkills)
        {
            this.jobSkills = jobSkills;
        }
    }
}
