﻿using System.Linq;
using System.Threading.Tasks;
using Job_Search_App.Models;
using Job_Search_App.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Job_Search_App.Controllers
{
    public class DashboardController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<User> _userManager;
        private readonly ILogger _logger;

        public DashboardController(ApplicationDbContext context, UserManager<User> userManager,
            ILogger<DashboardController> logger)
        {
            _context = context;
            _userManager = userManager;
            _logger = logger;
        }

        [Route("employer/dashboard")]
        [Authorize(Roles = "Employer")]
        public async Task<IActionResult> Index(int page = 1)
        {
            var user = await _userManager.GetUserAsync(HttpContext.User);
            var jobs = _context.Jobs.Where(x => x.User == user).Include(x => x.Applicants).ToList();

            return View(jobs);
        }

        [Route("employer/applicants")]
        public async Task<IActionResult> Applicants()
        {
            var user = await _userManager.GetUserAsync(HttpContext.User);
            var applicants = _context.Applicants.Where(x => x.Job.User == user).Include(x => x.User).Include(x => x.Job)
                .ToList();

            return View(applicants);
        }

        [Route("employer/jobs/{id}/applicants")]
        public async Task<IActionResult> ApplicantsByJob(int id)
        {
            var user = await _userManager.GetUserAsync(HttpContext.User);
            var job = _context.Jobs
                .Include(x => x.Applicants)
                    .ThenInclude(x => x.User)
                .SingleOrDefault(x => x.Id == id);
            var model = new JobApplicantsViewModel
            {
                Job = job,
                Applicants = job.Applicants
            };

            return View(model);
        }
    }
}