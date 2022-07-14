using Job_Search_App.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Job_Search_App.ViewModels
{
    public class DashboardViewModel
    {

        public Job jobInfo { get; set; }

        public string Title { get; set; }

        [Required, Display(Name = "Job Description", Prompt = "Job Description"),]
        
            [DataType(DataType.MultilineText)]
        public string Description { get; set; }
        [Required, Display(Name = "Location", Prompt = "Location")]
        public string Location { get; set; }
        [Required(ErrorMessage = "Type is required"), Display(Name = "Type", Prompt = "Type")]
        public string Type { get; set; }
        [Required, Display(Name = "Last Date", Prompt = "Last Date")]
        public DateTime LastDate { get; set; }
        [Required, Display(Name = "Company Name", Prompt = "Company Name")]
        public string CompanyName { get; set; }
        [Required, Display(Name = "Company Description", Prompt = "Company Description")]
        public string CompanyDescription { get; set; }
        [Display(Name = "Website", Prompt = "Website")]
        [Url]
        public string Website { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public bool Filled { get; set; } = false;
        public User User { get; set; }
        public List<Applicant> Applicants { get; set; }
    }
}
