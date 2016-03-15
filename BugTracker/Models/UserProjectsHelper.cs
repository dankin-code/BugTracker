using BugTracker.Models;

namespace BugTracker.Controllers
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