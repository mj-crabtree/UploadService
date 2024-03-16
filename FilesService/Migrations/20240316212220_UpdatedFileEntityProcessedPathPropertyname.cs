using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FilesService.Migrations
{
    /// <inheritdoc />
    public partial class UpdatedFileEntityProcessedPathPropertyname : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ProcessedPath",
                table: "Files",
                newName: "Path");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Path",
                table: "Files",
                newName: "ProcessedPath");
        }
    }
}
