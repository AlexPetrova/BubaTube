﻿// <auto-generated />
using System;
using DataAccess;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;

namespace BubaTube.Migrations
{
    [DbContext(typeof(BubaTubeDbContext))]
    partial class BubaTubeDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.0-rtm-35687")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("BubaTube.Data.Models.BlackList", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("AddedById");

                    b.Property<DateTime>("AddedOn");

                    b.Property<string>("Email");

                    b.Property<string>("IP");

                    b.Property<string>("Reason");

                    b.HasKey("Id");

                    b.HasIndex("AddedById");

                    b.ToTable("BlackList");
                });

            modelBuilder.Entity("BubaTube.Data.Models.Category", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("CategoryName")
                        .IsRequired()
                        .HasMaxLength(100);

                    b.Property<bool>("IsАpproved");

                    b.HasKey("Id");

                    b.ToTable("Category");
                });

            modelBuilder.Entity("BubaTube.Data.Models.Comment", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int?>("CommentId");

                    b.Property<string>("Content")
                        .IsRequired()
                        .HasMaxLength(1000);

                    b.Property<double>("Likes");

                    b.Property<int>("UserId");

                    b.Property<string>("UserId1");

                    b.Property<int?>("VideoId");

                    b.HasKey("Id");

                    b.HasIndex("CommentId");

                    b.HasIndex("UserId1");

                    b.HasIndex("VideoId");

                    b.ToTable("Comments");
                });

            modelBuilder.Entity("BubaTube.Data.Models.User", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("AccessFailedCount");

                    b.Property<byte[]>("AvatarImage");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<string>("Email")
                        .HasMaxLength(256);

                    b.Property<bool>("EmailConfirmed");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasMaxLength(15);

                    b.Property<DateTime>("LastLogin");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasMaxLength(15);

                    b.Property<bool>("LockoutEnabled");

                    b.Property<DateTimeOffset?>("LockoutEnd");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256);

                    b.Property<string>("PasswordHash");

                    b.Property<string>("PhoneNumber");

                    b.Property<bool>("PhoneNumberConfirmed");

                    b.Property<DateTime>("RegisteredOn");

                    b.Property<string>("SecurityStamp");

                    b.Property<bool>("TwoFactorEnabled");

                    b.Property<string>("UserId");

                    b.Property<string>("UserName")
                        .HasMaxLength(256);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasName("UserNameIndex")
                        .HasFilter("[NormalizedUserName] IS NOT NULL");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUsers");
                });

            modelBuilder.Entity("BubaTube.Data.Models.UserVideo", b =>
                {
                    b.Property<string>("UserId");

                    b.Property<int>("VideoId");

                    b.HasKey("UserId", "VideoId");

                    b.HasIndex("VideoId");

                    b.ToTable("UserVideo");
                });

            modelBuilder.Entity("BubaTube.Data.Models.Video", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("AuthorId")
                        .IsRequired();

                    b.Property<string>("Description")
                        .HasMaxLength(500);

                    b.Property<bool>("IsАpproved");

                    b.Property<double>("Likes");

                    b.Property<string>("Path")
                        .IsRequired();

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(200);

                    b.HasKey("Id");

                    b.HasIndex("AuthorId");

                    b.ToTable("Videos");
                });

            modelBuilder.Entity("BubaTube.Data.Models.VideoCategory", b =>
                {
                    b.Property<int>("CategoryId");

                    b.Property<int>("VideoId");

                    b.HasKey("CategoryId", "VideoId");

                    b.HasIndex("VideoId");

                    b.ToTable("VideoCategory");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRole", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<string>("Name")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasName("RoleNameIndex")
                        .HasFilter("[NormalizedName] IS NOT NULL");

                    b.ToTable("AspNetRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<string>("RoleId")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<string>("UserId")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider");

                    b.Property<string>("ProviderKey");

                    b.Property<string>("ProviderDisplayName");

                    b.Property<string>("UserId")
                        .IsRequired();

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId");

                    b.Property<string>("RoleId");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId");

                    b.Property<string>("LoginProvider");

                    b.Property<string>("Name");

                    b.Property<string>("Value");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens");
                });

            modelBuilder.Entity("BubaTube.Data.Models.BlackList", b =>
                {
                    b.HasOne("BubaTube.Data.Models.User", "AddedBy")
                        .WithMany()
                        .HasForeignKey("AddedById");
                });

            modelBuilder.Entity("BubaTube.Data.Models.Comment", b =>
                {
                    b.HasOne("BubaTube.Data.Models.Comment")
                        .WithMany("Comments")
                        .HasForeignKey("CommentId");

                    b.HasOne("BubaTube.Data.Models.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId1");

                    b.HasOne("BubaTube.Data.Models.Video")
                        .WithMany("Comments")
                        .HasForeignKey("VideoId");
                });

            modelBuilder.Entity("BubaTube.Data.Models.User", b =>
                {
                    b.HasOne("BubaTube.Data.Models.User")
                        .WithMany("FavouriteUsers")
                        .HasForeignKey("UserId");
                });

            modelBuilder.Entity("BubaTube.Data.Models.UserVideo", b =>
                {
                    b.HasOne("BubaTube.Data.Models.User", "User")
                        .WithMany("UserVideo")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("BubaTube.Data.Models.Video", "Video")
                        .WithMany("UserVideo")
                        .HasForeignKey("VideoId")
                        .OnDelete(DeleteBehavior.Restrict);
                });

            modelBuilder.Entity("BubaTube.Data.Models.Video", b =>
                {
                    b.HasOne("BubaTube.Data.Models.User", "Author")
                        .WithMany("UploadedVideos")
                        .HasForeignKey("AuthorId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("BubaTube.Data.Models.VideoCategory", b =>
                {
                    b.HasOne("BubaTube.Data.Models.Category", "Category")
                        .WithMany("VideoCategory")
                        .HasForeignKey("CategoryId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("BubaTube.Data.Models.Video", "Video")
                        .WithMany("VideoCategory")
                        .HasForeignKey("VideoId")
                        .OnDelete(DeleteBehavior.Restrict);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole")
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("BubaTube.Data.Models.User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("BubaTube.Data.Models.User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole")
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("BubaTube.Data.Models.User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.HasOne("BubaTube.Data.Models.User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
