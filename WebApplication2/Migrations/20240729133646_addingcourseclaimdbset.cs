using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApplication2.Migrations
{
    public partial class addingcourseclaimdbset : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CourseClaimReport_Course_CourseId",
                table: "CourseClaimReport");

            migrationBuilder.DropForeignKey(
                name: "FK_CourseClaimReport_Instructor_InstructorId",
                table: "CourseClaimReport");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CourseClaimReport",
                table: "CourseClaimReport");

            migrationBuilder.RenameTable(
                name: "CourseClaimReport",
                newName: "CourseClaimReports");

            migrationBuilder.RenameIndex(
                name: "IX_CourseClaimReport_InstructorId",
                table: "CourseClaimReports",
                newName: "IX_CourseClaimReports_InstructorId");

            migrationBuilder.RenameIndex(
                name: "IX_CourseClaimReport_CourseId",
                table: "CourseClaimReports",
                newName: "IX_CourseClaimReports_CourseId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CourseClaimReports",
                table: "CourseClaimReports",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_CourseClaimReports_Course_CourseId",
                table: "CourseClaimReports",
                column: "CourseId",
                principalTable: "Course",
                principalColumn: "courseID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CourseClaimReports_Instructor_InstructorId",
                table: "CourseClaimReports",
                column: "InstructorId",
                principalTable: "Instructor",
                principalColumn: "instructorID",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CourseClaimReports_Course_CourseId",
                table: "CourseClaimReports");

            migrationBuilder.DropForeignKey(
                name: "FK_CourseClaimReports_Instructor_InstructorId",
                table: "CourseClaimReports");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CourseClaimReports",
                table: "CourseClaimReports");

            migrationBuilder.RenameTable(
                name: "CourseClaimReports",
                newName: "CourseClaimReport");

            migrationBuilder.RenameIndex(
                name: "IX_CourseClaimReports_InstructorId",
                table: "CourseClaimReport",
                newName: "IX_CourseClaimReport_InstructorId");

            migrationBuilder.RenameIndex(
                name: "IX_CourseClaimReports_CourseId",
                table: "CourseClaimReport",
                newName: "IX_CourseClaimReport_CourseId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CourseClaimReport",
                table: "CourseClaimReport",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_CourseClaimReport_Course_CourseId",
                table: "CourseClaimReport",
                column: "CourseId",
                principalTable: "Course",
                principalColumn: "courseID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CourseClaimReport_Instructor_InstructorId",
                table: "CourseClaimReport",
                column: "InstructorId",
                principalTable: "Instructor",
                principalColumn: "instructorID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
