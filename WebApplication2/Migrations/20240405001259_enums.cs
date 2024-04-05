using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApplication2.Migrations
{
    public partial class enums : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "priceStatus",
                table: "Course",
                type: "int",
                nullable: true,
                oldClrType: typeof(bool),
                oldType: "bit",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "level",
                table: "Course",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(byte),
                oldType: "tinyint",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "claimed",
                table: "Course",
                type: "int",
                nullable: true,
                oldClrType: typeof(bool),
                oldType: "bit",
                oldNullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<bool>(
                name: "priceStatus",
                table: "Course",
                type: "bit",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<byte>(
                name: "level",
                table: "Course",
                type: "tinyint",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<bool>(
                name: "claimed",
                table: "Course",
                type: "bit",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);
        }
    }
}
