using BubaTube.Data.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace BubaTube.Data
{
    public class BubaTubeDbContext : IdentityDbContext<User>
    {
        public BubaTubeDbContext(DbContextOptions<BubaTubeDbContext> options)
            : base(options)
        {
        }

        public DbSet<Video> Videos { get; set; }

        public DbSet<Comment> Comments { get; set; }

        public DbSet<UserVideo> UserVideo { get; set; }

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

            //builder.Entity<Video>()
            //    .HasOne<User>(x => x.Author)
            //    .WithMany(x => x.UploadedVideos)
            //    .HasForeignKey(x => x.UserId)
            //    .OnDelete(DeleteBehavior.Restrict);

            base.OnModelCreating(builder);
        }
    }
}
