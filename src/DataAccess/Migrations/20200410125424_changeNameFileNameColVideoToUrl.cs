using Microsoft.EntityFrameworkCore.Migrations;

namespace BubaTube.Migrations
{
    public partial class changeNameFileNameColVideoToUrl : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FileName",
                table: "Videos");

            migrationBuilder.AddColumn<string>(
                name: "Url",
                table: "Videos",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Url",
                table: "Videos");

            migrationBuilder.AddColumn<string>(
                name: "FileName",
                table: "Videos",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
