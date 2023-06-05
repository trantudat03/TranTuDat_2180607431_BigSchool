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
    public class FollowingsController : ApiController
    {
        private readonly ApplicationDbContext _dbContext;
        public FollowingsController()
        {
            _dbContext = new ApplicationDbContext();
        }

        [HttpPost]

        public IHttpActionResult Follow(FollowingDto followingDto)
        {
            //  
            var userId = User.Identity.GetUserId();
            if (_dbContext.Followings.Any(f => f.FollowerId == userId && f.FolloweeId == followingDto.FolloweeId ))
            {
                return BadRequest("The Following already exists!");
            }

            var folowing = new Following
            {
                FollowerId = userId,
                FolloweeId = followingDto.FolloweeId
            };
            _dbContext.Followings.Add(folowing);
            _dbContext.SaveChanges();

            return Ok();
        }

        [HttpGet]

        public IHttpActionResult Get(FollowingDto followingDto)
        {



            return Ok(_dbContext.Followings);
        }

        [HttpDelete]
        [Route("{id}")]
        public IHttpActionResult Delete(string id)
        {
            var userId = User.Identity.GetUserId();

            var following = _dbContext.Followings.FirstOrDefault(a=> a.FollowerId == userId && a.FolloweeId == id);

            if (userId == null)
            {
                return BadRequest("Loiiiii 1213");
            }

            if (following == null)
            {
                return BadRequest("Loiiiii"); // Nếu không tìm thấy dữ liệu để xóa, trả về mã lỗi 404 Not Found
            }

            _dbContext.Followings.Remove(following);
            _dbContext.SaveChanges();

            return Ok(); // Trả về mã thành công 200 OK
        }
    }
}
