using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BugTracker.Models
{
    public class Ticket
    {
        public int Id { get; set; }
        public DateTime CreateDate { get; set; }
        public int CreateBy { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Attachment { get; set; }
        public int ProjectId { get; set; }
        public int StatusId { get; set; }
        public int PriorityId { get; set; }
        public virtual ICollection<TicketHistory> Histories { get; set; }
        public virtual ICollection<TicketComment> Comments { get; set; }
        public virtual ICollection<ApplicationUser> Users { get; set; }
        public virtual ICollection<TicketType> Types { get; set; }
        public virtual ICollection<TicketStatus> Statuses { get; set; }
        public virtual ICollection<TicketPriority> Priorities { get; set; }
        public virtual ICollection<TicketAssignment> Assignees { get; set; }

    }
    public class TicketType
    {
        public int Id { get; set; }
        public string Type { get; set; }
    }
    public class TicketStatus
    {
        public int Id { get; set; }
        public string Status { get; set; }
        public virtual ICollection<Ticket> Tickets { get; set; }


    }
    public class TicketPriority
    {
        public int Id { get; set; }
        public string Priority { get; set; }
        public virtual ICollection<Ticket> Tickets { get; set; }

    }
    public class TicketAssignment
    {
        public int Id { get; set; }
        public int AssignedTo { get; set; }
        public int AssingedBy { get; set; }
        public virtual ICollection<Ticket> Tickets { get; set; }


    }
    public class TicketHistory
    {
        public int Id { get; set; }
        public int TicketId { get; set; }
        public int OldPriority { get; set; }
        public int NewPriority { get; set; }
        public int OldDeveloper { get; set; }
        public int NewDeveloper { get; set; }
        public int UpdateDoneBy { get; set; }
    }
    public class TicketComment
    {
        public int Id { get; set; }
        public int TicketId { get; set; }
        public DateTime CommentDate { get; set; }
        public string CommentText { get; set; }
        public int CommentBy { get; set; }

    }
    public class Project
    {
        public int Id { get; set; }
        public string ProjectName { get; set; }
        public DateTime ProjectCreationDate { get; set; }
        public int ProjectManager { get; set; }
        public ICollection<Ticket> Tickets { get; set; }
        public ICollection<ApplicationUser> Users { get; set; }
    }

    public class ProjectTickets
    {
        public int ProjectId { get; set; }
        public virtual ICollection<Ticket> Tickets { get; set; }

    }

}