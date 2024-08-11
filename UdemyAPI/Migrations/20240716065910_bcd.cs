using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace UdemyAPI.Migrations
{
    /// <inheritdoc />
    public partial class bcd : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "RegisteredDate",
                table: "Users",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RegisteredDate",
                table: "Users");
        }
    }
}
