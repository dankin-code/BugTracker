﻿using System;
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
        private UserManager<ApplicationUser> manager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new ApplicationDbContext()));
        private ApplicationDbContext db;

        public UserRolesHelper(ApplicationDbContext db)
        {
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

    }

}