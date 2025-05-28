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
            List<Post> posts = this.postService.GetAllPosts(); // You should have this in your IPostService
            return this.View(posts);
        }
    }
}