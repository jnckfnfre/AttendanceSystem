using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AttendanceSystem.API.Migrations
{
    /// <inheritdoc />
    public partial class RemoveCourseIdAndTitleColumns : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Quiz_Course_COURSE_ID",
                table: "Quiz");

            migrationBuilder.DropForeignKey(
                name: "FK_Student_Course_COURSE_ID",
                table: "Student");

            migrationBuilder.DropIndex(
                name: "IX_Student_COURSE_ID",
                table: "Student");

            migrationBuilder.DropIndex(
                name: "IX_Quiz_COURSE_ID",
                table: "Quiz");

            migrationBuilder.DropIndex(
                name: "IX_Attended_By_SESSION_DATE_COURSE_ID",
                table: "Attended_By");

            migrationBuilder.DropColumn(
                name: "COURSE_ID",
                table: "Student");

            migrationBuilder.DropColumn(
                name: "COURSE_ID",
                table: "Quiz");

            migrationBuilder.DropColumn(
                name: "TITLE",
                table: "Quiz");

            migrationBuilder.AlterColumn<string>(
                name: "ANSWER3",
                table: "Submissions",
                type: "longtext",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "longtext")
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<string>(
                name: "ANSWER2",
                table: "Submissions",
                type: "longtext",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "longtext")
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<string>(
                name: "ANSWER1",
                table: "Submissions",
                type: "longtext",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "longtext")
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_Submissions_COURSE_ID_SESSION_DATE_UTD_ID_QUIZ_ID",
                table: "Submissions",
                columns: new[] { "COURSE_ID", "SESSION_DATE", "UTD_ID", "QUIZ_ID" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Attended_By_SESSION_DATE_COURSE_ID_UTD_ID",
                table: "Attended_By",
                columns: new[] { "SESSION_DATE", "COURSE_ID", "UTD_ID" },
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Submissions_COURSE_ID_SESSION_DATE_UTD_ID_QUIZ_ID",
                table: "Submissions");

            migrationBuilder.DropIndex(
                name: "IX_Attended_By_SESSION_DATE_COURSE_ID_UTD_ID",
                table: "Attended_By");

            migrationBuilder.UpdateData(
                table: "Submissions",
                keyColumn: "ANSWER3",
                keyValue: null,
                column: "ANSWER3",
                value: "");

            migrationBuilder.AlterColumn<string>(
                name: "ANSWER3",
                table: "Submissions",
                type: "longtext",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "longtext",
                oldNullable: true)
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.UpdateData(
                table: "Submissions",
                keyColumn: "ANSWER2",
                keyValue: null,
                column: "ANSWER2",
                value: "");

            migrationBuilder.AlterColumn<string>(
                name: "ANSWER2",
                table: "Submissions",
                type: "longtext",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "longtext",
                oldNullable: true)
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.UpdateData(
                table: "Submissions",
                keyColumn: "ANSWER1",
                keyValue: null,
                column: "ANSWER1",
                value: "");

            migrationBuilder.AlterColumn<string>(
                name: "ANSWER1",
                table: "Submissions",
                type: "longtext",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "longtext",
                oldNullable: true)
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "COURSE_ID",
                table: "Student",
                type: "varchar(255)",
                nullable: false,
                defaultValue: "")
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "COURSE_ID",
                table: "Quiz",
                type: "varchar(255)",
                nullable: false,
                defaultValue: "")
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "TITLE",
                table: "Quiz",
                type: "longtext",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_Student_COURSE_ID",
                table: "Student",
                column: "COURSE_ID");

            migrationBuilder.CreateIndex(
                name: "IX_Quiz_COURSE_ID",
                table: "Quiz",
                column: "COURSE_ID");

            migrationBuilder.CreateIndex(
                name: "IX_Attended_By_SESSION_DATE_COURSE_ID",
                table: "Attended_By",
                columns: new[] { "SESSION_DATE", "COURSE_ID" });

            migrationBuilder.AddForeignKey(
                name: "FK_Quiz_Course_COURSE_ID",
                table: "Quiz",
                column: "COURSE_ID",
                principalTable: "Course",
                principalColumn: "COURSE_ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Student_Course_COURSE_ID",
                table: "Student",
                column: "COURSE_ID",
                principalTable: "Course",
                principalColumn: "COURSE_ID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
