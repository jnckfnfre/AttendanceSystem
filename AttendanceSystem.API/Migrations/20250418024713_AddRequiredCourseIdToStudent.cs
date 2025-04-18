using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AttendanceSystem.API.Migrations
{
    /// <inheritdoc />
    public partial class AddRequiredCourseIdToStudent : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "COURSE_ID",
                table: "Student",
                type: "varchar(255)",
                nullable: false,
                defaultValue: "CS3345")
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "COURSE_ID",
                table: "Quiz",
                type: "varchar(255)",
                nullable: false,
                defaultValue: "CS3345")
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_Student_COURSE_ID",
                table: "Student",
                column: "COURSE_ID");

            migrationBuilder.CreateIndex(
                name: "IX_Quiz_COURSE_ID",
                table: "Quiz",
                column: "COURSE_ID");

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
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

            migrationBuilder.DropColumn(
                name: "COURSE_ID",
                table: "Student");

            migrationBuilder.DropColumn(
                name: "COURSE_ID",
                table: "Quiz");
        }
    }
}
