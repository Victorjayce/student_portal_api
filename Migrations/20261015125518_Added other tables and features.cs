using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Students_portal.Migrations
{
    /// <inheritdoc />
    public partial class Addedothertablesandfeatures : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Department",
                table: "StudentsTable");

            migrationBuilder.DropColumn(
                name: "Faculty",
                table: "StudentsTable");

            migrationBuilder.AlterColumn<string>(
                name: "MatriculationNumber",
                table: "StudentsTable",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Email",
                table: "StudentsTable",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<string>(
                name: "Address",
                table: "StudentsTable",
                type: "nvarchar(250)",
                maxLength: 250,
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DateOfBirth",
                table: "StudentsTable",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "DepartmentId",
                table: "StudentsTable",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "EntryYear",
                table: "StudentsTable",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Gender",
                table: "StudentsTable",
                type: "nvarchar(10)",
                maxLength: 10,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "PhoneNumber",
                table: "StudentsTable",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "StateOfOrigin",
                table: "StudentsTable",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Status",
                table: "StudentsTable",
                type: "nvarchar(15)",
                maxLength: 15,
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateTable(
                name: "FacultiesTable",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    Code = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FacultiesTable", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DepartmentsTable",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    Code = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    FacultyId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DepartmentsTable", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DepartmentsTable_FacultiesTable_FacultyId",
                        column: x => x.FacultyId,
                        principalTable: "FacultiesTable",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "CoursesTable",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Code = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    Title = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    CreditUnits = table.Column<int>(type: "int", nullable: false),
                    Level = table.Column<int>(type: "int", nullable: false),
                    Semester = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    DepartmentId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CoursesTable", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CoursesTable_DepartmentsTable_DepartmentId",
                        column: x => x.DepartmentId,
                        principalTable: "DepartmentsTable",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ResultsTable",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StudentId = table.Column<int>(type: "int", nullable: false),
                    CourseId = table.Column<int>(type: "int", nullable: false),
                    Session = table.Column<string>(type: "nvarchar(9)", maxLength: 9, nullable: false),
                    Semester = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    Score = table.Column<decimal>(type: "decimal(5,2)", nullable: false),
                    Grade = table.Column<string>(type: "nvarchar(2)", maxLength: 2, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ResultsTable", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ResultsTable_CoursesTable_CourseId",
                        column: x => x.CourseId,
                        principalTable: "CoursesTable",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ResultsTable_StudentsTable_StudentId",
                        column: x => x.StudentId,
                        principalTable: "StudentsTable",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_StudentsTable_DepartmentId",
                table: "StudentsTable",
                column: "DepartmentId");

            migrationBuilder.CreateIndex(
                name: "IX_StudentsTable_Email",
                table: "StudentsTable",
                column: "Email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_StudentsTable_MatriculationNumber",
                table: "StudentsTable",
                column: "MatriculationNumber",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_CoursesTable_Code",
                table: "CoursesTable",
                column: "Code",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_CoursesTable_DepartmentId",
                table: "CoursesTable",
                column: "DepartmentId");

            migrationBuilder.CreateIndex(
                name: "IX_DepartmentsTable_Code",
                table: "DepartmentsTable",
                column: "Code",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_DepartmentsTable_FacultyId",
                table: "DepartmentsTable",
                column: "FacultyId");

            migrationBuilder.CreateIndex(
                name: "IX_FacultiesTable_Code",
                table: "FacultiesTable",
                column: "Code",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ResultsTable_CourseId",
                table: "ResultsTable",
                column: "CourseId");

            migrationBuilder.CreateIndex(
                name: "IX_ResultsTable_StudentId_CourseId_Session",
                table: "ResultsTable",
                columns: new[] { "StudentId", "CourseId", "Session" },
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_StudentsTable_DepartmentsTable_DepartmentId",
                table: "StudentsTable",
                column: "DepartmentId",
                principalTable: "DepartmentsTable",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_StudentsTable_DepartmentsTable_DepartmentId",
                table: "StudentsTable");

            migrationBuilder.DropTable(
                name: "ResultsTable");

            migrationBuilder.DropTable(
                name: "CoursesTable");

            migrationBuilder.DropTable(
                name: "DepartmentsTable");

            migrationBuilder.DropTable(
                name: "FacultiesTable");

            migrationBuilder.DropIndex(
                name: "IX_StudentsTable_DepartmentId",
                table: "StudentsTable");

            migrationBuilder.DropIndex(
                name: "IX_StudentsTable_Email",
                table: "StudentsTable");

            migrationBuilder.DropIndex(
                name: "IX_StudentsTable_MatriculationNumber",
                table: "StudentsTable");

            migrationBuilder.DropColumn(
                name: "Address",
                table: "StudentsTable");

            migrationBuilder.DropColumn(
                name: "DateOfBirth",
                table: "StudentsTable");

            migrationBuilder.DropColumn(
                name: "DepartmentId",
                table: "StudentsTable");

            migrationBuilder.DropColumn(
                name: "EntryYear",
                table: "StudentsTable");

            migrationBuilder.DropColumn(
                name: "Gender",
                table: "StudentsTable");

            migrationBuilder.DropColumn(
                name: "PhoneNumber",
                table: "StudentsTable");

            migrationBuilder.DropColumn(
                name: "StateOfOrigin",
                table: "StudentsTable");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "StudentsTable");

            migrationBuilder.AlterColumn<string>(
                name: "MatriculationNumber",
                table: "StudentsTable",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AlterColumn<string>(
                name: "Email",
                table: "StudentsTable",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddColumn<string>(
                name: "Department",
                table: "StudentsTable",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Faculty",
                table: "StudentsTable",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
