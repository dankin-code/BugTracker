using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace BugTracker.Models
{

    public class UserViewModel
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public List<string> RoleName { get; set; }
        public List<Ticket> Tickets { get; set; }
    }

    public class TicketDetailsViewModel
    {
        public Ticket CurrentTicket { get; set; }
        public ICollection<History> Histories { get; set; }
        public ICollection<Comment> Comments { get; set; }
        public ICollection<TicketAttachment> TicketAttachments { get; set; }
    }

    public class AddUserToProjectViewModel
    {
        public int ProjectId { get; set; }
        public string ProjectName { get; set; }
        public string DeveloperToAddId { get; set; }
        public List<ApplicationUser> Developers { get; set; }
    }

    public class RemoveUserFromProjectViewModel
    {
        public int ProjectId { get; set; }
        public string ProjectName { get; set; }
        public string DeveloperToRemoveId { get; set; }
        public string DeveloperToRemoveName { get; set; }
    }

    public class AssignUserToRoleViewModel
    {
        public string DisplayName { get; set; }
        public string UserId { get; set; }
        public SelectList Roles { get; set; }
        public string SelectedRole { get; set; }
    }

    public class UserDropDownViewModel
    {
        public string Id { get; set; }
        public string Name { get; set; }
    }

    public class ProjectUsersViewModel
    {
        public int ProjectId { get; set; }
        [Display(Name = "Project Name")]
        public string ProjectName { get; set; }
        [Display(Name = "Assigned Developers")]
        public System.Web.Mvc.MultiSelectList Devs { get; set; }
        public string[] SelectedDevs { get; set; }
        [Display(Name = "Project Manager")]
        public System.Web.Mvc.SelectList PMs { get; set; }
        public string SelectedPM { get; set; }
    }


}