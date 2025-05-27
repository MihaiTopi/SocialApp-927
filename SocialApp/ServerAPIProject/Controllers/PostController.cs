using ServerLibraryProject.Interfaces;
using ServerLibraryProject.Models;
using Microsoft.AspNetCore.Mvc;

namespace Server.Controllers
{
    
    [ApiController]
    [Route("api/posts")]
    public class PostController : ControllerBase
    {
        private readonly IPostService postService;
        private readonly IReactionService reactionService;
        private readonly ICommentService commentService;

        public PostController(IPostService postService, IReactionService reactionService, ICommentService commentService)
        {
            this.postService = postService;
            this.reactionService = reactionService;
            this.commentService = commentService;
        }

     
        [HttpGet]
        public ActionResult<List<Post>> GetAllPosts()
        {
            try
            {
                return Ok(this.postService.GetAllPosts());
            }
            catch (Exception ex)
            {
                return StatusCode(404, $"Error retrieving posts: {ex.Message}");
            }
        }

        [HttpGet("{id}")]
        public ActionResult<Post> GetPostById(long id)
        {
            try
            {
                var post = this.postService.GetPostById(id);
                if (post == null)
                    return NotFound($"Post with ID {id} not found.");

                return Ok(post);
            }
            catch (Exception ex)
            {
                return StatusCode(404, $"Error retrieving post: {ex.Message}");
            }
        }

       
        [HttpGet("user/{userId}")]
        public ActionResult<List<Post>> GetPostsByUserId(long userId)
        {
            try
            {
                return Ok(this.postService.GetPostsByUserId(userId));
            }
            catch (Exception ex)
            {
                return StatusCode(404, $"Error retrieving user's posts: {ex.Message}");
            }
        }

        [HttpGet("group/{groupId}")]
        public ActionResult<List<Post>> GetPostsByGroupId(long groupId)
        {
            try
            {
                return Ok(this.postService.GetPostsByGroupId(groupId));
            }
            catch (Exception ex)
            {
                return StatusCode(404, $"Error retrieving group's posts: {ex.Message}");
            }
        }

   
        [HttpGet("user/{userId}/homefeed")]
        public ActionResult<List<Post>> FetHomeFeed(long userId)
        {
            try
            {
                return Ok(this.postService.GetPostsHomeFeed(userId));
            }
            catch (Exception ex)
            {
                return StatusCode(404, $"Error retrieving home feed: {ex.Message}");
            }
        }

   
        [HttpGet("user/{userId}/groupfeed")]
        public ActionResult<List<Post>> GetGroupFeed(long userId)
        {
            try
            {
                return Ok(this.postService.GetPostsGroupsFeed(userId));
            }
            catch (Exception ex)
            {
                return StatusCode(404, $"Error retrieving group feed: {ex.Message}");
            }
        }


        [HttpPost]
        public IActionResult SavePost(Post post)
        {
            try
            {
                if (post == null)
                    return BadRequest("Post data cannot be null.");

                this.postService.SavePost(post);
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(404, $"Error saving post: {ex.Message}");
            }
        }



        [HttpGet("{postId}/reactions")]
        public ActionResult<List<Reaction>> GetReactionsByPost(long postId)
        {
            return this.reactionService.GetReactionsByPostId(postId);
        }



        [HttpGet("{postId}/user/{userId}/reaction")]
        public ActionResult<Reaction> GetUserPostReaction(long userId, long postId)
        {
            return this.reactionService.GetReaction(userId, postId);
        }


        [HttpDelete("{postId}/user/{userId}/reaction")]
        public IActionResult DeleteReaction(long postId, long userId)
        {
            this.reactionService.DeleteReaction(postId, userId);
            return Ok();
        }


        [HttpGet("{postId}/comments")]
        public ActionResult<List<Comment>> GetCommentsByPostId(long postId)
        {
            var comments = commentService.GetCommentsByPostId(postId);
            return Ok(comments);
        }
    }
}
