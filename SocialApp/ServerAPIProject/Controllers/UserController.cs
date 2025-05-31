namespace ServerAPIProject.Controllers
{
    using System.Collections.Generic;
    using Microsoft.AspNetCore.Mvc;
    using ServerLibraryProject.Interfaces;
    using ServerLibraryProject.Models;

    [ApiController]
    [Route("api/users")]
    public class UserController : ControllerBase
    {
        private readonly IUserService userService;

        public UserController(IUserService userService)
        {
            this.userService = userService;
        }

        [HttpPost("{userId}/followers")]
        public IActionResult FollowUser(long userId, [FromBody] long followerId)
        {
            try
            {
                this.userService.FollowUserById(userId, followerId);
                return this.Ok();
            }
            catch (Exception e)
            {
                return this.BadRequest(e.Message);
            }
        }

        [HttpGet]
        public ActionResult<List<User>> GetAllUsers()
        {
            return this.userService.GetAllUsers();
        }

        [HttpGet("{id}")]
        public ActionResult<User> GetUserById(long id)
        {
            try
            {
                return this.userService.GetById(id);
            }
            catch (Exception e)
            {
                return this.BadRequest(e.Message);
            }
        }

        [HttpGet("user")]
        public ActionResult<User> GetUserByUsername([FromQuery] string username)
        {
            try
            {
                return this.userService.GetUserByUsername(username);
            }
            catch (Exception e)
            {
                return this.BadRequest(e.Message);
            }
        }

        [HttpGet("{userId}/followers")]
        public ActionResult<List<User>> GetUserFollowers(long userId)
        {
            return this.userService.GetUserFollowers(userId);
        }

        [HttpGet("{userId}/following")]
        public ActionResult<List<User>> GetUserFollowing(long userId)
        {
            return this.userService.GetUserFollowing(userId);
        }

        [HttpPost]
        public IActionResult SaveUser(User user)
        {
            try
            {
                var savedUser = this.userService.AddUser(user.Username, user.Password, user.Image);
                return this.Ok(savedUser);
            }
            catch (Exception e)
            {
                return this.BadRequest(e.Message);
            }
        }

        [HttpDelete("{userId}/followers/{unfollowUserId}")]
        public IActionResult UnfollowUser(long userId, long unfollowUserId)
        {
            try
            {
                this.userService.UnfollowUserById(userId, unfollowUserId);
                return this.Ok();
            }
            catch (Exception e)
            {
                return this.BadRequest(e.Message);
            }
        }
    }
}
