namespace ServerLibraryProject.Services
{
    using System;
    using System.Collections.Generic;
    using ServerLibraryProject.Models;
    using ServerLibraryProject.Enums;
    using ServerLibraryProject.Interfaces;

    public class ReactionService : IReactionService
    {
        private readonly IReactionRepository reactionRepository;

        public ReactionService(IReactionRepository reactionRepository)
        {
            this.reactionRepository = reactionRepository;
        }

        public void AddReaction(Reaction reaction)
        {
            if (reactionRepository.GetReaction(reaction.UserId, reaction.PostId) != null)
            {
                if (reactionRepository.GetReaction(reaction.UserId, reaction.PostId).Type == reaction.Type)
                {
                    reactionRepository.Delete(reaction.UserId, reaction.PostId);
                }
                else
                {
                    reactionRepository.Update(reaction.UserId, reaction.PostId, reaction.Type);
                }
            }
            else
            {
                reactionRepository.Add(reaction);
            }
        }


        public void DeleteReaction(long postId, long userId)
        {
            Reaction reaction = reactionRepository.GetReaction(userId, postId);
            if (reaction == null)
            {
                throw new Exception("Reaction does not exist");
            }

            reactionRepository.Delete(userId, postId);
        }

        public List<Reaction> GetAllReactions()
        {
            return reactionRepository.GetAllReactions();
        }

        public List<Reaction> GetReactionsByPostId(long postId)
        {
            return reactionRepository.GetReactionsByPostId(postId);
        }

        public Reaction GetReaction(long userId, long postId)
        {
            return this.reactionRepository.GetReaction(userId, postId);
        }

        public void UpdateReaction(Reaction reaction)
        {
            this.reactionRepository.Update(reaction.UserId, reaction.PostId, reaction.Type);
        }
    }
}
