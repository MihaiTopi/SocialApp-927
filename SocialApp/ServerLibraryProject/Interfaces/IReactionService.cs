using ServerLibraryProject.Models;
using ServerLibraryProject.Enums;
using System.Collections.Generic;

namespace ServerLibraryProject.Interfaces
{
    public interface IReactionService
    {
        List<Reaction> GetAllReactions();

        List<Reaction> GetReactionsForPost(long postId);

        Reaction AddReaction(long userId, long postId, ReactionType type);

        void DeleteReaction(long userId, long postId);
    }
}