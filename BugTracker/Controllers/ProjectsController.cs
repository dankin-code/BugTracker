using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using BugTracker.Models;

namespace BugTracker.Controllers
{
    public class ProjectsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Projects
        public ActionResult Dashboard()
        {
            var projects = db.Projects.Include(p => p.Developer).Include(p => p.ProjectManager);
            return View(projects.ToList());
        }

        // GET: Projects
        [Authorize(Roles = "Admin,Project Manager,Developer")]
        public ActionResult Index()
        {
            List<Project> projects = new List<Project>();
            var userId = db.Users.FirstOrDefault(u => u.Email == User.Identity.Name).Id;

            // List projects according to roles.
            // Administrators, Project Managers, and Developers must be able to view a listing of all existing projects

            if (User.IsInRole("Admin, ProjectManager"))
            {
                projects = db.Projects.ToList();
            }
           
            // The list for Project Managers and Developers must be limited to the Projects which they are assigned.
            else if (User.IsInRole("Developer"))
            {
                projects = db.Projects.Where(p => p.Developers.Any(dev => dev.Id == userId)).Include(pm => pm.ProjectManager).ToList();
            }

            // submitters are not allowed to view projects. Submitters will be redirected to the Dashboard
            else if (User.IsInRole("Submitter"))
            {
                return RedirectToAction("Dashboard", "Home");
            }


            // Administrators and Project Managers must be able to assign or unassign users to and from projects.

            //var 


            // send the list to the view
            return View(projects);
        }

        // GET: Projects/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Project project = db.Projects.Find(id);
            if (project == null)
            {
                return HttpNotFound();
            }
            return View(project);
        }

        // GET: Projects/Create
        [Authorize(Roles = "Admin,Project Manager")]
        public ActionResult Create()
        {
            ViewBag.DeveloperId = new SelectList(db.Users, "Id", "FirstName");
            ViewBag.ProjectManagerId = new SelectList(db.Users, "Id", "FirstName");
            return View();
        }

        // POST: Projects/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin,Project Manager")]
        public ActionResult Create([Bind(Include = "Id,ProjectName,ProjectManagerId,DeveloperId")] Project project)
        {
            if (ModelState.IsValid)
            {
                db.Projects.Add(project);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.DeveloperId = new SelectList(db.Users, "Id", "FirstName", project.DeveloperId);
            ViewBag.ProjectManagerId = new SelectList(db.Users, "Id", "FirstName", project.ProjectManagerId);
            return View(project);
        }

        // GET: Projects/Edit/5
        [Authorize(Roles = "Admin,Project Manager")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Project project = db.Projects.Find(id);
            if (project == null)
            {
                return HttpNotFound();
            }
            ViewBag.DeveloperId = new SelectList(db.Users, "Id", "FirstName", project.DeveloperId);
            ViewBag.ProjectManagerId = new SelectList(db.Users, "Id", "FirstName", project.ProjectManagerId);
            return View(project);
        }

        // POST: Projects/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin,Project Manager")]
        public ActionResult Edit([Bind(Include = "Id,ProjectName,ProjectManagerId,DeveloperId")] Project project)
        {
            if (ModelState.IsValid)
            {
                db.Entry(project).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.DeveloperId = new SelectList(db.Users, "Id", "FirstName", project.DeveloperId);
            ViewBag.ProjectManagerId = new SelectList(db.Users, "Id", "FirstName", project.ProjectManagerId);
            return View(project);
        }

        // GET: Projects/Delete/5
        [Authorize(Roles = "Admin,Project Manager")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Project project = db.Projects.Find(id);
            if (project == null)
            {
                return HttpNotFound();
            }
            return View(project);
        }

        // POST: Projects/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin,Project Manager")]
        public ActionResult DeleteConfirmed(int id)
        {
            Project project = db.Projects.Find(id);
            db.Projects.Remove(project);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
