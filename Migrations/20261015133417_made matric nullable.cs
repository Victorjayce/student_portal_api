using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Students_portal.Migrations
{
    /// <inheritdoc />
    public partial class madematricnullable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_StudentsTable_MatriculationNumber",
                table: "StudentsTable");

            migrationBuilder.AlterColumn<string>(
                name: "MatriculationNumber",
                table: "StudentsTable",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.CreateIndex(
                name: "IX_StudentsTable_MatriculationNumber",
                table: "StudentsTable",
                column: "MatriculationNumber",
                unique: true,
                filter: "[MatriculationNumber] IS NOT NULL");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_StudentsTable_MatriculationNumber",
                table: "StudentsTable");

            migrationBuilder.AlterColumn<string>(
                name: "MatriculationNumber",
                table: "StudentsTable",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_StudentsTable_MatriculationNumber",
                table: "StudentsTable",
                column: "MatriculationNumber",
                unique: true);
        }
    }
}
