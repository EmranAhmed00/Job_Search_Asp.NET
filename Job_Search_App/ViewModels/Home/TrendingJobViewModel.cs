using Job_Search_App.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Job_Search_App.ViewModels.Home
{
    public class TrendingJobViewModel
    {
        public List<Job> Jobs { get; set; }
        public List<Job> Trendings { get; set; }
    }
}

