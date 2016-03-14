using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web.Mvc;
   
namespace BugTracker.Models
{
    public class Ticket
    {
        public int Id { get; set; }
        public DateTimeOffset CreateDate { get; set; }
        public string CreateBy { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Attachment { get; set; }
        public int ProjectId { get; set; }
        public int StatusId { get; set; }
        public int PriorityId { get; set; }
        public int TypeId { get; set; }
        public int AssignedTo { get; set; }
        public int AssignedBy { get; set; }
        public virtual ICollection<History> Histories { get; set; }
        public virtual ICollection<Comment> Comments { get; set; }
    }

    public class TicketType
    {
        public int Id { get; set; }
        public string TypeofTicket { get; set; }
        public virtual ICollection<Ticket> Tickets { get; set; }
    }

    public class Status
    {
        public int Id { get; set; }
        public string TicketStatus { get; set; }
        public virtual ICollection<Ticket> Tickets { get; set; }
    }

    public class Priority
    {
        public int Id { get; set; }
        public string TicketPriority { get; set; }
        public virtual ICollection<Ticket> Tickets { get; set; }
    }

    public class Assignment
    {
        public int Id { get; set; }
        public int AssignedTo { get; set; }
        public int AssingedBy { get; set; }
        public Ticket Ticket { get; set; }
    }

    public class History
    {
        public int Id { get; set; }
        public DateTimeOffset HistoryDate { get; set; }
        public string Notes { get; set; }
        public int OldPriority { get; set; }
        public int NewPriority { get; set; }
        public int OldDeveloper { get; set; }
        public int NewDeveloper { get; set; }
        public int UpdateDoneBy { get; set; }
        public Ticket Ticket { get; set; }
    }

    public class Comment
    {
        public int Id { get; set; }
        public int TicketId { get; set; }
        public DateTimeOffset CommentDate { get; set; }
        public string CommentText { get; set; }
        public int CommentBy { get; set; }
        public Ticket Ticket { get; set; }

    }

    public class Project
    {
        public int Id { get; set; }
        public string ProjectName { get; set; }
        public string ProjectManager { get; set; }
        public virtual ICollection<Ticket> Tickets { get; set; }
        public virtual ICollection<ApplicationUser> Developers { get; set; }

    }

}