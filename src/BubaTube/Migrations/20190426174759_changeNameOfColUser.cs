using Microsoft.EntityFrameworkCore.Migrations;

namespace BubaTube.Migrations
{
    public partial class changeNameOfColUser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "DateOfRegistering",
                table: "AspNetUsers",
                newName: "RegisteredOn");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "RegisteredOn",
                table: "AspNetUsers",
                newName: "DateOfRegistering");
        }
    }
}
