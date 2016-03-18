using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using BugTracker.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.IO;

namespace BugTracker.Controllers
{
    public class TicketsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        private UserManager<ApplicationUser> userManager;
        private RoleManager<IdentityRole> roleManager;

        public TicketsController()
        {

        }

        // GET: Tickets
        [Authorize(Roles="Admin, Submitter, Developer, Project Manager")]
        public ActionResult Index()
        {
            var tickets = db.Tickets.Include(t => t.Project);
            return View(tickets.ToList());
        }

        // GET: Tickets/Details/5
        [Authorize(Roles = "Admin, Submitter, Developer, Project Manager")]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Ticket ticket = db.Tickets.Find(id);
            if (ticket == null)
            {
                return HttpNotFound();
            }
            return View(ticket);
        }

        // GET: Tickets/Create
        [Authorize(Roles = "Admin, Submitter, Developer, Project Manager")]
        public ActionResult Create()
        {
            // prepopulate dropdownlist for StatusId
            ViewBag.StatusId = new SelectList(db.Statuses, "Id", "TicketStatus");

            // prepopulate dropdownlist for PriorityId
            ViewBag.PriorityId = new SelectList(db.Priorities, "Id", "TicketPriority");

            // prepopulate dropdownlist for TypeId
            ViewBag.TicketTypeId = new SelectList(db.TicketTypes, "Id", "TicketTypeName");

            // prepopulate dropdownlist with available Developers
            ViewBag.AssignedToId = new SelectList(db.Users, "Id", "FirstName");
            
            // prepopulate dropdownlist with available Projects
            ViewBag.ProjectId = new SelectList(db.Projects, "Id", "ProjectName");
            return View();
        }

        // POST: Tickets/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin, Submitter, Project Manager, Developer")]
        public ActionResult Create( Ticket ticket, HttpPostedFileBase Attachment)
        {
            if (ModelState.IsValid)
            {
                ticket.CreateDate = new DateTimeOffset(DateTime.Now);
                ticket.CreateById = (db.Users.FirstOrDefault(u => u.UserName == User.Identity.Name).Id);
                if (Attachment.ContentLength > 0)
                {
                    var fileName = Path.GetFileName(Attachment.FileName);
                    Attachment.SaveAs(Path.Combine(Server.MapPath("~/Attachments/"), fileName));
                    ticket.Attachment = "~/Attachments/" + fileName;
                }
                
                //need to add code to generate a notificationto assigned Developer

                db.Tickets.Add(ticket);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ProjectId = new SelectList(db.Projects, "Id", "ProjectName", ticket.ProjectId);
            return View(ticket);
        }

        // GET: Tickets/Edit/5
        [Authorize(Roles = "Admin, Project Manager")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Ticket ticket = db.Tickets.Find(id);
            if (ticket == null)
            {
                return HttpNotFound();
            }
            ViewBag.ProjectId = new SelectList(db.Projects, "Id", "ProjectName", ticket.ProjectId);
            return View(ticket);
        }

        // POST: Tickets/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin, Project Manager")]
        public ActionResult Edit([Bind(Include = "Id,CreateDate,UpdateDate,CreateById,AssingedToId,Title,Description,Attachment,ProjectId,StatusId,PriorityId,TicketTypeId,NotificationId")] Ticket ticket)
        {
            if (ModelState.IsValid)
            {
                db.Entry(ticket).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ProjectId = new SelectList(db.Projects, "Id", "ProjectName", ticket.ProjectId);
            return View(ticket);
        }

        // GET: Tickets/Delete/5
        [Authorize(Roles = "Admin, Project Manager")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Ticket ticket = db.Tickets.Find(id);
            if (ticket == null)
            {
                return HttpNotFound();
            }
            return View(ticket);
        }

        // POST: Tickets/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin, Project Manager")]
        public ActionResult DeleteConfirmed(int id)
        {
            Ticket ticket = db.Tickets.Find(id);
            db.Tickets.Remove(ticket);
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
