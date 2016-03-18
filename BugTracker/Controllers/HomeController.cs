using BugTracker.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BugTracker.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        public ActionResult Index()
        {
            return View(db.Tickets.ToList());
        }

        public ActionResult Dashboard()
        {

            var model = new DashboardViewModel();
            model.tickets = db.Tickets.ToList();
            model.projects = db.Projects.ToList();
            model.statuses = db.Statuses.ToList();
            model.priorities = db.Priorities.ToList();            

            return View();
        }

    }
}