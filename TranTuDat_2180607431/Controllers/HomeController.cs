
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TranTuDat_2180607431.Models;
using System.Data.Entity;
using TranTuDat_2180607431.ViewModels;
using Microsoft.AspNet.Identity;

namespace TranTuDat_2180607431.Controllers
{
    public class HomeController : Controller
    {
        private ApplicationDbContext _dbContext;

        public HomeController()
        {
            _dbContext = new ApplicationDbContext();
        }
        public ActionResult Index()
        {

            
            var upcommingCourses = _dbContext.Courses
                .Include(c => c.Lecturer)
                .Include(c => c.Category)
                .Where(c => c.DateTime > DateTime.Now);

            var viewModel = new CoursesViewModel
            {
                UpcommingCourses = upcommingCourses,
                ShowAction = User.Identity.IsAuthenticated
            };

            var userId = User.Identity.GetUserId();
            List<string> followingIds = new List<string>();

            foreach(var f in _dbContext.Followings)
            {
                if(f.FollowerId == userId)
                followingIds.Add(f.FolloweeId);
            }

            List<int> GoingIds = new List<int>();

            foreach (var f in _dbContext.Attendances)
            {
                if(userId == f.AttendeeId)
                {
                    GoingIds.Add(f.CourseId);
                }
            }
            ViewBag.GoingIds = GoingIds;
            ViewBag.FollowingIds = followingIds;
            return View(viewModel);    
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}