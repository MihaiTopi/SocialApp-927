namespace Server.Controllers
{
    using ServerLibraryProject.Interfaces;
    using ServerLibraryProject.Models;
    using Microsoft.AspNetCore.Mvc;

    /// <summary>
    /// The controller that manages the reactions.
    /// </summary>
    [ApiController]
    [Route("api/reactions")]
    public class ReactionController : ControllerBase
    {
        private readonly IReactionService reactionService;


        public ReactionController(IReactionService reactionService)
        {
            this.reactionService = reactionService;
        }

        [HttpGet]
        public ActionResult<List<Reaction>> GetAllReactions()
        {
            return this.reactionService.GetAllReactions();
        }

        [HttpPost("reactions")]
        public IActionResult SaveReaction([FromBody] Reaction entity)
        {
            this.reactionService.AddReaction(entity);
            return Ok();
        }

        [HttpPut("reactions")]
        public IActionResult UpdateReaction([FromBody] Reaction entity)
        {
            this.reactionService.UpdateReaction(entity);
            return Ok();
        }
    }
}