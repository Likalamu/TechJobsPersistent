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
        public string Name { get; set; }
        public int EmployerId { get; set; }
        public int JobId { get; set; }
        public string Description { get; set; }

        public List<Skill> Skills { get; set; }

        public List<SelectListItem> Employers { get; set; }

        public AddJobViewModel(List<Employer> newEmployers, List<Skill> allSkills)
        {
            Skills = allSkills;
            Employers = new List<SelectListItem>();

            foreach (var employer in newEmployers)
            {
                Employers.Add(
                    new SelectListItem
                    {
                        Value = employer.Id.ToString(),
                        Text = employer.Name
                    }
                ); ;
            }
        }


        public AddJobViewModel()
        {
        }
    }
}
