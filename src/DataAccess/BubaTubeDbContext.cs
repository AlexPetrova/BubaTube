using Contracts.Data.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace DataAccess
{
    public class BubaTubeDbContext : IdentityDbContext<User>
    {
        public BubaTubeDbContext(DbContextOptions<BubaTubeDbContext> options)
            : base(options)
        {
        }

        public DbSet<Video> Videos { get; set; }

        public DbSet<Comment> Comments { get; set; }

        public DbSet<Category> Categories { get; set; }

        public DbSet<UserVideo> UserVideo { get; set; }

        public DbSet<VideoCategory> VideoCategory { get; set; }

        public DbSet<BlackList> BlackList { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<UserVideo>()
                .HasKey(x => new { x.UserId, x.VideoId });

            builder.Entity<UserVideo>()
                .HasOne<User>(x => x.User)
                .WithMany(x => x.UserVideo)
                .HasForeignKey(x => x.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<UserVideo>()
                .HasOne<Video>(x => x.Video)
                .WithMany(x => x.UserVideo)
                .HasForeignKey(x => x.VideoId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<VideoCategory>()
                 .HasKey(x => new { x.CategoryId, x.VideoId });

            builder.Entity<VideoCategory>()
                .HasOne(x => x.Video)
                .WithMany(x => x.VideoCategory)
                .HasForeignKey(x => x.VideoId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<VideoCategory>()
                .HasOne(x => x.Category)
                .WithMany(x => x.VideoCategory)
                .HasForeignKey(x => x.CategoryId)
                .OnDelete(DeleteBehavior.Restrict);

            base.OnModelCreating(builder);
        }
    }
}
