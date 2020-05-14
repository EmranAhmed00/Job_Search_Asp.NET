using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Job_Search_App.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Job_Search_App.ViewModels;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Job_Search_App.Controllers
{
    public class UpdateDashboardController : Controller
    {

        private readonly ILogger _logger;
        private readonly ApplicationDbContext _context;
        private UserManager<User> _userManager;

        public UpdateDashboardController(ApplicationDbContext context, UserManager<User> userManager, ILogger<JobController> logger)
        {
            _logger = logger;
            _context = context;
            _userManager = userManager;
        }


        //// Edit Dashboard

        [HttpGet]
        [Route("employer/jobs/{id}/edit-dashboard")]

        [Authorize(Roles = "Employer")]

        public IActionResult EditDashboard(int id)

        {
            var model = new DashboardViewModel();
            model.jobInfo = _context.Jobs.SingleOrDefault(k => k.Id == id);
            return View(model);
        }

        [HttpPost]
        [Authorize(Roles = "Employer")]
        [Route("employer/jobs/{id}/edit-dashboard")]

        [ValidateAntiForgeryToken]
        public IActionResult EditDashboard(DashboardViewModel model, int id)

        {

            var newCus = _context.Jobs.SingleOrDefault(k => k.Id == id);

            if (model.jobInfo == null)
            {
                model.jobInfo = newCus;
            }
            else
            {
                newCus.Title = model.jobInfo.Title;
                newCus.Type = model.jobInfo.Type;

                newCus.Description = model.jobInfo.Description;
                newCus.CompanyName = model.jobInfo.CompanyName;
                newCus.CompanyDescription = model.jobInfo.CompanyDescription;

                newCus.Location = model.jobInfo.Location;
                newCus.Filled = model.Filled;
                newCus.LastDate = model.LastDate;
                _context.SaveChanges();

                return RedirectToAction("updateDashboardSuccess", "UpdateDashboard");

            }

            return View(model);

        }

        //updateSuccess

        [HttpGet]
        public IActionResult updateDashboardSuccess()
        {
            return View();
        }
    }
}
