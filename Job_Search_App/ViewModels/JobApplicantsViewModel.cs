using System.Collections.Generic;
using Job_Search_App.Models;

namespace Job_Search_App.ViewModels
{
    public class JobApplicantsViewModel
    {
        public Job Job { get; set; }

        public List<Applicant> Applicants { get; set; }
    }
}
