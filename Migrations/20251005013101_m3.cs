using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SmithSwimmingSchool.Migrations
{
    /// <inheritdoc />
    public partial class m3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "SkillLevel",
                table: "Lessons",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "CoachId",
                table: "Lessons",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "SwimmerId",
                table: "Lessons",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "LessonId",
                table: "Enrollments",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Lessons_CoachId",
                table: "Lessons",
                column: "CoachId");

            migrationBuilder.CreateIndex(
                name: "IX_Lessons_SwimmerId",
                table: "Lessons",
                column: "SwimmerId");

            migrationBuilder.CreateIndex(
                name: "IX_Enrollments_LessonId",
                table: "Enrollments",
                column: "LessonId");

            migrationBuilder.AddForeignKey(
                name: "FK_Enrollments_Lessons_LessonId",
                table: "Enrollments",
                column: "LessonId",
                principalTable: "Lessons",
                principalColumn: "LessonId");

            migrationBuilder.AddForeignKey(
                name: "FK_Lessons_Coaches_CoachId",
                table: "Lessons",
                column: "CoachId",
                principalTable: "Coaches",
                principalColumn: "CoachId");

            migrationBuilder.AddForeignKey(
                name: "FK_Lessons_Swimmers_SwimmerId",
                table: "Lessons",
                column: "SwimmerId",
                principalTable: "Swimmers",
                principalColumn: "SwimmerId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Enrollments_Lessons_LessonId",
                table: "Enrollments");

            migrationBuilder.DropForeignKey(
                name: "FK_Lessons_Coaches_CoachId",
                table: "Lessons");

            migrationBuilder.DropForeignKey(
                name: "FK_Lessons_Swimmers_SwimmerId",
                table: "Lessons");

            migrationBuilder.DropIndex(
                name: "IX_Lessons_CoachId",
                table: "Lessons");

            migrationBuilder.DropIndex(
                name: "IX_Lessons_SwimmerId",
                table: "Lessons");

            migrationBuilder.DropIndex(
                name: "IX_Enrollments_LessonId",
                table: "Enrollments");

            migrationBuilder.DropColumn(
                name: "CoachId",
                table: "Lessons");

            migrationBuilder.DropColumn(
                name: "SwimmerId",
                table: "Lessons");

            migrationBuilder.DropColumn(
                name: "LessonId",
                table: "Enrollments");

            migrationBuilder.AlterColumn<string>(
                name: "SkillLevel",
                table: "Lessons",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");
        }
    }
}
