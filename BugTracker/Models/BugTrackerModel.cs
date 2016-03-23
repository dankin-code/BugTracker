using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web.Mvc;
   
namespace BugTracker.Models
{
    public class Project
    {
        public Project()
        {
            this.Developers = new HashSet<ApplicationUser>();
            this.Tickets = new HashSet<Ticket>();
        }
        [DisplayName("Project Id")]
        public int Id { get; set; }
        [DisplayName("Project")]
        public string ProjectName { get; set; }
        [DisplayName("Project Manager")]
        public string ProjectManagerId { get; set; }
        public virtual ICollection<Ticket> Tickets { get; set; }
        public virtual ICollection<ApplicationUser> Developers { get; set; }
        public virtual ApplicationUser ProjectManager { get; set; }

        }

        public class Ticket
        {
            //public Ticket()
            //{
            //    this.Comment = new HashSet<Comments>();
            //    this.Attachment = new HashSet<Attachment>();
            //    this.History = new HashSet<History>();
            //}

            [DisplayName("Ticket Id")]
            public int Id { get; set; }
            [DisplayName("Ticket Create Date")]
            public DateTimeOffset CreateDate { get; set; }
            [DisplayName("Ticket Update Date")]
            public DateTimeOffset UpdateDate { get; set; }
            [DisplayName("Ticket Submitter")]
            public string CreateById { get; set; }
            [DisplayName("Ticket Assigned To")]
            public string AssignedToId { get; set; }
            public string Title { get; set; }
            [AllowHtml]
            public string Description { get; set; }
            public string Attachment { get; set; }
            [DisplayName("Project")]
            public int ProjectId { get; set; }
            [DisplayName("Status")]
            public int StatusId { get; set; }
            [DisplayName("Priority")]
            public int PriorityId { get; set; }
            [DisplayName("Ticket Type")]
            public int TicketTypeId { get; set; }
            [DisplayName("Notification")]
            public int NotificationId { get; set; }
            public virtual Project Project { get; set; }
            public virtual ApplicationUser CreatedBy { get; set; }
            public virtual ApplicationUser AssignedTo { get; set; }
            //public virtual ICollection<History> Histories { get; set; }
            //public virtual ICollection<Comment> Comments { get; set; }
            public virtual ICollection<TicketAttachment> TicketAttachment { get; set; }

        }

        public class TicketAttachment
        {
            public int Id { get; set; }
            [DisplayName("Ticket Id")]
            public int TicketId { get; set; }
            [DisplayName("Attached By")]
            public string AttachedById { get; set; }
            [DisplayName("Attachment Url")]
            public string AttachmentUrl { get; set; }
            [DisplayName("Date Attached")]
            public DateTimeOffset DateAttached { get; set; }

            public virtual Ticket Ticket { get; set; }
            public virtual ApplicationUser AttachedBy { get; set; }
        }

        public class TicketType
        {
            public int Id { get; set; }
            public string TicketTypeName { get; set; }
        }

        public class Status
        {
            public int Id { get; set; }
            public string TicketStatus { get; set; }
        }

        public class Priority
        {
            public int Id { get; set; }
            public string TicketPriority { get; set; }
        }

        public class History
        {
            public int Id { get; set; }
            public DateTimeOffset HistoryDate { get; set; }
            public string OldName { get; set; }
            public string NewName { get; set; }
            public string OldDescription { get; set; }
            public string NewDescription { get; set; }
            [DisplayName("Old Priority")]
            public int OldPriority { get; set; }
            [DisplayName("New Priority")]
            public int NewPriority { get; set; }
            [DisplayName("Previous Developer")]
            public string OldDeveloperId { get; set; }
            [DisplayName("Current Developer")]
            public string NewDeveloperId { get; set; }
            [DisplayName("Update Done By")]
            public string UpdatedById { get; set; }
            public virtual Ticket Ticket { get; set; }
            public virtual ApplicationUser UpdatedBy { get; set; }
            public virtual ApplicationUser OldDeveloper { get; set; }
            public virtual ApplicationUser NewDeveloper { get; set; }
        }

        public class Comment
        {
            public int Id { get; set; }
            [DisplayName("Ticket Id")]
            public int TicketId { get; set; }
            [DisplayName("Comment Date")]
            public DateTimeOffset CommentDate { get; set; }
            [AllowHtml]
            [DisplayName("Comment")]
            public string CommentText { get; set; }
            [DisplayName("Written By")]
            public string CommentById { get; set; }
            public Ticket Ticket { get; set; }
            public virtual ApplicationUser CommentBy { get; set; }

        }

        public class Notification
        {
            public int Id { get; set; }
            public int TicketId { get; set; }
            public string CreatorId { get; set; }
            public virtual ApplicationUser Creator { get; set; }

        }

        public class DashboardViewModel
        {
            public DashboardViewModel()
            {

            }
            public ICollection<Project> Project { get; set; }
            public ICollection<Ticket> Ticket { get; set; }
            public ICollection<History> History { get; set; }
            public ICollection<Comment> Comment { get; set; }
            public ICollection<Status> Status { get; set; }
            public ICollection<Priority> Priority { get; set; }
            public ICollection<TicketType> TicketType { get; set; }
            public ICollection<Notification> Notification { get; set; }
            public ICollection<ApplicationUser> ApplicationUser { get; set; }
            public ICollection<TicketAttachment> TicketAttachment { get; set; }
        }
    }
