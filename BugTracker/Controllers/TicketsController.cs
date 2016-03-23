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
using System.Threading.Tasks;

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

        [Authorize(Roles = "Admin, Submitter, Developer, Project Manager")]
        public ActionResult Dashboard()
        {
            var tickets = db.Tickets.Include(t => t.Project);
            return View(tickets.ToList());
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
            ViewBag.StatusId = new SelectList(db.Statuses, "Id", "TicketStatus");
            ViewBag.PriorityId = new SelectList(db.Priorities, "Id", "TicketPriority");
            ViewBag.TicketTypeId = new SelectList(db.TicketTypes, "Id", "TicketTypeName");
            ViewBag.AssignedToId = new SelectList(db.Users, "Id", "FirstName");
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
                ticket.UpdateDate = new DateTimeOffset(DateTime.Now);
                ticket.CreateById = (db.Users.FirstOrDefault(u => u.UserName == User.Identity.Name).Id);
                //ticket.AssignedTo = ViewBag.AssignedToId;
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
            TempData["oldticket"] = ticket;
            if (ticket == null)
            {
                return HttpNotFound();
            }

            ViewBag.ProjectId = new SelectList(db.Projects, "Id", "ProjectName", ticket.ProjectId);
            ViewBag.StatusId = new SelectList(db.Statuses, "Id", "TicketStatus");
            ViewBag.PriorityId = new SelectList(db.Priorities, "Id", "TicketPriority");
            ViewBag.TicketTypeId = new SelectList(db.TicketTypes, "Id", "TicketTypeName");
            ViewBag.AssignedToId = new SelectList(db.Users, "Id", "FirstName");

            return View(ticket);
        }

        // POST: Tickets/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin, Project Manager")]
        public ActionResult Edit([Bind(Include = "Id,UpdateDate,CreateById,AssingedToId,Title,Description,Attachment,ProjectId,StatusId,PriorityId,TicketTypeId,NotificationId")] Ticket ticket)
        {
            if (ModelState.IsValid)
            {

                var oldTicket = (Ticket)TempData["oldticket"];

                //uncomment the line below after creating the  notification module.
                //TicketNotification notification = null;


                //determine what changes have been made to the ticket then add a new ticket history


                /*
                if(oldTicket.TicketTypeId != ticket.TicketTypeId)
                {
                    ticket.UpdateDate = DateTimeOffset.Now;
                    ticket.
                    db.Histories.Add(new History)
                    {

                        Property = "NewProperty",
                        
                        UpdatedBy = User.Identity.GetUserId(),

                 
                    }
                }

                

                if (oldTicket.PriorityId != ticket.PriorityId)
                {
                    Property = "Priority",
                    Changed = DateTimeOffset.Now,
                    UserId = User.Identity.GetUserId(),
                    TicketId = ticket.Id,
                    OldValue = oldTicket.PriorityId.Name,
                    NewValue = db.Priorities.Find
                        (ticket.PriorityId).Name});
                    notification = await NotifyFilters(ticket);
                }

                if (oldTicket.AssignedToId != ticket.AssingedToId)
                {
                    Property = "Assignee",
                    Changed = DateTimeOffset.Now,
                    UserId = User.Identity.GetUserId(),
                    TicketId = ticket.Id,
                    OldValue = oldTicket.AssignedUser.Name,
                    NewValue = db.Users.Find
                            (ticket.AssingedToId).Name});
                notification = await NotifyFilters(ticket);
                }

                ticket.UpdateDate = new DateTimeOffset(DateTime.Now);

                if (Notification != null)
                {
                    //notification was sent so log it in the db
                    db.Notifications.Add(notification)
                }

                */

                ticket.CreateById = (db.Users.FirstOrDefault(u => u.UserName == User.Identity.Name).Id);
                db.Entry(ticket).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ProjectId = new SelectList(db.Projects, "Id", "ProjectName", ticket.ProjectId);
            ViewBag.StatusId = new SelectList(db.Statuses, "Id", "TicketStatus");
            ViewBag.PriorityId = new SelectList(db.Priorities, "Id", "TicketPriority");
            ViewBag.TicketTypeId = new SelectList(db.TicketTypes, "Id", "TicketTypeName");
            ViewBag.AssignedToId = new SelectList(db.Users, "Id", "FirstName");

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


        private async Task<Notification> Notify(Ticket ticket)
        {
            string body = null;
            ApplicationUser toUser = null;
            var userId = User.Identity.GetUserId();
            var fromUser = db.Users.FirstOrDefault(u => u.Id == userId);
            var subject = "Changes to Ticket: " + ticket.Title;

            if(userId != ticket.AssignedToId)
                {
                    //person making the changes is not the developer, so notify the developer
                    toUser = db.Users.Find(ticket.AssignedToId);
                    //build the mail message
                    body = "<p>" + toUser.FirstName + ", <br />" +
                        fromUser.FirstName+ " has made some changes to an assigned ticket for project "
                        + db.Projects.Find(ticket.ProjectId).ProjectName + ".</p>";
                }

            EmailService e = new EmailService();
            await e.SendAsync(new IdentityMessage { Subject = subject, Body = body, Destination = toUser.Email });

            //if we get this far, we've successfully sent the notification so create a new entry
            // for the db

            return new Notification();
            //{
            //    Sent = new DateTimeOffset(DateTime.Now),
            //    TicketId = ticket.Id,
            //    FromUserId = fromUserId,
            //    ToUserId = toUser.Id
            //};


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
