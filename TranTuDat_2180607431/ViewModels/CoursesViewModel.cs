using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TranTuDat_2180607431.Models;

namespace TranTuDat_2180607431.ViewModels
{
    public class CoursesViewModel
    {
        public IEnumerable<Course> UpcommingCourses { get; set; }
        public bool ShowAction { get; set; }
    }
}