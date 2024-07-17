using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApplication2.Migrations
{
    public partial class exiplcitilyaddinUserInterestsModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserInterests_Student",
                table: "UserInterests");

            migrationBuilder.DropForeignKey(
                name: "FK_UserInterests_SubCategory",
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

            migrationBuilder.AlterColumn<byte>(
                name: "contributionNum",
                table: "Student",
                type: "tinyint",
                nullable: false,
                defaultValue: (byte)0,
                oldClrType: typeof(byte),
                oldType: "tinyint",
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserInterests",
                table: "UserInterests",
                columns: new[] { "StudentId", "SubsSubId" });

            migrationBuilder.CreateIndex(
                name: "IX_UserInterests_SubsSubId",
                table: "UserInterests",
                column: "SubsSubId");

            migrationBuilder.AddForeignKey(
                name: "FK_UserInterests_Student_StudentId",
                table: "UserInterests",
                column: "StudentId",
                principalTable: "Student",
                principalColumn: "studentID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserInterests_SubCategory_SubsSubId",
                table: "UserInterests",
                column: "SubsSubId",
                principalTable: "SubCategory",
                principalColumn: "subID",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserInterests_Student_StudentId",
                table: "UserInterests");

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

            migrationBuilder.AlterColumn<byte>(
                name: "contributionNum",
                table: "Student",
                type: "tinyint",
                nullable: true,
                oldClrType: typeof(byte),
                oldType: "tinyint");

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserInterests",
                table: "UserInterests",
                columns: new[] { "StudentId", "SubId" });

            migrationBuilder.CreateIndex(
                name: "IX_UserInterests_SubId",
                table: "UserInterests",
                column: "SubId");

            migrationBuilder.AddForeignKey(
                name: "FK_UserInterests_Student",
                table: "UserInterests",
                column: "StudentId",
                principalTable: "Student",
                principalColumn: "studentID");

            migrationBuilder.AddForeignKey(
                name: "FK_UserInterests_SubCategory",
                table: "UserInterests",
                column: "SubId",
                principalTable: "SubCategory",
                principalColumn: "subID");
        }
    }
}
