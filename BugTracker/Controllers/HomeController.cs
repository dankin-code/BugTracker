using BugTracker.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BugTracker.Controllers
{
    public class HomeController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        [Authorize]
        public ActionResult Index()
        {
            return View(db.Tickets.ToList());
        }

        [Authorize]
        public ActionResult Dashboard()
        {

            var model = new DashboardViewModel();

            model.Ticket = db.Tickets.ToList();
            model.Project = db.Projects.ToList();

            //restrict uses to seeing multipple things
            model.ApplicationUser = db.Users.ToList();

            return View(model);
        }

    }
}