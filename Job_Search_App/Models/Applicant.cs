﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Job_Search_App.Models
{
    public class Applicant
    {
        public int Id { get; set; }
        public User User { get; set; }
        public Job Job { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
