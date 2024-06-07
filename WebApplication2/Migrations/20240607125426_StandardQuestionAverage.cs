using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApplication2.Migrations
{
    public partial class StandardQuestionAverage : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "AverageContentQuality",
                table: "Course",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "AverageEngagementLevel",
                table: "Course",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "AverageMaterialQuality",
                table: "Course",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "AverageOverallSatisfaction",
                table: "Course",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "AverageSupportQuality",
                table: "Course",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "AverageTechnicalQuality",
                table: "Course",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AverageContentQuality",
                table: "Course");

            migrationBuilder.DropColumn(
                name: "AverageEngagementLevel",
                table: "Course");

            migrationBuilder.DropColumn(
                name: "AverageMaterialQuality",
                table: "Course");

            migrationBuilder.DropColumn(
                name: "AverageOverallSatisfaction",
                table: "Course");

            migrationBuilder.DropColumn(
                name: "AverageSupportQuality",
                table: "Course");

            migrationBuilder.DropColumn(
                name: "AverageTechnicalQuality",
                table: "Course");
        }
    }
}
