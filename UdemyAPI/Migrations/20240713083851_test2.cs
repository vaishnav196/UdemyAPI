using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace UdemyAPI.Migrations
{
    /// <inheritdoc />
    public partial class test2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CourseTopic",
                table: "addCourseContents");

            migrationBuilder.RenameColumn(
                name: "CourseCategoryName",
                table: "addCourseCategories",
                newName: "InstructorName");

            migrationBuilder.AddColumn<string>(
                name: "Question",
                table: "addCourseContents",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "SubCourse",
                table: "addCourseContents",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<double>(
                name: "Price",
                table: "addCourseCategories",
                type: "float",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)");

            migrationBuilder.AddColumn<string>(
                name: "Duration",
                table: "addCourseCategories",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateTable(
                name: "quizs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CourseName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SubCourse = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Question = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Option = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Ans = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_quizs", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "quizs");

            migrationBuilder.DropColumn(
                name: "Question",
                table: "addCourseContents");

            migrationBuilder.DropColumn(
                name: "SubCourse",
                table: "addCourseContents");

            migrationBuilder.DropColumn(
                name: "Duration",
                table: "addCourseCategories");

            migrationBuilder.RenameColumn(
                name: "InstructorName",
                table: "addCourseCategories",
                newName: "CourseCategoryName");

            migrationBuilder.AddColumn<string>(
                name: "CourseTopic",
                table: "addCourseContents",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "Price",
                table: "addCourseCategories",
                type: "decimal(18,2)",
                nullable: false,
                oldClrType: typeof(double),
                oldType: "float");
        }
    }
}
