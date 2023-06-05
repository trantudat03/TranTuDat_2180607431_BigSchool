using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using TranTuDat_2180607431.DTOs;
using TranTuDat_2180607431.Models;

namespace TranTuDat_2180607431.Controllers
{
    [Route("api/Attendances")]
    
    public class AttendancesController : ApiController
    {
        private ApplicationDbContext _dbContext;

        public AttendancesController()
        {
            _dbContext = new ApplicationDbContext();
        }

        [HttpPost]

        public IHttpActionResult Attend(AttendanceDto attendanceDto)
        {

            var userId = User.Identity.GetUserId();
            if (_dbContext.Attendances.Any(a => a.AttendeeId == userId && a.CourseId == attendanceDto.CourseId))
            {
                return BadRequest("The Attendance already exists!");
            }

            var attendance = new Attendance
            {
                CourseId = attendanceDto.CourseId,
                AttendeeId = userId
            };
            _dbContext.Attendances.Add(attendance);
            _dbContext.SaveChanges();

            return Ok();
        }


        [HttpGet]

        public IHttpActionResult Get(AttendanceDto attendanceDto)
        {

            

            return Ok(_dbContext.Attendances);
        }

        

        [HttpDelete]
        [Route("{id}")]
        public IHttpActionResult Delete(int id)
        {
            var userId = User.Identity.GetUserId();

            var attendance = _dbContext.Attendances.FirstOrDefault(a=>userId== a.AttendeeId && a.CourseId == id);

            if(userId == null)
            {
                return BadRequest("Loiiiii 1213");
            }

            if (attendance == null)
            {
                return BadRequest("Loiiiii"); // Nếu không tìm thấy dữ liệu để xóa, trả về mã lỗi 404 Not Found
            }

            _dbContext.Attendances.Remove(attendance);
            _dbContext.SaveChanges();

            return Ok(); // Trả về mã thành công 200 OK
        }


    }
}
