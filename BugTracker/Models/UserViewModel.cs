using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace BugTracker.Models
{

    public class UserViewModel
    {
        public string Id { get; set; }
        public string Name { get; set; }
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