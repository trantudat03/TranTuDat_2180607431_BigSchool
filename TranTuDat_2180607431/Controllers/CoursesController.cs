using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TranTuDat_2180607431.Models;
using TranTuDat_2180607431.Models.ViewModels;
using TranTuDat_2180607431.ViewModels;

namespace TranTuDat_2180607431.Controllers
{
    public class CoursesController : Controller
    {

        private readonly ApplicationDbContext _dbContext;
        public CoursesController()
        {
            _dbContext = new ApplicationDbContext();
        }
        // GET: Courses
        [Authorize]

        public ActionResult Create()
        {

            var viewModel = new CourseViewModel
            {
                Categories = _dbContext.categories.ToList()
            };
            return View(viewModel);
        }
        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CourseViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                viewModel.Categories = _dbContext.categories.ToList();
                return View("Create", viewModel);
            }
            var course = new Course
            {
                LecturerId = User.Identity.GetUserId(),
                // DateTime = DateTime.Now,
                DateTime = viewModel.GetDateTime(),
                CategoryId = viewModel.Category,
                Place = viewModel.Place

            };
            _dbContext.Courses.Add(course);
            _dbContext.SaveChanges();
            return RedirectToAction("index", "Home");
        }

        [Authorize]

        public ActionResult Attending()
        {
            var userId = User.Identity.GetUserId();

            var courses = _dbContext.Attendances 
                .Where(a => a.AttendeeId == userId)
                .Select(a => a.Course)
                .Include(l=> l.Lecturer)
                .Include (l=> l.Category)
                .ToList();

            var viewModel = new CoursesViewModel
            {
                UpcommingCourses = courses,
                ShowAction = User.Identity.IsAuthenticated,
            };
            return View(viewModel);
        }

        [Authorize]

        public ActionResult Following()
        {
            var userId = User.Identity.GetUserId();

            var followees = _dbContext.Followings
                    .Where(l => l.FollowerId == userId)
                    //.Select(l => l.Followee)
                    //.Select(l => l.Followers)
                    .Include(l => l.Followee)
                    .Include(l => l.Follower)
                    .ToList();
            /*
            var viewModel = new FolloweeViewModel
            {
                followingsItem = (IEnumerable<Following>)followees,
                ShowAction = User.Identity.IsAuthenticated,
            };
            */

            return View(followees);
        }

        public ActionResult Mine()
        {
            var userId = User.Identity.GetUserId();

            var viewModel = _dbContext.Courses
                .Where(c => c.LecturerId == userId && c.DateTime > DateTime.Now)
                .Include(l => l.Lecturer)
                .Include(l => l.Category)
                .ToList();

            return View(viewModel);
        }

        public ActionResult Edit(int id)
        {
            var userId = User.Identity.GetUserId();

            var course = _dbContext.Courses.Single(c => c.Id == id && c.LecturerId == userId);

            var viewModel = new CourseViewModel
            {
                Place = course.Place,
                Categories = _dbContext.categories.ToList(),
                Date = course.DateTime.Date.ToString("dd/M/yyyy"),
                Time = course.DateTime.ToString("HH:mm")
                
            };

            return View(viewModel);
        }
    }
}