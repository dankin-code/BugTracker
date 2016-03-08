using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BugTracker.Models
{
    public class UserProjectsHelper
    {
        private ApplicationDbContext db;

        public UserProjectsHelper(ApplicationDbContext db)
        {
            this.db = db;
        }
    }
}