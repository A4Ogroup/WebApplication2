using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApplication2.Migrations
{
    public partial class Userinterests : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserInterests_SubCategory_SubsSubId",
                table: "UserInterests");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UserInterests",
                table: "UserInterests");

            migrationBuilder.DropIndex(
                name: "IX_UserInterests_SubsSubId",
                table: "UserInterests");

            migrationBuilder.DropColumn(
                name: "SubsSubId",
                table: "UserInterests");

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserInterests",
                table: "UserInterests",
                columns: new[] { "StudentId", "SubId" });

            migrationBuilder.CreateIndex(
                name: "IX_UserInterests_SubId",
                table: "UserInterests",
                column: "SubId");

            migrationBuilder.AddForeignKey(
                name: "FK_UserInterests_SubCategory_SubId",
                table: "UserInterests",
                column: "SubId",
                principalTable: "SubCategory",
                principalColumn: "subID",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserInterests_SubCategory_SubId",
                table: "UserInterests");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UserInterests",
                table: "UserInterests");

            migrationBuilder.DropIndex(
                name: "IX_UserInterests_SubId",
                table: "UserInterests");

            migrationBuilder.AddColumn<int>(
                name: "SubsSubId",
                table: "UserInterests",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserInterests",
                table: "UserInterests",
                columns: new[] { "StudentId", "SubsSubId" });

            migrationBuilder.CreateIndex(
                name: "IX_UserInterests_SubsSubId",
                table: "UserInterests",
                column: "SubsSubId");

            migrationBuilder.AddForeignKey(
                name: "FK_UserInterests_SubCategory_SubsSubId",
                table: "UserInterests",
                column: "SubsSubId",
                principalTable: "SubCategory",
                principalColumn: "subID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
