using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApplication2.Migrations
{
    public partial class m1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ContentQuality",
                table: "Review",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "EngagementLevel",
                table: "Review",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "MaterialQuality",
                table: "Review",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "OverAllSatisfication",
                table: "Review",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "SupportQuality",
                table: "Review",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "TechnicalQuality",
                table: "Review",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ContentQuality",
                table: "Review");

            migrationBuilder.DropColumn(
                name: "EngagementLevel",
                table: "Review");

            migrationBuilder.DropColumn(
                name: "MaterialQuality",
                table: "Review");

            migrationBuilder.DropColumn(
                name: "OverAllSatisfication",
                table: "Review");

            migrationBuilder.DropColumn(
                name: "SupportQuality",
                table: "Review");

            migrationBuilder.DropColumn(
                name: "TechnicalQuality",
                table: "Review");
        }
    }
}
