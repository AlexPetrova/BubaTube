using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace BubaTube.Migrations
{
    public partial class VideoCategory : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserVideo_AspNetUsers_UserId",
                table: "UserVideo");

            migrationBuilder.DropForeignKey(
                name: "FK_UserVideo_Videos_VideoId",
                table: "UserVideo");

            migrationBuilder.DropColumn(
                name: "Category",
                table: "Videos");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "Videos",
                newName: "Path");

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "Videos",
                maxLength: 500,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Title",
                table: "Videos",
                maxLength: 200,
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateTable(
                name: "Category",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CategoryName = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Category", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CategoryVideo",
                columns: table => new
                {
                    CategoryId = table.Column<int>(nullable: false),
                    VideoId = table.Column<int>(nullable: false),
                    CategoryId1 = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CategoryVideo", x => new { x.CategoryId, x.VideoId });
                    table.ForeignKey(
                        name: "FK_CategoryVideo_Category_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Category",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CategoryVideo_Category_CategoryId1",
                        column: x => x.CategoryId1,
                        principalTable: "Category",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CategoryVideo_Videos_VideoId",
                        column: x => x.VideoId,
                        principalTable: "Videos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CategoryVideo_CategoryId1",
                table: "CategoryVideo",
                column: "CategoryId1");

            migrationBuilder.CreateIndex(
                name: "IX_CategoryVideo_VideoId",
                table: "CategoryVideo",
                column: "VideoId");

            migrationBuilder.AddForeignKey(
                name: "FK_UserVideo_AspNetUsers_UserId",
                table: "UserVideo",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_UserVideo_Videos_VideoId",
                table: "UserVideo",
                column: "VideoId",
                principalTable: "Videos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserVideo_AspNetUsers_UserId",
                table: "UserVideo");

            migrationBuilder.DropForeignKey(
                name: "FK_UserVideo_Videos_VideoId",
                table: "UserVideo");

            migrationBuilder.DropTable(
                name: "CategoryVideo");

            migrationBuilder.DropTable(
                name: "Category");

            migrationBuilder.DropColumn(
                name: "Description",
                table: "Videos");

            migrationBuilder.DropColumn(
                name: "Title",
                table: "Videos");

            migrationBuilder.RenameColumn(
                name: "Path",
                table: "Videos",
                newName: "Name");

            migrationBuilder.AddColumn<int>(
                name: "Category",
                table: "Videos",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddForeignKey(
                name: "FK_UserVideo_AspNetUsers_UserId",
                table: "UserVideo",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserVideo_Videos_VideoId",
                table: "UserVideo",
                column: "VideoId",
                principalTable: "Videos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
