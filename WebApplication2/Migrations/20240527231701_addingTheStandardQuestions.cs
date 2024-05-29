using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApplication2.Migrations
{
    public partial class addingTheStandardQuestions : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Review_Course",
                table: "Review");

            migrationBuilder.AlterColumn<int>(
                name: "courseID",
                table: "Review",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Review_Course",
                table: "Review",
                column: "courseID",
                principalTable: "Course",
                principalColumn: "courseID",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Review_Course",
                table: "Review");

            migrationBuilder.AlterColumn<int>(
                name: "courseID",
                table: "Review",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_Review_Course",
                table: "Review",
                column: "courseID",
                principalTable: "Course",
                principalColumn: "courseID");
        }
    }
}
