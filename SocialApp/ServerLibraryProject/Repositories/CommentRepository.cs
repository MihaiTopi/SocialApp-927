using ServerLibraryProject.Data;
using ServerLibraryProject.Interfaces;
using ServerLibraryProject.Models;

namespace ServerLibraryProject.Repositories
{
    public class CommentRepository : ICommentRepository
    {
        private readonly SocialAppDbContext dbContext;

        public CommentRepository(SocialAppDbContext context)
        {
            dbContext = context;
        }

        /// <summary>
        /// Retrieves all comments from the database.
        /// </summary>
        /// <returns>A list of all Comment entities in the system.</returns>
        public List<Comment> GetAllComments()
        {
            return dbContext.Comments.ToList();
        }

        /// <summary>
        /// Retrieves all comments associated with a specific post.
        /// </summary>
        /// <param name="postId">The ID of the post to retrieve comments for.</param>
        /// <returns>A list of Comment entities for the specified post.</returns>
        public List<Comment> GetCommentsByPostId(long postId)
        {
            return dbContext.Comments.Where(c => c.PostId == postId).ToList();
        }

        /// <summary>
        /// Deletes a comment from the database by its ID.
        /// </summary>
        /// <param name="id">The ID of the comment to delete.</param>
        //public void DeleteCommentById(long id)
        //{
        //    try
        //    {
        //        var comment = dbContext.Comments.Find(id);
        //        if (comment != null)
        //        {
        //            dbContext.Comments.Remove(comment);
        //            dbContext.SaveChanges();
        //        }
        //    }
        //    catch
        //    {
        //        throw new Exception("Comment not found or could not be deleted.");
        //    }

        // }

        /// <summary>
        /// Retrieves a single comment by its ID.
        /// </summary>
        /// <param name="id">The ID of the comment to retrieve.</param>
        /// <returns>The Comment entity with the specified ID, or null if not found.</returns>
        //public Comment GetCommentById(long id)
        //{
        //    var comment = dbContext.Comments.Find(id);
        //    if(comment == null)
        //        throw new Exception("Comment not found.");
        //    return comment;
        //}

        /// <summary>
        /// Saves a new comment to the database.
        /// </summary>
        /// <param name="entity">The Comment entity to be saved.</param>
        public void SaveComment(Comment entity)
        {
            try
            {
                dbContext.Comments.Add(entity);
                dbContext.SaveChanges();
            }
            catch
            {
                throw new Exception("Error saving comment. Please try again later.");
            }
            
        }

        /// <summary>
        /// Updates the content of an existing comment.
        /// </summary>
        /// <param name="id">The ID of the comment to update.</param>
        /// <param name="content">The new content for the comment.</param>
        //public void UpdateCommentContentById(long id, string content)
        //{
        //    try
        //    {
        //        var comment = dbContext.Comments.Find(id);
        //        if (comment != null)
        //        {
        //            comment.Content = content;
        //            dbContext.SaveChanges();
        //        }
        //    }
        //    catch
        //    {
        //        throw new Exception("Comment not found or could not be updated.");

        //    }
        //}
    }
}