using ServerLibraryProject.Models;
using ServerLibraryProject.Enums;
using System.Collections.Generic;

namespace ServerLibraryProject.Interfaces
{
    public interface IReactionService
    {
        List<Reaction> GetAllReactions();

        List<Reaction> GetReactionsByPostId(long postId);

        void AddReaction(Reaction reaction);

        void UpdateReaction(Reaction reaction);

        void DeleteReaction(long userId, long postId);

        Reaction GetReaction(long userId, long postId);
    }
}