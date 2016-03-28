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

namespace BugTracker.Controllers
{
    public class TicketsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

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
        [Authorize(Roles = "Admin, Submitter, Developer, Project Manager")]
        public ActionResult Index()
        {

            var tickets = db.Tickets.Include(t => t.AssignedTo).Include(t => t.Project);
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
        [Authorize(Roles = "Admin, Submitter, Developer, Project Manager")]
        public ActionResult Create([Bind(Include = "Id,CreateDate,UpdateDate,CreateById,AssignedToId,Title,Description,ProjectId,StatusId,PriorityId,TicketTypeId,NotificationId")] Ticket ticket)
        {
            if (ModelState.IsValid)
            {
                ticket.CreateDate = new DateTimeOffset(DateTime.Now);
                ticket.UpdateDate = new DateTimeOffset(DateTime.Now);
                ticket.CreateById = User.Identity.GetUserId();
                db.Tickets.Add(ticket);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.StatusId = new SelectList(db.Statuses, "Id", "TicketStatus");
            ViewBag.PriorityId = new SelectList(db.Priorities, "Id", "TicketPriority");
            ViewBag.TicketTypeId = new SelectList(db.TicketTypes, "Id", "TicketTypeName");
            ViewBag.AssignedToId = new SelectList(db.Users, "Id", "FirstName");
            ViewBag.ProjectId = new SelectList(db.Projects, "Id", "ProjectName");

            return View(ticket);
        }

        // GET: Tickets/Edit/5
        [Authorize(Roles = "Admin, Submitter, Developer, Project Manager")]
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
            ViewBag.StatusId = new SelectList(db.Statuses, "Id", "TicketStatus", ticket.StatusId);
            ViewBag.PriorityId = new SelectList(db.Priorities, "Id", "TicketPriority", ticket.PriorityId);
            ViewBag.TicketTypeId = new SelectList(db.TicketTypes, "Id", "TicketTypeName", ticket.TicketTypeId);
            ViewBag.AssignedToId = new SelectList(db.Users, "Id", "FirstName", ticket.AssignedToId);
            ViewBag.ProjectId = new SelectList(db.Projects, "Id", "ProjectName", ticket.ProjectId);
            
            return View(ticket);
        }

        // POST: Tickets/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin, Submitter, Developer, Project Manager")]
        public ActionResult Edit([Bind(Include = "Id,CreateDate,UpdateDate,CreateById,AssignedToId,Title,Description,ProjectId,StatusId,PriorityId,TicketTypeId,NotificationId")] Ticket ticket)
        {
            if (ModelState.IsValid)
            {
                var oldTicket = (Ticket)TempData["oldTicket"];

                /*Determine what's changed for each property, and add a new History item
                in the db
                */
                
                if(oldTicket.Description != ticket.Description)
                {
                    db.Histories.Add(new History
                    {
                        Property = "Description",
                        HistoryDate = DateTimeOffset.Now,
                        UpdatedById = User.Identity.GetUserId(),
                        TicketId = ticket.Id,
                        PreviousValue = oldTicket.Description,
                        NewValue = ticket.Description
                    });
                }

                if (oldTicket.ProjectId != ticket.ProjectId)
                {
                    db.Histories.Add(new History
                    {
                        Property = "Project",
                        HistoryDate = DateTimeOffset.Now,
                        UpdatedById = User.Identity.GetUserId(),
                        TicketId = ticket.Id,
                        PreviousValue = oldTicket.Project.ProjectName,
                        NewValue = db.Projects.Find(ticket.ProjectId).ProjectName
                    });
                }


                if (oldTicket.TicketTypeId != ticket.TicketTypeId)
                {
                    db.Histories.Add(new History
                    {
                        Property = "TicketType",
                        HistoryDate = DateTimeOffset.Now,
                        UpdatedById = User.Identity.GetUserId(),
                        TicketId = ticket.Id,
                        PreviousValue = oldTicket.TicketTypeId.ToString(), //check this line to remove ToString()
                        NewValue = db.TicketTypes.Find(ticket.TicketTypeId).TicketTypeName
                    });
                }


                if (oldTicket.PriorityId != ticket.PriorityId)
                {
                    db.Histories.Add(new History
                    {
                        Property = "Priority",
                        HistoryDate = DateTimeOffset.Now,
                        UpdatedById = User.Identity.GetUserId(),
                        TicketId = ticket.Id,
                        PreviousValue = oldTicket.PriorityId.ToString(),
                        NewValue = db.Priorities.Find(ticket.PriorityId).TicketPriority
                    });
                }

                if (oldTicket.StatusId != ticket.StatusId)
                {
                    db.Histories.Add(new History
                    {
                        Property = "Status",
                        HistoryDate = DateTimeOffset.Now,
                        UpdatedById = User.Identity.GetUserId(),
                        TicketId = ticket.Id,
                        PreviousValue = oldTicket.StatusId.ToString(),
                        NewValue = db.Statuses.Find(ticket.StatusId).TicketStatus
                    });
                }

                if (oldTicket.AssignedToId != ticket.AssignedToId)
                {
                    db.Histories.Add(new History
                    {
                        Property = "AssignedToId",
                        HistoryDate = DateTimeOffset.Now,
                        UpdatedById = User.Identity.GetUserId(),
                        TicketId = ticket.Id,
                        PreviousValue = oldTicket.AssignedTo.FirstName,
                        NewValue = db.Users.Find(ticket.AssignedToId).FirstName
                    });
                }


                ticket.UpdateDate = new DateTimeOffset(DateTime.Now);
                db.Entry(ticket).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.StatusId = new SelectList(db.Statuses, "Id", "TicketStatus");
            ViewBag.PriorityId = new SelectList(db.Priorities, "Id", "TicketPriority");
            ViewBag.TicketTypeId = new SelectList(db.TicketTypes, "Id", "TicketTypeName");
            ViewBag.AssignedToId = new SelectList(db.Users, "Id", "FirstName");
            ViewBag.ProjectId = new SelectList(db.Projects, "Id", "ProjectName");

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
