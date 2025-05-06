using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MnemosAPI.Migrations
{
    /// <inheritdoc />
    public partial class Colonneetabellabu : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "BusinessUnitId",
                table: "Projects",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "GoalSolutions",
                table: "Projects",
                type: "nvarchar(2000)",
                maxLength: 2000,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Repository",
                table: "Projects",
                type: "nvarchar(1000)",
                maxLength: 1000,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SolutionsImpact",
                table: "Projects",
                type: "nvarchar(2000)",
                maxLength: 2000,
                nullable: true);

            migrationBuilder.CreateTable(
                name: "BusinessUnits",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BusinessUnits", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Projects_BusinessUnitId",
                table: "Projects",
                column: "BusinessUnitId");

            migrationBuilder.AddForeignKey(
                name: "FK_Projects_BusinessUnits",
                table: "Projects",
                column: "BusinessUnitId",
                principalTable: "BusinessUnits",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Projects_BusinessUnits",
                table: "Projects");

            migrationBuilder.DropTable(
                name: "BusinessUnits");

            migrationBuilder.DropIndex(
                name: "IX_Projects_BusinessUnitId",
                table: "Projects");

            migrationBuilder.DropColumn(
                name: "BusinessUnitId",
                table: "Projects");

            migrationBuilder.DropColumn(
                name: "GoalSolutions",
                table: "Projects");

            migrationBuilder.DropColumn(
                name: "Repository",
                table: "Projects");

            migrationBuilder.DropColumn(
                name: "SolutionsImpact",
                table: "Projects");
        }
    }
}
