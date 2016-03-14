using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace BugTracker.Models
{
    public class UserRolesHelper
    {
        private UserManager<ApplicationUser> manager = new UserManager<ApplicationUser>(
                                                       new UserStore<ApplicationUser>(
                                                       new ApplicationDbContext()));
        private ApplicationDbContext db;
        private UserManager<ApplicationUser> userManager;
        private RoleManager<IdentityRole> roleManager;



        public UserRolesHelper(ApplicationDbContext db)
        {
            this.userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(db));
            this.roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(db));
            this.db = db;
        }

        public bool IsUserInRole(string userId, string roleName)
        {
            return manager.IsInRole(userId, roleName);
        }

        public IList<string>ListUserRoles(string userId)
        {
            return manager.GetRoles(userId);
        }

        public bool AddUserToRole(string userId, string roleName)
        {
            var result = manager.AddToRole(userId, roleName);
            return result.Succeeded;
        }

        public bool RemoveUserFromRole(string userId, string roleName)
        {
            var result = manager.RemoveFromRole(userId, roleName);
            return result.Succeeded;
        }

        public ICollection<UserDropDownViewModel> UsersInRole(string roleName)
        {
            var userIDs = roleManager.FindByName(roleName).Users.Select(r => r.UserId);
            return userManager.Users.Where(u => userIDs.Contains(u.Id)).Select(u => new UserDropDownViewModel { Id = u.Id, Name = u.UserName }).ToList();
        }

        public ICollection<UserDropDownViewModel> UsersNotInRole(string roleName)
        {
            var userIDs = System.Web.Security.Roles.GetUsersInRole(roleName);
            return userManager.Users.Where(u => !userIDs.Contains(u.Id)).Select(u => new UserDropDownViewModel { Id = u.Id, Name = u.UserName }).ToList();
        }


    }

}