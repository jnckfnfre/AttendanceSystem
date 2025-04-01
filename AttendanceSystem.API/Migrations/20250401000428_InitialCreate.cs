using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AttendanceSystem.API.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Course",
                columns: table => new
                {
                    COURSE_ID = table.Column<string>(type: "varchar(255)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    COURSE_NAME = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    START_TIME = table.Column<TimeSpan>(type: "time(6)", nullable: false),
                    END_TIME = table.Column<TimeSpan>(type: "time(6)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Course", x => x.COURSE_ID);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Question_Pool",
                columns: table => new
                {
                    POOL_ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    POOL_NAME = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Question_Pool", x => x.POOL_ID);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Student",
                columns: table => new
                {
                    UTD_ID = table.Column<string>(type: "varchar(255)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    FIRST_NAME = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    LAST_NAME = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    NET_ID = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Student", x => x.UTD_ID);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Quiz",
                columns: table => new
                {
                    QUIZ_ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    DUE_DATE = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    POOL_ID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Quiz", x => x.QUIZ_ID);
                    table.ForeignKey(
                        name: "FK_Quiz_Question_Pool_POOL_ID",
                        column: x => x.POOL_ID,
                        principalTable: "Question_Pool",
                        principalColumn: "POOL_ID",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "class_session",
                columns: table => new
                {
                    SESSION_DATE = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    COURSE_ID = table.Column<string>(type: "varchar(255)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    PASSWORD = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    QUIZ_ID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_class_session", x => new { x.SESSION_DATE, x.COURSE_ID });
                    table.ForeignKey(
                        name: "FK_class_session_Course_COURSE_ID",
                        column: x => x.COURSE_ID,
                        principalTable: "Course",
                        principalColumn: "COURSE_ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_class_session_Quiz_QUIZ_ID",
                        column: x => x.QUIZ_ID,
                        principalTable: "Quiz",
                        principalColumn: "QUIZ_ID",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Questions",
                columns: table => new
                {
                    QUESTIONS_ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    OPTION_A = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    OPTION_B = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    OPTION_C = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    OPTION_D = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    CORRECT_ANSWER = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    QUIZ_ID = table.Column<int>(type: "int", nullable: false),
                    POOL_ID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Questions", x => x.QUESTIONS_ID);
                    table.ForeignKey(
                        name: "FK_Questions_Question_Pool_POOL_ID",
                        column: x => x.POOL_ID,
                        principalTable: "Question_Pool",
                        principalColumn: "POOL_ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Questions_Quiz_QUIZ_ID",
                        column: x => x.QUIZ_ID,
                        principalTable: "Quiz",
                        principalColumn: "QUIZ_ID",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Attended_By",
                columns: table => new
                {
                    ATTENDANCE_ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    SESSION_DATE = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    COURSE_ID = table.Column<string>(type: "varchar(255)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    UTD_ID = table.Column<string>(type: "varchar(255)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Attended_By", x => x.ATTENDANCE_ID);
                    table.ForeignKey(
                        name: "FK_Attended_By_Student_UTD_ID",
                        column: x => x.UTD_ID,
                        principalTable: "Student",
                        principalColumn: "UTD_ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Attended_By_class_session_SESSION_DATE_COURSE_ID",
                        columns: x => new { x.SESSION_DATE, x.COURSE_ID },
                        principalTable: "class_session",
                        principalColumns: new[] { "SESSION_DATE", "COURSE_ID" },
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Submissions",
                columns: table => new
                {
                    SUBMISSION_ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    COURSE_ID = table.Column<string>(type: "varchar(255)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    SESSION_DATE = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    UTD_ID = table.Column<string>(type: "varchar(255)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    QUIZ_ID = table.Column<int>(type: "int", nullable: false),
                    IP_ADDRESS = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    SUBMISSION_TIME = table.Column<TimeSpan>(type: "time(6)", nullable: false),
                    ANSWER1 = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    ANSWER2 = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    ANSWER3 = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    STATUS = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Submissions", x => x.SUBMISSION_ID);
                    table.ForeignKey(
                        name: "FK_Submissions_Quiz_QUIZ_ID",
                        column: x => x.QUIZ_ID,
                        principalTable: "Quiz",
                        principalColumn: "QUIZ_ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Submissions_Student_UTD_ID",
                        column: x => x.UTD_ID,
                        principalTable: "Student",
                        principalColumn: "UTD_ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Submissions_class_session_SESSION_DATE_COURSE_ID",
                        columns: x => new { x.SESSION_DATE, x.COURSE_ID },
                        principalTable: "class_session",
                        principalColumns: new[] { "SESSION_DATE", "COURSE_ID" },
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_Attended_By_SESSION_DATE_COURSE_ID",
                table: "Attended_By",
                columns: new[] { "SESSION_DATE", "COURSE_ID" });

            migrationBuilder.CreateIndex(
                name: "IX_Attended_By_UTD_ID",
                table: "Attended_By",
                column: "UTD_ID");

            migrationBuilder.CreateIndex(
                name: "IX_class_session_COURSE_ID",
                table: "class_session",
                column: "COURSE_ID");

            migrationBuilder.CreateIndex(
                name: "IX_class_session_QUIZ_ID",
                table: "class_session",
                column: "QUIZ_ID");

            migrationBuilder.CreateIndex(
                name: "IX_Questions_POOL_ID",
                table: "Questions",
                column: "POOL_ID");

            migrationBuilder.CreateIndex(
                name: "IX_Questions_QUIZ_ID",
                table: "Questions",
                column: "QUIZ_ID");

            migrationBuilder.CreateIndex(
                name: "IX_Quiz_POOL_ID",
                table: "Quiz",
                column: "POOL_ID");

            migrationBuilder.CreateIndex(
                name: "IX_Submissions_QUIZ_ID",
                table: "Submissions",
                column: "QUIZ_ID");

            migrationBuilder.CreateIndex(
                name: "IX_Submissions_SESSION_DATE_COURSE_ID",
                table: "Submissions",
                columns: new[] { "SESSION_DATE", "COURSE_ID" });

            migrationBuilder.CreateIndex(
                name: "IX_Submissions_UTD_ID",
                table: "Submissions",
                column: "UTD_ID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Attended_By");

            migrationBuilder.DropTable(
                name: "Questions");

            migrationBuilder.DropTable(
                name: "Submissions");

            migrationBuilder.DropTable(
                name: "Student");

            migrationBuilder.DropTable(
                name: "class_session");

            migrationBuilder.DropTable(
                name: "Course");

            migrationBuilder.DropTable(
                name: "Quiz");

            migrationBuilder.DropTable(
                name: "Question_Pool");
        }
    }
}
