using ServerLibraryProject.Interfaces;
using ServerLibraryProject.Models;
using Microsoft.AspNetCore.Mvc;

namespace Server.Controllers
{
    [ApiController]
    [Route("api/comments")]
    public class CommentController : ControllerBase
    {
        private readonly ICommentService commentService;

        public CommentController(ICommentService commentService)
        {
            this.commentService = commentService;
        }

     
        [HttpGet]
        public ActionResult<List<Comment>> GetAllComments()
        {

            var comments = commentService.GetAllComments();
            return Ok(comments);
        }

   
        //[HttpGet("{id}")]
        //public ActionResult<Comment> GetCommentById(long id)
        //{
        //    var comment = commentService.GetCommentById((int)id);
        //    if (comment == null)
        //    {
        //        return NotFound($"Comment with ID {id} not found.");
        //    }
        //    return Ok(comment);
        //}



        [HttpPost]
        public IActionResult SaveComment([FromBody] Comment comment)
        {
            if (comment == null)
            {
                return BadRequest("Comment cannot be null.");
            }
            return Ok(commentService.AddComment(comment.Content, comment.UserId, comment.PostId));
        }


    }
}
