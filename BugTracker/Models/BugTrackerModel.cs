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
        public int Id { get; set; }
        public string ProjectName { get; set; }
        public string ProjectManagerId { get; set; }
        public virtual ICollection<Ticket> Tickets { get; set; }
        public virtual ICollection<ApplicationUser> Developers { get; set; }
        public virtual ApplicationUser ProjectManager { get; set; }

    }

    public class Ticket
    {
        public int Id { get; set; }
        public DateTimeOffset CreateDate { get; set; }
        public DateTimeOffset UpdateDate { get; set; }
        public string CreateById { get; set; }
        public string AssingedToId { get; set; }
        public string Title { get; set; }
        [AllowHtml]
        public string Description { get; set; }
        public string Attachment { get; set; }
        public int ProjectId { get; set; }
        public int StatusId { get; set; }
        public int PriorityId { get; set; }
        public int TicketTypeId { get; set; }
        public int NotificationId { get; set; }
        public virtual Project Project { get; set; }
        public virtual ApplicationUser CreatedBy { get; set; }
        public virtual ApplicationUser AssignedTo { get; set; }
        public virtual ICollection<History> History { get; set; }
        public virtual ICollection<Comment> Comment { get; set; }

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
        [AllowHtml]
        public string Notes { get; set; }
        public int OldPriority { get; set; }
        public int NewPriority { get; set; }
        public string OldDeveloperId { get; set; }
        public string NewDeveloperId { get; set; }
        public string UpdatedById { get; set; }
        public virtual Ticket Ticket { get; set; }
        public virtual ApplicationUser UpdatedBy { get; set; }
        public virtual ApplicationUser OldDeveloper { get; set; }
        public virtual ApplicationUser NewDeveloper { get; set; }

    }

    public class Comment
    {
        public int Id { get; set; }
        public int TicketId { get; set; }
        public DateTimeOffset CommentDate { get; set; }
        public string CommentText { get; set; }
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

        public ICollection<Project> projects { get; set; }
        public ICollection<Ticket> tickets { get; set; }
        public ICollection<Status> statuses { get; set; }
        public ICollection<Priority> priorities { get; set; }
        public ICollection<TicketType> TicketTypes { get; set; }
        public ICollection<Notification> notifications { get; set; }

        }
}