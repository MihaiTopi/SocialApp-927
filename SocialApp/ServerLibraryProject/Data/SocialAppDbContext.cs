namespace ServerLibraryProject.Data
{
    using ServerLibraryProject.DbRelationshipEntities;
    using ServerLibraryProject.Models;
    using Microsoft.EntityFrameworkCore;

    public class SocialAppDbContext : DbContext
    {
        public SocialAppDbContext(DbContextOptions<SocialAppDbContext> options)
            : base(options)
        {
        }


        public DbSet<Post> Posts { get; set; } = default!;

        public DbSet<Comment> Comments { get; set; } = default!;

        public DbSet<UserFollower> UserFollowers { get; set; } = default!;

        public DbSet<GroupUser> GroupUsers { get; set; } = default!;

        public DbSet<Group> Groups { get; set; } = default!;

        public DbSet<Reaction> Reactions { get; set; } = default!;

        public DbSet<User> Users { get; set; } = default!;



        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<GroupUser>()
                .HasKey(groupUser => new { groupUser.UserId, groupUser.GroupId });

            modelBuilder.Entity<UserFollower>()
                .HasKey(userFollower => new { userFollower.UserId, userFollower.FollowerId });

            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable("Users", tableBuilder =>
                {
                    tableBuilder.HasCheckConstraint("CK_User_Height_Positive", "[height] > 0");
                    tableBuilder.HasCheckConstraint("CK_User_Weight_Positive", "[weight] > 0");
                    tableBuilder.HasCheckConstraint("CK_User_Goal_Valid", "[goal] IN ('lose weight', 'mentain', 'gain muscles')");
                });

                entity.HasIndex(user => user.Username).IsUnique();
            });

            modelBuilder.Entity<Reaction>()
                .HasKey(reaction => new { reaction.UserId, reaction.PostId });

            modelBuilder.Entity<Reaction>()
                .Property(reaction => reaction.Type)
                .HasColumnName("ReactionType");
        }
    }
}