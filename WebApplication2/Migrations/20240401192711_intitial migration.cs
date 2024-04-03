using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApplication2.Migrations
{
    public partial class intitialmigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Category",
                columns: table => new
                {
                    categoryID = table.Column<byte>(type: "tinyint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    status = table.Column<bool>(type: "bit", nullable: true),
                    categoryName = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Category", x => x.categoryID);
                });

            migrationBuilder.CreateTable(
                name: "Language",
                columns: table => new
                {
                    languageID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Language", x => x.languageID);
                });

            migrationBuilder.CreateTable(
                name: "User",
                columns: table => new
                {
                    userID = table.Column<string>(type: "nvarchar(400)", maxLength: 400, nullable: false),
                    firstName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    lastName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    password = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    pic = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    registerDate = table.Column<DateTime>(type: "date", nullable: true),
                    status = table.Column<bool>(type: "bit", nullable: true),
                    userType = table.Column<byte>(type: "tinyint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.userID);
                });

            migrationBuilder.CreateTable(
                name: "SubCategory",
                columns: table => new
                {
                    subID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    subName = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: true),
                    categoryID = table.Column<byte>(type: "tinyint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SubCategory", x => x.subID);
                    table.ForeignKey(
                        name: "FK_SubCategory_Category",
                        column: x => x.categoryID,
                        principalTable: "Category",
                        principalColumn: "categoryID");
                });

            migrationBuilder.CreateTable(
                name: "Instructor",
                columns: table => new
                {
                    instructorID = table.Column<string>(type: "nvarchar(400)", maxLength: 400, nullable: false),
                    countryCode = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    phoneNumber = table.Column<int>(type: "int", nullable: true),
                    profession = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    yearsExperince = table.Column<byte>(type: "tinyint", nullable: true),
                    about = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Instructor", x => x.instructorID);
                    table.ForeignKey(
                        name: "FK_Instructor_User",
                        column: x => x.instructorID,
                        principalTable: "User",
                        principalColumn: "userID");
                });

            migrationBuilder.CreateTable(
                name: "Student",
                columns: table => new
                {
                    studentID = table.Column<string>(type: "nvarchar(400)", maxLength: 400, nullable: false),
                    contributionNum = table.Column<byte>(type: "tinyint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Student", x => x.studentID);
                    table.ForeignKey(
                        name: "FK_Student_User",
                        column: x => x.studentID,
                        principalTable: "User",
                        principalColumn: "userID");
                });

            migrationBuilder.CreateTable(
                name: "Course",
                columns: table => new
                {
                    courseID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AddedByUserId = table.Column<string>(type: "nvarchar(400)", maxLength: 400, nullable: true),
                    courseDescription = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    topicsCovered = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    categoryID = table.Column<byte>(type: "tinyint", nullable: true),
                    subcategoryID = table.Column<int>(type: "int", nullable: true),
                    platform = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: true),
                    languageID = table.Column<int>(type: "int", nullable: true),
                    level = table.Column<byte>(type: "tinyint", nullable: true),
                    priceStatus = table.Column<bool>(type: "bit", nullable: true),
                    lastUpdate = table.Column<DateTime>(type: "date", nullable: true),
                    addingDate = table.Column<DateTime>(type: "date", nullable: true),
                    picture = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    averageRating = table.Column<int>(type: "int", nullable: true),
                    claimed = table.Column<bool>(type: "bit", nullable: true),
                    status = table.Column<bool>(type: "bit", nullable: true),
                    instructorID = table.Column<string>(type: "nvarchar(400)", maxLength: 400, nullable: true),
                    instructorFullName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    vedioLenght = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true),
                    courseDuration = table.Column<string>(type: "varchar(10)", unicode: false, maxLength: 10, nullable: true),
                    link = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Course", x => x.courseID);
                    table.ForeignKey(
                        name: "FK_Course_Language",
                        column: x => x.languageID,
                        principalTable: "Language",
                        principalColumn: "languageID");
                    table.ForeignKey(
                        name: "FK_Course_SubCategory",
                        column: x => x.subcategoryID,
                        principalTable: "SubCategory",
                        principalColumn: "subID");
                    table.ForeignKey(
                        name: "FK_Course_User_AddedByUserId",
                        column: x => x.AddedByUserId,
                        principalTable: "User",
                        principalColumn: "userID");
                    table.ForeignKey(
                        name: "FK_Course_User_InstructroID",
                        column: x => x.instructorID,
                        principalTable: "Instructor",
                        principalColumn: "instructorID");
                });

            migrationBuilder.CreateTable(
                name: "SocialMediaAccount",
                columns: table => new
                {
                    accountID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    account = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    instructorID = table.Column<string>(type: "nvarchar(400)", maxLength: 400, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SocialMediaAccount", x => x.accountID);
                    table.ForeignKey(
                        name: "FK_SocialMediaAccount_Instructor",
                        column: x => x.instructorID,
                        principalTable: "Instructor",
                        principalColumn: "instructorID");
                });

            migrationBuilder.CreateTable(
                name: "UserInterests",
                columns: table => new
                {
                    studentID = table.Column<string>(type: "nvarchar(400)", maxLength: 400, nullable: false),
                    subID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserInterests", x => new { x.studentID, x.subID });
                    table.ForeignKey(
                        name: "FK_UserInterests_Student",
                        column: x => x.studentID,
                        principalTable: "Student",
                        principalColumn: "studentID");
                    table.ForeignKey(
                        name: "FK_UserInterests_SubCategory",
                        column: x => x.subID,
                        principalTable: "SubCategory",
                        principalColumn: "subID");
                });

            migrationBuilder.CreateTable(
                name: "Review",
                columns: table => new
                {
                    reviewID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Descritipn = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    status = table.Column<bool>(type: "bit", nullable: true),
                    rate = table.Column<byte>(type: "tinyint", nullable: true),
                    ratingDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    courseID = table.Column<int>(type: "int", nullable: true),
                    studentID = table.Column<string>(type: "nvarchar(400)", maxLength: 400, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Review", x => x.reviewID);
                    table.ForeignKey(
                        name: "FK_Review_Course",
                        column: x => x.courseID,
                        principalTable: "Course",
                        principalColumn: "courseID");
                    table.ForeignKey(
                        name: "FK_Review_Student",
                        column: x => x.studentID,
                        principalTable: "Student",
                        principalColumn: "studentID");
                });

            migrationBuilder.CreateTable(
                name: "Report",
                columns: table => new
                {
                    reportID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    message = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ReportDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    reviewID = table.Column<int>(type: "int", nullable: true),
                    status = table.Column<bool>(type: "bit", nullable: false),
                    studentID = table.Column<string>(type: "nvarchar(400)", maxLength: 400, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Report", x => x.reportID);
                    table.ForeignKey(
                        name: "FK_Report_Review",
                        column: x => x.reviewID,
                        principalTable: "Review",
                        principalColumn: "reviewID");
                    table.ForeignKey(
                        name: "FK_Report_Student",
                        column: x => x.studentID,
                        principalTable: "Student",
                        principalColumn: "studentID");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Course_AddedByUserId",
                table: "Course",
                column: "AddedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Course_instructorID",
                table: "Course",
                column: "instructorID");

            migrationBuilder.CreateIndex(
                name: "IX_Course_languageID",
                table: "Course",
                column: "languageID");

            migrationBuilder.CreateIndex(
                name: "IX_Course_subcategoryID",
                table: "Course",
                column: "subcategoryID");

            migrationBuilder.CreateIndex(
                name: "IX_Report_reviewID",
                table: "Report",
                column: "reviewID");

            migrationBuilder.CreateIndex(
                name: "IX_Report_studentID",
                table: "Report",
                column: "studentID");

            migrationBuilder.CreateIndex(
                name: "IX_Review_courseID",
                table: "Review",
                column: "courseID");

            migrationBuilder.CreateIndex(
                name: "IX_Review_studentID",
                table: "Review",
                column: "studentID");

            migrationBuilder.CreateIndex(
                name: "IX_SocialMediaAccount_instructorID",
                table: "SocialMediaAccount",
                column: "instructorID");

            migrationBuilder.CreateIndex(
                name: "IX_SubCategory_categoryID",
                table: "SubCategory",
                column: "categoryID");

            migrationBuilder.CreateIndex(
                name: "IX_UserInterests_subID",
                table: "UserInterests",
                column: "subID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Report");

            migrationBuilder.DropTable(
                name: "SocialMediaAccount");

            migrationBuilder.DropTable(
                name: "UserInterests");

            migrationBuilder.DropTable(
                name: "Review");

            migrationBuilder.DropTable(
                name: "Course");

            migrationBuilder.DropTable(
                name: "Student");

            migrationBuilder.DropTable(
                name: "Language");

            migrationBuilder.DropTable(
                name: "SubCategory");

            migrationBuilder.DropTable(
                name: "Instructor");

            migrationBuilder.DropTable(
                name: "Category");

            migrationBuilder.DropTable(
                name: "User");
        }
    }
}
