using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TranTuDat_2180607431.Models;
using TranTuDat_2180607431.Models.ViewModels;

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
        public ActionResult Create()
        {

            var viewModel = new CourseViewModel
            {
                Categories = _dbContext.categories.ToList()
            };
            return View(viewModel);
        }
    }
}