using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace BubaTube.Migrations
{
    public partial class nameOfColInVideoTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Videos_AspNetUsers_UserId",
                table: "Videos");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "Videos",
                newName: "AuthorId");

            migrationBuilder.RenameIndex(
                name: "IX_Videos_UserId",
                table: "Videos",
                newName: "IX_Videos_AuthorId");

            migrationBuilder.AddForeignKey(
                name: "FK_Videos_AspNetUsers_AuthorId",
                table: "Videos",
                column: "AuthorId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Videos_AspNetUsers_AuthorId",
                table: "Videos");

            migrationBuilder.RenameColumn(
                name: "AuthorId",
                table: "Videos",
                newName: "UserId");

            migrationBuilder.RenameIndex(
                name: "IX_Videos_AuthorId",
                table: "Videos",
                newName: "IX_Videos_UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Videos_AspNetUsers_UserId",
                table: "Videos",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
