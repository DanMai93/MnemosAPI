using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MnemosAPI.Migrations
{
    /// <inheritdoc />
    public partial class businessunitcolumnnamerenamedtotitle : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Name",
                table: "BusinessUnits",
                newName: "Title");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Title",
                table: "BusinessUnits",
                newName: "Name");
        }
    }
}
