//localhost/posts
namespace ServerMVCProject.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using ServerLibraryProject.Interfaces;
    using ServerLibraryProject.Models;

    [Route("posts")]
    public class ViewPostsController : Controller
    {
        private readonly IPostService postService;

        public ViewPostsController(IPostService postService)
        {
            this.postService = postService;
        }

        [HttpGet("")]
        public IActionResult Index()
        {
            string userIdStr = this.HttpContext.Session.GetString("UserId");

            long userId = long.Parse(userIdStr);

            List<Post> posts = this.postService.GetPostsHomeFeed(userId);
            return this.View(posts);
        }
    }
}