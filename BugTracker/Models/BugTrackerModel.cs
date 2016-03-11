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
        [DisplayName("Creation Date")]
        public DateTimeOffset CreateDate { get; set; }
        [DisplayName("Created By")]
        public int CreateBy { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Attachment { get; set; }
        [DisplayName("Attachment Description")]
        public string AttachmentDescription { get; set; }
        [DisplayName("Project")]
        public int ProjectId { get; set; }
        [DisplayName("Status")]
        public int StatusId { get; set; }
        [DisplayName("Priority")]
        public int PriorityId { get; set; }
        [DisplayName("Type")]
        public int TypeId { get; set; }
        [DisplayName("Assigned To")]
        public int AssignedTo { get; set; }
        [DisplayName("Assigned By")]
        public int AssignedBy { get; set; }
        public virtual ICollection<History> Histories { get; set; }
        public virtual ICollection<Comment> Comments { get; set; }
    }

    public class Type
    {
        public int Id { get; set; }
        public string TicketType { get; set; }
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
        [Display(Name = "Project Id")]
        public int Id { get; set; }
        [Display(Name ="Project Name")]
        public string ProjectName { get; set; }
        [Display(Name = "Project Description")]
        public string ProjectDescription { get; set; }
        [Display(Name = "Project Creation Date")]
        public DateTimeOffset CreationDate { get; set; }
        [Display(Name = "Project Manager")]
        public System.Web.Mvc.SelectList ProjectManagers { get; set; }
        [Display(Name = "Assigned Name")]
        public string AssignedProjectManager { get; set; }
        [Display(Name = "Developers")]
        public System.Web.Mvc.MultiSelectList Developers { get; set; }
        [Display(Name = "Assigned Developers")]
        public string[] SelectedDevelopers { get; set; }
        public virtual ICollection<Ticket> Tickets { get; set; }
    }

}