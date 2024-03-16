using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FilesService.Migrations
{
    /// <inheritdoc />
    public partial class UpdatedFileEntityProcessedPath : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Path",
                table: "Files",
                newName: "UploadPath");

            migrationBuilder.AddColumn<string>(
                name: "ProcessedPath",
                table: "Files",
                type: "TEXT",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ProcessedPath",
                table: "Files");

            migrationBuilder.RenameColumn(
                name: "UploadPath",
                table: "Files",
                newName: "Path");
        }
    }
}
