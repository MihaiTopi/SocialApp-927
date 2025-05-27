using ServerLibraryProject.Models;
using System.Collections.Generic;

namespace ServerLibraryProject.Interfaces
{
    public interface ICommentService
    {

        List<Comment> GetAllComments();
        Comment GetCommentById(int commentId);
        List<Comment> GetCommentsByPostId(long postId);
        Comment AddComment(string content, long userId, long postId);
        void DeleteComment(long commentId);
        void UpdateComment(long commentId, string content);
    }
}