using Job_Search_App.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Job_Search_App.ViewModels.Home
{
    public class JobDetailsViewModel
    {
        public Job Job { get; set; }
        public bool IsApplied { get; set; }
    }
}

