namespace DesktopProject.Proxies
{
    using System;
    using System.Collections.Generic;
    using System.Net.Http;
    using System.Net.Http.Json;
    using ServerAPIProject.DTO;
    using ServerLibraryProject.Enums;
    using ServerLibraryProject.Interfaces;
    using ServerLibraryProject.Models;

    public class ReactionServiceProxy : IServiceRepository
    {
        private readonly HttpClient httpClient;

        public ReactionRepositoryProxy()
        {
            this.httpClient = new HttpClient();
            this.httpClient.BaseAddress = new Uri("https://localhost:7106/reactions/");
        }




        public List<Reaction> GetReactionsByPostId(long postId)
        {
            var response = this.httpClient.GetAsync($"post/{postId}").Result!;

            if (response.IsSuccessStatusCode)
            {
                return response.Content.ReadFromJsonAsync<List<Reaction>>().Result ?? new List<Reaction>();
            }

            return new List<Reaction>();
        }

        public Reaction? GetReaction(long userId, long postId)
        {
            var response = this.httpClient.GetAsync($"{userId}/{postId}").Result;

            if (response.IsSuccessStatusCode)
            {
                var content = response.Content.ReadAsStringAsync().Result;
                if (!string.IsNullOrWhiteSpace(content))
                {
                    var reaction = response.Content.ReadFromJsonAsync<Reaction>().Result;
                    return reaction;
                }
            }

            return null;
        }

        public void Add(Reaction reaction)
        {
            this.httpClient.PostAsJsonAsync(string.Empty, reaction).Wait();
        }

    }
}
