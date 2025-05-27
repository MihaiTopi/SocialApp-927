namespace ServerLibraryProject.Repositories
{
    using ServerLibraryProject.Data;
    using ServerLibraryProject.Interfaces;

    public class BodyMetricRepository : IBodyMetricRepository
    {
        private readonly SocialAppDbContext dbContext;

        public BodyMetricRepository(SocialAppDbContext context)
        {
            this.dbContext = context;
        }

        public void UpdateUserBodyMetrics(long userId, float weight, float height, float? targetWeight)
        {
            var user = this.dbContext.Users.Find(userId);
            if (user != null)
            {
                user.Weight = weight;
                user.Height = height;
                this.dbContext.SaveChanges();
            }
        }
    }
}